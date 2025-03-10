using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Commanding;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;
using Microsoft.VisualStudio.Utilities;

namespace SqlFormatter
{
    [Export(typeof(ICommandHandler))]
    [Name(nameof(SaveCommandHandler))]
    [ContentType("SQL")]
    [ContentType("SQL Server Tools")]
    [Order(Before = DefaultOrderings.Highest)]
    public class SaveCommandHandler : ICommandHandler<SaveCommandArgs>
    {
        public string DisplayName => nameof(SaveCommandHandler);

        public bool ExecuteCommand(SaveCommandArgs args, CommandExecutionContext executionContext)
        {
            if (!General.Instance.FormatOnSave)
            {
                return false;
            }

            ThreadHelper.JoinableTaskFactory.Run(async () =>
            {
                var lineNumber = args.TextView.TextViewLines.FirstVisibleLine.Start.GetContainingLineNumber();
                var caretPosition = args.TextView.Caret.Position.BufferPosition.Position;

                if (await FormatCommandHandler.FormatAsync(args.TextView.TextBuffer, 0, args.TextView.TextBuffer.CurrentSnapshot.Length))
                {
                    args.TextView.ViewScroller.ScrollViewportVerticallyByLines(ScrollDirection.Down, lineNumber);
                    SnapshotPoint point = new(args.TextView.TextBuffer.CurrentSnapshot, caretPosition);
                    _ = args.TextView.Caret.MoveTo(point, PositionAffinity.Predecessor);
                }
            });

            return false;
        }

        public CommandState GetCommandState(SaveCommandArgs args) => CommandState.Available;
    }
}
