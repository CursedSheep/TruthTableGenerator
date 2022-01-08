using System.Text;
using TruthTableGenerator.LogicEvaluator;
namespace TruthTableGenerator
{
    public partial class TruthTableGenerator : Form
    {
        public TruthTableGenerator()
        {
            InitializeComponent();
        }
        bool[,] GenerateTable(int col)
        {
            int rows = (int)Math.Pow(2, col);
            bool[,] generatedTable = new bool[col, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = col -1; j >= 0; j--)
                {
                    int num = (i / (int)Math.Pow(2, j)) % 2;
                    generatedTable[col - (j + 1), i] = Convert.ToBoolean(num);
                }
            }
            return generatedTable;
        }
        void ListViewHeaderWidth(ListView listViewInfo)
        {
            int HeaderWidth = (listViewInfo.Parent.Width - 2) / listViewInfo.Columns.Count;
            foreach (ColumnHeader header in listViewInfo.Columns)
                header.Width = HeaderWidth;
        }
        void TruthTableListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = TruthTableListView.Columns[e.ColumnIndex].Width;
        }
        List<string> GetVariables(string str)
        {
            List<string> a = new List<string>();
            foreach (var i in str)
                if (!a.Exists(x => x == i.ToString()) && ((i >= 'A' && i <= 'Z') || (i >= 'a' && i <= 'z')))
                    a.Add(i.ToString());

            return a;
        }
        void SetProgramStatus(SyntaxErrorCode errorCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Status: ");
            switch(errorCode)
            {
                case SyntaxErrorCode.Parenthesis:
                    sb.Append("Invalid Syntax! (Check parentheses)");
                    break;
                case SyntaxErrorCode.InvalidSymbol:
                    sb.Append("Invalid Symbol Detected!");
                    break;
                case SyntaxErrorCode.Variables:
                    sb.Append("No Variables Detected!");
                    break;
                case SyntaxErrorCode.InvalidStack:
                    sb.Append("Invalid Stack detected! (Check syntax)");
                    break;
                case SyntaxErrorCode.Valid:
                    sb.Append("Success");
                    break;
                default:
                    sb.Append("Running");
                    break;
            }
            programStatus.Text = sb.ToString();
        }
        void formulaTxt_TextChanged(object sender, EventArgs e)
        {
            if(TruthTableLogic.isInputValid(formulaTxt.Text) == SyntaxErrorCode.Valid)
            {
                var LogicVariables = GetVariables(formulaTxt.Text);
                var GeneratedTruthTable = GenerateTable(LogicVariables.Count);
                var isValid1 = TruthTableLogic.GetExpressions(formulaTxt.Text, LogicVariables, out List<(string str, LogicInstructions[] Instructions)> InstructionBlocks);
                if(isValid1 == SyntaxErrorCode.Valid)
                {
                    Dictionary<string, List<bool>> formulaResult = new Dictionary<string, List<bool>>();
                    for (int i = 0; i < GeneratedTruthTable.GetLength(1); i++)
                    {
                        Dictionary<string, bool> varList = new Dictionary<string, bool>();
                        for (int j = 0; j < GeneratedTruthTable.GetLength(0); j++)
                            varList.Add(LogicVariables[j], GeneratedTruthTable[j, i]);

                        foreach(var formula in InstructionBlocks)
                        {
                            var evalItem = TruthTableLogic.EvaluateExpression(formula.Instructions, varList, out bool result);
                            if (evalItem == SyntaxErrorCode.Valid)
                            {
                                if (!formulaResult.ContainsKey(formula.str))
                                    formulaResult[formula.str] = new List<bool>();

                                formulaResult[formula.str].Add(result);
                            }
                            else
                            {
                                SetProgramStatus(evalItem);
                                TruthTableListView.Columns.Clear();
                                TruthTableListView.Items.Clear();
                                return;
                            }
                        }

                    }

                    //clear all data in table
                    TruthTableListView.Columns.Clear();
                    TruthTableListView.Items.Clear();

                    foreach (var v in LogicVariables)
                        TruthTableListView.Columns.Add(v);

                    var formulaList = InstructionBlocks.Select(x => x.str);
                    foreach (var f in formulaList)
                        TruthTableListView.Columns.Add(f);

                    for (int i = 0; i < GeneratedTruthTable.GetLength(1); i++)
                    {
                        List<string> str = new List<string>();
                        for (int j = 0; j < GeneratedTruthTable.GetLength(0); j++)
                        {
                            str.Add(GeneratedTruthTable[j, i] ? "T" : "F");
                        }
                        foreach (var f in formulaList)
                        {
                            str.Add(formulaResult[f][i] ? "T" : "F");
                        }
                           
                        TruthTableListView.Items.Add(new ListViewItem(str.ToArray()));
                    }

                    TruthTableListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    ListViewHeaderWidth(TruthTableListView);

                    SetProgramStatus(SyntaxErrorCode.Valid);
                }
                else
                {
                    SetProgramStatus(isValid1);
                    TruthTableListView.Columns.Clear();
                    TruthTableListView.Items.Clear();
                    return;
                }
            }
            else
            {
                TruthTableListView.Columns.Clear();
                TruthTableListView.Items.Clear();
            }
        }
    }
}
