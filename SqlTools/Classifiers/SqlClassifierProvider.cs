using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using SqlTools.NaturalTextTaggers;

namespace SqlTools.Classifiers
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("code")]
    internal class SqlClassifierProvider : IClassifierProvider
    {
#pragma warning disable 649
        [Import]
        private IClassificationTypeRegistryService classificationRegistry;

        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry;

        [Import]
        internal IBufferTagAggregatorFactoryService TagAggregatorFactory;
#pragma warning restore 649

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            var tagAggregator = TagAggregatorFactory.CreateTagAggregator<NaturalTextTag>(buffer);
            return new SqlClassifier(tagAggregator, classificationRegistry);
        }
    }
}
