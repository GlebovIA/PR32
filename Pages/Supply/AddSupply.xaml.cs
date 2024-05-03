using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PR32.Pages.Supply
{
    /// <summary>
    /// Логика взаимодействия для AddSupply.xaml
    /// </summary>
    public partial class AddSupply : Page
    {
        IEnumerable<Classes.Manufacturer> AllManufacturers = Classes.Manufacturer.AllManufacturers();
        IEnumerable<Classes.Record> AllRecords = Classes.Record.AllRecords();
        Classes.Supply ChangeSupply;
        public AddSupply(Classes.Supply changeSupply = null)
        {
            InitializeComponent();
            foreach (var manufacturer in AllManufacturers)
                ManufacturerCB.Items.Add(manufacturer.Name);
            if (ManufacturerCB.Items.Count > 0)
                ManufacturerCB.SelectedIndex = 0;
            foreach (var record in AllRecords)
                RecordCB.Items.Add(record.Name);
            if (RecordCB.Items.Count > 0)
                RecordCB.SelectedIndex = 0;
            if (changeSupply != null)
            {
                ChangeSupply = changeSupply;
                ManufacturerCB.SelectedIndex = AllManufacturers.ToList().FindIndex(x => x.Id == changeSupply.Manufacturer);
                RecordCB.SelectedIndex = AllRecords.ToList().FindIndex(x => x.Id == changeSupply.Record);
                CountTBx.Text = changeSupply.Count.ToString();
                DateTime dt = new DateTime();
                DateTime.TryParse(changeSupply.DateDelivery, out dt);
                DateDeliveryDP.SelectedDate = dt;
                AddBtn.Content = "Изменить";
            }
        }

        private void AddNewSupply(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime();
            if (DateTime.TryParse(DateDeliveryDP.SelectedDate.ToString(), out dt))
                if (!String.IsNullOrEmpty(CountTBx.Text))
                {
                    if (ChangeSupply == null)
                    {
                        Classes.Supply newSupply = new Classes.Supply()
                        {
                            Manufacturer = AllManufacturers.Where(x => x.Name == ManufacturerCB.SelectedItem.ToString()).First().Id,
                            Record = AllRecords.Where(x => x.Name == RecordCB.SelectedItem.ToString()).First().Id,
                            Count = Convert.ToInt32(CountTBx.Text),
                            DateDelivery = CorrectDate(DateDeliveryDP.SelectedDate.ToString())
                        };
                        newSupply.Save();
                        MessageBox.Show($"Поставка №{newSupply.Id} успешно добавлена", "Уведомление");
                        MainWindow.MW.OpenPages(new AddSupply(newSupply));
                    }
                    else
                    {
                        ChangeSupply.Manufacturer = AllManufacturers.Where(x => x.Name == ManufacturerCB.SelectedItem.ToString()).First().Id;
                        ChangeSupply.Record = AllRecords.Where(x => x.Name == RecordCB.SelectedItem.ToString()).First().Id;
                        ChangeSupply.Count = Convert.ToInt32(CountTBx.Text);
                        ChangeSupply.DateDelivery = CorrectDate(DateDeliveryDP.SelectedDate.ToString());
                        ChangeSupply.Save(true);
                        MessageBox.Show($"Поставка №{ChangeSupply.Id} успешно изменена", "Уведомление");
                    }
                }
                else MessageBox.Show("Пожалуйста, укажите количество поставки", "Предупреждение");
            else MessageBox.Show("Пожалуйста, укажите дату поставки", "Предупреждение");
        }

        private void PreviewNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
        public string CorrectDate(string Value)
        {
            DateTime dt = new DateTime();
            DateTime.TryParse(Value, out dt);
            return dt.Year + "-" + dt.Month + "-" + dt.Day;
        }
    }
}
