using System.ComponentModel.Composition;
using System.Linq;
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
        public string DisplayName => nameof(FormatCommandHandler2);

        public bool ExecuteCommand(FormatDocumentCommandArgs args, CommandExecutionContext executionContext)
        {
            var success = Format(args.SubjectBuffer, 0, args.SubjectBuffer.CurrentSnapshot.Length);

            return false;
        }

        public bool ExecuteCommand(FormatSelectionCommandArgs args, CommandExecutionContext executionContext)
        {
            SnapshotSpan span = args.TextView.Selection.SelectedSpans.FirstOrDefault();
            var success = Format(args.SubjectBuffer, span.Start, span.Length);

            return false;
        }

        public static bool Format(ITextBuffer buffer, int start, int length)
        {
            TSqlStandardFormatterOptions options = GetFormatterOptions();
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

                return false;
            }

            return true;
        }

        private static TSqlStandardFormatterOptions GetFormatterOptions()
        {
            return new()
            {
                KeywordStandardization = true
            };
        }

        public CommandState GetCommandState(FormatDocumentCommandArgs args) => CommandState.Available;
        public CommandState GetCommandState(FormatSelectionCommandArgs args) => CommandState.Available;
    }
}
