global using System;
global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Text;

namespace SqlFormatter
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.SqlFormatterString)]
    [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), "SQL Server Tools", "Formatting", 0, 0, true, SupportsProfiles = true)]
    [ProvideAutoLoad(PackageGuids.autoloadString, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideUIContextRule(PackageGuids.autoloadString,
        name: "auto load",
        expression: "sql",
        termNames: ["sql"],
        termValues: ["ActiveEditorContentType:SQL Server Tools"])]
    public sealed class SqlFormatterPackage : ToolkitPackage
    {
        private CommandEvents _events;
        private DTE2 _dte;
        private Command _formatDocumentCmd;

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            _dte = await VS.GetRequiredServiceAsync<DTE, DTE2>();

            _formatDocumentCmd = _dte.Commands.Item("Edit.FormatDocument");

            _events = _dte.Events.CommandEvents;
            _events.BeforeExecute += BeforeExecute;
        }

        private void BeforeExecute(string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (Guid == _formatDocumentCmd.Guid && ID == _formatDocumentCmd.ID && _dte.ActiveDocument?.Language == "SQL Server Tools")
            {
                CancelDefault = true;
                JoinableTaskFactory.RunAsync(async () =>
                {
                    DocumentView document = await VS.Documents.GetActiveDocumentViewAsync();
                    ITextBuffer buffer = document.TextBuffer;

                    await FormatCommandHandler2.FormatAsync(buffer, 0, buffer.CurrentSnapshot.Length);

                }).FireAndForget();
            }
        }
    }
}