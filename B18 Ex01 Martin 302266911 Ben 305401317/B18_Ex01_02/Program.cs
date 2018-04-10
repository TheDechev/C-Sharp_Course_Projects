using System;
using System.Text;

namespace B18_Ex01_02
{
    public class Program
    {
        public static void Main()
        {
            StringBuilder sandClockToPrint = new StringBuilder();
            Console.WriteLine("Begginers Sandclock:\n");
            CreateSandClock(ref sandClockToPrint, 5);
            Console.WriteLine(sandClockToPrint);
        }

        public static void CreateSandClock(ref StringBuilder io_TriangleToPrint, int i_SizeToPrint)
        {
            int previousStringLength = 0;
            StringBuilder singleLineOfStars = new StringBuilder();
            string myStr = new string('*', i_SizeToPrint);

            singleLineOfStars.AppendLine(myStr);
            io_TriangleToPrint.AppendLine(myStr);
            io_TriangleToPrint.AppendLine(myStr);

            for (int i = 1; i <= i_SizeToPrint / 2; i++)
            {
                previousStringLength += singleLineOfStars.Length;
                singleLineOfStars.Remove(0, i + 1);
                singleLineOfStars.Insert(0, " ", i);
                io_TriangleToPrint.Insert(previousStringLength, singleLineOfStars);

                // won't print one too many * in the middle of the triangle
                if (i != i_SizeToPrint / 2) 
                {
                    io_TriangleToPrint.Insert(previousStringLength, singleLineOfStars);
                } 
                else if (i_SizeToPrint % 2 == 0)
                {
                    io_TriangleToPrint.Remove(previousStringLength + i - 1, i + 2);
                }
            }
        }
    }
}
