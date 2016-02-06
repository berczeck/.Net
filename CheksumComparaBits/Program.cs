using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CheksumComparaBits
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //F:\Software\android-sdk-windows.rar
            //F:\Software\Photoshop.rar
            //D:\Alex\Programas\smtp4dev-2.0.9-binaries.zip

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            byte[] checkSum = GenerarCheckSum(@"F:\Software\Photoshop.rar");
            stopWatch.Stop();
            byte[] checkSum2 = GenerarCheckSum(@"F:\Software\Photoshop.rar");
            string cadenaCheckSum = ConvertByteArrayToString(checkSum);
            string cadenaCheckSum2 = ConvertByteArrayToString(checkSum2);

            int intCheckSum = BitConverter.ToInt32(checkSum, 0);
            int intCheckSum2 = BitConverter.ToInt32(checkSum2, 0);

            byte[] checkSumFromInt = BitConverter.GetBytes(intCheckSum);
            byte[] checkSumFromInt2 = BitConverter.GetBytes(intCheckSum2);

            byte[] checkSumFromString = ConvertStringToByte(cadenaCheckSum);

            string generacion3 = Convert.ToBase64String(checkSum);
            byte[] generacion4 = Convert.FromBase64String(generacion3);

            Console.WriteLine("CheckSum byte[]: {0}", checkSum);

            Console.WriteLine("CheckSum string: {0}", cadenaCheckSum);
            Console.WriteLine("CheckSum string: {0}", cadenaCheckSum2);

            Console.WriteLine("int == int: {0}", intCheckSum.Equals(intCheckSum2));

            Console.WriteLine("byte[] == byte[]: {0}", checkSum.SequenceEqual(checkSum2));

            Console.WriteLine("string == string: {0}", cadenaCheckSum.Equals(cadenaCheckSum2));

            Console.WriteLine("byte[] == byte[]: {0} - From Int", checkSumFromInt.SequenceEqual(checkSumFromInt2));

            Console.WriteLine("byte[] == byte[]: {0}", checkSum.SequenceEqual(checkSumFromString));

            Console.WriteLine("byte[] == byte[]: {0}", checkSum.SequenceEqual(generacion4));

            Console.WriteLine("Tiempo: {0}", stopWatch.Elapsed);

            Console.ReadLine();
        }

        private static byte[] GenerarCheckSum(string filename)
        {
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }

        private static string ConvertByteArrayToString(Byte[] ByteOutput)
        {
            string StringOutput = Encoding.ASCII.GetString(ByteOutput);
            return StringOutput;
        }

        public static byte[] ConvertStringToByte(string Input)
        {
            return Encoding.UTF8.GetBytes(Input);
        }
    }
}