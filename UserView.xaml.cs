using System.Windows.Controls;
using Task2.ViewModels;

namespace Task2
{
    /// <summary>
    /// Логика взаимодействия для UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        private UserViewModel _viewModel;
        public UserView()
        {
            InitializeComponent();
            DataContext = _viewModel = new UserViewModel();
        }
    }
}