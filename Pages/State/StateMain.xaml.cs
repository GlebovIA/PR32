using System.Collections.Generic;
using System.Windows.Controls;

namespace PR32.Pages.State
{
    /// <summary>
    /// Логика взаимодействия для StateMain.xaml
    /// </summary>
    public partial class StateMain : Page
    {
        public IEnumerable<Classes.State> AllState = Classes.State.AllState();
        public StateMain()
        {
            InitializeComponent();
            foreach (var state in AllState)
            {
                ParentSP.Children.Add(new Pages.State.Elements.StateItem(state, this));
            }
        }
    }
}
