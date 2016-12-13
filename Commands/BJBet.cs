
namespace Blackjack.Commands
{
    using System.Windows.Input;
    using Blackjack.ViewModel;

    public class BJBet : ICommand
    {
        public BJBet(BlackJack _blackjack)
        {
            _Blackjack = _blackjack;
        }

        private BlackJack _Blackjack;

        public bool CanExecute(object parameter)
        {
            return _Blackjack.can_bet;
        }

        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _Blackjack.bet();
        }
    }
}
