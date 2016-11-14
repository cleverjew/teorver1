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

namespace teorver1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] inpArr = textBox1.Text.Split(',');
            List<double> inp = new List<double>();
            foreach (var item in inpArr)
            {
                inp.Add(double.Parse(item));
            }
            switch (listBox1.SelectedIndex)
            {
                case 1:
                    label1.Content=Average(inp);
                    break;
                case 2:
                    label1.Content = Dispersion(inp);
                    break;
                case 3:
                    label1.Content = Deviation(inp);
                    break;
                case 4:
                    label1.Content = Mediana(inp);
                    break;
                case 5:
                    label1.Content = Moda(inp);
                    break;
                case 6:
                    label1.Content = Max(inp);
                    break;
                case 7:
                    label1.Content = Min(inp);
                    break;
                case 8:
                    label1.Content = Sweep(inp);
                    break;
                case 9:
                    label1.Content = Quantile(inp, double.Parse(textBox2.Text));
                    break;
                default:
                    break;
            }                
        }

        private double Average(List<double> inpList)
        {
            return inpList.Average();
        }

        private double Dispersion(List<double> inpList)
        {
            double res = 0;
            double average = Average(inpList);
            foreach (double num in inpList)
            {
                res += Math.Pow((num - average), 2);
            }
            return res / (inpList.Count-1);
        }

        private double Deviation(List<double> inpList)
        {
            return Math.Sqrt(Dispersion(inpList));
        }

        private double Mediana(List<double> inpList)
        {
            inpList.Sort();

            if (inpList.Count % 2 == 0)
            {
                return (inpList[inpList.Count / 2 - 1] + inpList[inpList.Count / 2]) / 2;
            }
            return inpList[(inpList.Count - 1) / 2];
        }

        private double Moda(List<double> inpList)
        {
            return inpList.GroupBy(x => x).OrderByDescending(g => g.Count()).SelectMany(g => g).ToList()[0];
        }

        private double Min(List<double> inpList)
        {
            return inpList.Min();
        }

        private double Max(List<double> inpList)
        {
            return inpList.Max();
        }

        private double Sweep(List<double> inpList)
        {
            return Max(inpList) - Min(inpList);
        }

        private double Quantile(List<double> inpList, double quantile)
        {
            switch (quantile.ToString())
            {
                case "0.1":
                    return inpList.Count > 10 ? inpList[inpList.Count / 10] : inpList[0];
                case "0.25":
                    return inpList.Count > 4 ? inpList[inpList.Count / 4] : inpList[0];
                case "0.5":
                    return Mediana(inpList);
                case "0.75":
                    return inpList.Count > 2 ? inpList[inpList.Count * 3 / 4] : inpList[0];
                default:
                    return 0;
            }
        }
    }
}
