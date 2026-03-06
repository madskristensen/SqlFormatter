using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.VisualStudio.Text.Editor;

namespace SqlFormatter
{
    internal static class FormatterConfig
    {
        /// <summary>
        /// Try to get formatting options from the Visual Studio CodingConventionsSnapshot exposed on the text view
        /// options. Falls back to file-based parsing when the snapshot isn't available.
        /// </summary>
        public static async Task<SqlScriptGeneratorOptions> GetOptionsAsync(ITextView textView)
        {
            try
            {
                IReadOnlyDictionary<string, object> conventions = textView?.Options?.GetOptionValue<IReadOnlyDictionary<string, object>>("CodingConventionsSnapshot");
                if (conventions != null)
                {
                    General options = await General.GetLiveInstanceAsync();

                    var scriptOptions = new SqlScriptGeneratorOptions()
                    {
                        AlignClauseBodies = GetValue(conventions, "align_clause_bodies", options.AlignClauseBodies),
                        AlignColumnDefinitionFields = GetValue(conventions, "align_column_definition_fields", options.AlignColumnDefinitionFields),
                        AllowExternalLanguagePaths = GetValue(conventions, "allow_external_language_paths", options.AllowExternalLanguagePaths),
                        AlignSetClauseItem = GetValue(conventions, "align_set_clause_item", options.AlignSetClauseItem),
                        AllowExternalLibraryPaths = GetValue(conventions, "allow_external_library_paths", options.AllowExternalLibraryPaths),
                        AsKeywordOnOwnLine = GetValue(conventions, "as_keyword_on_own_line", options.AsKeywordOnOwnLine),
                        IncludeSemicolons = GetValue(conventions, "include_semicolons", options.IncludeSemicolons),
                        IndentSetClause = GetValue(conventions, "indent_set_clause", options.IndentSetClause),
                        KeywordCasing = GetValue(conventions, "keyword_casing", options.KeywordCasing),
                        IndentationSize = GetValue(conventions, "indentation_size", options.IndentationSize),
                        IndentViewBody = GetValue(conventions, "indent_view_body", options.IndentViewBody),
                        MultilineInsertSourcesList = GetValue(conventions, "multiline_insert_sources_list", options.MultilineInsertSourcesList),
                        MultilineInsertTargetsList = GetValue(conventions, "multiline_insert_targets_list", options.MultilineInsertTargetsList),
                        MultilineSelectElementsList = GetValue(conventions, "multiline_select_elements_list", options.MultilineSelectElementsList),
                        MultilineSetClauseItems = GetValue(conventions, "multiline_set_clause_items", options.MultilineSetClauseItems),
                        MultilineViewColumnsList = GetValue(conventions, "multiline_view_columns_list", options.MultilineViewColumnsList),
                        MultilineWherePredicatesList = GetValue(conventions, "multiline_where_predicates_list", options.MultilineWherePredicatesList),
                        NewLineBeforeCloseParenthesisInMultilineList = GetValue(conventions, "new_line_before_close_parenthesis_in_multiline_list", options.NewLineBeforeCloseParenthesisInMultilineList),
                        NewLineBeforeFromClause = GetValue(conventions, "new_line_before_from_clause", options.NewLineBeforeFromClause),
                        NewLineBeforeGroupByClause = GetValue(conventions, "new_line_before_group_by_clause", options.NewLineBeforeGroupByClause),
                        NewLineBeforeHavingClause = GetValue(conventions, "new_line_before_having_clause", options.NewLineBeforeHavingClause),
                        NewLineBeforeJoinClause = GetValue(conventions, "new_line_before_join_clause", options.NewLineBeforeJoinClause),
                        NewLineBeforeOffsetClause = GetValue(conventions, "new_line_before_offset_clause", options.NewLineBeforeOffsetClause),
                        NewLineBeforeOpenParenthesisInMultilineList = GetValue(conventions, "new_line_before_open_parenthesis_in_multiline_list", options.NewLineBeforeOpenParenthesisInMultilineList),
                        NewLineBeforeOrderByClause = GetValue(conventions, "new_line_before_order_by_clause", options.NewLineBeforeOrderByClause),
                        NewLineBeforeOutputClause = GetValue(conventions, "new_line_before_output_clause", options.NewLineBeforeOutputClause),
                        NewLineBeforeWhereClause = GetValue(conventions, "new_line_before_where_clause", options.NewLineBeforeWhereClause),
                        NewLineBeforeWindowClause = GetValue(conventions, "new_line_before_window_clause", options.NewLineBeforeWindowClause),
                        NewlineFormattedCheckConstraint = GetValue(conventions, "newline_formatted_check_constraint", options.NewlineFormattedCheckConstraint),
                        NewLineFormattedIndexDefinition = GetValue(conventions, "newline_formatted_index_definition", options.NewLineFormattedIndexDefinition),
                        NumNewlinesAfterStatement = GetValue(conventions, "num_newlines_after_statement", options.NumNewlinesAfterStatement),
                        SpaceBetweenDataTypeAndParameters = GetValue(conventions, "space_between_data_type_and_parameters", options.SpaceBetweenDataTypeAndParameters),
                        SpaceBetweenParametersInDataType = GetValue(conventions, "space_between_parameters_in_data_type", options.SpaceBetweenParametersInDataType),
                        SqlEngineType = GetValue(conventions, "sql_engine_type", options.SqlEngineType),
                        SqlVersion = GetValue(conventions, "sql_version", options.SqlVersion),
                    };

                    TrySetPreserveComments(scriptOptions, GetValue(conventions, "preserve_comments", options.PreserveComments));

                    return scriptOptions;
                }
            }
            catch (Exception ex)
            {
                ex.Log();
            }

            // Fall back to file-based parsing
            return new SqlScriptGeneratorOptions();
        }

        private static T GetValue<T>(IReadOnlyDictionary<string, object> conventions, string property, T defaultValue)
        {
            try
            {
                if (conventions != null && conventions.TryGetValue(property, out var valueObj))
                {
                    if (valueObj is string value)
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

                    // If the value is already the correct type
                    if (valueObj is T tVal)
                    {
                        return tVal;
                    }

                    return (T)Convert.ChangeType(valueObj, typeof(T));
                }
            }
            catch (Exception ex)
            {
                ex.Log();
            }

            return defaultValue;
        }

        private static void TrySetPreserveComments(SqlScriptGeneratorOptions scriptOptions, bool preserveComments)
        {
            try
            {
                var property = typeof(SqlScriptGeneratorOptions).GetProperty("PreserveComments");

                if (property?.CanWrite == true && property.PropertyType == typeof(bool))
                {
                    property.SetValue(scriptOptions, preserveComments);
                }
            }
            catch (Exception ex)
            {
                ex.Log();
            }
        }
    }
}
