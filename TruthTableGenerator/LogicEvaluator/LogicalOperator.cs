using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthTableGenerator.LogicEvaluator
{
    internal enum LogicalOperator
    {
        Negation,
        Conjunction,
        Disjunction,
        Conditional,
        Biconditional,
        ExclusiveOr,
        LdBool = 999
    }
}
