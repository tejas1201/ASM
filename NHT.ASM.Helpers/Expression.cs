using System;
using System.Collections.Generic;
using Flee.PublicTypes;

namespace NHT.ASM.Helpers
{
    public static class Expression<T> where T : Type
    {
        /// <summary>
        /// Evaluates an expression to an object like <see cref="bool"/> or <see cref="int"/> bases on a set of variables that should be used when evaluating
        /// For example evaluating to bool:
        ///     Expression:     a > 6 AND b = true
        ///     With variables: a = 7 and b = false (these values will come from user input for parameters in the projectmodel)
        ///     Will result in: false
        /// Uses https://github.com/mparlak/Flee for evaluation
        /// </summary>
        /// <param name="expression">The expression to be evaluated like a > 6 AND b = true</param>
        /// <param name="variables">A dictionary with the values for the variables and the letters they should replace in the evaluation</param>
        /// <returns></returns>
        public static T Evaluate(string expression, Dictionary<string, object> variables)
        {
            var context = new ExpressionContext();
            VariableCollection vars = context.Variables;
            foreach (KeyValuePair<string, object> variable in variables)
            {
                vars.Add(variable.Key, variable.Value);
            }
           return context.CompileGeneric<T>(expression).Evaluate();
        }
    }
}
