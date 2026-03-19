using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactSecond.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Инициализирует новый экземпляр класса RelayCommand.
        /// </summary>
        /// <param name="execute">Делегат, вызываемый при выполнении команды. Определяет действие, которое будет выполнено.</param>
        /// <param name="canExecute">Делегат, определяющий, может ли команда быть выполнена в текущем состоянии.
        /// Если параметр равен null, команда считается всегда доступной для выполнения.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute; //что делать при нажатии
            _canExecute = canExecute; //Когда кнопка должна быть активна
        }

        /// <summary>
        /// Определяет, может ли команда быть выполнена в текущем состоянии.
        /// </summary>
        /// <param name="parameter">Параметр команды, используемый делегатом canExecute.</param>
        /// <returns>true, если команда может быть выполнена; в противном случае — false.
        /// Если делегат canExecute не задан, всегда возвращает true.</returns>
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        /// <summary>
        /// Выполняет команду, вызывая переданный делегат execute.
        /// </summary>
        /// <param name="parameter">Параметр команды, передаваемый делегату execute.</param>
        public void Execute(object parameter) => _execute(parameter);

        /// <summary>
        /// Событие, возникающее при изменении условий, влияющих на возможность выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
