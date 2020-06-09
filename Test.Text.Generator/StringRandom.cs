using System;
using System.Linq;

namespace Test.Text.Generator
{

    public class StringRandom
    {

       private char _sepparator = default;
        public StringRandom(char sepparator ='.')
        {
            _sepparator = sepparator;
        }

        private Random random = new Random();

        public string GetRandomString() =>
             $"{random.Next(1, 1000000)}{_sepparator} {Guid.NewGuid()}-{Guid.NewGuid()}-{Guid.NewGuid()}\r\n";
     
   
    }

}
