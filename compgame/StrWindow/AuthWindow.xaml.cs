using compgame.BDModel;
using compgame.StrWindow;
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

namespace compgame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<User> userObj;
        public MainWindow()
        {
            InitializeComponent();
            userObj = compgameEntities.GetContext().User.ToList();
        }

        private void Btn_ClickEnter(object sender, RoutedEventArgs e)
        {
            var currentUsers = userObj.Where(user => user.UserLogin == log.Text && user.UserPassword == pass.Password).FirstOrDefault();
            if (currentUsers != null)
            {
                if (currentUsers.UserRole == 1)
                {
                    HomeAdmin h = new HomeAdmin();
                    h.Show();
                    this.Close();
                }
                else if (currentUsers.UserRole == 2)
                {
                    HomeManager h = new HomeManager();
                    h.Show();
                    this.Close();
                }
                else if (currentUsers.UserRole == 3)
                {
                    HomeClientandGost h = new HomeClientandGost();
                    h.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Вы ввели неправильный логин или пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Btn_ClickEnter2(object sender, RoutedEventArgs e)
        {
            HomeClientandGost h = new HomeClientandGost();
            h.Show();
            this.Close();
        }
    }
}