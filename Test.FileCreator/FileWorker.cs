using System;
using System.IO;
using System.Linq;
using Test.Text.Generator;

namespace Test.FileCreator
{
    public class FileWorker
    {

        public static void Create(string fileName, int limitMb)
        {

            File.Delete(fileName);

            Int64 limit = limitMb * (Int64)Math.Pow(1024, 2);
            
            var sizeo = sizeof(byte);

            StringRandom randomLineData = new StringRandom('.');

            using (var file = File.CreateText(fileName))
            {

                while (0 < limit)
                {
                    var d = randomLineData.GetRandomString();
                    file.Write(d);
                    limit -= d.Length * sizeo;
                }
                ///повторяющиеся значения
                var id = Guid.NewGuid().ToString();
                var dublicateLine = $"1254. {id}\r\n";
                file.Write(dublicateLine);
                file.Write($"125. {id}\r\n");
                file.Write($"135. {id}\r\n");
                file.Write($"1254. {new string(id.Take(14).ToArray())}\r\n");
                file.Write($"1254. {new string(id.Take(14).ToArray())}\r\n");
                file.Write($"1254. {new string(id.Take(18).ToArray())}\r\n");
                file.Write($"254. {new string(id.Take(13).ToArray())}\r\n");

            }


        }


    }
}
