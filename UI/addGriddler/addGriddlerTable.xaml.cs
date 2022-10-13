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
using GriddlersSolver;

namespace UI
{
    /// <summary>
    /// Interaction logic for addGriddlerTable.xaml
    /// </summary>
    public partial class addGriddlerTable : Window
    {
        private bool isInSelectionChangedFunc = false;
        public addGriddlerTable()
        {
            InitializeComponent();
            Griddler g = GriddlerFunctions.griddler;

            for (int i = 0; i < 3 * g.X / 2 + Convert.ToInt32(g.XZ); i++)
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < 3 * g.Y / 2 + Convert.ToInt32(g.YZ); i++)
                mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.ShowGridLines = true;

            for (int i = 0; i < mainGrid.RowDefinitions.Count; i++)//insert ComboBoxes to the num tables
            {
                for (int j = 0; j < mainGrid.ColumnDefinitions.Count; j++)
                {
                    if (j > g.X / 2 - Convert.ToInt32(!g.XZ) && i > g.Y / 2 - Convert.ToInt32(!g.YZ))//not doing anything to the table
                        continue;
                    if (j <= g.X / 2 - Convert.ToInt32(!g.XZ) && i <= g.Y / 2 - Convert.ToInt32(!g.YZ))//not doing anything to the little left-up empty rectangle
                        continue;
                    ComboBox cb = new ComboBox();
                    if (j < g.X / 2 + Convert.ToInt32(g.XZ))//fill the ComboBoxes of num_x
                        for (int n = 0; n <= g.X; n++)
                            cb.Items.Add(n);
                    if (i < g.Y / 2 + Convert.ToInt32(g.YZ))//fill the ComboBoxes of num_y
                        for (int n = 0; n <= g.Y; n++)
                            cb.Items.Add(n);
                    cb.SetValue(Grid.RowProperty, i);
                    cb.SetValue(Grid.ColumnProperty, j);
                    cb.SelectedItem = 0;
                    cb.SelectionChanged += cb_selectionChanged;
                    mainGrid.Children.Add(cb);
                }
            }
            this.Height = 80 + (mainGrid.Height = 40 * 3 / 2 * (g.Y + Convert.ToInt32(g.YZ)));//set the mainGrid's height, and the window's height
            this.Width = 20 + (mainGrid.Width = 40 * 3 / 2 * (g.X + Convert.ToInt32(g.XZ)));//set the mainGrid's width, and the window's width
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
        }
        /// <summary>
        /// prevent to insert to many numbers in the table (to many - more than the capacity)
        /// </summary>
        private void cb_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).Items.Count == 0 || isInSelectionChangedFunc)
            {
                return;
            }
            isInSelectionChangedFunc = true;

            ComboBox cb = sender as ComboBox;
            int row = Grid.GetRow(cb);
            int column = Grid.GetColumn(cb);
            Griddler g = GriddlerFunctions.griddler;

            if (isIn_num_x(row, column))
            {
                int selectedSum = 0, moreSpaceForNew, moreSpaceForOld;
                foreach (var item in mainGrid.Children)
                {
                    if (item is ComboBox && (int)((ComboBox)item).GetValue(Grid.RowProperty) == row && (int)((ComboBox)item).SelectedItem != 0)
                        selectedSum += (int)((ComboBox)item).SelectedItem + 1;
                }
                moreSpaceForNew = (g.X - selectedSum);
                moreSpaceForOld = (g.X - selectedSum + 1);
                foreach (var item in mainGrid.Children)
                {
                    if (item is ComboBox && (int)((ComboBox)item).GetValue(Grid.RowProperty) == row && (ComboBox)item != sender)
                    {
                        int selected = (int)((ComboBox)item).SelectedItem;
                        ((ComboBox)item).Items.Clear();
                        ((ComboBox)item).Items.Add(0);
                        for (int i = 1; i <= selected + (selected == 0 ? moreSpaceForNew : moreSpaceForOld); i++)
                        {
                            ((ComboBox)item).Items.Add(i);
                        }
                        ((ComboBox)item).SelectedItem = selected;
                    }
                }
            }
            else if (isIn_num_y(row, column))
            {
                int selectedSum = 0, moreSpaceForNew, moreSpaceForOld;
                foreach (var item in mainGrid.Children)
                {
                    if (item is ComboBox && (int)((ComboBox)item).GetValue(Grid.ColumnProperty) == column && (int)((ComboBox)item).SelectedItem != 0)
                        selectedSum += (int)((ComboBox)item).SelectedItem + 1;
                }
                moreSpaceForNew = (g.Y - selectedSum);
                moreSpaceForOld = (g.Y - selectedSum + 1);
                foreach (var item in mainGrid.Children)
                {
                    if (item is ComboBox && (int)((ComboBox)item).GetValue(Grid.ColumnProperty) == column && (ComboBox)item != sender)
                    {
                        int selected = (int)((ComboBox)item).SelectedItem;
                        ((ComboBox)item).Items.Clear();
                        ((ComboBox)item).Items.Add(0);
                        for (int i = 1; i <= selected + (selected == 0 ? moreSpaceForNew : moreSpaceForOld); i++)
                        {
                            ((ComboBox)item).Items.Add(i);
                        }
                        ((ComboBox)item).SelectedItem = selected;

                    }
                }
            }
            isInSelectionChangedFunc = false;
        }
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            var temp = GriddlerFunctions.getNumsListFromGrid(mainGrid);
            GriddlerFunctions.griddler.num_x = temp[0];
            GriddlerFunctions.griddler.num_y = temp[1];
            this.Close();
        }
        private bool isIn_num_x(int row, int column)
        {
            Griddler g = GriddlerFunctions.griddler;
            if (column <= g.X / 2 - Convert.ToInt32(!g.XZ) && row > g.Y / 2 - Convert.ToInt32(!g.YZ))
                return true;
            return false;
        }
        private bool isIn_num_y(int row, int column)
        {
            Griddler g = GriddlerFunctions.griddler;
            if (column > g.X / 2 - Convert.ToInt32(!g.XZ) && row <= g.Y / 2 - Convert.ToInt32(!g.YZ))
                return true;
            return false;
        }
    }
}
