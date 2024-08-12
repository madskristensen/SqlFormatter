using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Commanding;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;
using Microsoft.VisualStudio.Utilities;
using PoorMansTSqlFormatterRedux;
using PoorMansTSqlFormatterRedux.Formatters;

namespace SqlFormatter
{
    [Export(typeof(ICommandHandler))]
    [Name(nameof(FormatCommandHandler2))]
    [ContentType("SQL")]
    [ContentType("SQL Server Tools")]
    [Order(Before = "FormatCommandHandler")]
    public class FormatCommandHandler2 :
        ICommandHandler<FormatDocumentCommandArgs>
    //,ICommandHandler<FormatSelectionCommandArgs>
    {
        public static RatingPrompt _ratingPrompt = new("", Vsix.Name, General.Instance);
        public string DisplayName => nameof(FormatCommandHandler2);

        public bool ExecuteCommand(FormatDocumentCommandArgs args, CommandExecutionContext executionContext)
        {
            FormatAsync(args.SubjectBuffer, 0, args.SubjectBuffer.CurrentSnapshot.Length).FireAndForget();

            return false;
        }

        public bool ExecuteCommand(FormatSelectionCommandArgs args, CommandExecutionContext executionContext)
        {
            SnapshotSpan span = args.TextView.Selection.SelectedSpans.FirstOrDefault();
            FormatAsync(args.SubjectBuffer, span.Start, span.Length).FireAndForget();

            return false;
        }

        public static async Task<bool> FormatAsync(ITextBuffer buffer, int start, int length)
        {
            TSqlStandardFormatterOptions options = await GetFormatterOptionsAsync();
            TSqlStandardFormatter formatter = new(options);
            SqlFormattingManager manager = new(formatter);

            var hasErrors = false;
            var formattedSql = manager.Format(buffer.CurrentSnapshot.GetText(start, length), ref hasErrors);

            if (!hasErrors)
            {
                using (ITextEdit edit = buffer.CreateEdit())
                {
                    _ = edit.Replace(start, length, formattedSql);
                    _ = edit.Apply();
                }

                _ratingPrompt.RegisterSuccessfulUsage();

                return false;
            }

            return true;
        }

        private static async Task<TSqlStandardFormatterOptions> GetFormatterOptionsAsync()
        {
            General options = await General.GetLiveInstanceAsync();
            return new()
            {
                KeywordStandardization = options.KeywordStandardization,
                SpacesPerTab = options.SpacesPerTab,
                ExpandCommaLists = options.ExpandCommaLists,
                TrailingCommas = options.TrailingCommas,
                SpaceAfterExpandedComma = options.SpaceAfterExpandedComma,
                UppercaseKeywords = options.UppercaseKeywords,
                BreakJoinOnSections = options.BreakJoinOnSections,
                ExpandBooleanExpressions = options.ExpandBooleanExpressions,
                ExpandBetweenConditions = options.ExpandBetweenConditions,
                ExpandCaseStatements = options.ExpandCaseStatements,
                ExpandInLists = options.ExpandInLists,
                MaxLineWidth = options.MaxLineWidth,
                IndentString = options.IndentString,
                NewClauseLineBreaks = options.NewClauseLineBreaks,
                NewStatementLineBreaks = options.NewStatementLineBreaks,
            };
        }

        public CommandState GetCommandState(FormatDocumentCommandArgs args) => CommandState.Available;
        public CommandState GetCommandState(FormatSelectionCommandArgs args) => CommandState.Available;
    }
}
