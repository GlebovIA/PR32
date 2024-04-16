using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PR32.Pages.State.Elements
{
    /// <summary>
    /// Логика взаимодействия для StateItem.xaml
    /// </summary>
    public partial class StateItem : UserControl
    {
        Classes.State ThisState;
        Pages.State.StateMain Main;
        public StateItem(Classes.State state, StateMain main)
        {
            InitializeComponent();
            ThisState = state;
            Main = main;
            NameTBx.Text = state.Name;
            SubnameTBx.Text = state.Subname;
            DescriptionTBx.Text = state.Description;
        }

        private void EditState(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.OpenPages(new Pages.State.Add(ThisState));
        }

        private void DeleteState(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Удалить состояние: {this.ThisState.Name}?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                IEnumerable<Classes.Record> AllRecords = Classes.Record.AllRecords();
                if (AllRecords.Where(x => x.State == ThisState.Id).Count() > 0)
                {
                    MessageBox.Show($"Состояние {this.ThisState.Name} невозможно удалить. Для начала Удалите зависимости.", "Уведомление");
                }
                else
                {
                    this.ThisState.Delete();
                    Main.ParentSP.Children.Remove(this);
                    MessageBox.Show($"Состояние {this.ThisState.Name} успешно удалено.", "Уведомление");
                }
            }
        }
    }
}
