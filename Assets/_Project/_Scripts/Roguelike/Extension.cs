using System;
using System.Collections.Generic;

namespace SnekTech.Roguelike
{
    public static class Extension
    {
        public static T GetRandom<T>(this List<T> list, IRandomGenerator randomGenerator = null)
        {
            randomGenerator ??= RandomGenerator.Instance;
            
            int listCount = list.Count;
            if (listCount == 0)
            {
                throw new ArgumentException("cannot get a random element from an empty list");
            }
            
            return list[randomGenerator.Next(listCount)];
        }

        /// <summary>
        /// Get a relative short <see cref="string"/> representation from a <see cref="Guid"/> instance.
        /// </summary>
        /// <remarks>
        /// Convert the 128 bits of a <see cref="Guid"/> to a base64 string format.
        /// </remarks>
        /// <param name="guid">caller <see cref="Guid"/> instance</param>
        /// <returns>a base64 string with the trailing "==" characters removed</returns>
        public static string ToShortString(this Guid guid)
        {
            string base64Guid = Convert.ToBase64String(guid.ToByteArray());
            
            // remove trailing ==
            return base64Guid[..^2];
        }
    }
}