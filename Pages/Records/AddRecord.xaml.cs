using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PR32.Pages.Records
{
    /// <summary>
    /// Логика взаимодействия для AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Page
    {
        public IEnumerable<Classes.Manufacturer> Manufacturers = Classes.Manufacturer.AllManufacturers();
        public IEnumerable<Classes.State> AllStates = Classes.State.AllState();
        private Classes.Record ChangeRecord;
        public AddRecord(Classes.Record changeRecord = null)
        {
            InitializeComponent();
            foreach (var item in Manufacturers)
                ManufacturerCB.Items.Add(item.Name);
            if (Manufacturers.Count() > 0)
                ManufacturerCB.SelectedIndex = 0;
            foreach (var item in AllStates)
                StateCB.Items.Add(item.Name);
            if (AllStates.Count() > 0)
                StateCB.SelectedIndex = 0;
            if (changeRecord != null)
            {
                ChangeRecord = changeRecord;
                NameTBx.Text = changeRecord.Name;
                YearTBx.Text = changeRecord.Year.ToString();
                PriceTBx.Text = changeRecord.Price.ToString();
                DescriptionTBx.Text = changeRecord.Description;
                FormatCB.SelectedIndex = changeRecord.Format;
                ManufacturerCB.SelectedIndex = Manufacturers.ToList().FindIndex(x => x.Id == changeRecord.Manufacturer);
                SizeCB.SelectedIndex = changeRecord.Size;
                StateCB.SelectedIndex = AllStates.ToList().FindIndex(x => x.Id == changeRecord.State);
                AddRecordBtn.Content = "Изменить";
            }
        }
        private void AddNewRecord(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(NameTBx.Text))
                if (!String.IsNullOrEmpty(YearTBx.Text))
                    if (!String.IsNullOrEmpty(PriceTBx.Text))
                        if (NameTBx.Text.Length <= 250)
                        {
                            if (ChangeRecord == null)
                            {
                                Classes.Record newRecord = new Classes.Record()
                                {
                                    Name = NameTBx.Text,
                                    Year = Convert.ToInt32(YearTBx.Text),
                                    Format = FormatCB.SelectedIndex,
                                    Size = SizeCB.SelectedIndex,
                                    Manufacturer = Manufacturers.Where(x => x.Name == ManufacturerCB.SelectedValue.ToString()).First().Id,
                                    Price = float.Parse(PriceTBx.Text.Replace(".", ",")),
                                    State = AllStates.Where(x => x.Name == StateCB.SelectedItem.ToString()).First().Id,
                                    Description = DescriptionTBx.Text
                                };
                                newRecord.Save();
                                MessageBox.Show($"Пластинка {newRecord.Name} успешно добавлена.", "Уведомление");
                                MainWindow.MW.OpenPages(new AddRecord(newRecord));
                            }
                            else
                            {
                                ChangeRecord.Name = NameTBx.Text;
                                ChangeRecord.Year = Convert.ToInt32(YearTBx.Text);
                                ChangeRecord.Format = FormatCB.SelectedIndex;
                                ChangeRecord.Size = SizeCB.SelectedIndex;
                                ChangeRecord.Manufacturer = Manufacturers.Where(x => x.Name == ManufacturerCB.SelectedValue.ToString()).First().Id;
                                ChangeRecord.Price = float.Parse(PriceTBx.Text.Replace(".", ","));
                                ChangeRecord.State = AllStates.Where(x => x.Name == StateCB.SelectedItem.ToString()).First().Id;
                                ChangeRecord.Description = DescriptionTBx.Text;
                                ChangeRecord.Save(true);
                                MessageBox.Show($"Пластинка {ChangeRecord.Name} успешно изменена.", "Уведомление");
                            }
                        }
                        else MessageBox.Show($"Наименование пластинки слишком большое", "Предупреждение");
                    else MessageBox.Show($"Пожалуйста, укажите стоимость пластинки", "Предупреждение");
                else MessageBox.Show($"Пожалуйста, укажите год выпуска пластинки", "Предупреждение");
            else MessageBox.Show($"Пожалуйста, укажите наименование пластинки", "Предупреждение");
        }

        private void PreviewFloat(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void PreviewNumber(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0)) && e.Text != ".";
        }
    }
}
