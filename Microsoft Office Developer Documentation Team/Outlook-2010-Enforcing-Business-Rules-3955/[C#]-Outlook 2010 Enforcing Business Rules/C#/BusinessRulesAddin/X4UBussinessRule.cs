using System;

namespace BusinessRulesAddin {

    /// <summary>
    /// Delegate declares the Method signature that checks the BusinessRule and returns true or false
    /// </summary>
    /// <returns>True if the rule is ok</returns>
    public delegate bool BusinessRuleCheckMethod();

    /// <summary>
    /// This class defines a business rule
    /// </summary>
    public class X4UBusinessRule {

        /// <summary>
        /// The Name of the business rule
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A description displayed to the user if the rule is not met
        /// </summary>
        public string Description { get; private set; }        
        
        /// <summary>
        /// A function pointer that points to the method that checks if the business rule is valid
        /// </summary>
        public BusinessRuleCheckMethod IsValid { get; private set; }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        /// <param name="ruleValidator">Function pointer to the validation method</param>
        public X4UBusinessRule(string name, string description, BusinessRuleCheckMethod ruleValidator) {
            IsValid = ruleValidator;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// This method executes the method that the rule points to and returns the result
        /// </summary>
        /// <param name="rule">The businesrule to check</param>
        /// <returns>True if the rule is valid</returns>
        public static bool IsRuleValid(X4UBusinessRule rule) {
            return rule.IsValid();
        }
    }
}
