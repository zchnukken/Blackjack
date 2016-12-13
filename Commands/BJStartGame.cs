
namespace Blackjack.Commands
{
    using System.Windows.Input;
    using Blackjack.ViewModel;

    public class BJStartGame : ICommand
    {
        public BJStartGame(BlackJackMenu menu)
        {
            _menu = menu;
        }

        private BlackJackMenu _menu;

        public bool CanExecute(object parameter)
        {
            return _menu.can_start;
        }

        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _menu.startGame();
        }
    }
}
