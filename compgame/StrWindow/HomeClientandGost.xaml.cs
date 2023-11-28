using compgame.Class;
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

namespace compgame.StrWindow
{
    /// <summary>
    /// Логика взаимодействия для HomeClientandGost.xaml
    /// </summary>
    public partial class HomeClientandGost : Window
    {
        public HomeClientandGost()
        {
            InitializeComponent();
            Manager.HomeFrame = HomeFrame;
        }

        private void Btn_Product_Click(object sender, RoutedEventArgs e)
        {
            Manager.HomeFrame.Navigate(new ProductClientandGost());
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow h = new MainWindow();
            h.Show();
            this.Close();
        }
    }
}