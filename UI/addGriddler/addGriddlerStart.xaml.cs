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
    /// Interaction logic for addGriddler.xaml
    /// </summary>
    public partial class addGriddlerStart : Window
    {
        public addGriddlerStart()
        {
            InitializeComponent();
            for (int i = 1; i < 31; i++)
            {
                xComboBox.Items.Add(i);
                yComboBox.Items.Add(i);
            }
            this.DataContext = GriddlerFunctions.griddler;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Window addTable = new addGriddlerTable();//insert the num tables in:    GriddlerFunctions.griddler
            addTable.ShowDialog();
            this.Close();
        }
    }
}
