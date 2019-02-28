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
        //binded propeties
        public string Caption { get; private set; }
        public string Description { get; private set; }
        public string InputUsageDescription { get; private set; }
        public string KeyUsageDescription { get; private set; }
        public string Output { get; private set; }
        public string CurrentInputText { get; set; }
        public string CurrentKey { get; set; }
        public string KeyVisibility { get; set; } 

        //property change event handler
        public event PropertyChangedEventHandler PropertyChanged;
        void CallChange(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Methods called when cipher is changed from the side menu
        /// Methods change the caption and descrition of the cipher
        /// </summary>
        #region CipherChange
        void CipherChange()
        {
            CallChange("Caption"); 
            CallChange("Description");
            CallChange("InputUsageDescription");
            CallChange("KeyUsageDescription");
            CallChange("KeyVisibility");
        }

        public void ToCaesar()
        {
            Caption = "Cézarova šifra";
            Description = "Velmi jednoduchá substituční monoalfabetická šifra poprvé použita římským vojevůdcem Gaiem Juliem Caesarem, který ji popsal v Zápiscích o válce galské. \n" +
                          "Klíčem je 1 písmeno anglické abecedy, které udává posun otevřeného textu (a = 0, b = 2, ...). Caesar s úspěchem používal výhradně posun o tři pozice (písmeno d). \n" +
                          "Jedná se však o velmi slabou šifru, kterou lze jednoduše prolomit prostým vyzkoušením všech (26) kombinací nebo dokonce metodou zvanou frekvenční analýza.";
            InputUsageDescription = "Odebrány háčky a čárky nad písmeny. \n" +
                                    "Ostatní nepísmenné znaky včetně mezer zůstávají, ale jsou šifrovány sami na sebe, ve výstupním textu zůstávají nezměněny";
            KeyUsageDescription = "Převeden na malé znaky anglické abecedy. \n" + 
                                  "Musí obsahovat právě jeden znak.";
            KeyVisibility = "Visible";

            CipherChange();
        }

        public void ToVigenere()
        {
            Caption = "Vigenèrova šifra";
            Description = "Substituční polyalfabetická šifra, která je díky více abecedám o měco bezpečnější a složitější než Cézarova šifra. Je například mnohem více odolnější vůči frekvenční analýze a má větší klíčový prostor. \n" +
                          "Klíčem je libovolně dlouhý textový řetězec, který se opakuje tak dlouho, dokud není tak dlouhý jako otevřený text. Princip šifrování je stejný jako u Cézarovy šifry s tím rozdílem, že nešifrujeme celou zprávu jedním znakem, ale každý znak otevřeného textu odpovídajícím znakem klíče (každé písmeno jinou abecedou) \n" +
                          "Nevýhoda této šifry je opakování klíče. Čím dělší klíč je, tím je šifra bezpečnější.";
            InputUsageDescription = "Odebrány háčky a čárky nad písmeny. \n" +
                                    "Ostatní nepísmenné znaky včetně mezer zůstávají, ale jsou šifrovány sami na sebe, ve výstupním textu zůstávají nezměněny";
            KeyUsageDescription = "Převeden na malé znaky anglické abecedy. \n" +
                                  "Musí obsahovat alespoň jeden znak.";
            KeyVisibility = "Visible";

            CipherChange();
        }

        public void ToVernam()
        {
            Caption = "Vernamova šifra";
            Description = "Vernamova šifra (anglicky One time pad) je bez znalosti klíče nerozluštitelnou šifrou. (Klíčový prostor je tak velmý, že není možné v rozumném čase najít správný klíč) \n" +
                          "Klíčem je náhodně vygenerovaný řetězec, který je stejně dlouhý jako otevřený text. Zaniká tak slabina Vigenèrovy šifry kvůli opakování klíče. Princip šifrování je však stejný. /n" +
                          "Aby byla šifra opravdu neprolomitelná, musí být použitý generátor opravdu reálných čísel, má implementace Vernamovy šifry používá tzv generátor pseudonáhodných čísel. \n" +
                          "U této šifry je při každém šifrování automaticky vygenerován pseudonáhodný klíč, z tohoto důvodu slouží pole k zadávání klíče pro dešifrování. (Při šifrování bude přepsáno.)";
            InputUsageDescription = "Odebrány háčky a čárky nad písmeny. \n" +
                                    "Ostatní nepísmenné znaky včetně mezer zůstávají, ale jsou šifrovány sami na sebe, ve výstupním textu zůstávají nezměněny";
            KeyUsageDescription = "Převeden na malé znaky anglické abecedy. \n" +
                                  "Musí obsahovat stejný počet znaků jako má vstupní text. Pokud necháte pole prázné automaticky se vygeneruje (pseudo) náhodný klíč.";
            KeyVisibility = "Visible";

            CipherChange();
        }

        public void ToScytale()
        {
            Caption = "Skytalé";
            Description = "Jednoduchý historický transpoziční šifrovací princip využívající válce a pásku papíru. Pásek papíru se namotá na válec a na pásek se napíše vzkaz, který je po rozmotání proházený. \n" +
                          "Opět se jedná o velmi primitivní šifru, kterou lze prolomit například namotáním pásku na kužel a zjištěním správného průměru původního válce." +
                          "Klíčem je číslo udávající počet omotání pásku s texten kolem válce. \n" +
                          "U této šifry jsou ze vztupního textu odstraněny mezery z důvodu fungování transpozičních šifer, které pouze prohazují znaky otevřeného textu.";
            InputUsageDescription = "Bez omezení.";
            KeyUsageDescription = "Musí být číslo.";
            KeyVisibility = "Visible";

            CipherChange();
        }

        public void ToMorse()
        {
            Caption = "Morseova abeceda";
            Description = "";
            InputUsageDescription = "Odebrány háčky a čárky nad písmeny. \n" +
                                    "Velká písmena převedena na malá. \n" +
                                    "Kromě znaků anglické abecedy kóduje i tyto znaky: ? , ! . ; / = - ( ) : + _ @";
            KeyUsageDescription = "Klíč se nepoužívá.";
            KeyVisibility = "Hidden";

            CipherChange();
        }
        #endregion

        #region Encryption/Decryption
        /// <summary>
        /// Main encrypt method called everytime something is encrypted (or decrypted)
        /// </summary>
        /// <param name="b">encryption or decryption</param>
        /// <param name="inputText">Input text</param>
        /// <param name="key">Key used to encrypt or decrypt</param>
        public void Encrypt(bool b, string inputText, string key)
        {
            //chooses right cipher to use
            switch (Caption)
            {
                case "Cézarova šifra":
                    Caesar(b, inputText, key);
                    break;
                case "Vigenèrova šifra":
                    Vigenere(b, inputText, key);
                    break;
                case "Vernamova šifra":
                    Vernam(b, inputText, key);
                    break;
                case "Skytalé":
                    Scytale(b, inputText, key);
                    break;
                case "Morseova abeceda":
                    Morse(b, inputText);
                    break;
            }

            ApplyCallChange();
        }

        /// <summary>
        /// Calls encrypt method with parameter to decrypt a cipher
        /// </summary>
        /// <param name="ciphertext">Input cipher text</param>
        /// <param name="key">Key used to decrypt</param>
        public void Decrypt(string ciphertext, string key)
        {
            Encrypt(false, ciphertext, key);
        }

        #region Actual cipher models callings
        void Caesar(bool encrypt, string inputText, string key)
        {
            if (key.Length != 1)
                throw new Exception("Klíč musí obsahovat právě 1 znak.");

            Vigenere(encrypt, inputText, key);
        }

        void Vigenere(bool encrypt, string inputText, string key)
        {
            if (key.Length == 0)
                throw new Exception("Klíč musí obsahovat alespoň 1 znak.");

            inputText = RemoveCzech(inputText);
            CurrentInputText = inputText;

            key = NormaliseKey(key);
            CurrentKey = key;

            if (encrypt)
                Output = VigenereCipher.Encrypt(inputText, key);
            else
                Output = VigenereCipher.Decrypt(inputText, key);
        }

        void Vernam(bool encrypt, string inputText, string key)
        {
            if ((key != string.Empty || !encrypt) && inputText.Length != key.Length)
                throw new Exception("Klíč musí být stejně dlouhý jako vstupní text.");
            else if (encrypt && key == string.Empty)
            {
                Random random = new Random();

                for (int i = 0; i < inputText.Length; i++)
                {
                    key += (char)random.Next('a', 'z' + 1);
                }
            }

            Vigenere(encrypt, inputText, key);
        }

        void Scytale(bool encrypt, string text, string key)
        {
            CurrentInputText = text;

            int turns = 0;
            if (!int.TryParse(key, out turns))
                throw new Exception("Klíč musí být číslo.");

            if (encrypt)
                Output = ScytaleCipher.Encrypt(text, turns);
            else
                Output = ScytaleCipher.Decrypt(text, turns);
        }

        void Morse(bool encrypt, string text)
        {
            text = RemoveCzech(text);
            text = text.ToLower();
            CurrentInputText = text;

            if (encrypt)
                Output = MorseCode.TranslateTo(text);
            else
                Output = MorseCode.TranslateFrom(text);
        }
        #endregion
        #endregion

        #region Input handling by user
        /// <summary>
        /// Removes spaces from the input text
        /// </summary>
        public void RemoveSpaces()
        {
            if (CurrentInputText != null)
            {
                CurrentInputText = CurrentInputText.Replace(" ", string.Empty);
                CallChange("CurrentInputText");
            }
        }

        /// <summary>
        /// Reverse the input text
        /// </summary>
        public void TextReverse()
        {
            if (CurrentInputText != null)
            {
                char[] charArray = CurrentInputText.ToCharArray();
                Array.Reverse(charArray);
                CurrentInputText = new string(charArray);
                CallChange("CurrentInputText");
            }
        }
        #endregion

        void ApplyCallChange()
        {
            CallChange("CurrentInputText");
            CallChange("CurrentKey");
            CallChange("Output");
        }

        /// <summary>
        /// Removes all non-alphabetical characters, Czech characters, spaces from the key and makes the key lowercase
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string NormaliseKey(string key)
        {
            if (key == null)
                return string.Empty;

            key = key.ToLower();
            key = RemoveCzech(key);

            foreach (char c in key)
            {
                if (!((c >= 65 && c <= 90) || (c >= 97 && c <= 122)))
                key = key.Replace(c, ' ');
            }
            key = key.Replace(" ", string.Empty);

            return key;
        }

        /// <summary>
        /// Removes the Czech characters from the text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string RemoveCzech(string text)
        {
            var decomposed = text.Normalize(NormalizationForm.FormD);
            var filtered = decomposed.Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark); //removes czech characters
            text = new String(filtered.ToArray());

            return text;
        }
    }
}
