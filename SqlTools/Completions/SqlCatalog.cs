using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SqlTools.Completions
{
    [Export]
    public class SqlCatalog
    {
        public List<Keyword> Keywords { get; } = new List<Keyword>
        {
            //keywords
            new Keyword("var",Category.Keyword),
            new Keyword("create",Category.Keyword),
            new Keyword("alter",Category.Keyword),
            new Keyword("drop",Category.Keyword),
            new Keyword("table",Category.Keyword),
            new Keyword("view",Category.Keyword),
            new Keyword("function",Category.Keyword),
            new Keyword("procedure",Category.Keyword),
            new Keyword("schema",Category.Keyword),
            new Keyword("select",Category.Keyword),
            new Keyword("top",Category.Keyword),
            new Keyword("insert",Category.Keyword),
            new Keyword("update",Category.Keyword),
            new Keyword("delete",Category.Keyword),
            new Keyword("from",Category.Keyword),
            new Keyword("where",Category.Keyword),
            new Keyword("group",Category.Keyword),
            new Keyword("order",Category.Keyword),
            new Keyword("by",Category.Keyword),
            new Keyword("asc",Category.Keyword),
            new Keyword("desc",Category.Keyword),
            new Keyword("set",Category.Keyword),
            new Keyword("into",Category.Keyword),
            new Keyword("values",Category.Keyword),
            new Keyword("except",Category.Keyword),
            new Keyword("union",Category.Keyword),
            new Keyword("inner",Category.Keyword),
            new Keyword("cross",Category.Keyword),
            new Keyword("join",Category.Keyword),
            new Keyword("left",Category.Keyword),
            new Keyword("right",Category.Keyword),
            new Keyword("outer",Category.Keyword),
            new Keyword("case",Category.Keyword),
            new Keyword("when",Category.Keyword),
            new Keyword("then",Category.Keyword),
            new Keyword("begin",Category.Keyword),
            new Keyword("end",Category.Keyword),
            new Keyword("if",Category.Keyword),
            new Keyword("else",Category.Keyword),
            new Keyword("while",Category.Keyword),
            new Keyword("for",Category.Keyword),
            new Keyword("over",Category.Keyword),
            new Keyword("distinct",Category.Keyword),
            new Keyword("with",Category.Keyword),
            new Keyword("tran",Category.Keyword),
            new Keyword("transaction",Category.Keyword),
            new Keyword("commit",Category.Keyword),
            new Keyword("rollback",Category.Keyword),
            //operators
            new Keyword("like",Category.Operator),
            new Keyword("between",Category.Operator),
            new Keyword("having",Category.Operator),
            new Keyword("exists",Category.Operator),
            new Keyword("all",Category.Operator),
            new Keyword("any",Category.Operator),
            new Keyword("some",Category.Operator),
            new Keyword("and",Category.Operator),
            new Keyword("on",Category.Operator),
            new Keyword("or",Category.Operator),
            new Keyword("not",Category.Operator),
            new Keyword("in",Category.Operator),
            new Keyword("as",Category.Operator),
            new Keyword("is",Category.Operator),
            //functions
            new Keyword("exec",Category.Function),
            new Keyword("execute",Category.Function),
            new Keyword("count",Category.Function),
            new Keyword("sum",Category.Function),
            new Keyword("max",Category.Function),
            new Keyword("min",Category.Function),
            new Keyword("avg",Category.Function),
            new Keyword("parse",Category.Function),
            new Keyword("cast",Category.Function),
            new Keyword("convert",Category.Function),
            new Keyword("format",Category.Function),
            new Keyword("len",Category.Function),
            new Keyword("rand",Category.Function),
            new Keyword("log",Category.Function),
            new Keyword("sqrt",Category.Function),
            new Keyword("trim",Category.Function),
            new Keyword("concat",Category.Function),
            new Keyword("datepart",Category.Function),
            new Keyword("isnull",Category.Function),
            new Keyword("isdate",Category.Function),

        };

        public enum Category
        {
            Keyword,
            Function,
            Operator,
            Variable
        }

        public class Keyword
        {
            public string Name { get; }
            public Category Category { get; }

            internal Keyword(string name, Category category)
            {
                Name = name;
                Category = category;
            }
        }
    }
}
