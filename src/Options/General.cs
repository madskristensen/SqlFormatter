using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace SqlFormatter
{
    internal partial class OptionsProvider
    {
        [ComVisible(true)]
        public class GeneralOptions : BaseOptionPage<General> { }
    }

    public class General : BaseOptionModel<General>, IRatingConfig
    {
        // Used for rating requests
        [Browsable(false)]
        public int RatingRequests { get; set; }



        [Category("General")]
        [DisplayName("SQL version")]
        [Description("Determines the version of the T-SQL language to use.")]
        [DefaultValue(SqlVersion.Sql160)]
        [TypeConverter(typeof(EnumConverter))]
        public SqlVersion SqlVersion { get; set; } = SqlVersion.Sql160;

        [Category("General")]
        [DisplayName("SQL engine")]
        [Description("Determines the SQL engine to use.")]
        [DefaultValue(SqlEngineType.All)]
        [TypeConverter(typeof(EnumConverter))]
        public SqlEngineType SqlEngineType { get; set; } = SqlEngineType.All;


        [Category("Alignment")]
        [DisplayName("Align clause bodies")]
        [Description("Align the bodies of clauses.")]
        [DefaultValue(true)]
        public bool AlignClauseBodies { get; set; } = true;

        [Category("Alignment")]
        [DisplayName("Align column definition fields")]
        [Description("Align the fields of column definitions.")]
        [DefaultValue(true)]
        public bool AlignColumnDefinitionFields { get; set; } = true;

        [Category("Alignment")]
        [DisplayName("Align set clause item")]
        [Description("Align the items in set clauses.")]
        [DefaultValue(true)]
        public bool AlignSetClauseItem { get; set; } = true;

        [Category("Paths")]
        [DisplayName("Allow external language paths")]
        [Description("Allow paths to external languages.")]
        [DefaultValue(true)]
        public bool AllowExternalLanguagePaths { get; set; } = true;

        [Category("Paths")]
        [DisplayName("Allow external library paths")]
        [Description("Allow paths to external libraries.")]
        [DefaultValue(true)]
        public bool AllowExternalLibraryPaths { get; set; } = true;

        [Category("Formatting")]
        [DisplayName("Format on save")]
        [Description("Format the document on save.")]
        [DefaultValue(false)]
        public bool FormatOnSave { get; set; }

        [Category("Formatting")]
        [DisplayName("As keyword on own line")]
        [Description("Place the AS keyword on its own line.")]
        [DefaultValue(true)]
        public bool AsKeywordOnOwnLine { get; set; } = true;

        [Category("Formatting")]
        [DisplayName("Include semicolons")]
        [Description("Include semicolons at the end of statements.")]
        [DefaultValue(false)]
        public bool IncludeSemicolons { get; set; }

        [Category("Indentation")]
        [DisplayName("Indent set clause")]
        [Description("Indent the set clause.")]
        [DefaultValue(false)]
        public bool IndentSetClause { get; set; }

        [Category("Formatting")]
        [DisplayName("Keyword casing")]
        [Description("Set the casing for keywords.")]
        [DefaultValue(KeywordCasing.Uppercase)]
        [TypeConverter(typeof(EnumConverter))]
        public KeywordCasing KeywordCasing { get; set; } = KeywordCasing.Uppercase;

        [Category("Indentation")]
        [DisplayName("Indentation size")]
        [Description("Set the size of indentation.")]
        [DefaultValue(4)]
        public int IndentationSize { get; set; } = 4;

        [Category("Indentation")]
        [DisplayName("Indent view body")]
        [Description("Indent the body of views.")]
        [DefaultValue(false)]
        public bool IndentViewBody { get; set; }

        [Category("Multiline")]
        [DisplayName("Multiline insert sources list")]
        [Description("Format the sources list in insert statements as multiline.")]
        [DefaultValue(true)]
        public bool MultilineInsertSourcesList { get; set; } = true;

        [Category("Multiline")]
        [DisplayName("Multiline insert targets list")]
        [Description("Format the targets list in insert statements as multiline.")]
        [DefaultValue(true)]
        public bool MultilineInsertTargetsList { get; set; } = true;

        [Category("Multiline")]
        [DisplayName("Multiline select elements list")]
        [Description("Format the select elements list as multiline.")]
        [DefaultValue(true)]
        public bool MultilineSelectElementsList { get; set; } = true;

        [Category("Multiline")]
        [DisplayName("Multiline set clause items")]
        [Description("Format the set clause items as multiline.")]
        [DefaultValue(true)]
        public bool MultilineSetClauseItems { get; set; } = true;

        [Category("Multiline")]
        [DisplayName("Multiline view columns list")]
        [Description("Format the view columns list as multiline.")]
        [DefaultValue(true)]
        public bool MultilineViewColumnsList { get; set; } = true;

        [Category("Multiline")]
        [DisplayName("Multiline where predicates list")]
        [Description("Format the where predicates list as multiline.")]
        [DefaultValue(true)]
        public bool MultilineWherePredicatesList { get; set; } = true;

        [Category("New Line")]
        [DisplayName("New line before close parenthesis in multiline list")]
        [Description("Insert a new line before the closing parenthesis in multiline lists.")]
        [DefaultValue(true)]
        public bool NewLineBeforeCloseParenthesisInMultilineList { get; set; } = true;

        [Category("New Line")]
        [DisplayName("New line before from clause")]
        [Description("Insert a new line before the from clause.")]
        [DefaultValue(true)]
        public bool NewLineBeforeFromClause { get; set; } = true;

        [Category("New Line")]
        [DisplayName("New line before group by clause")]
        [Description("Insert a new line before the group by clause.")]
        [DefaultValue(true)]
        public bool NewLineBeforeGroupByClause { get; set; } = true;

        [Category("New Line")]
        [DisplayName("New line before having clause")]
        [Description("Insert a new line before the having clause.")]
        [DefaultValue(true)]
        public bool NewLineBeforeHavingClause { get; set; } = true;

        [Category("New Line")]
        [DisplayName("New line before join clause")]
        [Description("Insert a new line before the join clause.")]
        [DefaultValue(true)]
        public bool NewLineBeforeJoinClause { get; set; } = true;

        [Category("New Line")]
        [DisplayName("New line before offset clause")]
        [Description("Insert a new line before the offset clause.")]
        [DefaultValue(true)]
        public bool NewLineBeforeOffsetClause { get; set; } = true;

        [Category("New Line")]
        [DisplayName("New line before open parenthesis in multiline list")]
        [Description("Insert a new line before the opening parenthesis in multiline lists.")]
        [DefaultValue(false)]
        public bool NewLineBeforeOpenParenthesisInMultilineList { get; set; }

        [Category("New Line")]
        [DisplayName("New line before order by clause")]
        [Description("Insert a new line before the order by clause.")]
        [DefaultValue(true)]
        public bool NewLineBeforeOrderByClause { get; set; } = true;

        [Category("New Line")]
        [DisplayName("New line before output clause")]
        [Description("Insert a new line before the output clause.")]
        [DefaultValue(true)]
        public bool NewLineBeforeOutputClause { get; set; } = true;

        [Category("New Line")]
        [DisplayName("New line before where clause")]
        [Description("Insert a new line before the where clause.")]
        [DefaultValue(true)]
        public bool NewLineBeforeWhereClause { get; set; } = true;

        [Category("New Line")]
        [DisplayName("New line before window clause")]
        [Description("Insert a new line before the window clause.")]
        [DefaultValue(true)]
        public bool NewLineBeforeWindowClause { get; set; } = true;

        [Category("New Line")]
        [DisplayName("Newline formatted check constraint")]
        [Description("Format check constraints with new lines.")]
        [DefaultValue(false)]
        public bool NewlineFormattedCheckConstraint { get; set; }

        [Category("New Line")]
        [DisplayName("New line formatted index definition")]
        [Description("Format index definitions with new lines.")]
        [DefaultValue(false)]
        public bool NewLineFormattedIndexDefinition { get; set; }

        [Category("New Line")]
        [DisplayName("Number of newlines after statement")]
        [Description("Set the number of new lines after a statement.")]
        [DefaultValue(1)]
        public int NumNewlinesAfterStatement { get; set; } = 1;

        [Category("Spacing")]
        [DisplayName("Space between data type and parameters")]
        [Description("Insert a space between data type and parameters.")]
        [DefaultValue(true)]
        public bool SpaceBetweenDataTypeAndParameters { get; set; } = true;

        [Category("Spacing")]
        [DisplayName("Space between parameters in data type")]
        [Description("Insert a space between parameters in data type.")]
        [DefaultValue(true)]
        public bool SpaceBetweenParametersInDataType { get; set; } = true;
    }
}
