using System;
using System.Collections.Generic;

namespace Test.File.Format
{

    public class LineData: IComparable<LineData>
    {
        public int CompareTo(LineData p2)
        {

            if (string.IsNullOrEmpty(Value) && !string.IsNullOrEmpty(p2.Value))
                return -1;

            if (!string.IsNullOrEmpty(Value) && string.IsNullOrEmpty(p2.Value))
                return 0;

            if (string.IsNullOrEmpty(Value) && string.IsNullOrEmpty(p2.Value))
                return 1;


            if (Number == p2.Number)
            {
                if (Number > p2.Number)
                    return 1;
                else if (Number < p2.Number)
                    return -1;
                else
                    return 0;
            }

            for (int i = 0; i < Value.Length && i < p2.Value.Length; i++)
            {
                if (Value[i] < p2.Value[i])
                    return -1;
                if (Value[i] > p2.Value[i])
                    return 1;

            }

            if (Value.Length > p2.Value.Length)
                return 1;
            return -1;

        }
        public override string ToString() => Str;
        public int Number;
        public string Value = null;
        public string Str { get =>$"{Number}{Sep}{Value}"; }
        public char Sep { get; private set; }

        public LineData(string str, char sepparator)
        {
            if (string.IsNullOrEmpty(str) || char.IsWhiteSpace(sepparator))
                throw new ArgumentNullException();

            var strs = str.Split(sepparator, 2);
            if (int.TryParse(strs[0], out Number))
            {
                Value = strs[1];
                Sep = sepparator;
            }
            else
                throw new ArgumentException($"Неверный формат строки {str}"); 
        }


    }
 
   


}
