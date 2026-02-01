using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[][] ints = readFile();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int block = 1;
                    int r = 0;
                    int c = 0;
                    if (ints[i][j] != 0)
                    {
                        if (i <= 2)
                        {

                            if (j <= 2)
                            {
                                block = 1;
                            }
                            else if (j <= 5)
                            {
                                block = 2;
                            }
                            else
                            {
                                block = 3;
                            }
                        }
                        else if (i <= 5)
                        {

                            if (j <= 2)
                            {
                                block = 4;
                            }
                            else if (j <= 5)
                            {
                                block = 5;
                            }
                            else
                            {
                                block = 6;
                            }

                        }
                        else
                        {


                            if (j <= 2)
                            {
                                block = 7;
                            }
                            else if (j <= 5)
                            {
                                block = 8;
                            }
                            else
                            {
                                block = 9;
                            }
                        }

                        r = i % 3;
                        c = j % 3;

                        updateBlock(block, r, c, ints[i][j]);
                    }
                }
            }

            PriorityQueue = new PriorityQueue<(int block, UserControl1 uc), int>();
            fillPQ();
        }

        static int[][] readFile()
        {
            string contents = File.ReadAllText(@"C:\Users\anand\Downloads\play\639036710242318728");
            int[][] board = new int[9][];
            int index = 0;
            for (int i = 0; i < 9; i++)
            {
                board[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    while (!char.IsDigit(contents[index]))
                    {
                        index++;
                    }
                    board[i][j] = contents[index++] - '0';
                }
            }

            return board;
        }

        private void updateBlock(int block, int row, int col, int value)
        {

            var childs = UIHelper.FindVisualChildren<UserControl2>(this);
            var x = childs.FirstOrDefault(y => y.Block == block);

            x.Update(row, col, value);
        }


        private void UserControl2_TextChanged(object sender, EventArgs e)
        {
            ValueUpdatedEventArgs x = e as ValueUpdatedEventArgs;
            switch (x.Box)
            {
                case 1:
                    updateOtherBlocks(x, 2, 3, 4, 7);
                    break;
                case 2:
                    updateOtherBlocks(x, 1, 3, 5, 8);
                    break;
                case 3:
                    updateOtherBlocks(x, 1, 2, 6, 9);
                    break;
                case 4:
                    updateOtherBlocks(x, 5, 6, 1, 7);
                    break;
                case 5:
                    updateOtherBlocks(x, 4, 6, 2, 8);
                    break;
                case 6:
                    updateOtherBlocks(x, 4, 5, 3, 9);
                    break;
                case 7:
                    updateOtherBlocks(x, 8, 9, 1, 4);
                    break;
                case 8:
                    updateOtherBlocks(x, 7, 9, 2, 5);
                    break;
                case 9:
                    updateOtherBlocks(x, 7, 8, 3, 6);
                    break;
                default:
                    break;
            }
        }

        private void updateOtherBlocks(ValueUpdatedEventArgs x, int rowBlock1, int rowBlock2, int colBlock1, int colBlock2)
        {
            var childs = UIHelper.FindVisualChildren<UserControl2>(this);
            var xx = childs.FirstOrDefault(x => x.Block == rowBlock1);
            if (xx != null)
            {
                xx.UpdateRow(x.Row, x.Value);
            }
            xx = childs.FirstOrDefault(x => x.Block == rowBlock2);
            if (xx != null)
            {
                xx.UpdateRow(x.Row, x.Value);
            }
            xx = childs.FirstOrDefault(x => x.Block == colBlock1);
            if (xx != null)
            {
                xx.UpdateCol(x.Col, x.Value);
            }
            xx = childs.FirstOrDefault(x => x.Block == colBlock2);
            if (xx != null)
            {
                xx.UpdateCol(x.Col, x.Value);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            nextSolve();
        }

        private void nextSolve()
        {
            if (PriorityQueue.Count > 0)
            {

                var peek = PriorityQueue.Peek();
                while (PriorityQueue.Count > 0 && peek.uc.VisibleNumbers.Count == 0)
                {
                    peek = PriorityQueue.Dequeue();
                }
                if (peek.uc.VisibleNumbers.Count == 1)
                {

                    var dq = PriorityQueue.Dequeue();
                    dq.uc.UpdateValue();
                }
                else
                {
                    var uc = matchBlockUniqueElement(peek.block);
                    if (uc.val.HasValue && uc.uc != null)
                    {
                        uc.uc.UpdateValue(uc.val.Value);
                    }
                    else
                    {
                        uc = matchRowUniqueElement(peek.block, peek.uc.UCROW);

                        if(uc.uc != null && uc.val.HasValue)
                        {
                            uc.uc.UpdateValue(uc.val.Value);
                        }
                        else
                        {
                            uc = matchColUniqueElement(peek.block, peek.uc.UCCOL);

                            if(uc.uc != null && uc.val.HasValue)
                            {
                                uc.uc.UpdateValue(uc.val.Value);
                            }
                            else
                            {
                                PriorityQueue.Dequeue();
                            }
                        }
                    }
                }
            }
            else
            {

            }
        }

        private IEnumerable<int> getRowBlocks(int block)
        {
            if (block <= 3) return [1, 2, 3];

            if (block <= 6) return [4, 5, 6];

            return [7, 8, 9];
        }

        private IEnumerable<int> getColBlocks(int block)
        {
            if (block % 3 == 1) return [1, 4, 7];

            if (block % 3 == 2) return [2, 5, 8];

            return [3, 6, 9];
        }

        private (UserControl1? uc, int? val) matchColUniqueElement(int block, int ucCol)
        {
            var colBlocks = getColBlocks(block);

            IEnumerable<UserControl2> Childs = UIHelper.FindVisualChildren<UserControl2>(this).Where(x => colBlocks.Contains(x.Block));

            List<UserControl1> userControl1s = new List<UserControl1>();

            foreach (var child in Childs)
            {
                userControl1s.AddRange(child.GetControlsBasedOnCol(ucCol));
            }

            var xx = FindUniqueElementControl(userControl1s);

            if (xx.uc != null && xx.val.HasValue) return xx;

            GetGroupElement(userControl1s.ToArray());
            return (null, null);
        }

        private (UserControl1? uc, int? val) matchRowUniqueElement(int block, int uCROW)
        {
            var rowBlocks = getRowBlocks(block);

            IEnumerable<UserControl2> Childs = UIHelper.FindVisualChildren<UserControl2>(this).Where(x => rowBlocks.Contains(x.Block));

            List<UserControl1> userControl1s = new List<UserControl1>();

            foreach (var child in Childs)
            {
                userControl1s.AddRange(child.GetControlsBasedOnRow(uCROW));
            }

            var xx = FindUniqueElementControl(userControl1s);

            if (xx.uc != null && xx.val.HasValue) return xx;

            GetGroupElement(userControl1s.ToArray());
            return (null, null);
        }

        private (UserControl1? uc, int? val) matchBlockUniqueElement(int block)
        {
            UserControl1 uc = null;
            int? n = null;
            IEnumerable<UserControl2> Childs = UIHelper.FindVisualChildren<UserControl2>(this);
            var bc = Childs.FirstOrDefault(x => x.Block == block);
            if (bc != null)
            {
                var el = bc.FindUniqueElementControl();

                if (el.uc == null)
                {
                    var childs = bc.GetUserControls();
                    GetGroupElement(childs);
                }
                else
                {
                    return el;
                }
            }
            return (null, null);
        }

        internal (UserControl1? uc, int? val) FindUniqueElementControl(IEnumerable<UserControl1> childs)
        {
            UserControl1? userControl = null;
            int? val = null;
            Dictionary<int, List<UserControl1>> map = new Dictionary<int, List<UserControl1>>()
            {
                { 1, new List<UserControl1>() },
                { 2, new List<UserControl1>() },
                { 3, new List<UserControl1>() },
                { 4, new List<UserControl1>() },
                { 5, new List<UserControl1>() },
                { 6, new List<UserControl1>() },
                { 7, new List<UserControl1>() },
                { 8, new List<UserControl1>() },
                { 9, new List<UserControl1>() }
            };

            foreach (var child in childs)
            {
                foreach (var item in child.VisibleNumbers)
                {
                    map[item].Add(child);
                }
            }



            foreach (var item in map.Keys)
            {
                if (map[item].Count == 1)
                {
                    return (map[item].FirstOrDefault(), item);
                }
            }

            return (userControl, val);
        }

        internal bool GetGroupElement(UserControl1[] childs)
        {
            bool isGrouped = false;
            Dictionary<UserControl1, int> usArrayMap = new Dictionary<UserControl1, int>();
            List<int[]> arrays = new List<int[]>();
            foreach (var item in childs)
            {
                if (item.VisibleNumbers.Count > 1)
                {
                    usArrayMap.Add(item, arrays.Count);
                    arrays.Add(item.VisibleNumbers.ToArray());
                }
            }

            var groupedElements = Utilities.GroupNakedElements(arrays);

            if (groupedElements.elements.Length >= 2 && groupedElements.indexes.Length == groupedElements.elements.Length)
            {
                isGrouped = true;
                foreach (var key in usArrayMap.Keys)
                {
                    if (!groupedElements.indexes.Contains(usArrayMap[key]))
                    {
                        foreach (var el in groupedElements.elements)
                        {
                            key.RemoveFrom(el);
                        }
                    }
                }
            }
            return isGrouped;
        }

        private void fillPQ()
        {
            IEnumerable<UserControl2> Childs = UIHelper.FindVisualChildren<UserControl2>(this);

            for (int i = 1; i <= 9; i++)
            {
                var x = Childs.FirstOrDefault(x => x.Block == i);
                if (x != null)
                {
                    var xChilds = UIHelper.FindVisualChildren<UserControl1>(x);
                    for (int a = 0; a < 3; a++)
                    {
                        for (int b = 0; b < 3; b++)
                        {
                            var uc = xChilds.FirstOrDefault(c => c.UCCOL == b && c.UCROW == a);
                            if (uc != null && uc.VisibleNumbers.Count >= 1)
                            {
                                PriorityQueue.Enqueue((i, uc), uc.VisibleNumbers.Count);
                            }
                        }
                    }
                }
            }
        }

        PriorityQueue<(int block, UserControl1 uc), int> PriorityQueue;

        private void UserControl2_CollectionChanged(object sender, EventArgs e)
        {
            if (PriorityQueue != null)
            {
                if (sender is UserControl1)
                {
                    var uc = sender as UserControl1;
                    if (uc != null)
                    {
                        var ee = e as ValueUpdatedEventArgs;
                        if (uc.VisibleNumbers.Count > 0)
                        {
                            PriorityQueue.Enqueue((ee.Box, uc), uc.VisibleNumbers.Count);
                        }
                    }
                }
            }
        }
    }
}