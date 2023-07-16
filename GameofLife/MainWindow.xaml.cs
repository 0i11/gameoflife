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
using System.Windows.Threading;
using System.Diagnostics;
using Microsoft.Win32;

namespace GameofLife
{
    public partial class MainWindow : Window
    {
        
        SolidColorBrush colD = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E2E2E"));
        SolidColorBrush col = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C7C7C7"));
        List<Button> Cell = new List<Button>();
        DispatcherTimer Tick = new DispatcherTimer();
        bool p = true;
        int siz = 30;
        int gridsize;
        int rows;
        int columns;
        int upperrow;
        int lowerrow;
        List<int> neighbor = new List<int>();

        public MainWindow()
        {
            InitializeComponent();

            gridsize = g_.Rows * g_.Columns;
            rows = g_.Rows;
            columns = g_.Columns;
            upperrow = rows - 1;
            lowerrow = rows + 1;
            Tick.Tick += new EventHandler(update);
            Tick.Interval = TimeSpan.FromMilliseconds(10);
            EventManager.RegisterClassHandler(typeof(Button), Button.ClickEvent, new RoutedEventHandler(Button_Click));

            InstantiateGrid();
        }

        private void update(object sender, EventArgs e)
        {
            for (int i = 0; i < gridsize; i++)
            {
                neighbor[i] = 0;
            }

            for (int i = 0; i < gridsize; i++)
            {
                if (i + 1 < gridsize && i % rows != upperrow && Cell[i + 1].Background == col) neighbor[i]++;
                if (i - 1 >= 0 && i % rows != 0 && Cell[i - 1].Background == col) neighbor[i]++;
                if (i + rows < gridsize && Cell[i + rows].Background == col) neighbor[i]++;
                if (i - rows >= 0 && Cell[i - rows].Background == col) neighbor[i]++;
                if (i - upperrow >= 0 && (i - upperrow) % rows != upperrow && Cell[i - upperrow].Background == col) neighbor[i]++;
                if (i - lowerrow >= 0 && (i - lowerrow) % rows != 0 && Cell[i - lowerrow].Background == col) neighbor[i]++;
                if (i + lowerrow < gridsize && (i + lowerrow) % rows != 0 && Cell[i + lowerrow].Background == col) neighbor[i]++;
                if (i + upperrow < gridsize && (i + upperrow) % rows != upperrow && Cell[i + upperrow].Background == col) neighbor[i]++;
            }

            for (int i = 0; i < gridsize; i++)
            {
                if ((neighbor[i] == 2 || neighbor[i] == 3) && Cell[i].Background == col)
                {
                    Cell[i].Background = col;
                }
                else if (neighbor[i] == 3 && Cell[i].Background == colD)
                {
                    Cell[i].Background = col;
                }
                else
                {
                    Cell[i].Background = colD;
                }
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = col;
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (p)
            {
                Tick.Start();
                Play.Content = "❚❚";
                p = false;
            }
            else
            {
                Tick.Stop();
                Play.Content = "▶";
                p = true;
            }
        }
        private void Button_RClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = colD;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < gridsize; i++)
            {
                neighbor[i] = 0;
                Cell[i].Background = colD;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            rows += 10;
            columns += 10;
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            rows -= 10 ;
            columns -= 10;
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void InstantiateGrid()
        {
            for (int i = 0; i < gridsize; i++)
            {
                Button Field = new Button();
                Field.Background = colD;
                Field.Click += Button_Click;
                Field.MouseRightButtonDown += Button_RClick;
                Cell.Add(Field);
                neighbor.Add(i);
                g_.Children.Add(Field);
            }

        }
    }
}
