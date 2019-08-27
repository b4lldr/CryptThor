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

            SetLanguage("Lang_EN.xaml");
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
        private void Btn_Click_Caesar(object sender, RoutedEventArgs e)
        {
            DataContext = cvm;
            cvm.ToCaesar();

        }

        private void Btn_Click_Vigenere(object sender, RoutedEventArgs e)
        {
            DataContext = cvm;
            cvm.ToVigenere();
        }

        private void Btn_Click_Vernam(object sender, RoutedEventArgs e)
        {
            DataContext = cvm;
            cvm.ToVernam();
        }

        private void Btn_Click_Scytale(object sender, RoutedEventArgs e)
        {
            DataContext = cvm;
            cvm.ToScytale();
        }

        private void Btn_Click_Morse(object sender, RoutedEventArgs e)
        {
            DataContext = cvm;
            cvm.ToMorse();
        }

        private void Btn_Click_Cs(object sender, RoutedEventArgs e)
        {
            SetLanguage("Lang_CS.xaml");
        }

        private void Btn_Click_En(object sender, RoutedEventArgs e)
        {
            SetLanguage("Lang_EN.xaml");
        }
        #endregion

        private void SetLanguage(string fileName) {
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri("..\\Locales\\" + fileName, UriKind.Relative);
            this.Resources.MergedDictionaries.Add(dict);

            cvm.SetLanguage(fileName.Substring(5, 2).ToLower());
        }
    }
}
