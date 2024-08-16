using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Text;

namespace SqlFormatter
{
    [Command(PackageGuids.SqlFormatterString, PackageIds.FormatSql)]
    internal sealed class FormatSqlCommand : BaseCommand<FormatSqlCommand>
    {
        private DTE2 _dte;

        protected override async Task InitializeCompletedAsync()
        {
            _dte = await VS.GetRequiredServiceAsync<DTE, DTE2>();
        }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            DocumentView doc = await VS.Documents.GetActiveDocumentViewAsync();
            SnapshotSpan selection = doc.TextView.Selection.SelectedSpans[0];

            _ = await FormatCommandHandler.FormatAsync(doc.TextBuffer, selection.Start, selection.Length);
        }

        protected override void BeforeQueryStatus(EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            Command.Visible = _dte.ActiveDocument.Selection is TextSelection sel && !sel.IsEmpty;
        }
    }
}
