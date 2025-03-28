using Microsoft.VisualStudio.Text;

namespace SqlFormatter
{
    [Command(PackageGuids.SqlFormatterString, PackageIds.FormatSql)]
    internal sealed class FormatSqlCommand : BaseCommand<FormatSqlCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            DocumentView doc = await VS.Documents.GetActiveDocumentViewAsync();
            Span span;

            if (!doc.TextView.Selection.IsEmpty)
            {
                span = doc.TextView.Selection.SelectedSpans[0];
            }
            else
            {
                span = new Span(0, doc.TextView.TextSnapshot.Length);
            }

            await FormatCommandHandler.FormatAsync(doc.TextBuffer, span.Start, span.Length);
        }
    }
}
