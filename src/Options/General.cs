using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SqlFormatter
{
    internal partial class OptionsProvider
    {
        [ComVisible(true)]
        public class GeneralOptions : BaseOptionPage<General> { }
    }

    public class General : BaseOptionModel<General>
    {
        [Category("General")]
        [DisplayName("Indent string")]
        [DefaultValue("\t")]
        public string IndentString { get; set; } = "\t";

        [Category("General")]
        [DisplayName("Spaces per tab")]
        [DefaultValue(4)]
        public int SpacesPerTab { get; set; } = 4;

        [Category("General")]
        [DisplayName("Max line width")]
        [DefaultValue(999)]
        public int MaxLineWidth { get; set; } = 999;

        [Category("General")]
        [DisplayName("Expand comma lists")]
        [DefaultValue(true)]
        public bool ExpandCommaLists { get; set; } = true;

        [Category("General")]
        [DisplayName("Trailing commas")]
        [DefaultValue(false)]
        public bool TrailingCommas { get; set; } = false;

        [Category("General")]
        [DisplayName("Space after expanded comma")]
        [DefaultValue(false)]
        public bool SpaceAfterExpandedComma { get; set; } = false;

        [Category("General")]
        [DisplayName("Uppercase keywords")]
        [DefaultValue(true)]
        public bool UppercaseKeywords { get; set; } = true;

        [Category("General")]
        [DisplayName("Break join on sections")]
        [DefaultValue(false)]
        public bool BreakJoinOnSections { get; set; } = false;

        [Category("General")]
        [DisplayName("Keyword standardization")]
        [DefaultValue(false)]
        public bool KeywordStandardization { get; set; } = false;

        [Category("Expand")]
        [DisplayName("Expand boolean expressions")]
        [DefaultValue(true)]
        public bool ExpandBooleanExpressions { get; set; } = true;

        [Category("Expand")]
        [DisplayName("Expand between conditions")]
        [DefaultValue(true)]
        public bool ExpandBetweenConditions { get; set; } = true;

        [Category("Expand")]
        [DisplayName("Expand case statements")]
        [DefaultValue(true)]
        public bool ExpandCaseStatements { get; set; } = true;

        [Category("Expand")]
        [DisplayName("Expand in lists")]
        [DefaultValue(true)]
        public bool ExpandInLists { get; set; } = true;

        [Category("Line breaks")]
        [DisplayName("New clause line breaks")]
        [DefaultValue(1)]
        public int NewClauseLineBreaks { get; set; } = 1;

        [Category("Line breaks")]
        [DisplayName("New statement line breaks")]
        [DefaultValue(2)]
        public int NewStatementLineBreaks { get; set; } = 2;
    }
}
