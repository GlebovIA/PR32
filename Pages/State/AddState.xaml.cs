using System;
using System.Windows;
using System.Windows.Controls;

namespace PR32.Pages.State
{
    /// <summary>
    /// Логика взаимодействия для AddState.xaml
    /// </summary>
    public partial class AddState : UserControl
    {
        Classes.State ChangeState;
        public AddState(Classes.State state = null)
        {
            InitializeComponent();
            if (state != null)
            {
                ChangeState = state;
                NameTBx.Text = state.Name;
                SubnameTBx.Text = state.Subname;
                DescriptionTBx.Text = state.Description;
                AddBtn.Content = "Изменить";
            }
        }

        private void AddNewState(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(NameTBx.Text))
            {
                if (!String.IsNullOrEmpty(SubnameTBx.Text))
                {
                    if (this.ChangeState == null)
                    {
                        Classes.State newState = new Classes.State()
                        {
                            Name = NameTBx.Text,
                            Subname = SubnameTBx.Text,
                            Description = DescriptionTBx.Text,
                        };
                        newState.Save();
                        MessageBox.Show($"Состояние {newState.Name} успешно добавлено.", "Уведомление");
                        MainWindow.mainWindow.OpenPages(new Pages.State.AddState(newState));
                    }
                    else
                    {
                        ChangeState.Name = NameTBx.Text;
                        ChangeState.Subname = SubnameTBx.Text;
                        ChangeState.Description = DescriptionTBx.Text;
                        ChangeState.Save(true);
                        MessageBox.Show($"Состояние {ChangeState.Name} успешно изменено.", "Уведомление");
                    }
                }
                else MessageBox.Show("Пожалуйста, укажите сокращенное наименование состояния", "Предупреждение");
            }
            else MessageBox.Show("Пожалуйста, укажите наименование состояния", "Предупреждение");
        }
    }
}
