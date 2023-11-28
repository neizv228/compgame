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
    /// Логика взаимодействия для HomeAdmin.xaml
    /// </summary>
    public partial class HomeAdmin : Window
    {
        public HomeAdmin()
        {
            InitializeComponent();
            Manager.HomeFrame = HomeFrame;
        }

        private void Btn_Product_Click(object sender, RoutedEventArgs e)
        {
            Manager.HomeFrame.Navigate(new ProductAdmin());
        }

        private void Btn_Order_Click(object sender, RoutedEventArgs e)
        {
            Manager.HomeFrame.Navigate(new Order1());
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow h = new MainWindow();
            h.Show();
            this.Close();
        }

    }
}
