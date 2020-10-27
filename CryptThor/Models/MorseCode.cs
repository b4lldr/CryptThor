using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptThor.Models
{
    public static class MorseCode
    {
        //dictionary with letters numbers and several other characters and their Morse code representation
        static Dictionary<char, string> morseAlphabetDictionary = new Dictionary<char, string>()
        {
            {'a', ".-"}, {'b', "-..."}, {'c', "-.-."}, {'d', "-.."}, {'e', "."}, {'f', "..-."}, {'g', "--."},
            {'h', "...."}, {'i', ".."}, {'j', ".---"}, {'k', "-.-"}, {'l', ".-.."}, {'m', "--"}, {'n', "-."},
            {'o', "---"}, {'p', ".--."}, {'q', "--.-"}, {'r', ".-."}, {'s', "..."}, {'t', "-"}, {'u', "..-"},
            {'v', "...-"}, {'w', ".--"}, {'x', "-..-"}, {'y', "-.--"}, {'z', "--.."},

            {'0', "-----"}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"}, {'5', "....."},
            {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."},

            {'?', "..--.."}, {',', "--..--"}, {'!', "--...-"}, {'.', ".-.-.-"}, {';', "-.-.-."}, {'/', "-..-."},
            {'=', "-...-"}, {'-', "-....-"}, {'(', "-.--."}, {')', "-.--.-"}, {':', "---..."}, {'+', ".-.-."},
            {'_', "..--.-"}, {'@', ".--.-."}
        };

        /// <summary>
        /// Function which translates plain text into Morse code
        /// </summary>
        /// <param name="plainText">Text to translate</param>
        /// <returns></returns>
        public static string TranslateTo(string plainText)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char character in plainText)
            {
                if (morseAlphabetDictionary.ContainsKey(character))
                {
                    stringBuilder.Append(morseAlphabetDictionary[character] + " ");
                }
                else if (character == ' ')
                {
                    stringBuilder.Append("|");
                }
                else
                {
                    stringBuilder.Append("# ");
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Function which translates Morse code into plain text
        /// </summary>
        /// <param name="morseCodeText">Text to translate</param>
        /// <returns></returns>
        public static string TranslateFrom(string morseCodeText)
        {
            StringBuilder stringBuilder = new StringBuilder();

            string[] morseCharacters = morseCodeText.Split(' ', '|');
            foreach (string morseChar in morseCharacters)
            {
                char newChar = ' ';

                if (morseChar == "#")
                {
                    stringBuilder.Append(morseChar);
                    continue;
                }

                foreach (KeyValuePair<char, string> pair in morseAlphabetDictionary)
                {
                    if (morseChar == pair.Value)
                    {
                        newChar = pair.Key;
                        break;
                    }
                }

                stringBuilder.Append(newChar);
            }

            return stringBuilder.ToString();
        }
    }
}
