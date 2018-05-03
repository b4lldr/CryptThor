using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptThor.Models;

namespace CryptThor.ViewModels
{
    public class CipherViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void CallChange(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public string Caption { get; private set; }
        public string Description { get; private set; }
        public string Output { get; private set; }
        public string CurrentText { get; set; }
        public string CurrentKey { get; set; }
        #region CipherChange
        void CipherChange()
        {
            CallChange("Caption");
            CallChange("Description");
        }

        public void ToCaesar()
        {
            Caption = "Cézarova šifra";
            Description = "Velmi jednoduchá substituční monoalfabetická šifra poprvé použita římským vojevůdcem Gaiem Juliem Caesarem, který ji popsal v Zápiscích o válce galské. \n" +
                          "Klíčem je 1 písmeno anglické abecedy, které udává posun otevřeného textu (a = 0, b = 2, ...). Caesar s úspěchem používal výhradně posun o tři pozice (písmeno d). \n" +
                          "Jedná se však o velmi slabou šifru, kterou lze jednoduše prolomit prostým vyzkoušením všech (26) kombinací nebo dokonce metodou zvanou frekvenční analýza.";

            CipherChange();
        }

        public void ToVigenere()
        {
            Caption = "Vigenèrova šifra";
            Description = "Substituční polyalfabetická šifra, která je díky více abecedám o měco bezpečnější a složitější než Cézarova šifra. Je například mnohem více odolnější vůči frekvenční analýze a má větší klíčový prostor. \n" +
                          "Klíčem je libovolně dlouhý textový řetězec, který se opakuje tak dlouho, dokud není tak dlouhý jako otevřený text. Princip šifrování je stejný jako u Cézarovy šifry s tím rozdílem, že nešifrujeme celou zprávu jedním znakem, ale každý znak otevřeného textu odpovídajícím znakem klíče (každé písmeno jinou abecedou) \n" +
                          "Nevýhoda této šifry je opakování klíče. Čím dělší klíč je, tím je šifra bezpečnější.";

            CipherChange();
        }

        public void ToVernam()
        {
            Caption = "Vernamova šifra";
            Description = "Vernamova šifra (anglicky One time pad) je bez znalosti klíče nerozluštitelnou šifrou. (Klíčový prostor je tak velmý, že není možné v rozumném čase najít správný klíč) \n" +
                          "Klíčem je náhodně vygenerovaný řetězec, který je stejně dlouhý jako otevřený text. Zaniká tak slabina Vigenèrovy šifry kvůli opakování klíče. Princip šifrování je však stejný. /n" +
                          "Aby byla šifra opravdu neprolomitelná, musí být použitý generátor opravdu reálných čísel, má implementace Vernamovy šifry používá tzv generátor pseudonáhodných čísel. \n" +
                          "U této šifry je při každém šifrování automaticky vygenerován pseudonáhodný klíč, z tohoto důvodu slouží pole k zadávání klíče pro dešifrování. (Při šifrování bude přepsáno.)";

            CipherChange();
        }

        public void ToScytale()
        {
            Caption = "Skytalé";
            Description = "Jednoduchý historický transpoziční šifrovací princip využívající válce a pásku papíru. Pásek papíru se namotá na válec a na pásek se napíše vzkaz, který je po rozmotání proházený. \n" +
                          "Opět se jedná o velmi primitivní šifru, kterou lze prolomit například namotáním pásku na kužel a zjištěním správného průměru původního válce." +
                          "Klíčem je číslo udávající počet omotání pásku s texten kolem válce. \n" +
                          "U této šifry jsou ze vztupního textu odstraněny mezery z důvodu fungování transpozičních šifer, které pouze prohazují znaky otevřeného textu.";

            CipherChange();
        }
        #endregion

        #region Encryption/Decryption
        public void Encrypt(bool b, string plainText, string key)
        {
            if(key != null)
            {
                if(Caption != "Skytalé")
                {
                    foreach (char c in key)
                    {
                        if (c < 65 || (c > 90 && c < 97))
                            key = key.Replace(c, ' ');
                    }
                }

                key = key.Replace(" ", string.Empty);

                var decomposed = key.Normalize(NormalizationForm.FormD);
                var filtered = decomposed.Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
                key = new String(filtered.ToArray());

                CurrentKey = key;
                CallChange("CurrentKey");
            }

            if (plainText != null)
            {
                var decomposed = plainText.Normalize(NormalizationForm.FormD);
                var filtered = decomposed.Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
                plainText = new String(filtered.ToArray());

                CurrentText = plainText;
                CallChange("CurrentText");
            }

            switch (Caption)
            {
                case "Cézarova šifra":
                    Caesar(b, plainText, key);
                    break;
                case "Vigenèrova šifra":
                    Vigenere(b, plainText, key);
                    break;
                case "Vernamova šifra":
                    Vernam(b, plainText, key);
                    break;
                case "Skytalé":
                    Scytale(b, plainText, key);
                    break;
            }
        }

        public void Decrypt(string ciphertext, string key)
        {
            Encrypt(false, ciphertext, key);
        }

        public void RemoveSpaces()
        {
            if(CurrentText != null)
            {
                CurrentText = CurrentText.Replace(" ", string.Empty);
                CallChange("CurrentText");
            }
        }

        public void TextReverse()
        {
            if(CurrentText != null)
            {
                char[] charArray = CurrentText.ToCharArray();
                Array.Reverse(charArray);
                CurrentText = new string(charArray);
                CallChange("CurrentText");
            }
        }

        void Caesar(bool encrypt, string text, string key)
        {
            if (key.Length != 1)
                throw new Exception("Klíč musí obsahovat právě 1 znak.");

            Vigenere(encrypt, text, key);
        }

        void Vigenere(bool encrypt, string text, string key)
        {
            if (key.Length == 0)
                throw new Exception("Klíč musí obsahovat alespoň 1 znak.");

            if (encrypt)
            {
                Output = VigenereCipher.Encrypt(text, key);
                CallChange("Output");
            }
            else
            {
                Output = VigenereCipher.Decrypt(text, key);
                CallChange("Output");
            }
        }

        void Vernam(bool encrypt, string text, string key)
        {
            if(encrypt)
            {
                key = null;
                Random random = new Random();

                for (int i = 0; i < text.Length; i++)
                {
                    key += (char)random.Next('a', 'z' + 1);
                }

                CurrentKey = key;
                CallChange("CurrentKey");
            }

            Vigenere(encrypt, text, key);
        }

        void Scytale(bool encrypt, string text, string key)
        {
            text = text.Replace(" ", string.Empty);
            CurrentText = text;
            CallChange("CurrentText");

            int turns = 0;
            if (!int.TryParse(key, out turns))
                throw new Exception("Klíč musí být číslo.");

            if (encrypt)
            {
                Output = ScytaleCipher.Encrypt(text, turns);
                CallChange("Output");
            }
            else
            {
                Output = ScytaleCipher.Decrypt(text, turns);
                CallChange("Output");
            }
        }
        #endregion
    }
}
