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
            bool isoperandnull = Operand == null;
            if (Operator == LogicalOperator.LdBool || Operator == LogicalOperator.Ldloc)
                return $"{Operator}{(isoperandnull ? "" : ",")}{Operand ?? ""}";
            else
                return $"{stringOperators[Operator]}";
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
