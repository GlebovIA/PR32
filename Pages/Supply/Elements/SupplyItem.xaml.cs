using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PR32.Pages.Supply.Elements
{
    /// <summary>
    /// Логика взаимодействия для SupplyItem.xaml
    /// </summary>
    public partial class SupplyItem : UserControl
    {
        IEnumerable<Classes.Manufacturer> AllManufacturers = Classes.Manufacturer.AllManufacturers();
        IEnumerable<Classes.Record> AllRecords = Classes.Record.AllRecords();
        Classes.Supply Supply;
        Pages.Supply.SupplyMain Main;
        public SupplyItem(Classes.Supply supply, SupplyMain main)
        {
            InitializeComponent();
            Supply = supply;
            Main = main;
            ManufacturerTBx.Text = AllManufacturers.Where(x => x.Id == Supply.Manufacturer).First().Name;
            RecordTBx.Text = AllRecords.Where(x => x.Id == Supply.Record).First().Name;
            DateDeliveryTBx.Text = CorrectDate(Supply.DateDelivery);
            CountTBx.Text = Supply.Count.ToString();
        }
        private string CorrectDate(string Value)
        {
            return Value.Split(' ')[0];
        }

        private void EditSupply(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.OpenPages(new AddSupply(Supply));
        }

        private void DeleteSupply(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Удалить поставку №: {Supply.Id}?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Supply.Delete();
                Main.ParentSP.Children.Remove(this);
                MessageBox.Show($"Поставка №{Supply.Id} успешно удалена.", "Уведомление");
            }
        }
    }
}
