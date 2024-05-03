using System.Collections.Generic;
using System.Windows.Controls;

namespace PR32.Pages.Supply
{
    /// <summary>
    /// Логика взаимодействия для SupplyMain.xaml
    /// </summary>
    public partial class SupplyMain : Page
    {
        public IEnumerable<Classes.Supply> AllSupplies = Classes.Supply.AllSupplies();
        public SupplyMain()
        {
            InitializeComponent();
            foreach (var supply in AllSupplies)
                ParentSP.Children.Add(new Pages.Supply.Elements.SupplyItem(supply, this));
        }
    }
}
