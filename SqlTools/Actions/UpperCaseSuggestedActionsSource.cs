using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SqlTools.Actions
{
    internal class UpperCaseSuggestedActionsSource : ISuggestedActionsSource
    {
        private readonly UpperCaseSuggestedActionsSourceProvider _factory;
        private readonly ITextBuffer _textBuffer;
        private readonly ITextView _textView;

        public UpperCaseSuggestedActionsSource(UpperCaseSuggestedActionsSourceProvider upperCaseSuggestedActionsSourceProvider, ITextView textView, ITextBuffer textBuffer)
        {
            _factory = upperCaseSuggestedActionsSourceProvider;
            _textBuffer = textBuffer;
            _textView = textView;
        }

#pragma warning disable 0067
        public event EventHandler<EventArgs> SuggestedActionsChanged;
#pragma warning restore 0067

        public void Dispose()
        {
        }

        public IEnumerable<SuggestedActionSet> GetSuggestedActions(ISuggestedActionCategorySet requestedActionCategories, SnapshotSpan range, CancellationToken cancellationToken)
        {
            if (TryGetWordUnderCaret(out TextExtent extent) && extent.IsSignificant)
            {
                ITrackingSpan trackingSpan = range.Snapshot.CreateTrackingSpan(extent.Span, SpanTrackingMode.EdgeInclusive);
                var upperAction = new UpperCaseSuggestedAction(trackingSpan);
                //var lowerAction = new LowerCaseSuggestedAction(trackingSpan);
                return new SuggestedActionSet[] { new SuggestedActionSet(new ISuggestedAction[] { upperAction/*, lowerAction*/ }) };
            }
            return Enumerable.Empty<SuggestedActionSet>();
        }

        public Task<bool> HasSuggestedActionsAsync(ISuggestedActionCategorySet requestedActionCategories, SnapshotSpan range, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                if (TryGetWordUnderCaret(out TextExtent extent))
                {
                    // don't display the tag if the extent has whitespace
                    return extent.IsSignificant;
                }
                return false;
            });
        }

        public bool TryGetTelemetryId(out Guid telemetryId)
        {
            telemetryId = Guid.Empty;
            return false;
        }

        private bool TryGetWordUnderCaret(out TextExtent wordExtent)
        {
            ITextCaret caret = _textView.Caret;
            SnapshotPoint point;

            if (caret.Position.BufferPosition > 0)
            {
                point = caret.Position.BufferPosition - 1;
            }
            else
            {
                wordExtent = default;
                return false;
            }

            ITextStructureNavigator navigator = _factory.NavigatorService.GetTextStructureNavigator(_textBuffer);

            wordExtent = navigator.GetExtentOfWord(point);
            return true;
        }
    }
}
