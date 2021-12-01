using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthTableGenerator.LogicEvaluator
{
    internal class LogicInstructions
    {
        public LogicalOperator Operator;
        public object Operand;
        public LogicInstructions(LogicalOperator _Operator, object _Operand)
        {
            Operator = _Operator;
            Operand = _Operand;
        }
        public override string ToString()
        {
            if (Operator == LogicalOperator.LdBool)
                return $"{Operator},{Operand ?? ""}";
            else
                return $"{stringOperators[Operator]},{Operand ?? ""}";
        }
        static Dictionary<LogicalOperator, string> stringOperators = new Dictionary<LogicalOperator, string>() {

              {
                LogicalOperator.Negation, "~"
              }, {
                LogicalOperator.Conjunction,
                "∧"
              }, {
                LogicalOperator.Disjunction,
                "∨"
              }, {
                LogicalOperator.Conditional,
                "→"
              }, {
                LogicalOperator.Biconditional,
                "⟷"
              }, {
                LogicalOperator.ExclusiveOr,
                "⊕"
              }

            };
    }
}
