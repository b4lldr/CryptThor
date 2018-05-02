using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptThor.Models;

namespace CryptThor
{
    public class ViewModel : INotifyPropertyChanged
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
            Description = "Toto je Cézarova šifra";

            CipherChange();
        }

        public void ToVigenere()
        {
            Caption = "Vigenèrova šifra";
            Description = "Toto je Vigenerova šifra";

            CipherChange();
        }

        public void ToScytale()
        {
            Caption = "Skytalé";
            Description = "Toto je šifra Skytalé";

            CipherChange();
        }
        #endregion

        #region Encryption/Decryption
        public void Encrypt(bool b, string plainText, string key)
        {
            if(CurrentKey != null)
            {
                CurrentKey = CurrentKey.Replace(" ", string.Empty);
                CallChange("CurrentKey");
            }

            if (CurrentText != null)
            {
                var decomposed = CurrentText.Normalize(NormalizationForm.FormD);
                var filtered = decomposed.Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
                CurrentText = new String(filtered.ToArray());

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

            if (encrypt)
            {
                Output = VigenereCipher.Encrypt(text, key);
            }
            else
            {
                Output = VigenereCipher.Decrypt(text, key);
            }
            CallChange("Output");
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

        void Scytale(bool encrypt, string text, string key)
        {
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
