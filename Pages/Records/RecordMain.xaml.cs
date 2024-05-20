using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace PR32.Pages.Records
{
    public partial class RecordMain : Page
    {
        public IEnumerable<Classes.State> AllStates = Classes.State.AllState();
        public IEnumerable<Classes.Record> AllRecords = Classes.Record.AllRecords();
        public IEnumerable<Classes.Manufacturer> AllManufacturers = Classes.Manufacturer.AllManufacturers();
        private bool CreateUI = false;
        public List<Classes.Record> searchRecords;

        public RecordMain()
        {
            InitializeComponent();
            searchRecords = AllRecords.ToList();
            CreateUI = true;
            LoadAllRecords(AllRecords.ToList());
            LoadAllManufacturers();
            LoadAllStates();
        }
        public void LoadRecord()
        {
            AllRecords = Classes.Record.AllRecords();
            LoadAllRecords(AllRecords.ToList());
        }
        public void LoadAllRecords(List<Classes.Record> AllRecords)
        {
            ParentSP.Children.Clear();
            foreach (var record in AllRecords)
                ParentSP.Children.Add(new Pages.Records.Elements.RecordItem(record, this));
        }
        public void LoadAllManufacturers()
        {
            ManufacturerCB.Items.Clear();
            foreach (var manufacturer in AllManufacturers)
                ManufacturerCB.Items.Add(manufacturer.Name);
            ManufacturerCB.Items.Add("Выберите...");
            ManufacturerCB.SelectedIndex = ManufacturerCB.Items.Count - 1;
        }
        public void LoadAllStates()
        {
            StateCB.Items.Clear();
            foreach (var state in AllStates)
                StateCB.Items.Add(state.Name);
            StateCB.Items.Add("Выберите...");
            StateCB.SelectedIndex = StateCB.Items.Count - 1;
        }
        public void RecordsFilter()
        {
            List<Classes.Record> FilterRecords = new List<Classes.Record>();
            if (ManufacturerCB.SelectedIndex != ManufacturerCB.Items.Count - 1)
                FilterRecords = AllRecords.Where(x => x.IdManufacturer == AllManufacturers.Where(y => y.Name == ManufacturerCB.SelectedItem.ToString()).First().Id).ToList();
            else
                FilterRecords = AllRecords.ToList();
            if (StateCB.SelectedIndex != StateCB.Items.Count - 1)
                FilterRecords = FilterRecords.FindAll(x => x.IdState == AllStates.Where(y => y.Name == StateCB.SelectedItem.ToString()).First().Id);
            if (NameTBx.Text != "")
            {
                if ("моно".Contains(NameTBx.Text.ToLower()))
                {
                    FilterRecords = FilterRecords.FindAll(x => x.Format == 0);
                }
                else if ("стерео".Contains(NameTBx.Text.ToLower()))
                {
                    FilterRecords = FilterRecords.FindAll(x => x.Format == 1);
                }
                else
                {
                    FilterRecords = FilterRecords.FindAll(x =>
                    x.Name.ToLower().Contains(NameTBx.Text.ToLower()) ||
                    x.Year.ToString().Contains(NameTBx.Text) ||
                    x.Price.ToString().Contains(NameTBx.Text) ||
                    x.Description.ToLower().Contains(NameTBx.Text.ToLower()));
                }
            }
            searchRecords.Clear();
            searchRecords = FilterRecords;
            LoadAllRecords(FilterRecords);
        }

        private void FilterRecords(object sender, SelectionChangedEventArgs e)
        {
            if (CreateUI) RecordsFilter();
        }

        private void SearchRecords(object sender, System.Windows.Input.KeyEventArgs e)
        {
            RecordsFilter();
        }
    }
}
