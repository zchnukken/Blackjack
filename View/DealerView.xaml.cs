﻿using Blackjack.Model;
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
    /// <summary>
    /// Interaction logic for DealerView.xaml
    /// </summary>
    public partial class DealerView : UserControl
    {

        public DealerView(BJDealer dealer)
        {
            InitializeComponent();
            DataContext = dealer;
        }
    }
}
