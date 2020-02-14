﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using SqlTools.NaturalTextTaggers;

namespace SqlTools
{
    internal class SqlClassifier : IClassifier
    {
        //https://github.com/EWSoftware/VSSpellChecker/
        //https://github.com/fbdegroot/SqlSyntaxHighlighting
        private readonly char[] keywordPrefixCharacters = new[] { '\t', ' ', '"', '(' };
        private readonly char[] keywordPostfixCharacters = new[] { '\t', ' ', '"', ')' };
        private readonly char[] functionPrefixCharacters = new[] { '\t', ' ', '"', ',', '(' };
        private readonly char[] functionPostfixCharacters = new[] { '\t', '(' };

        private readonly List<string> detects = new List<string>
        {
            "select", "insert", "delete", "update", "create", "alter", "drop", "exec", "execute"
        };

        private readonly List<string> keywords = new List<string> {
            "select", "insert", "delete", "update",
            "into", "values", "truncate", "distinct", "top", "with","from",
            "union", "except","where",
            "order by", "asc", "desc", "over", "group by",
            "on", "as",
            "create", "alter", "drop",
            "table", "function", "procedure", "view", "schema",
            "declare", "set",
            "if", "begin", "then", "else", "end", "for", "while",
            "transaction", "commit", "rollback",
            "exec", "execute", "return", "returns", "print", "use",
            "dbcc", "fetch", "open", "deallocate", "inserted", "deleted",
            "bigint", "numeric", "bit", "smallint", "decimal", "smallmoney", "int", "tinyint", "money", "float", "real",
            "date", "datetimeoffset", "datetime2", "smalldatetime", "datetime", "time", "timestamp",
            "char", "varchar", "text", "nchar", "nvarchar", "ntext",
            "binary", "varbinary", "image",
            "cursor", "hierarchyid", "uniqueidentifier", "sql_variant", "xml",
            "pivot", "unpivot"
        };

        private readonly List<string> operators = new List<string>
        {
            "*", ",", ".", "(", ")", "=", ">", "<", "+", "-", "/", "%", "~", "&", "^", "|", "'", ":"
        };

        private readonly List<string> logicals = new List<string>
        {
            "join", "inner join", "outer join", "left outer join", "right outer join", "left join", "right join", "cross join", "and", "or", "is", "null", "not", "in", "like", "between", "having", "exists", "all", "any", "some"
        };


        private readonly List<string> functions = new List<string> {
            "count", "count_big", "sum", "min", "max", "avg", "row_number",
            "abs", "degrees", "rand", "acos", "exp", "round", "asin", "floor", "sign", "atan", "log", "sin", "atan2", "log10", "sqrt", "ceiling", "pi", "square", "cos", "power", "tan", "cot", "radians",
            "newid", "isnull", "coalesce", "choose", "iif",
            "left", "right", "substring", "trim", "ltrim", "rtrim", "upper", "lower", "charindex", "len", "stuff", "format", "unicode",
            "reverse", "soundex", "quotename", "patindex","string_agg", "string_split", "string_escape", "str", "concat", "concat_ws",
            "space", "translate", "ascii", "replicate", "difference",
            "getdate", "getutcdate", "isdate", "dateadd", "datediff", "datepart", "datename", "eomonth", "smalldatetimefromparts", "timefromparts", "sysutcdatetime", "switchoffset",
            "convert", "cast", "parse", "try_convert", "try_cast", "try_parse",
            "json", "json_value", "json_query", "json_modify",
            "@@datefirst", "@@cursor_rows", "@@fetch_status", "@@dbts", "@@langid", "@@language", "@@lock_timeout", "@@max_connections", "@@max_precision", "@@nestlevel", "@@options", "@@remserver", "@@servername", "@@servicename", "@@textsize", "@@version"
        };

