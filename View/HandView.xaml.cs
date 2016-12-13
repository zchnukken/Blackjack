using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blackjack.View
{
    using Model;
    /// <summary>
    /// Interaction logic for HandView.xaml
    /// </summary>
    /// 
    public partial class HandView : UserControl
    {
        private Player _player;

        public HandView(ref Player p)
        {
            _player = p;
            InitializeComponent();
            DataContext = this;
        }

        public Player Player
        {
            get { return _player; }
            private set { ;}
        }
    }
}
