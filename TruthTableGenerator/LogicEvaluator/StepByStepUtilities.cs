using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthTableGenerator.LogicEvaluator
{
    internal class StepByStepUtilities
    {
        LogicInstructions[] InstrList;
        public StepByStepUtilities(LogicInstructions[] LogicalInstructionList)
        {
            InstrList = LogicalInstructionList;
        }
        public int GetBlockCount()
        {
            int count = 0;
            for (int i = 0; i < InstrList.Length; i++)
                if ((int)InstrList[i].Operator <= 5)
                    count++;
            return count;
        }
        private int GetIndexFromOffset(LogicInstructions[] list, int offset)
        {
            int count = 0;
            for (int i = 0; i < InstrList.Length; i++)
            {
                if ((int)InstrList[i].Operator <= 5)
                    count++;
                if (i >= offset)
                    return count;
            }
            throw new NotSupportedException();
        }
        private int GetStartOffsetFromIndex(int index)
        {
            int count = 0;
            for (int i = 0; i < InstrList.Length; i++)
            {
                if ((int)InstrList[i].Operator <= 5)
                    count++;
                if (count >= index + 1)
                    return i;
            }
            throw new NotSupportedException();
        }
        private int GetEndOffsetFromIndex(int startIndex)
        {
            int count = 0;
            for (int i = startIndex; i >= 0; i--)
            {
                if ((int)InstrList[i].Operator >= 1 && (int)InstrList[i].Operator <= 5)
                    count -= 1;
                else if ((int)InstrList[i].Operator == 0)
                    continue;
                else
                    count++;

                if (count == 1)
                    return i;
            }
            throw new NotSupportedException();
        }
        public List<LogicInstructions> GetBlock(int index)
        {
            List<LogicInstructions> list = new List<LogicInstructions>();
            int start = GetStartOffsetFromIndex(index);
            int end = GetEndOffsetFromIndex(start);
            for (int i = end; i <= start; i++)
                list.Add(InstrList[i]);
            return list;
        }
        public string InstrsToString(LogicInstructions[] Instrs, out int numOut)
        {
            StringBuilder sb = new StringBuilder();
            numOut = 0;
            int i = Instrs.Length - 1;
            while(i >= 0)
            {
                if ((int)Instrs[i].Operator >= 1 && (int)Instrs[i].Operator <= 5)
                {
                    sb.Append('(');
                    if (i - 1 > -1 && (int)Instrs[i - 1].Operator >= 0 && (int)Instrs[i - 1].Operator <= 5)
                    {
                        var operatorstr = Instrs[i].ToString();
                        var z = new StepByStepUtilities(Instrs);
                        var blokCount = z.GetBlockCount() - 2;
                        var getBlockz = z.GetBlock(blokCount);
                        var secondpart = z.InstrsToString(getBlockz.ToArray(), out int nMin);
                        i += nMin;
                        string lclname1;
                        if(i - 1 > -1 && (int)Instrs[i - 1].Operator >= 0 && (int)Instrs[i - 1].Operator <= 5)
                        {
                            var indexFromOffset = GetIndexFromOffset(Instrs,i);
                            var getBlockz2 = z.GetBlock(indexFromOffset-1);
                            lclname1 = z.InstrsToString(getBlockz2.ToArray(), out int nMin2);
                            i += nMin2;
                        }
                        else
                        {
                            lclname1 = (string)Instrs[i - 1].Operand;
                        }
                        sb.Append(lclname1);
                        sb.Append(operatorstr);
                        sb.Append(secondpart);
                        i -= 1;

                    }
                    else
                    {
                        var lclname1 = (string)Instrs[i - 2].Operand;
                        var lclname2 = (string)Instrs[i - 1].Operand;
                        var operatorstr = Instrs[i].ToString();
                        sb.Append(lclname1);
                        sb.Append(operatorstr);
                        sb.Append(lclname2);
                        i -= 2;
                    }
                    sb.Append(')');
                }
                else if (Instrs[i].Operator == LogicalOperator.Negation)
                {
                    if (i - 1 > -1 && (int)Instrs[i - 1].Operator >= 0 && (int)Instrs[i - 1].Operator <= 5)
                    {
                        var operatorstr = Instrs[i].ToString();
                        var z = new StepByStepUtilities(Instrs);
                        var blokCount = z.GetBlockCount() - 2;
                        var getBlockz = z.GetBlock(blokCount);
                        var secondpart = z.InstrsToString(getBlockz.ToArray(), out int nMin);
                        i += nMin;
                        sb.Append(operatorstr);
                        sb.Append(secondpart);

                        i -= 1;
                    }
                    else
                    {
                        var lclname1 = (string)Instrs[i - 1].Operand;
                        var operatorstr = Instrs[i].ToString();
                        sb.Append(operatorstr);
                        sb.Append(lclname1);
                        i -= 1;
                    }
                }
                else
                {
                    sb.Append(Instrs[i].Operand);
                }
                i--;
            }
            numOut = i - (Instrs.Length - 1);
            return sb.ToString();
        }
    }
}
