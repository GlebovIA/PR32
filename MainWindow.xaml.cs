using PR32.Classes;
using System.Windows;
using System.Windows.Controls;

namespace PR32
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MW;
        public Pages.Records.RecordMain RecordMain = new Pages.Records.RecordMain();
        public MainWindow()
        {
            InitializeComponent();
            MW = this;
            OpenPages(RecordMain);
        }

        public void OpenPages(Page page)
        {
            MainFrame.Navigate(page);
        }

        private void OpenRecordList(object sender, RoutedEventArgs e)
        {
            OpenPages(RecordMain);
            RecordMain.LoadRecord();
        }

        private void OpenRecordAdd(object sender, RoutedEventArgs e)
        {
            OpenPages(new Pages.Records.AddRecord());
        }

        private void OpenManufacturersList(object sender, RoutedEventArgs e)
        {
            OpenPages(new Pages.Manufacturer.ManufacturerMain());
        }

        private void OpenManufacturersAdd(object sender, RoutedEventArgs e)
        {
            OpenPages(new Pages.Manufacturer.AddManufacturer());
        }

        private void OpenSupplyList(object sender, RoutedEventArgs e)
        {
            OpenPages(new Pages.Supply.SupplyMain());
        }

        private void OpenSupplyAdd(object sender, RoutedEventArgs e)
        {
            OpenPages(new Pages.Supply.AddSupply());
        }

        private void OpenStateList(object sender, RoutedEventArgs e)
        {
            OpenPages(new Pages.State.StateMain());
        }

        private void OpenStateAdd(object sender, RoutedEventArgs e)
        {
            OpenPages(new Pages.State.AddState());
        }

        private void ExportRecord(object sender, RoutedEventArgs e)
        {
            Record.Export();
        }
    }
}
