using System.Collections.Generic;
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
            var text = buffer.CurrentSnapshot.GetText(start, length);

            if (string.IsNullOrWhiteSpace(text) || !TryParse(text, out TSqlFragment fragment))
            {
                return false;
            }

            SqlScriptGeneratorOptions options = await FormatterConfig.GetOptionsAsync(buffer.GetFileName());
            Sql170ScriptGenerator generator = new(options);

            generator.GenerateScript(fragment, out var formattedSql);

            if (text.Trim() != formattedSql.Trim())
            {
                using (ITextEdit edit = buffer.CreateEdit())
                {
                    _ = edit.Replace(start, length, formattedSql.Trim());
                    _ = edit.Apply();
                }

                _ratingPrompt.RegisterSuccessfulUsage();
                return true;
            }

            return false;
        }

        public static bool TryParse(string text, out TSqlFragment fragment)
        {
            try
            {
                TSql170Parser parser = new(true, SqlEngineType.All);

                using (var reader = new StringReader(text))
                {
                    fragment = parser.Parse(reader, out IList<ParseError> errors);
                    return !errors.Any();
                }
            }
            catch (Exception)
            {
                fragment = null;
                return false;
            }
        }

        public CommandState GetCommandState(FormatDocumentCommandArgs args) => CommandState.Available;
    }
}
