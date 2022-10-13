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
using GriddlersSolver;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Window addG = new addGriddlerStart();//create a new griddler in:    GriddlerFunctions.griddler
            addG.ShowDialog();


            tableGrid.Children.Add(GriddlerFunctions.getNumsGridFromList(new List<List<List<int>>> { GriddlerFunctions.griddler.num_x, GriddlerFunctions.griddler.num_y }));
            tableGrid.ShowGridLines = true;
        }
    }
}
