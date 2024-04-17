using System.Collections.Generic;
using System.Windows.Controls;

namespace PR32.Pages.Manufacturer
{
    /// <summary>
    /// Логика взаимодействия для ManufacturerMain.xaml
    /// </summary>
    public partial class ManufacturerMain : Page
    {
        public IEnumerable<Classes.Manufacturer> AllManufacturers = Classes.Manufacturer.AllManufacturers();
        public ManufacturerMain()
        {
            InitializeComponent();
            foreach (Classes.Manufacturer manufacturer in AllManufacturers)
                ParentSP.Children.Add(new Manufacturer.Elements.ManufacturerItem(manufacturer, this));
        }
    }
}
