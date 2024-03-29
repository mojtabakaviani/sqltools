﻿using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace SqlTools.Classifiers
{
    internal static class SqlClassifierClassificationDefinition
    {
#pragma warning disable 169
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("Sql-Keyword")]
        internal static ClassificationTypeDefinition KeywordDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("Sql-Operator")]
        internal static ClassificationTypeDefinition OperatorDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("Sql-Function")]
        internal static ClassificationTypeDefinition FunctionDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("Sql-Variable")]
        internal static ClassificationTypeDefinition VariableDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("Sql-Literal")]
        internal static ClassificationTypeDefinition LiteralDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("Sql-Defined")]
        internal static ClassificationTypeDefinition DefinedDefinition;
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("Sql-Workflow")]
        internal static ClassificationTypeDefinition WorkflowDefinition;
#pragma warning restore 169
    }
}
