using System;
using System.Text;

namespace B18_Ex01_03
{
    public class Program
    {
        public static void Main()
        {
            int sizeToPrint;
            StringBuilder triangleToPrint = new StringBuilder();

            sizeToPrint = int.Parse(Console.ReadLine());
            B18_Ex01_02.Program.CreateSandClock(ref triangleToPrint, sizeToPrint);

            Console.WriteLine(triangleToPrint);
        }
    }
}
