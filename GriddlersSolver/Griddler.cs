using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GriddlersSolver
{
    public class Griddler
    {
        public string name { get; set; }//Griddler's name
        public int[,] table;//the part to fill - the solve part
        public List<List<int>> num_x;//the numbers: left side
        public List<List<int>> num_y;//the numbers: right side
        private int x;//the x size of the table
        private int y;//the y size of the table
        public int X { get { return x; } set { x = value; if (x < 0) x = 0; } }
        public int Y { get { return y; } set { y = value; if (y < 0) y = 0; } }
        public bool XZ { get { return x % 2 != 0; } }//if zugy - false. if e'zugy - true.
        public bool YZ { get { return y % 2 != 0; } }//if zugy - false. if e'zugy - true.

        /// <summary>
        /// create new Griddler object
        /// </summary>
        /// <param name="toSet">do you want to set the table now?</param>
        public Griddler(bool toSet)
        {
            if (!toSet)
            {
                name = "";
                table = null;
                num_x = null;
                num_y = null;
                x = 0;
                y = 0;
                return;
            }

            //name
            Console.WriteLine("Enter the name for the griddler: ");
            name = Console.ReadLine();

            //reset size
        _x: Console.WriteLine("Enter size of x: ");
            if (!int.TryParse(Console.ReadLine(), out x))
                goto _x;
        _y: Console.WriteLine("Enter size of y: ");
            if (!int.TryParse(Console.ReadLine(), out y))
                goto _y;

            //reset table
            table = new int[x, y];

            //fill table by ?
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    table[i, j] = (int)'?';

            //reset numbers
            num_x = new List<List<int>>();
            num_y = new List<List<int>>();
            num_x.Clear();
            num_y.Clear();

            //input numbers & set rows and columns
            //add rows
            int xInput = 0, yInput = 0;
        _row_try: try
            {
                Console.WriteLine("\n--------------\nEnter rows's numbers: row by row from up to bottom \n  -->(numbers with one Space between, and Enter at the end):\n");
                for (; xInput < x; xInput++)
                    num_x.Add((new List<int>(from s in Console.ReadLine().Split(' ')
                                             select int.Parse(s))));
                foreach (var item in num_x)
                    item.Reverse();
            }
            catch
            {
                Console.WriteLine("\nEnter only numbers!");
                goto _row_try;
            }

            //add columns
        _column_try: try
            {
                Console.WriteLine("\n--------------\nEnter columns's numbers: column by column from up to bottom \n  -->(numbers with one Space between, and Enter at the end):\n");
                for (; yInput < y; yInput++)
                    num_y.Add(new List<int>(from s in Console.ReadLine().Split(' ')
                                            select int.Parse(s)));
                foreach (var item in num_y)
                    item.Reverse();
            }
            catch
            {
                Console.WriteLine("\nEnter only numbers!");
                goto _column_try;
            }
        }
        ~Griddler()
        {
        }
        public List<int> getRowNumbers(int i)
        {
            return num_x[i];
        }
        public List<int> getColumnNumbers(int i)
        {
            return num_y[i];
        }
        public int[,] getTable(int All)
        {
            return table;
        }
        public static bool saveToFile<T>(string path, T x)
        {
            //TODO
            return false;
        }
        public static bool loadFromFile<T>(string path, T x)
        {
            //TODO
            return false;
        }
    }
}
