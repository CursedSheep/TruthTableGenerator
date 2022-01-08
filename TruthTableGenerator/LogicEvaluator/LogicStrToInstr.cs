using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthTableGenerator.LogicEvaluator
{
    internal class LogicStrToInstr
    {
        static Dictionary<string, LogicalOperator> LogicalOperators = new Dictionary<string, LogicalOperator>() {

            {
              "~",
              LogicalOperator.Negation
            }, {
              "∧",
              LogicalOperator.Conjunction
            }, {
              "∨",
              LogicalOperator.Disjunction
            }, {
              "→",
              LogicalOperator.Conditional
            }, {
              "⟷",
              LogicalOperator.Biconditional
            }, {
              "⊕",
              LogicalOperator.ExclusiveOr
            }

        };
        internal static SyntaxErrorCode ParseExpressions(string s, List<string> variables, out LogicInstructions[] Result)
        {
            Result = null;
            List<LogicInstructions> result = new List<LogicInstructions>();
            SortedDictionary<int, Stack<LogicInstructions>> tmpOperator = new SortedDictionary<int, Stack<LogicInstructions>>();
            StringBuilder sb = new StringBuilder();
            int parenthesisCount = 0;
            for (int i = 0; i < s.Length; i++)
            {
                sb.Append(s[i]);
                string currstr = sb.ToString();
                if (LogicalOperators.ContainsKey(currstr))
                {
                    var instruction = new LogicInstructions(LogicalOperators[currstr], null);

                    if (!tmpOperator.ContainsKey(parenthesisCount))
                        tmpOperator[parenthesisCount] = new Stack<LogicInstructions>();
                    tmpOperator[parenthesisCount].Push(instruction);
                    sb.Clear();
                }
                else if (currstr.All(x => char.IsLetter(x)) && variables.Exists(x => x == currstr))
                {
                    //var selectedData = data[currstr];
                    result.Add(new LogicInstructions(LogicalOperator.Ldloc, currstr));
                    sb.Clear();

                    if (tmpOperator.ContainsKey(parenthesisCount) && tmpOperator[parenthesisCount].Count > 0)
                    {
                        //result.Add(tmpOperator[parenthesisCount].Pop());
                        var item = tmpOperator[parenthesisCount];
                        if (item.Count > 0)
                        {
                            for (int z = item.Count; z > 0; z--)
                                result.Add(item.Pop());
                        }
                    }
                }
                else if (currstr == "(" || currstr == "[")
                {
                    parenthesisCount++;
                    sb.Clear();
                }
                else if (currstr == ")" || currstr == "]")
                {
                    parenthesisCount--;
                    sb.Clear();
                    if (tmpOperator.ContainsKey(parenthesisCount) && tmpOperator[parenthesisCount].Count > 0)
                    {
                        //result.Add(tmpOperator[parenthesisCount].Pop());
                        var item = tmpOperator[parenthesisCount];
                        if (item.Count > 0)
                        {
                            for (int z = item.Count; z > 0; z--)
                                result.Add(item.Pop());
                        }
                    }
                }
                else return SyntaxErrorCode.InvalidSymbol;
            }
            var reverse = tmpOperator.Reverse();
            foreach (var item in reverse)
            {
                if (item.Value.Count > 0)
                {
                    int num = item.Value.Count;
                    for (int i = 0; i < num; i++)
                        result.Add(item.Value.Pop());
                }
            }
            Result = result.ToArray();

            return SyntaxErrorCode.Valid;
        }
    }
}
