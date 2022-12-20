using System;
using System.Collections.Generic;

namespace SnekTech.Roguelike
{
    public static class Extension
    {
        public static T GetRandom<T>(this List<T> list, Random random = null)
        {
            // todo: use global random seed
            random ??= new Random();

            int randomIndex = random.Next() % list.Count;
            return list[randomIndex];
        }
    }
}