using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diary.Commands
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        //konstruktor przyjmuje delegata czyli po prostu metodę
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }
        /*
        To samo co wyżej tylko z dwoma delegatami. Delegat, który jest przekazany jako pierwszy on będzie wykonywał tę metodę,
        a delegat, który jest przekazany jako drugi będzie nam mówił czy ta metoda może być wykonana.
        Czyli jeżeli np zbindujemy to sobie z jakimś przyciskiem to ta pierwsza metoda, która została przekazana jako parametr zostanie wywołana,
        np. jeśli przycisk zostanie kliknięty a drugi delegat będzie nam mówił o tym czy ten przycisk może zostać kliknięty, czy on może się wykonać.
        Ta druga metoda będzie zwracała true albo false. Czyli jeśli ta metoda zwróci false, to ta przekazana jako pierwsza nie będzie mogła
        zostać wykonana.
        */
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
