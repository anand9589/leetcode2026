using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public int Block { get; set; }

        public event EventHandler TextChanged;
        public event EventHandler CollectionChanged;
        public UserControl2()
        {
            InitializeComponent();
        }

        private void UserControl1_TextChanged(object sender, EventArgs e)
        {
            var x = e as ValueUpdatedEventArgs;

            updateOtherUCs(x.Value);
            x.Box = Block;
            TextChanged?.Invoke(this, x);
        }

        private void updateOtherUCs(int value)
        {
            var childs = UIHelper.FindVisualChildren<UserControl1>(this);

            foreach (var item in childs)
            {
                item.RemoveFrom(value);
            }
        }

        public void UpdateRow(int row, int value)
        {
            var childs = UIHelper.FindVisualChildren<UserControl1>(this);
            foreach (var item in childs)
            {
                if (item.UCROW == row)
                {
                    item.RemoveFrom(value);
                }
            }
        }

        public void UpdateCol(int col, int value)
        {
            var childs = UIHelper.FindVisualChildren<UserControl1>(this);
            foreach (var item in childs)
            {
                if (item.UCCOL == col)
                {
                    item.RemoveFrom(value);
                }
            }
        }
        public void Update(int row, int col, int value)
        {
            var childs = UIHelper.FindVisualChildren<UserControl1>(this);
            var k = childs.FirstOrDefault(x => x.UCROW == row && x.UCCOL == col);
            k.UpdateValue(value);
        }

        private void UserControl1_CollectionChanged(object sender, EventArgs e)
        {
            CollectionChanged?.Invoke(sender, new ValueUpdatedEventArgs() { Box = Block });
        }

        internal (UserControl1? uc, int? val) FindUniqueElementControl()
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

            var childs = UIHelper.FindVisualChildren<UserControl1>(this);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var k = childs.FirstOrDefault(x => x.UCROW == i && x.UCCOL == j);

                    foreach (var item in k.VisibleNumbers)
                    {
                        map[item].Add(k);
                    }
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

        internal UserControl1[] GetUserControls()
        {
            return UIHelper.FindVisualChildren<UserControl1>(this).ToArray();
        }
        internal UserControl1[] GetControlsBasedOnRow(int row)
        {
            return UIHelper.FindVisualChildren<UserControl1>(this).Where(x => x.UCROW == row).ToArray();
        }
        internal UserControl1[] GetControlsBasedOnCol(int col)
        {

            return UIHelper.FindVisualChildren<UserControl1>(this).Where(x => x.UCCOL == col).ToArray();

        }
    }
    public static class Utilities
    {
        public static (int[] indexes, int[] elements) GroupNakedElements(List<int[]> arrays)
        {
            HashSet<int> indexes = new HashSet<int>();
            HashSet<int> elements = new HashSet<int>();
            GroupNakedElements_Helper(arrays, indexes, elements);

            return (indexes.ToArray(), elements.ToArray());
        }

        private static void GroupNakedElements_Helper(List<int[]> arrays, HashSet<int> indexes, HashSet<int> elements)
        {
            for (int i = 2; i < arrays.Count; i++)
            {
                for (int j = 0; j < arrays.Count; j++)
                {
                    int[] array = arrays[j];

                    if (array.Length > i) continue;

                    foreach (var item in array)
                    {
                        elements.Add(item);
                    }
                    if (elements.Count > i) break;
                    indexes.Add(j);
                }
                if (elements.Count == i && indexes.Count == i)
                {
                    return;
                }
                indexes.Clear();
                elements.Clear();
            }

        }
    }
    public static class UIHelper
    {
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T t)
                {
                    yield return t;
                }

                foreach (T childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }
}
