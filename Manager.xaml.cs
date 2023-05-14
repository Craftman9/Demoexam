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

namespace WpfAppAAA
{
    /// <summary>
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Page
    {
        public string name;
        public int supplier;

        public Manager()
        {
            InitializeComponent();
            DataS.ItemsSource = TradeEntities.GetContext().Product.ToList();
            CmbSupplier.ItemsSource = TradeEntities.GetContext().Supplier.ToList();
            CmbSupplier.DisplayMemberPath = "SupplierName";
            CmbSupplier.SelectedValuePath = "id";
            All.Content = TradeEntities.GetContext().Product.Count().ToString();
            Count.Content = DataS.Items.Count.ToString();


        }

        private void Filter()
        {
            var Obj = TradeEntities.GetContext().Product.ToList();
            if (CmbSupplier.SelectedIndex > -1)
            {
                Obj = Obj.Where(x => x.ProductSupplier == supplier).ToList();
            }
            if (TbName.Text != "")
            {
                Obj = Obj.Where(x => x.ProductName.ToLower().Contains(name.ToLower())).ToList(); //ToLower() и Containt()
            }
            DataS.ItemsSource = Obj;
            Count.Content = DataS.Items.Count.ToString();

        }

        private void CmbCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void CmbSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            supplier = CmbSupplier.SelectedIndex;
            Filter();
        }

        private void TbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = TbName.Text;
            Filter();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddingWindow win = new AddingWindow(null);
            win.ShowDialog();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var Obj = DataS.SelectedItem as Product;
            AddingWindow win = new AddingWindow(Obj);
            win.ShowDialog();
        }

        private void DelitBtn_Click(object sender, RoutedEventArgs e)
        {
            var remove = DataS.SelectedItems.Cast<Product>().ToList();
            if (MessageBox.Show("Удалить следующие записи?", "Уведомление!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                TradeEntities.GetContext().Product.RemoveRange(remove);
                TradeEntities.GetContext().SaveChanges();
                DataS.ItemsSource = TradeEntities.GetContext().Product.ToList();
            }
            Count.Content = DataS.Items.Count.ToString();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility.Visible == Visibility)
            {
                TradeEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataS.ItemsSource = TradeEntities.GetContext().Product.ToList();
                Count.Content = DataS.Items.Count.ToString();
            }
        }
    }
}
