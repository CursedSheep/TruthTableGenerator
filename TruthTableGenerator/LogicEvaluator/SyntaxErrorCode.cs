using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthTableGenerator.LogicEvaluator
{
    internal enum SyntaxErrorCode : sbyte
    {
        Valid,
        Parenthesis,
        Variables,
        InvalidSymbol,
        InvalidStack
    }
}
