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
using CryptThor.ViewModels;

namespace CryptThor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CipherViewModel cvm = Creator.cvm;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new IntroViewModel();
        }

        #region TopButtons
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new IntroViewModel();
        }

        private void btnSett_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new DictionaryViewModel();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            imgMax.Icon = WindowState == WindowState.Maximized ? FontAwesome.WPF.FontAwesomeIcon.WindowRestore : FontAwesome.WPF.FontAwesomeIcon.WindowMaximize;
        }
        #endregion

        #region NavButtons
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = cvm;
            cvm.ToCaesar();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataContext = cvm;
            cvm.ToVigenere();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataContext = cvm;
            cvm.ToVernam();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DataContext = cvm;
            cvm.ToScytale();
        }
        #endregion
    }
}
