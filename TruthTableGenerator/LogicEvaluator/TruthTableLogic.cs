using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthTableGenerator.LogicEvaluator
{
    internal class TruthTableLogic
    {
        static bool hasVariables(string input)
        {
            foreach (var c in input)
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    return true;
            return false;
        }
        static bool hasValidParenthesisValue(string input)
        {
            int num = 0;
            for (int i = 0; i < input.Length; i++)
                if (input[i] == '(')
                    num++;
                else if (input[i] == ')')
                    num--;
            return num == 0;
        }
        static string ReplaceSymbols(string str)
        {
            return str.Replace(" ", "")
                      .Replace("¬", "~")
                      .Replace("↔", "⟷")
                      .Replace("∼", "~")
                      .Replace("⇒", "→");
        }
        public static SyntaxErrorCode isInputValid(string input)
        {
            if (!hasVariables(input))
                return SyntaxErrorCode.Variables;
            else if (!hasValidParenthesisValue(input))
                return SyntaxErrorCode.Parenthesis;

            return SyntaxErrorCode.Valid;
        }
        public static SyntaxErrorCode EvaluateExpression(string s, Dictionary<string, bool> data, out bool Result)
        {
            Result = false;
            s = ReplaceSymbols(s);
            var isValid = isInputValid(s);
            if(isValid == SyntaxErrorCode.Valid)
            {
                isValid = LogicStrToInstr.ParseExpressions(s, data, out LogicInstructions[] r);
                if(isValid == SyntaxErrorCode.Valid)
                {
                    isValid = EvalLogic.ExecuteInstructions(r, out bool Result2);
                    if(isValid == SyntaxErrorCode.Valid)
                    {
                        Result = Result2;
                    }
                }
            }
            return isValid;
        }
    }
}
