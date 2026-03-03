using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.ViewModel
{
    /// <summary>
    /// Представляет команду для сохранения контакта в файл.
    /// Реализует интерфейс <see cref="ICommand"/> для использования в паттерне MVVM.
    /// </summary>
    class SaveCommand : ICommand
    {
        private readonly MainVM _viewModel;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SaveCommand"/>.
        /// </summary>
        /// <param name="viewModel">
        /// Ссылка на главную модель представления <see cref="MainVM"/>, 
        /// в которой будет вызван метод сохранения контакта.
        /// </param>
        public SaveCommand(MainVM viewModel)
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
        /// Всегда возвращает <c>true</c>, так как команда сохранения доступна всегда.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Выполняет логику команды сохранения контакта.
        /// Вызывает метод <see cref="MainVM.SaveContact"/> у связанной модели представления.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        public void Execute(object parameter)
        {
            _viewModel.SaveContact();
        }
    }
}
