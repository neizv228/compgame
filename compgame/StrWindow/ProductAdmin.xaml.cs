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
    /// Логика взаимодействия для ProductAdmin.xaml
    /// </summary>
    public partial class ProductAdmin : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IEnumerable<Tovar> _ProductList;
        private int ManufacturerFilterId = 0;
        public List<Manufacturer> ManufacturerList { get; set; }
        private int SortType = 0;
        public string[] SortList { get; set; } =
        {
            "Без сортировки",
            "Стоимость по возрастанию",
            "Стоимость по убыванию",
                 "Количество на складе по возрастанию",
                 "Количество на складе по убыванию",
                 "Скидка по возрастанию",
                 "Скидка по убыванию"

        };
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
                if (ManufacturerFilterId > 0)
                    Result = Result.Where(i => i.Manufacturer.ManufacturerID == ManufacturerFilterId);

                switch (SortType)
                {
                    case 1:
                        Result = compgameEntities.GetContext().Tovar.OrderBy(p => p.TovarCost);
                        break;
                    case 2:
                        Result = compgameEntities.GetContext().Tovar.OrderByDescending(p => p.TovarCost);
                        break;
                    case 3:
                        Result = compgameEntities.GetContext().Tovar.OrderBy(p => p.TovarQuantityInStock);
                        break;
                    case 4:
                        Result = compgameEntities.GetContext().Tovar.OrderByDescending(p => p.TovarQuantityInStock);
                        break;
                    case 5:
                        Result = compgameEntities.GetContext().Tovar.OrderBy(p => p.TovarDiscountAmount);
                        break;
                    case 6:
                        Result = compgameEntities.GetContext().Tovar.OrderByDescending(p => p.TovarDiscountAmount);
                        break;
                }
                if (SearchFilter != "")
                    Result = Result.Where(
                        p => p.TovarName.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                             p.TovarDescription.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0);

                return Result.Skip((CurrentPage - 1) * 15).Take(15);
            }
            set
            {
                _ProductList = value;
                Invalidate();
            }
        }

        public ProductAdmin()
        {
            InitializeComponent();
            DataContext = this;
            ProductList = compgameEntities.GetContext().Tovar.ToList();
            ManufacturerList = compgameEntities.GetContext().Manufacturer.ToList();
            ManufacturerList.Insert(0, new Manufacturer { ManufacturerName = "Все производители" });
        }

        private void TBox_T(object sender, TextChangedEventArgs e)
        {
            SearchFilter = TBox.Text;
            Invalidate();
        }

        private void ComboTypeSort_Sel(object sender, SelectionChangedEventArgs e)
        {
            SortType = ComboTypeSort.SelectedIndex;
            Invalidate();
        }

        private void ComboTypeFilter_Sel(object sender, SelectionChangedEventArgs e)
        {
            ManufacturerFilterId = (ComboTypeFilter.SelectedItem as Manufacturer).ManufacturerID;
            Invalidate();
        }
        private string SearchFilter = "";

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage--;
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage++;
        }

        private void Btn_AddProduct(object sender, RoutedEventArgs e)
        {
            AddProduct addproduct = new AddProduct(null);
            addproduct.ShowDialog();
        }

        private void Btn_Delete(object sender, RoutedEventArgs e)
        {
            var ProductForRemoving = ProductListView.SelectedItems.Cast<Tovar>().ToList();
            if (MessageBox.Show($" Вы точно хотите удалить следующие {ProductForRemoving.Count()} элементы?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    compgameEntities.GetContext().Tovar.RemoveRange(ProductForRemoving);
                    compgameEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    ProductListView.ItemsSource = compgameEntities.GetContext().Tovar.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Btn_EditProduct(object sender, RoutedEventArgs e)
        {
            AddProduct addproduct = new AddProduct((sender as Button).DataContext as Tovar);
            addproduct.ShowDialog();
        }
    }
}

