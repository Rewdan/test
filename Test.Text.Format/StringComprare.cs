using System;
using System.Collections.Generic;

namespace Test.Text.Format
{
    public class StringComparer : IComparer<string>
    {
        public int Compare(string p1, string p2)
        {
            bool isNullp1 = string.IsNullOrEmpty(p1);
            bool isNullp2 = string.IsNullOrEmpty(p2);

            if (isNullp1 && !isNullp2)
                return -1;

            if (!isNullp1 && isNullp2)
                return 0;

            if (isNullp1 && isNullp2)
                return 1;

            var chars1 = p1.Split('.');
            var chars2 = p2.Split('.');

            var number1 = int.Parse(chars1[0]);
            var number2 = int.Parse(chars2[0]);

            if (chars1[1] == chars2[1])
            {
                if (number1 > number2)
                    return 1;
                else if (number1 < number2)
                    return -1;
                else
                    return 0;
            }

            for (int i = 0; i < chars1[1].Length && i < chars2[1].Length; i++)
            {
                if (chars1[1][i] < chars2[1][i])
                    return -1;
                if (chars1[1][i] > chars2[1][i])
                    return 1;

            }

            if (chars1[1].Length > chars2[1].Length)
                return 1;
            return -1;

        }
    }
}
