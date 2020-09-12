using System;
using System.Collections.Generic;
using System.Text;

namespace RubiconTest.Infrastructure.Shared
{
    /// <summary>
    /// Class that holds several functions used in the proccess of data Generation
    /// </summary>
    public class DataGeneration
    {

        public Random r { get; set; }

        public DataGeneration()
        {
            r = new Random();
        }
        /// <summary>
        /// Used in case we want to generate a string using GenerateString  but with a random lenght factor
        /// </summary>
        /// <param name="min">minimum string lenght</param>
        /// <param name="max">maximum string lenghts</param>
        /// <returns></returns>
        public string GenerateStringRandomLenght(int min, int max)
        {

            if (max < 1 || min < 1)
                throw new ArgumentException("Params cant be smaller than 1");

            if (max < min)
            {
                var v = max;
                max = min;
                min = v;
            }

            if (max == min)
                throw new ArgumentException("Parameters cant be the same value");

            return GenerateString(r.Next(min, max));
        }

        /// <summary>
        /// Generates a string combined from 1 upercase letter at the start and the rest with small letters
        /// </summary>
        /// <param name="len"> represents the overall lenghts of the string</param>
        /// <returns></returns>
        public string GenerateString(int len)
        {
            if (len < 2)
                throw new ArgumentException("Lenghts must be more than 1 character!");

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[len];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[r.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;

        }
        /// <summary>
        /// Generate a random positive integer used ofr random acess of list
        /// </summary>
        /// <param name="count"> the maximum number and the lenght of the list </param>
        /// <returns></returns>
        public int RandomPositiveNumberInRange(int count)
        {
            return r.Next(0, count);
        }

        /// <summary>
        /// Generates a random integer number in between the set range 
        /// </summary>
        /// <param name="min">minimum value for generation</param>
        /// <param name="max">minimum value for generation</param>
        /// <returns></returns>
        public int GenerateNumber(int min, int max)
        {
            if (max < min)
            {
                var v = max;
                max = min;
                min = v;
            }

            if (max == min)
                throw new ArgumentException("Parameters cant be the same value");


            int number = r.Next(min, max);
            return number;

        }
        /// <summary>
        /// Generates a random boolean
        /// </summary>
        /// <returns></returns>
        public bool RandomBit()
        {
            return r.Next() > (Int32.MaxValue / 2);
        }

        /// <summary>
        /// Generates a random date in between today and  5 years ago
        /// </summary>
        /// <returns></returns>
        public DateTime RandomDate()
        {
            DateTime start = new DateTime(DateTime.UtcNow.Year - 5, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(r.Next(range));

        }



    }
}
