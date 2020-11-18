using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Login_Hash
{
    class HashManager
    {
        /// <summary>
        /// Generates the hash with the salt
        /// </summary>
        /// <param name="toBeHashed"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public byte[] GenerateSHA256(byte[] toBeHashed, byte[] salt)
        {
            return SHA256.Create().ComputeHash(Combine(toBeHashed, salt));
        }

        /// <summary>
        /// Generates the salt with a length of 32
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateSalt()
        {
            const int saltLength = 32;

            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[saltLength];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }
        /// <summary>
        /// Combine two byte[]
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }
    }
}
