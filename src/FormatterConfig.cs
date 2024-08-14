using System.Threading.Tasks;
using EditorConfig.Core;
using PoorMansTSqlFormatterRedux.Formatters;

namespace SqlFormatter
{
    internal static class FormatterConfig
    {
        public static async Task<TSqlStandardFormatterOptions> GetOptionsAsync(string fullPath)
        {
            var parser = new EditorConfigParser();
            FileConfiguration rules = parser.Parse(fullPath);

            General options = await General.GetLiveInstanceAsync();

            return new TSqlStandardFormatterOptions()
            {
                KeywordStandardization = GetValue(rules, "keyword_standardization", options.KeywordStandardization),
                SpacesPerTab = GetValue(rules, "spaces_per_tab", options.SpacesPerTab),
                ExpandCommaLists = GetValue(rules, "expand_comma_lists", options.ExpandCommaLists),
                TrailingCommas = GetValue(rules, "trailing_commas", options.TrailingCommas),
                SpaceAfterExpandedComma = GetValue(rules, "space_after_expanded_comma", options.SpaceAfterExpandedComma),
                UppercaseKeywords = GetValue(rules, "uppercase_keywords", options.UppercaseKeywords),
                BreakJoinOnSections = GetValue(rules, "break_join_on_sections", options.ExpandBooleanExpressions),
                ExpandBooleanExpressions = GetValue(rules, "expand_boolean_expressions", options.ExpandBetweenConditions),
                ExpandBetweenConditions = GetValue(rules, "expand_between_conditions", options.ExpandBetweenConditions),
                ExpandCaseStatements = GetValue(rules, "expand_case_statements", options.ExpandCaseStatements),
                ExpandInLists = GetValue(rules, "expand_in_lists", options.ExpandInLists),
                MaxLineWidth = GetValue(rules, "max_line_width", options.MaxLineWidth),
                IndentString = GetValue(rules, "indent_string", options.IndentString),
                NewClauseLineBreaks = GetValue(rules, "new_clause_line_breaks", options.NewClauseLineBreaks),
                NewStatementLineBreaks = GetValue(rules, "new_statement_line_breaks", options.NewStatementLineBreaks),
            };
        }

        private static T GetValue<T>(FileConfiguration rule, string property, T defaultValue)
        {
            try
            {
                return rule.Properties.TryGetValue(property, out var value) ? (T)Convert.ChangeType(value, typeof(T)) : defaultValue;
            }
            catch
            {
                throw new ArgumentException($"The property '{property}' must be of type {typeof(T).Name} in the .editorconfig");
            }
        }
    }
}
