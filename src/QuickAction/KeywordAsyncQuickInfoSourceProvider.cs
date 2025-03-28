using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;

namespace SqlFormatter.QuickAction
{
    [Export(typeof(IAsyncQuickInfoSourceProvider))]
    [Name(nameof(KeywordAsyncQuickInfoSourceProvider))]
    [ContentType("SQL")]
    [ContentType("SQL Server Tools")]
    internal class KeywordAsyncQuickInfoSourceProvider : IAsyncQuickInfoSourceProvider
    {
        [Import]
        internal ITextStructureNavigatorSelectorService TextStructureNavigatorSelector { get; set; }

        public IAsyncQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer)
        {
            ITextStructureNavigator navigator = TextStructureNavigatorSelector.GetTextStructureNavigator(textBuffer);
            return textBuffer.Properties.GetOrCreateSingletonProperty(() => new KeywordAsyncQuickInfoSource(textBuffer, navigator));
        }
    }
}