        private readonly Regex variables = new Regex(@"(?:^|[""\s(+,=])(?<Variable>@[a-z0-9_]+)(?:$|[""\s)+,])", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private readonly Regex defines = new Regex(@"(?:[.:])(?<Variable>[a-z0-9_]+)(?:\s*\()", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private readonly IClassificationType keywordType;
        private readonly IClassificationType operatorType;
        private readonly IClassificationType functionType;
        private readonly IClassificationType variableType;
        private readonly IClassificationType literalType;
        private readonly IClassificationType definedType;
        readonly ITagAggregator<NaturalTextTag> tagger;

        internal SqlClassifier(ITagAggregator<NaturalTextTag> tagger, IClassificationTypeRegistryService registry)
        {
            this.tagger = tagger;
            keywordType = registry.GetClassificationType("sql-keyword");
            operatorType = registry.GetClassificationType("sql-operator");
            functionType = registry.GetClassificationType("sql-function");
            variableType = registry.GetClassificationType("sql-variable");
            literalType = registry.GetClassificationType("sql-literal");
            definedType = registry.GetClassificationType("sql-defined");
        }

#pragma warning disable 67
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 67

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            IList<ClassificationSpan> classifiedSpans = new List<ClassificationSpan>();

            foreach (IMappingTagSpan<NaturalTextTag> tagSpan in tagger.GetTags(span).ToList())
            {
                SnapshotSpan snapshot = tagSpan.Span.GetSpans(span.Snapshot).First();

                string text = snapshot.GetText().ToLowerInvariant();

                int index = -1;

                if (tagSpan.Tag.State != State.MultiLineString)
                {
                    bool detected = false;
                    foreach (var detect in detects)
                    {
                        while (snapshot.Length > index + 1 && (index = text.IndexOf(detect, index + 1)) > -1)
                        {
                            //classifiedSpans.Add(new ClassificationSpan(new SnapshotSpan(snapshot.Start, text.Length), literalType));
                            detected = true;
                        }
                    }

                    if (!detected) return classifiedSpans;
                }
                //else
                //    classifiedSpans.Add(new ClassificationSpan(new SnapshotSpan(snapshot.Start, text.Length), literalType));

                // keywords
                foreach (string keyword in keywords)
                {
                    while (snapshot.Length > index + 1 && (index = text.IndexOf(keyword, index + 1)) > -1)
                    {
                        if ((index > 0 && !keywordPrefixCharacters.Contains(text[index - 1])) ||
                            (index + keyword.Length < text.Length && keywordPostfixCharacters.Contains(text[index + keyword.Length]) == false))
                            continue;

                        classifiedSpans.Add(new ClassificationSpan(new SnapshotSpan(snapshot.Start + index, keyword.Length), keywordType));
                    }
                }

                // operators
                foreach (string logical in logicals)
                {
                    while (snapshot.Length > index + 1 && (index = text.IndexOf(logical, index + 1)) > -1)
                    {
                        if ((index > 0 && !keywordPrefixCharacters.Contains(text[index - 1])) ||
                           (index + logical.Length < text.Length && keywordPostfixCharacters.Contains(text[index + logical.Length]) == false))
                            continue;
                        classifiedSpans.Add(new ClassificationSpan(new SnapshotSpan(snapshot.Start + index, logical.Length), operatorType));
                    }
                }

                // functions
                foreach (string function in functions)
                {
                    while (snapshot.Length > index + 1 && (index = text.IndexOf(function, index + 1)) > -1)
                    {
                        if ((index > 0 && !functionPrefixCharacters.Contains(text[index - 1])) ||
                            (index + function.Length < text.Length && functionPostfixCharacters.Contains(text[index + function.Length]) == false))
                            continue;

                        //if ((index > 0 && text.Substring(0, index - 1).IndexOf('{') > 0) || (index + function.Length < text.Length && text.Substring(index + function.Length).IndexOf('}') > 0))
                        //    continue;

                        classifiedSpans.Add(new ClassificationSpan(new SnapshotSpan(snapshot.Start + index, function.Length), functionType));
                    }
                }

                // variables
                foreach (Match match in variables.Matches(text))
                    classifiedSpans.Add(new ClassificationSpan(new SnapshotSpan(snapshot.Start + match.Groups["Variable"].Index, match.Groups["Variable"].Length), variableType));

                // user defined
                foreach (Match match in defines.Matches(text))
                    classifiedSpans.Add(new ClassificationSpan(new SnapshotSpan(snapshot.Start + match.Groups["Variable"].Index, match.Groups["Variable"].Length), definedType));

                // operators
                foreach (string op in operators)
                {
                    while (snapshot.Length > index + 1 && (index = text.IndexOf(op, index + 1)) > -1)
                    {
                        classifiedSpans.Add(new ClassificationSpan(new SnapshotSpan(snapshot.Start + index, op.Length), operatorType));
                    }
                }
            }

            return classifiedSpans;
        }

    }
}