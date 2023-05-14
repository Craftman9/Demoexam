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

namespace WpfAppAAA
{
    /// <summary>
    /// Логика взаимодействия для AddingWindow.xaml
    /// </summary>
    public partial class AddingWindow : Window
    {
        public Product product = new Product();
        public AddingWindow(Product Obj)
        {
            InitializeComponent();
            if (Obj != null) product = Obj;
            DataContext = product;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (product == null)
            {
                TradeEntities.GetContext().Product.Add(product);
            }
            TradeEntities.GetContext().SaveChanges();
            this.Close();

        }

        private void Comeback_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
