using compgame.BDModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Tovar _currentTovar = new Tovar();
        public AddProduct(Tovar selectedTovar)
        {
            InitializeComponent();
            DataContext = this;
            ComboCategory.ItemsSource = compgameEntities.GetContext().Manufacturer.ToList();
            if (selectedTovar != null)
                _currentTovar = selectedTovar;
            DataContext = _currentTovar;
        }


        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            
            if (_currentTovar.TovarID == 0)
                compgameEntities.GetContext().Tovar.Add(_currentTovar);
            try
            {
               compgameEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnImageClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog GetImageDialog = new OpenFileDialog();
            GetImageDialog.Filter = "Файлы изображений: (*.png, *.jpg, *.jpeg)|*.png; *.jpg; *.jpeg";
            GetImageDialog.InitialDirectory = Environment.GetEnvironmentVariable("/Resources/");
            if (GetImageDialog.ShowDialog() == true)
            {
                _currentTovar.TovarPhoto = GetImageDialog.FileName.Substring(Environment.CurrentDirectory.Length - 28);
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_currentTovar"));
                }
            }
        }
    }
}

