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

namespace CryptThor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm = new ViewModel();


        public MainWindow()
        {
            InitializeComponent();

            DataContext = vm;
        }

        #region TopButtons
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSett_Click(object sender, RoutedEventArgs e)
        {

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
            vm.ToCaesar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.ToVigenere();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            vm.ToScytale();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Encryption/Decryption
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                vm.Encrypt(true, textboxInput.Text, textboxKey.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            try
            {
                vm.Decrypt(textboxInput.Text, textboxKey.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            vm.RemoveSpaces();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            vm.TextReverse();
        }
        #endregion

    }
}
