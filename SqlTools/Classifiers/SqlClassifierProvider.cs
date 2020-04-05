using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using SqlTools.NaturalTextTaggers;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.PlatformUI;

namespace SqlTools.Classifiers
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("csharp")]
    internal class SqlClassifierProvider : IClassifierProvider
    {
#pragma warning disable 649
        [Import]
        private IClassificationTypeRegistryService ClassificationRegistry;

        [Import]
        private IClassificationFormatMapService ClassificationFormatMapService;

        [Import]
        private IBufferTagAggregatorFactoryService TagAggregatorFactory;

#pragma warning restore 649

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            var tagAggregator = TagAggregatorFactory.CreateTagAggregator<NaturalTextTag>(buffer);
            return new SqlClassifier(tagAggregator, ClassificationRegistry, ClassificationFormatMapService);
        }
    }
}
