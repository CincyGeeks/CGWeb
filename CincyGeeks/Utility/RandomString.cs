using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Utility
{
    public class RandomString
    {
        private char[] _randomValue;
        private Random _rand;

        public RandomString(int length)
        {
            _randomValue = new char[length];
            _rand = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < length; i++)
            {
                _randomValue[i] = Convert.ToChar(_rand.Next(33, 127));
            }
        }

        public override string ToString()
        {
            return new string(_randomValue);
        }
    }
}