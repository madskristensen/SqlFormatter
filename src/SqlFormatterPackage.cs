global using System;
global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;
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
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.RegisterCommandsAsync();
            await VS.Commands.InterceptAsync("Edit.FormatDocument", CallSqlFormatter);
        }

        private CommandProgression CallSqlFormatter()
        {
            JoinableTaskFactory.RunAsync(async () =>
            {
                DocumentView document = await VS.Documents.GetActiveDocumentViewAsync();
                ITextBuffer buffer = document.TextBuffer;

                await FormatCommandHandler.FormatAsync(buffer, 0, buffer.CurrentSnapshot.Length);
            }).FireAndForget();

            return CommandProgression.Stop;
        }
    }
}