using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PR32.Pages.Manufacturer
{
    /// <summary>
    /// Логика взаимодействия для AddManufacturer.xaml
    /// </summary>
    public partial class AddManufacturer : Page
    {
        public IEnumerable<Classes.Country> AllCountries = Classes.Country.AllCountries();
        Classes.Manufacturer ChangeManufacturer;
        public AddManufacturer(Classes.Manufacturer manufacturer = null)
        {
            InitializeComponent();
            foreach (var country in AllCountries)
                CountryCB.Items.Add(country.Name);
            if (AllCountries.Count() > 0)
                CountryCB.SelectedIndex = 0;
            if (ChangeManufacturer != null)
            {
                ChangeManufacturer = manufacturer;
                NameTBx.Text = ChangeManufacturer.Name;
                PhoneTBx.Text = ChangeManufacturer.Phone;
                MailTBx.Text = ChangeManufacturer.Mail;
                CountryCB.SelectedIndex = AllCountries.ToList().FindIndex(x => x.Id == ChangeManufacturer.CountryCode);
                AddBtn.Content = "Изменить";
            }
        }

        private void AddNewManufacturer(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(NameTBx.Text))
            {
                if (!String.IsNullOrEmpty(PhoneTBx.Text))
                {
                    if (!String.IsNullOrEmpty(MailTBx.Text))
                    {
                        if (CorrectPhone(PhoneTBx.Text))
                        {
                            if (CorrectMail(MailTBx.Text))
                            {
                                if (ChangeManufacturer == null)
                                {
                                    Classes.Manufacturer newManufacturer = new Classes.Manufacturer()
                                    {
                                        Name = NameTBx.Text,
                                        Phone = PhoneTBx.Text,
                                        Mail = MailTBx.Text,
                                        CountryCode = AllCountries.Where(x => x.Name == CountryCB.SelectedItem.ToString()).First().Id
                                    };
                                    newManufacturer.Save();
                                    MessageBox.Show($"Поставщик {newManufacturer.Name} успешно добавлен.", "Уведомление");
                                    MainWindow.MW.OpenPages(new AddManufacturer(newManufacturer));
                                }
                                else
                                {
                                    ChangeManufacturer.Name = NameTBx.Text;
                                    ChangeManufacturer.Phone = PhoneTBx.Text;
                                    ChangeManufacturer.Mail = MailTBx.Text;
                                    ChangeManufacturer.CountryCode = AllCountries.Where(x => x.Name == CountryCB.SelectedItem.ToString()).First().Id;
                                    ChangeManufacturer.Save(true);
                                    MessageBox.Show($"Поставщик {ChangeManufacturer.Name} успешно изменен.", "Уведомление");
                                }
                            }
                            else MessageBox.Show("Пожалуйста, укажите почту поставщика в формате хх@xx.xx.", "Предупреждение");
                        }
                        else MessageBox.Show("Пожалуйста, укажите номер поставщика в формате 89000000000.", "Предупреждение");
                    }
                    else MessageBox.Show("Пожалуйста, укажите почту поставщика.", "Предупреждение");
                }
                else MessageBox.Show("Пожалуйста, укажите телефон поставщика.", "Предупреждение");
            }
            else MessageBox.Show("Пожалуйста, укажите наименование поставщика.", "Предупреждение");
        }
        public bool CorrectPhone(string someNumber)
        {
            string RegexStr = "89[0-9]{9}$";
            Regex regex = new Regex(RegexStr);
            MatchCollection collection = regex.Matches(someNumber);
            return collection.Count > 0;
        }
        public bool CorrectMail(string someMail)
        {
            string RegexStr = "[aA-zZ]{2,20}@[aA-zZ]{2,20}\\.[aA-zZ]{2-3}$";
            Regex regex = new Regex(RegexStr);
            MatchCollection collection = regex.Matches(someMail);
            return collection.Count > 0;
        }
        private void PreviewNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
