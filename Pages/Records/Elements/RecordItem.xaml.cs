using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PR32.Pages.Records.Elements
{
    /// <summary>
    /// Логика взаимодействия для RecordItem.xaml
    /// </summary>
    public partial class RecordItem : UserControl
    {
        private IEnumerable<Classes.State> AllStates = Classes.State.AllState();
        private Classes.Record Record;
        private Pages.Records.RecordMain Main;
        public RecordItem(Classes.Record record, Pages.Records.RecordMain main)
        {
            InitializeComponent();
            IEnumerable<Classes.Manufacturer> AllManufacturers = Classes.Manufacturer.AllManufacturers();
            Record = record;
            Main = main;
            NameTBx.Text = Record.Name;
            YearTBx.Text = Record.Year.ToString();
            FormatTBx.Text = Record.Format == 0 ? "Моно" : "Стерео";
            switch (record.Size)
            {
                case 0:
                    SizeTBx.Text = "7 дюймов";
                    break;
                case 1:
                    SizeTBx.Text = "10 дюймов";
                    break;
                case 2:
                    SizeTBx.Text = "12 дюймов";
                    break;
                case 3:
                    SizeTBx.Text = "Иной";
                    break;
            }
            ManufacturerTBx.Text = AllManufacturers.Where(x => x.Id == Record.Manufacturer).First().Name;
            PriceTBx.Text = Record.Price.ToString();
            StateTBx.Text = AllStates.Where(x => x.Id == Record.State).First().Name;
            DescriptionTBx.Text = Record.Description;
        }

        private void EditRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.OpenPages(new AddRecord(Record))
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Удалить виниловую пластинку: {Record.Name}?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                IEnumerable<Classes.Supply> AllSuppies = Classes.Supply.AllSupplies();
                if (AllSuppies.Where(x => x.Record == Record.Id).Count() > 0)
                {
                    MessageBox.Show($"Виниловую пластинку {Record.Name} невозможно удалить. Для начала удалите зависимости.", "Уведомление");
                }
                else
                {
                    Record.Delete();
                    Main.ParentSP.Children.Remove(this);
                    MessageBox.Show($"Пластинка {Record.Name} успешно удалена.", "Уведомление");
                }
            }
        }
    }
}
