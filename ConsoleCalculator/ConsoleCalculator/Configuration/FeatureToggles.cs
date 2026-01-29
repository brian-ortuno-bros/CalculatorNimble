namespace ConsoleCalculator.Configuration
{
    public static class FeatureToggles
    {
        //The values here can be set based on environment, config file, database,apis, etc.
        #region Feature Toggles
        private static readonly bool _is_support_max_two_numbers_enabled = true;
        private static readonly bool _is_remove_max_rule_for_numbers_enabled = true;
        private static readonly bool _is_support_new_line_delimiter_enabled = true;
        private static readonly bool _is_deny_negative_numbers_enabled = true;
        private static readonly bool _is_greater_than_1000_enabled = true;
        private static readonly bool _is_support_custom_single_char_delimiter_enabled = true;
        private static readonly bool _is_support_custom_many_char_delimiter_enabled = true;
        private static readonly bool _is_support_custom_multiple_delimiter_enabled = true;
        #endregion

        #region Feature Toggle Accessors

        //requirement 1
        public static bool IsSupportMaxTwoNumbersEnabled
        {
            get { return _is_support_max_two_numbers_enabled; }
        }
        //requirement 2
        public static bool IsRemoveMaxRuleForNumbersEnabled
        {
            get { return _is_remove_max_rule_for_numbers_enabled; }
        }
        //requirement 3
        public static bool IsSupportNewLineDelimiterEnabled
        {
            get { return _is_support_new_line_delimiter_enabled; }
        }
        //requirement 4
        public static bool IsDenyNegativeNumbersEnabled
        {
            get { return _is_deny_negative_numbers_enabled; }
        }
        //requirement 5
        public static bool IsGreaterThan1000Enabled
        {
            get { return _is_greater_than_1000_enabled; }
        }
        //requirement 6
        public static bool IsSupportCustomSingleCharDelimiterEnabled
        {
            get { return _is_support_custom_single_char_delimiter_enabled; }
        }
        //requirement 7
        public static bool IsSupportCustomManyCharDelimiterEnabled
        {
            get { return _is_support_custom_many_char_delimiter_enabled; }
        }
        //requirement 8
        public static bool IsSupportCustomMultipleDelimiterEnabled
        {
            get { return _is_support_custom_multiple_delimiter_enabled; }
        }
        #endregion
    }
}
