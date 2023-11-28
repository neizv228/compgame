using compgame.BDModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace compgame.StrWindow
{
    /// <summary>
    /// Логика взаимодействия для ProductClientandGost.xaml
    /// </summary>
    public partial class ProductClientandGost : Page
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IEnumerable<Tovar> _ProductList;
        public List<Tovar> ManufacturerList { get; set; }
        private int _CurrentPage = 1;
        public int CurrentPage
        {
            get
            {
                return _CurrentPage;
            }
            set
            {

                _CurrentPage = value;
                Invalidate();

            }
        }

        private void Invalidate(string ComponentName = "ProductList")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProductList"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(ComponentName));
        }
        public IEnumerable<Tovar> ProductList
        {
            get
            {
                var Result = _ProductList;



                return Result.Skip((CurrentPage - 1) * 15).Take(15);
            }
            set
            {
                _ProductList = value;
                Invalidate();
            }
        }

        public ProductClientandGost()
        {
            InitializeComponent();
            DataContext = this;
            ProductList = compgameEntities.GetContext().Tovar.ToList();
        }



        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage--;
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage++;
        }

    }
}
