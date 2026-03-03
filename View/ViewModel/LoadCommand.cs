using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.ViewModel
{
    class LoadCommand : ICommand
    {
        private readonly MainVM _viewModel;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LoadCommand"/>.
        /// </summary>
        /// <param name="viewModel">
        /// Ссылка на главную модель представления <see cref="MainVM"/>, 
        /// в которой будет вызван метод загрузки контакта.
        /// </param>
        public LoadCommand(MainVM viewModel)
        {
            _viewModel = viewModel;
        }

        /// <summary>
        /// Событие, которое возникает при изменении возможности выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Определяет, может ли команда выполняться в текущем состоянии.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        /// <returns>
        /// Всегда возвращает <c>true</c>, так как команда загрузки доступна всегда.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Выполняет логику команды загрузки контакта.
        /// Вызывает метод <see cref="MainVM.LoadContact"/> у связанной модели представления.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        public void Execute(object parameter)
        {
            _viewModel.LoadContact();
        }
    }
}
