using System;
using System.Text;


namespace Labs3Xor
{
    class Program
    {
        static void Main(string[] args)
        {
            string InpStr = Console.ReadLine();
            string Encrypted = Encrypt(InpStr);

            Console.WriteLine("\nЗашифрованная строка: "+Encrypted);
            Console.WriteLine("Расшифрованная строка:\n"+Encrypt(Encrypted, true));
            Console.ReadKey();
        }

        static string Encrypt(string InpStr, bool Decrypt = false)
        {
            string Res = "";
            // Смена кодировки
            //byte[] bInpStr = Encoding.Unicode.GetBytes(InpStr);
            byte[] bInpStr = Encoding.GetEncoding(1251).GetBytes(InpStr);
            //bInpStr = Encoding.Convert(Encoding.Unicode, Encoding.GetEncoding(1251), bInpStr);
            // Наш датчик ПСЧ
            PsRandom psRand = new PsRandom();
            
            if (Decrypt == false)
            {
                // Складываем с гаммой
                Console.WriteLine();
                Console.WriteLine("Гамма: ");
                for (int i = 0; i < bInpStr.Length; i++)
                {
                    int iRand = psRand.Next();
                    bInpStr[i] = (byte)((bInpStr[i] + iRand) %256);
                    Console.Write(iRand.ToString() + ", ");
                    //if (i % 16 == 0 && i != 0) Console.WriteLine();
                }
            }
            ///*
            else
                // Вычитаем гамму
                for (int i = 0; i < bInpStr.Length; i++)
                {
                    int iRand = psRand.Next();
                    bInpStr[i] = (byte)((bInpStr[i] - iRand));

                }
                //*/
            Res = Encoding.GetEncoding(1251).GetString(bInpStr);
            return Res;
        }
    }

    class PsRandom
    {
        int CurrentVal;
        public PsRandom(int Seed)
        {
            CurrentVal = Seed;
        }
        public PsRandom()
        {
            // T(0)=41
            CurrentVal = 41;
        }
        public int Next()
        {
            int OldVal = CurrentVal;
            // T(i+1)=(A*T(i)+C) mod B
            // A=17, C=39, B=256
            CurrentVal = (17 * CurrentVal + 39) % 256;
            return OldVal;
        }
    }
}
