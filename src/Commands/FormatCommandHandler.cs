using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.VisualStudio.Commanding;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;
using Microsoft.VisualStudio.Utilities;

namespace SqlFormatter
{
    [Export(typeof(ICommandHandler))]
    [Name(nameof(FormatCommandHandler))]
    [ContentType("SQL")]
    [ContentType("SQL Server Tools")]
    [Order(Before = DefaultOrderings.Lowest)]
    public class FormatCommandHandler : ICommandHandler<FormatDocumentCommandArgs>
    {
        public static RatingPrompt _ratingPrompt = new("MadsKristensen.SqlFormatter", Vsix.Name, General.Instance);
        public string DisplayName => nameof(FormatCommandHandler);

        public bool ExecuteCommand(FormatDocumentCommandArgs args, CommandExecutionContext executionContext)
        {
            FormatAsync(args.SubjectBuffer, 0, args.SubjectBuffer.CurrentSnapshot.Length).FireAndForget();

            return false;
        }

        public static async Task<bool> FormatAsync(ITextBuffer buffer, int start, int length)
        {
            SqlScriptGeneratorOptions options = await FormatterConfig.GetOptionsAsync(buffer.GetFileName());
            //TSqlStandardFormatter formatter = new(options);
            //SqlFormattingManager manager = new(formatter);
            TSql170Parser parser = new(true, SqlEngineType.All);


            //var hasErrors = false;
            var text = buffer.CurrentSnapshot.GetText(start, length);
            TSqlFragment fragment = parser.Parse(new StringReader(text), out System.Collections.Generic.IList<ParseError> errors);
            Sql170ScriptGenerator generator = new(options);
            //var formattedSql = manager.Format(text, ref hasErrors);

            generator.GenerateScript(fragment, out var formattedSql);

            if (!errors.Any() && text != formattedSql)
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
