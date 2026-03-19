using ContactSecond.ViewModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactSecond
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainVM _mainVm;

        public MainWindow()
        {
            InitializeComponent();
            _mainVm = new MainVM();
            this.DataContext = _mainVm;

            // Подписываемся на событие закрытия окна
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            // Сохраняем данные при закрытии
            if (_mainVm.SaveCommand.CanExecute(null))
            {
                _mainVm.SaveCommand.Execute(null);
            }
        }
    }
}