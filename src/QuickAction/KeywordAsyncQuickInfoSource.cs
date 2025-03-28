using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Operations;

namespace SqlFormatter.QuickAction
{
    internal class KeywordAsyncQuickInfoSource(ITextBuffer buffer, ITextStructureNavigator navigator) : IAsyncQuickInfoSource
    {
        public async Task<QuickInfoItem> GetQuickInfoItemAsync(IAsyncQuickInfoSession session, CancellationToken cancellationToken)
        {
            SnapshotPoint? triggerPoint = session.GetTriggerPoint(buffer.CurrentSnapshot);

            if (!triggerPoint.HasValue)
            {
                return null;
            }

            var point = new SnapshotPoint(buffer.CurrentSnapshot, triggerPoint.Value.Position);
            TextExtent word = navigator.GetExtentOfWord(point);

            if (word.IsSignificant)
            {
                SqlKeyword keyword = SqlKeyword.All.FirstOrDefault(k => k.Keyword.Equals(word.Span.GetText(), StringComparison.OrdinalIgnoreCase));

                if (keyword == null)
                {
                    return null;
                }

                var example = keyword.Example;

                if (buffer.Properties.TryGetProperty(typeof(ITextDocument), out ITextDocument doc) && File.Exists(doc.FilePath))
                {
                    SqlScriptGeneratorOptions options = await FormatterConfig.GetOptionsAsync(doc.FilePath);
                    Sql170ScriptGenerator generator = new(options);

                    var parsedSuccesfully = FormatCommandHandler.TryParse(keyword.Example, out TSqlFragment fragment);

                    if (parsedSuccesfully)
                    {
                        generator.GenerateScript(fragment, out example);
                        example = example.Replace("\r\n\r\n\r\n", "\r\n\r\n").Trim();
                    }
                }

                var control = new ContainerElement(
                    ContainerElementStyle.Stacked,
                    new ClassifiedTextElement(
                        new ClassifiedTextRun(PredefinedClassificationTypeNames.Text, keyword.Keyword, ClassifiedTextRunStyle.Bold)
                    ),
                    new ClassifiedTextElement(
                        new ClassifiedTextRun(PredefinedClassificationTypeNames.Text, keyword.Description)
                    ),
                    new ClassifiedTextElement(
                        new ClassifiedTextRun(PredefinedClassificationTypeNames.Text, Environment.NewLine + "Example", ClassifiedTextRunStyle.Bold)
                    ),
                    new ClassifiedTextElement(
                        new ClassifiedTextRun(PredefinedClassificationTypeNames.Text, example ?? "")
                    )
                );

                ITrackingSpan span = buffer.CurrentSnapshot.CreateTrackingSpan(word.Span, SpanTrackingMode.EdgeInclusive);
                return new QuickInfoItem(span, control);
            }

            return null;
        }

        public void Dispose()
        {

        }
    }
}