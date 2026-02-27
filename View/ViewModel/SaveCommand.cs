using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.ViewModel
{
    class SaveCommand : ICommand
    {
        private readonly MainVM _viewModel;
        public SaveCommand(MainVM viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;


        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Сохранение контакта в файл
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _viewModel.SaveContact();
        }
    }
}
