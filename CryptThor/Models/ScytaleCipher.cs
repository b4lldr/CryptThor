using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptThor.Models
{
    public static class ScytaleCipher
    {
        /*
        | 0 | 1 | 2 | 3 | 4 | 
        | 5 | 6 | 7 | 8 | 9 | 
        |10 |11 |12 |13 |14 |
        */

        /// <summary>
        /// Scytale encryption
        /// </summary>
        /// <param name="plainText">Text to encipher</param>
        /// <param name="turns">Number of turns of the band</param>
        /// <returns></returns>
        public static string Encrypt(string plainText, int turns)
        {
            
            string cipherText = "";
            string noSpaces = plainText.Replace(" ", string.Empty);

            for (int i = 0; i < turns; i++) //collumns
            {
                for (int j = i; j < noSpaces.Length; j += turns) //rows in current column
                {
                    cipherText += noSpaces[j];
                }
            }

            return ReturnSpaces(plainText, cipherText); //calls a private function which adds the spaces back
        }

        /// <summary>
        /// Scytale decryption
        /// </summary>
        /// <param name="cipherText">Text to decipher</param>
        /// <param name="turns">Number of turns of the band</param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, int turns) 
        {
            //prepares the cipher to be decrypted using the original encryption function above
            string noSpace = cipherText.Replace(" ", string.Empty);
            int turnsNew = (int)Math.Ceiling(noSpace.Length / (double)turns); //used to switch the sides of the table 

            int modulo = noSpace.Length % turns; //used to determinate where are the table rows full and where are not
            for (int i = (modulo +1) * turnsNew - 1; i <= noSpace.Length; i += turnsNew) //fills empty spaces in the table with € sign to be able to corretly decrypt the cipher
            {
                noSpace = noSpace.Insert(i, "€");
            }

            //actual decryption
            string plainText = Encrypt(noSpace, turnsNew);
            plainText = plainText.Replace("€", string.Empty);

            return ReturnSpaces(cipherText, plainText);
        }

        /// <summary>
        /// Returns all the spaces from the original text to the new one
        /// </summary>
        /// <param name="withSpaces">Original text with spaces</param>
        /// <param name="withoutSpaces">Text without spaces used to perform encryption/decryption</param>
        /// <returns></returns>
        private static string ReturnSpaces(string withSpaces, string withoutSpaces)
        {
            for (int i = 0; i < withoutSpaces.Length; i++)
            {
                if (char.IsWhiteSpace(withSpaces[i]))
                {
                    withoutSpaces = withoutSpaces.Insert(i, " ");
                }
            }
            return withoutSpaces;
        }
    }
}
