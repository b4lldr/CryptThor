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

        public string InputText { get; set; }
        public string Key { get; set; }
        public string Output { get; private set; }

        public string KeyVisibility { get; set; } 

        public string DescriptionInputTextUsage { get; private set; }
        public string DescriptionKeyUsage { get; private set; }
        public string DescriptionPrinciple { get; private set; }
        public string DescriptionHistory { get; private set; }

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
        #region Cipher change methods and Descriptions
        void CipherChange()
        {
            CallChange("Caption"); 
            CallChange("KeyVisibility");
            CallChange("DescriptionInputTextUsage");
            CallChange("DescriptionKeyUsage");
            CallChange("DescriptionPrinciple");
            CallChange("DescriptionHistory");
        }

        public void ToCaesar()
        {
            Caption = "Caesarova šifra";
            KeyVisibility = "Visible";
            DescriptionInputTextUsage =
                "Odebrány háčky a čárky nad písmeny. \n" +
                "Ostatní nepísmenné znaky včetně mezer zůstávají, ale jsou šifrovány sami na sebe, ve výstupním textu zůstávají nezměněny";
            DescriptionKeyUsage =
                "Převeden na malé znaky anglické abecedy. \n" +
                "Musí obsahovat právě jeden znak.";
            DescriptionPrinciple =
                "Velmi jednoduchá symetrická substituční monoalfabetická šifra. \n \n" +
                "Klíčem je 1 písmeno anglické abecedy, které udává posun (a = 0, b = 2, ..., z = 25) jednotlivých znaků otevřeného textu v abecedě. Písmeno \"a\" nijak nezmění podobu otevřeného textu. \n" +
                "Při \"přetečení\" přes konec abecedy se přeskočí opět na začátek a opačně. \n \n" +
                "Dešifrování probíhá stejně jako šifrování, pouze s opačnou hodnotou klíče. \n \n" +
                "Speciální varianta Caesarovy šifry je tzv. ROT 13, která je speciální v tom, že šifrování i dešifrování nám dá vždy stejný výsledek. Jedná se o posun o 13 znaků (písmeno n). \n \n" +
                "Každé písmeno otevřeného textu se šifruje stejnou abecedou (tím samým písmenem), což dělá Caesarovu šifru velmi zranitelnou vůči medodě zvané frekvenční analýza. Je to dáno tím, že se jeden znak otevřeného textu šifruje vždy na jeden jiný znak šifrového textu a tím zůstává frekvence těchto znaků stejná. \n" +
                "Kromě frekvenční analýzy je možné šifru prolomit prohledáním celého klíčového prostoru - prostým vyzkoušením všech 25 kombinací. Tomuto typu útoku se říká útok hrubou silou. \n" +
                "Z těchto důvodů se jedná o velmi slabou šifru." ;
            DescriptionHistory =
                "Jak už název napovídá, tato šifra byla poprvé použita římským vojevůdcem Gaiem Juliem Caesarem (1. st. př. n. l.), který ji používal při korespondenci s Kleopatrou a popsal ji ve své knize Zápisky o válce galské. \n" +
                "Caesar s úspěchem používal výhradně posun o tři pozice (písmeno d), ale označení Caesarova šifra se používá pro libovolný posun. \n";

            CipherChange();
        }

        public void ToVigenere()
        {
            Caption = "Vigenèrova šifra";
            KeyVisibility = "Visible";
            DescriptionInputTextUsage =
                "Odebrány háčky a čárky nad písmeny. \n" +
                "Ostatní nepísmenné znaky včetně mezer zůstávají, ale jsou šifrovány sami na sebe, ve výstupním textu zůstávají nezměněny";
            DescriptionKeyUsage =
                "Převeden na malé znaky anglické abecedy. \n" +
                "Musí obsahovat alespoň jeden znak.";
            DescriptionPrinciple =
                "Symetrická substituční polyalfabetická šifra. \n \n" +
                "Klíčem je libovolně dlouhý řetězec znaků anglické abecedy, který se opakuje tak dlouho, dokud není stejně dlouhý jako otevřený text. \n \n" +
                "Princip šifrování je stejný jako u Caesarovy šifry s tím rozdílem, že nešifrujeme celou zprávu jedním znakem, ale každý znak otevřeného textu odpovídajícím znakem klíče (každé písmeno šifrováno jinou abecedou). \n" +
                "Můžeme tedy říci, že každý znak otevřeného textu šifrujeme jinou Caesarovou šifrou (jejich počet odpovídá délce klíče). \n \n" +
                "Při použití klíče, který je tvořen jediným znakem, zredukujeme Vigenèrovu šifru na šifru Caesarovu - Caesarova šifra je pouze speciální případ Vigenèrovy šifry. \n \n" +
                "Vzhledem k tomu, že klíčový prostor této šifry roste exponenciálně s délkou klíče (26^n, kde n je délka klíče), je šifra mnohem odolnější vůči útoku hrubou silou než Caesarova šifra. \n" +
                "Klasickou frekvenční analýzu na Vigenèrovu šifru nelze použít vůbec, protože je to polyalfabetická šifra a jeden znak otevřeného textu se tak nutně nemusí šifrovat na jeden jiný znak šifrového textu. Existuje ale metoda, kterou lze pomocí analýzy frekvence znaků prolomit i tuto šifru, ačkoliv se jedná o složitější proces a jeho úspěšnost závisí na poměru délky klíče a délky zprávy - čím delší klíč a kratší zpráva, tím obtížnější je šifru prolomit, protože se klíč nemusí tolikrát opakovat.";
            DescriptionHistory =
                "S rostoucí znalostí frekvenční analýzy už monoalfabetické šifry začínaly být nedostačující a bylo tak potřeba vymyslet způsob šifrování, který by byl vůči frekvenční analýze imunní. \n \n" +
                "Vigenèrova šifra vychází z původního návrhu Leona Battisty Albertiho z 15. století, který následně upravil Giovan Battista Bellaso roku 1553 ve své knize. \n" +
                "Svůj název však tako šifra nese po francouzi Blaisi de Vigenèrovi, který zveřejnil její finální podobu roku 1586. Nesprávně mu byla připsána až v 19. století. \n \n" +
                "Tato polyalfabetická šifra byla dlouho považována za nerozluštitelnou. První, komu se podařilo ji rozluštit, byl britský matematik Charles Babbage a to až roku 1854.";

            CipherChange();
        }

        public void ToVernam()
        {
            Caption = "Vernamova šifra";
            KeyVisibility = "Visible";
            DescriptionInputTextUsage =
                "Odebrány háčky a čárky nad písmeny. \n" +
                "Ostatní nepísmenné znaky včetně mezer zůstávají, ale jsou šifrovány sami na sebe, ve výstupním textu zůstávají nezměněny";
            DescriptionKeyUsage =
                "Převeden na malé znaky anglické abecedy. \n" +
                "Musí obsahovat stejný počet znaků jako má vstupní text. Pokud při šifrování necháte pole prázné, automaticky se vygeneruje (pseudo) náhodný klíč.";
            DescriptionPrinciple =
                "Vernamova šifra (anglicky One-time pad) funguje na úplně stejném principu jako Vigenèrova (případně Caesarova) šifra. \n \n" +
                "Aby se ale jednalo skutečně o Vernamovu šifru, musí být splněny následující podmínky: \n" +
                "Zaprvé, klíčem je náhodně (ideálně skutečně náhodně, ne pseudonáhodně) vygenerovaný řetězec, který je stejně dlouhý jako otevřený text. \n" +
                "Zadruhé, pro každé nové šifrování je vygenerován nový náhodný klíč (odtud název One-time pad). \n \n" +
                "Za těchto podmínek neuniká při šifrování žádná informace, protože četnost výskytu znaků v zašifrované zprávě je rovnoměrná, a jedná se tak bez znalosti klíče o nerozluštitelnou šifru - klíčový prostor je tak velký, že není možné útokem hrubou silou v rozumném čase najít správný klíč. \n" +
                "Znamená to, že zaniká slabina Vigenèrovy šifry, která se dala rozluštit pomocí opakování se klíče, případně pomocí jeho nenáhodnosti. \n \n" +
                "Ačkoliv se jedná o nerozluštitelnou šifru, v praxi se v podstatě nepoužívá, například z dúvodu přílišné náročnosti generování klíče a jeho velikosti (velikost klíče je stejná jako velikost šifrovaných dat).";
            DescriptionHistory =
                "Tento šifrovací systém byl roku 1917 patentovám zaměstnancem firmy AT&T Gilbertem Vernamem. \n \n" +
                "Během studené války byla Vernamovou šifrou zabezpečena horká linka mezi Washingtonem a Moskvou. \n \n" +
                "Existuje několik historických textů zašifrovaných touto šifrou, které se však z důvodu její neprolomitelnosti nikdy nepovede rozluštit.";

            CipherChange();
        }

        public void ToScytale()
        {
            Caption = "Skytalé";
            KeyVisibility = "Visible";
            DescriptionInputTextUsage = "Bez omezení.";
            DescriptionKeyUsage = "Musí být číslo.";
            DescriptionPrinciple =
                "Jednoduchý historický transpoziční šifrovací systém. \n \n" +
                "Princip funguje na využití válce a pásku papíru. \n" +
                "Pásek se nejprve obnotá kolem válce tak, aby se nepřekrýval. \n" +
                "Poté se na vzniklou plochu na pásce napíše ve směru osy válce, řádek po řádce, vzkaz. \n" +
                "Po rozmotání pásku nám zůstanou znaky původní zprávy proházené tak, že nedávají smysl. \n \n" +
                "K přečtení zprávy stačí opět pásek namotat na válec o stejném průměru. \n \n" +
                "Klíčem je číslo udávající počet omotání pásku se zprávou kolem válce. \n" +
                "Pokud se klíč rovná 1 nebo je větší než je počet znaků ve zprávě, výsledná zpráva se nezmění od té původní. \n \n" +
                "Skytalé lze velice jednoduše prolomit namotáním pásku na kužel a posouváním jím, dokud nenajdeme smysluplné slovo, čímž zároveň získáme i původní průměr válce.";
            DescriptionHistory =
                "První zmínky o Skytalé pocházejí již ze 7. stolení př. n. l., avšak popis toho, jak šifra funguje napsal až řecký filozof a historik Plútarchos.";

            CipherChange();
        }

        public void ToMorse()
        {
            Caption = "Morseova abeceda";
            KeyVisibility = "Hidden";
            DescriptionInputTextUsage =
                "Odebrány háčky a čárky nad písmeny. \n" +
                "Velká písmena převedena na malá. \n" +
                "Kromě znaků anglické abecedy kóduje i tyto znaky: ? , ! . ; / = - ( ) : + _ @";
            DescriptionKeyUsage = "Klíč se nepoužívá.";
            DescriptionPrinciple =
                "Nejedná se ve skutečnosti o šifru, pouze o zůsob kódování. Zakódovaná zpráva se pak dále může šifrovat. \n \n" +
                "Kódování probíhá tak, že se jednotlivé znaky abecedy, číslice a další speciální znaky změní na definovanou kombinaci teček a čárek (každý znak má svou vlastní kombinaci) oddělených dělícím znakem - typicky mezera, slova jsou pak oddělena lomítkem nebo svislou čarou. \n \n" +
                "Kombinace teček a čátek pro jednotlivé znaky nejsou náhodné, ale vycházejí z četností výskytů znaků v angličtině - nejfrekventovanější znaky mají nejkratší sekvence. \n \n" +
                "Takto zakódované znaky se dají mnohém lépe přenášet pomocí signálů než znaky původní. \n" + 
                "Díky svému binárnímu charakteru lze k přenosu Morseovy abecedy lehce použít zvukový, elektrický (telegraf) nebo třeba optický (záznam na papír) signál. ";
            DescriptionHistory =
                "První způsob takového kódování podobnému dnešní Morseově abecedě vymyslel asistent amerického vynálezce Alfred Vail, který zároveň uskutečnil roku 1844 první telegrafické spojení a to mezi Washingtonem a Baltimorem. \n \n" +
                "Po dlouhém vývoji a četných úpravách byly roku 1918 různé verze kódování sjednoceny pod názven International Code. \n \n" +
                "V minulosti byla hojně využívaná v telegrafii, kterou však v dnešní době nahradily jiné systémy včetně internetu. \n \n" +
                "Různé znaky se do Morseovy abecedy přidávali postupně. \n" +
                "Posledním přidaným znakem byl v roce 2003 zavináč. Byl to první nově přidaný znak od druhé světové války.";

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
                case "Caesarova šifra":
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
            InputText = inputText;

            key = NormaliseKey(key);
            Key = key;

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
            InputText = text;

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
            InputText = text;

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
            if (InputText != null)
            {
                InputText = InputText.Replace(" ", string.Empty);
                CallChange("InputText");
            }
        }

        /// <summary>
        /// Reverse the input text
        /// </summary>
        public void TextReverse()
        {
            if (InputText != null)
            {
                char[] charArray = InputText.ToCharArray();
                Array.Reverse(charArray);
                InputText = new string(charArray);
                CallChange("InputText");
            }
        }
        #endregion

        void ApplyCallChange()
        {
            CallChange("InputText");
            CallChange("Key");
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
