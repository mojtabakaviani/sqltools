using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace SqlTools.Classifiers
{
    internal static class SqlClassifierClassificationDefinition
    {
#pragma warning disable 169
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("sql-keyword")]
        private static ClassificationTypeDefinition keywordDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("sql-operator")]
        private static ClassificationTypeDefinition operatorDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("sql-function")]
        internal static ClassificationTypeDefinition functionDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("sql-variable")]
        internal static ClassificationTypeDefinition variableDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("sql-literal")]
        internal static ClassificationTypeDefinition literalDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("sql-defined")]
        internal static ClassificationTypeDefinition definedDefinition;
#pragma warning restore 169
    }
}
