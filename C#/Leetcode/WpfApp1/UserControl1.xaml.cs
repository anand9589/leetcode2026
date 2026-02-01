using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl, INotifyPropertyChanged
    {
        public event EventHandler TextChanged;
        public event EventHandler CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<int> _visibleNumbers;
        public int UCROW { get; set; }
        public int UCCOL { get; set; }
        public ObservableCollection<int> VisibleNumbers
        {
            get => _visibleNumbers;
            set
            {
                _visibleNumbers = value;
                OnPropertyChanged(nameof(VisibleNumbers));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public UserControl1()
        {
            VisibleNumbers = new ObservableCollection<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // ADD THIS: This forces the Visibility binding to refresh 
            // every time you add, remove, or clear items.
            VisibleNumbers.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(VisibleNumbers));
            };

            InitializeComponent();
            this.DataContext = this;
        }

        public void RemoveFrom(int n)
        {
            _visibleNumbers.Remove(n);

            if (_visibleNumbers.Count > 0)
            {
                CollectionChanged?.Invoke(this, null);
            }
            //if (_visibleNumbers.Count == 1)
            //{
            //    this.UpdateValue(_visibleNumbers.FirstOrDefault());
            //}

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _visibleNumbers.Clear();
            TextChanged?.Invoke(this, new ValueUpdatedEventArgs()
            {
                Row = UCROW,
                Col = UCCOL,
                Value = int.Parse((sender as TextBox).Text)
            });
        }

        public void UpdateValue()
        {
            if (VisibleNumbers.Count == 1)
            {
                var v = VisibleNumbers[0];

                _visibleNumbers.Clear();
                this.UpdateValue(v);
            }
        }

        public void UpdateValue(int value)
        {
            var childs = UIHelper.FindVisualChildren<TextBox>(this);
            var p = childs.FirstOrDefault();

            p.Text = value.ToString();
        }
    }

    public class ValueUpdatedEventArgs : EventArgs
    {
        public int Box { get; set; }
        public int Value { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
    }
    public class CollectionChangedEventArgs : EventArgs
    {
        public UserControl1 UC { get; set; }
    }
}
