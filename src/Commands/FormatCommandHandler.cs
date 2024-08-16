using System.ComponentModel.Composition;
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
    [Name(nameof(FormatCommandHandler))]
    [ContentType("SQL")]
    [ContentType("SQL Server Tools")]
    [Order(Before = "FormatCommandHandler")]
    public class FormatCommandHandler : ICommandHandler<FormatDocumentCommandArgs>
    {
        public static RatingPrompt _ratingPrompt = new("", Vsix.Name, General.Instance);
        public string DisplayName => nameof(FormatCommandHandler);

        public bool ExecuteCommand(FormatDocumentCommandArgs args, CommandExecutionContext executionContext)
        {
            FormatAsync(args.SubjectBuffer, 0, args.SubjectBuffer.CurrentSnapshot.Length).FireAndForget();

            return false;
        }

        public static async Task<bool> FormatAsync(ITextBuffer buffer, int start, int length)
        {
            TSqlStandardFormatterOptions options = await FormatterConfig.GetOptionsAsync(buffer.GetFileName());
            TSqlStandardFormatter formatter = new(options);
            SqlFormattingManager manager = new(formatter);

            var hasErrors = false;
            var text = buffer.CurrentSnapshot.GetText(start, length);
            var formattedSql = manager.Format(text, ref hasErrors);

            if (!hasErrors && text != formattedSql)
            {
                using (ITextEdit edit = buffer.CreateEdit())
                {
                    _ = edit.Replace(start, length, formattedSql.Trim());
                    _ = edit.Apply();
                }

                _ratingPrompt.RegisterSuccessfulUsage();

                return false;
            }

            return true;
        }

        public CommandState GetCommandState(FormatDocumentCommandArgs args) => CommandState.Available;
    }
}
