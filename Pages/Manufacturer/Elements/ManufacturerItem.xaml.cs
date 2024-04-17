using PR32.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PR32.Pages.Manufacturer.Elements
{
    /// <summary>
    /// Логика взаимодействия для ManufacturerItem.xaml
    /// </summary>
    public partial class ManufacturerItem : UserControl
    {
        IEnumerable<Classes.Country> Countries = Country.AllCountries();
        Pages.Manufacturer.ManufacturerMain Main;
        Classes.Manufacturer ThisManufacturer;
        public ManufacturerItem(Classes.Manufacturer manufacturer, Pages.Manufacturer.ManufacturerMain main)
        {
            InitializeComponent();
            this.Main = main;
            this.ThisManufacturer = manufacturer;
            NameTBx.Text = manufacturer.Name;
            PhoneTBx.Text = manufacturer.Phone;
            CountryTBx.Text = Countries.Where(x => x.Id == manufacturer.CountryCode).First().Name;
            MailTBx.Text = manufacturer.Mail;
        }

        private void EditManufacturer(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.OpenPages(new Pages.Manufacturer.AddManufacturer(ThisManufacturer));
        }

        private void DeleteManufacturer(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Удалить поставщика: {this.ThisManufacturer.Name}?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Classes.Record.AllRecords().Where(x => x.Manufacturer == ThisManufacturer.Id).Count() > 0)
                {
                    MessageBox.Show($"Поставщика {this.ThisManufacturer.Name} невозможно удалить. Для начала удалите зависимости.", "Уведомление");
                }
                else
                {
                    this.ThisManufacturer.Delete();
                    Main.ParentSP.Children.Remove(this);
                    MessageBox.Show($"Поставщик {this.ThisManufacturer.Name} успешно удален.", "Уведомление");
                }
            }
        }
    }
}
