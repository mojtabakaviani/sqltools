using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace SqlTools.Actions
{
    [Export(typeof(ISuggestedActionsSourceProvider))]
    [Name("UpperCase SQL Literals Suggested Actions")]
    [ContentType("csharp")]
    internal class UpperCaseSuggestedActionsSourceProvider : ISuggestedActionsSourceProvider
    {
        [Import(typeof(ITextStructureNavigatorSelectorService))]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }

        public ISuggestedActionsSource CreateSuggestedActionsSource(ITextView textView, ITextBuffer textBuffer)
        {
            if (textBuffer == null && textView == null)
                return null;
            return new UpperCaseSuggestedActionsSource(this, textView, textBuffer);
        }
    }
}
