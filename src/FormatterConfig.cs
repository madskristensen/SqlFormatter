using System.Threading.Tasks;
using EditorConfig.Core;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace SqlFormatter
{
    internal static class FormatterConfig
    {
        public static async Task<SqlScriptGeneratorOptions> GetOptionsAsync(string fullPath)
        {
            var parser = new EditorConfigParser();
            FileConfiguration rules = parser.Parse(fullPath);

            General options = await General.GetLiveInstanceAsync();

            return new SqlScriptGeneratorOptions()
            {
                AlignClauseBodies = GetValue(rules, "align_clause_bodies", options.AlignClauseBodies),
                AlignColumnDefinitionFields = GetValue(rules, "align_column_definition_fields", options.AlignColumnDefinitionFields),
                AllowExternalLanguagePaths = GetValue(rules, "allow_external_language_paths", options.AllowExternalLanguagePaths),
                AlignSetClauseItem = GetValue(rules, "align_set_clause_item", options.AlignSetClauseItem),
                AllowExternalLibraryPaths = GetValue(rules, "allow_external_library_paths", options.AllowExternalLibraryPaths),
                AsKeywordOnOwnLine = GetValue(rules, "as_keyword_on_own_line", options.AsKeywordOnOwnLine),
                IncludeSemicolons = GetValue(rules, "include_semicolons", options.IncludeSemicolons),
                IndentSetClause = GetValue(rules, "indent_set_clause", options.IndentSetClause),
                KeywordCasing = GetValue(rules, "keyword_casing", options.KeywordCasing),
                IndentationSize = GetValue(rules, "indentation_size", options.IndentationSize),
                IndentViewBody = GetValue(rules, "indent_view_body", options.IndentViewBody),
                MultilineInsertSourcesList = GetValue(rules, "multiline_insert_sources_list", options.MultilineInsertSourcesList),
                MultilineInsertTargetsList = GetValue(rules, "multiline_insert_targets_list", options.MultilineInsertTargetsList),
                MultilineSelectElementsList = GetValue(rules, "multiline_select_elements_list", options.MultilineSelectElementsList),
                MultilineSetClauseItems = GetValue(rules, "multiline_set_clause_items", options.MultilineSetClauseItems),
                MultilineViewColumnsList = GetValue(rules, "multiline_view_columns_list", options.MultilineViewColumnsList),
                MultilineWherePredicatesList = GetValue(rules, "multiline_where_predicates_list", options.MultilineWherePredicatesList),
                NewLineBeforeCloseParenthesisInMultilineList = GetValue(rules, "new_line_before_close_parenthesis_in_multiline_list", options.NewLineBeforeCloseParenthesisInMultilineList),
                NewLineBeforeFromClause = GetValue(rules, "new_line_before_from_clause", options.NewLineBeforeFromClause),
                NewLineBeforeGroupByClause = GetValue(rules, "new_line_before_group_by_clause", options.NewLineBeforeGroupByClause),
                NewLineBeforeHavingClause = GetValue(rules, "new_line_before_having_clause", options.NewLineBeforeHavingClause),
                NewLineBeforeJoinClause = GetValue(rules, "new_line_before_join_clause", options.NewLineBeforeJoinClause),
                NewLineBeforeOffsetClause = GetValue(rules, "new_line_before_offset_clause", options.NewLineBeforeOffsetClause),
                NewLineBeforeOpenParenthesisInMultilineList = GetValue(rules, "new_line_before_open_parenthesis_in_multiline_list", options.NewLineBeforeOpenParenthesisInMultilineList),
                NewLineBeforeOrderByClause = GetValue(rules, "new_line_before_order_by_clause", options.NewLineBeforeOrderByClause),
                NewLineBeforeOutputClause = GetValue(rules, "new_line_before_output_clause", options.NewLineBeforeOutputClause),
                NewLineBeforeWhereClause = GetValue(rules, "new_line_before_where_clause", options.NewLineBeforeWhereClause),
                NewLineBeforeWindowClause = GetValue(rules, "new_line_before_window_clause", options.NewLineBeforeWindowClause),
                NewlineFormattedCheckConstraint = GetValue(rules, "newline_formatted_check_constraint", options.NewlineFormattedCheckConstraint),
                NewLineFormattedIndexDefinition = GetValue(rules, "newline_formatted_index_definition", options.NewLineFormattedIndexDefinition),
                NumNewlinesAfterStatement = GetValue(rules, "num_newlines_after_statement", options.NumNewlinesAfterStatement),
                SpaceBetweenDataTypeAndParameters = GetValue(rules, "space_between_data_type_and_parameters", options.SpaceBetweenDataTypeAndParameters),
                SpaceBetweenParametersInDataType = GetValue(rules, "space_between_parameters_in_data_type", options.SpaceBetweenParametersInDataType),
                SqlEngineType = GetValue(rules, "sql_engine_type", options.SqlEngineType),
                SqlVersion = GetValue(rules, "sql_version", options.SqlVersion),
            };
        }

        private static T GetValue<T>(FileConfiguration rule, string property, T defaultValue)
        {
            try
            {
                if (rule.Properties.TryGetValue(property, out var value))
                {
                    if (defaultValue is KeywordCasing && Enum.TryParse(value, true, out KeywordCasing casing))
                    {
                        return (T)((object)casing);
                    }

                    if (defaultValue is SqlVersion && Enum.TryParse(value, true, out SqlVersion version))
                    {
                        return (T)((object)version);
                    }

                    if (defaultValue is SqlEngineType && Enum.TryParse(value, true, out SqlEngineType engine))
                    {
                        return (T)((object)engine);
                    }

                    return (T)Convert.ChangeType(value, typeof(T));
                }

            }
            catch (Exception ex)
            {
                ex.Log();
            }

            return defaultValue;
        }
    }
}
