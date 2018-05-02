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
        | 1 | 2 | 3 | 4 | 5 | 
        | 6 | 7 | 8 | 9 |10 | 
        |11 |12 |13 |14 |15 |
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

            for (int i = 0; i < turns; i++) //rows iteration
            {
                for (int j = 0; j < Math.Ceiling(plainText.Length / (double)turns); j++) //columns iteration
                {
                    if ((i + turns * j) >= plainText.Length) //prevents OutOfrange Exception
                        continue;

                    cipherText += plainText[i + turns * j];
                }
            }
            return cipherText;
        }

        /// <summary>
        /// Scytale decryption
        /// </summary>
        /// <param name="cipherText">Text to decipher</param>
        /// <param name="turns">Number of turns of the band</param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, int turns)
        {
            turns = cipherText.Length / turns; //switches sides of the table / rotate the table

            return Encrypt(cipherText, turns);
        }
    }
}
