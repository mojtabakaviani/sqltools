using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace SqlTools.Classifiers
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "sql-keyword")]
    [Name("sql-keyword")]
    [UserVisible(true)]
    [Order(Before = Priority.High, After = Priority.High)]
    internal sealed class SqlKeyworkFormat : ClassificationFormatDefinition
    {
        public SqlKeyworkFormat()
        {
            this.DisplayName = "sql-keyword";
            this.ForegroundColor = Colors.Blue;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "sql-operator")]
    [Name("sql-operator")]
    [UserVisible(true)]
    [Order(Before = Priority.High, After = Priority.High)]
    internal sealed class SqlOperatorFormat : ClassificationFormatDefinition
    {
        public SqlOperatorFormat()
        {
            this.DisplayName = "sql-operator";
            this.ForegroundColor = Colors.Gray;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "sql-function")]
    [Name("sql-function")]
    [UserVisible(true)]
    [Order(Before = Priority.High, After = Priority.High)]
    internal sealed class SqlDunctionFormat : ClassificationFormatDefinition
    {
        public SqlDunctionFormat()
        {
            this.DisplayName = "sql-function";
            this.ForegroundColor = Colors.Magenta;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "sql-variable")]
    [Name("sql-variable")]
    [UserVisible(true)]
    [Order(Before = Priority.High, After = Priority.High)]
    internal sealed class SqlVariableFormat : ClassificationFormatDefinition
    {
        public SqlVariableFormat()
        {
            this.DisplayName = "sql-variable";
            this.ForegroundColor = Colors.Green;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "sql-literal")]
    [Name("sql-literal")]
    [UserVisible(true)]
    [Order(Before = Priority.High, After = Priority.High)]
    internal sealed class SqlLiteralFormat : ClassificationFormatDefinition
    {
        public SqlLiteralFormat()
        {
            this.DisplayName = "sql-literal";
            this.ForegroundColor = Colors.Black;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "sql-defined")]
    [Name("sql-defined")]
    [UserVisible(true)]
    [Order(Before = Priority.High, After = Priority.High)]
    internal sealed class SqlDefineFormat : ClassificationFormatDefinition
    {
        public SqlDefineFormat()
        {
            this.DisplayName = "sql-defined";
            this.ForegroundColor = new Color() { R = 116, G = 83, B = 31 };
        }
    }
}
