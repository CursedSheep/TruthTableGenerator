using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthTableGenerator.LogicEvaluator
{
    internal class EvalLogic
    {

        static bool IsValidStackPop(Stack<bool> s, LogicalOperator op)
        {
            switch(op)
            {
                case LogicalOperator.Conjunction:
                case LogicalOperator.Disjunction:
                case LogicalOperator.Conditional:
                case LogicalOperator.Biconditional:
                case LogicalOperator.ExclusiveOr:
                    return s.Count >= 2;
                case LogicalOperator.Negation:
                    return s.Count >= 1;
            }
            return true;
        }
        internal static SyntaxErrorCode ExecuteInstructions(LogicInstructions[] instructions, out bool retResult)
        {
            retResult = false;
            Stack<bool> MachineStack = new Stack<bool>();
            foreach (var item in instructions)
            {
                if (!IsValidStackPop(MachineStack, item.Operator))
                    return SyntaxErrorCode.InvalidStack;
                switch (item.Operator)
                {
                    case LogicalOperator.LdBool:
                        MachineStack.Push((bool)item.Operand);
                        break;
                    case LogicalOperator.Negation:
                        MachineStack.Push(!MachineStack.Pop());
                        break;
                    case LogicalOperator.Conjunction:
                        {
                            bool o2 = MachineStack.Pop();
                            bool o1 = MachineStack.Pop();
                            MachineStack.Push(o1 && o2);
                        }
                        break;
                    case LogicalOperator.Disjunction:
                        {
                            bool o2 = MachineStack.Pop();
                            bool o1 = MachineStack.Pop();
                            MachineStack.Push(o1 || o2);
                        }
                        break;
                    case LogicalOperator.Conditional:
                        {
                            bool o2 = MachineStack.Pop();
                            bool o1 = MachineStack.Pop();
                            if (o1 && o2)
                                MachineStack.Push(true);
                            else if (!o1 && o2)
                                MachineStack.Push(true);
                            else if (!o1 && !o2)
                                MachineStack.Push(true);
                            else
                                MachineStack.Push(false);
                        }
                        break;
                    case LogicalOperator.Biconditional:
                        {
                            bool o2 = MachineStack.Pop();
                            bool o1 = MachineStack.Pop();
                            MachineStack.Push(GetBiConditional(o1, o2));
                        }
                        break;
                    case LogicalOperator.ExclusiveOr:
                        {
                            bool o2 = MachineStack.Pop();
                            bool o1 = MachineStack.Pop();
                            MachineStack.Push(!GetBiConditional(o1, o2));
                        }
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            if (MachineStack.Count == 1)
                retResult = MachineStack.Pop();
            else
                return SyntaxErrorCode.InvalidStack;
            return SyntaxErrorCode.Valid;
        }
        static bool GetBiConditional(bool o1, bool o2)
        {
            if (o1 && o2)
                return true;
            else if (!o1 && !o2)
                return true;
            else
                return false;
        }
    }
}
