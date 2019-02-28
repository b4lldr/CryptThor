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

namespace CryptThor.Views
{
    /// <summary>
    /// Interaction logic for CipherView.xaml
    /// </summary>
    public partial class CipherView : UserControl
    {
        CipherViewModel cvm = Creator.cvm;


        public CipherView()
        {
            InitializeComponent();
        }

        #region ContentButtons
        private void btnEncode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cvm.Encrypt(true, textboxInput.Text, textboxKey.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnDecode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cvm.Decrypt(textboxInput.Text, textboxKey.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnRemoveSpaces_Click(object sender, RoutedEventArgs e)
        {
            cvm.RemoveSpaces();
        }

        private void btnReverseText_Click(object sender, RoutedEventArgs e)
        {
            cvm.TextReverse();
        }

        private void btnPasteToInput_Click(object sender, RoutedEventArgs e)
        {
            textboxInput.Text = Clipboard.GetText();
        }

        private void btnCopyFromOutput_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(textboxOutput.Text);
        }
        #endregion

    }
}
