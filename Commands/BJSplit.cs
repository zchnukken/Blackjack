
namespace Blackjack.Commands
{
    using System.Windows.Input;
    using Blackjack.ViewModel;

    internal class BJSplit : ICommand
    {
        public BJSplit(BlackJack _blackjack)
        {
            _Blackjack = _blackjack;
        }

        private BlackJack _Blackjack;

        public bool CanExecute(object parameter)
        {
            return _Blackjack.can_split;

        }

        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _Blackjack.split();
        }
    }
}
