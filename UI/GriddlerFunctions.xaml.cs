using GriddlersSolver;
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
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for GriddlerFunctions.xaml
    /// </summary>
    public partial class GriddlerFunctions : Window
    {
        public static Griddler griddler = new Griddler(false);
        public GriddlerFunctions()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ordering the num_x, num_y
        /// </summary>
        /// <param name="list">!!!!!!!!!     Only list with right pattern: list[0]:num_x, list[1]:num_y     !!!!!!!!!</param>
        /// <returns>good num_x, num_y</returns>
        public static List<List<List<int>>> moveZeroesBack(List<List<List<int>>> list)
        {
            var iter0 = list[0].GetEnumerator();//move all the zeroes to the back of the List
            int count0 = list[0].Count;
            for (; count0 > 0 && iter0.MoveNext(); count0--)
            {
                int deletedCount = iter0.Current.AsEnumerable().Where(a => a == 0).Select(b => 1).Sum();
                iter0.Current.RemoveAll(a => a == 0);
                for (int i = 0; i < deletedCount; i++)
                    iter0.Current.Add(0);
            }
            var iter1 = list[1].GetEnumerator();//move all the zeroes to the back of the List
            int count1 = list[1].Count;
            for (; count1 > 0 && iter1.MoveNext(); count1--)
            {
                int deletedCount = iter1.Current.AsEnumerable().Where(a => a == 0).Select(b => 1).Sum();
                iter1.Current.RemoveAll(a => a == 0);
                for (int i = 0; i < deletedCount; i++)
                    iter1.Current.Add(0);
            }
            return list;
        }
        /// <summary>
        /// converts Grid to table of num_x, num_y
        /// </summary>
        /// <param name="g">grid with numbers table</param>
        /// <returns>list[0] = num_x, list[1] = num_y</returns>
        public static List<List<List<int>>> getNumsListFromGrid(Grid g)
        {
            List<List<List<int>>> list = new List<List<List<int>>>();
            list.Add(new List<List<int>>());//num_x
            list.Add(new List<List<int>>());//num_y
            var enumerator = g.Children.GetEnumerator();
            int X = g.ColumnDefinitions.Count % 3 == 2 ? (g.ColumnDefinitions.Count * 2 - 1) / 3 : g.ColumnDefinitions.Count * 2 / 3;//2-zugi: ((3X+1)/2), 0-E'zugi: ((3X)/2)
            int Y = g.RowDefinitions.Count % 3 == 2 ? (g.RowDefinitions.Count * 2 - 1) / 3 : g.RowDefinitions.Count * 2 / 3;//2-zugi: ((3X+1)/2), 0-E'zugi: ((3X)/2)
            int numX = g.ColumnDefinitions.Count - X;
            int numY = g.RowDefinitions.Count - Y;
            for (int i = 0; i < Y; i++)
                list[0].Add(new List<int>());
            for (int i = 0; i < X; i++)
                list[1].Add(new List<int>());

            while (enumerator.MoveNext())
            {
                if (!(enumerator.Current is ComboBox))
                    continue;
                int column = (int)((ComboBox)enumerator.Current).GetValue(Grid.ColumnProperty);
                int row = (int)((ComboBox)enumerator.Current).GetValue(Grid.RowProperty);
                int selectedValue = (int)((ComboBox)enumerator.Current).SelectedItem;//the SelectedItem is also the SelectedValue
                if (column >= numX)//this is column number
                    list[1][column - numX].Add(selectedValue);
                else//this is row number
                    list[0][row - numY].Add(selectedValue);
            }

            foreach (var item in list[0])
                item.Reverse();
            foreach (var item in list[1])
                item.Reverse();

            return moveZeroesBack(list);
        }
        /// <summary>
        /// converts table of num_x, num_y to Grid
        /// </summary>
        /// <param name="list">list[0] = num_x, list[1] = num_y</param>
        /// <returns>grid with numbers table</returns>
        public static Grid getNumsGridFromList(List<List<List<int>>> list)
        {
            Grid g = new Grid();
            int Y = list[0].Count;
            int X = list[1].Count;
            int numXwidth = X / 2 + Convert.ToInt32(X % 2 == 0 ? false : true);
            int numYheight = Y / 2 + Convert.ToInt32(Y % 2 == 0 ? false : true);
            for (int i = 0; i < Y + numYheight; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(15);
                g.RowDefinitions.Add(rd);
            }
            for (int i = 0; i < X + numXwidth; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(15);
                g.ColumnDefinitions.Add(cd);
            }

            for (int i = 0; i < Y + numYheight; i++)//insert TextBoxes to the num tables
            {
                for (int j = 0; j < X + numXwidth; j++)
                {
                    if (j >= numXwidth && i >= numYheight)//not doing anything to the table
                        continue;
                    if (j < numXwidth && i < numYheight)//not doing anything to the little left-up empty rectangle
                        continue;
                    TextBlock tb = new TextBlock();
                    if (j < numXwidth)//fill the TextBoxes of num_x
                        tb.Text = list[0][i - numYheight][numXwidth - 1 - j].ToString();
                    if (i < numYheight)//fill the TextBoxes of num_y
                        tb.Text = list[1][j - numXwidth][numYheight - 1 - i].ToString();
                    tb.SetValue(Grid.RowProperty, i);
                    tb.SetValue(Grid.ColumnProperty, j);
                    tb.Height = 40;
                    tb.Width = 40;
                    g.Children.Add(tb);
                }
            }
            return g;
        }
    }
}
