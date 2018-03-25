using System;
using System.Text;

namespace B18_Ex01_03
{
    class Program
    {
        private const string k_Space = " ";

        public static void Main()
        {
            int sizeToPrint;
            StringBuilder triangleToPrint = new StringBuilder();

            sizeToPrint = int.Parse(Console.ReadLine());
            createTriangle(ref triangleToPrint, sizeToPrint);

            Console.WriteLine(triangleToPrint);

        }

        private static void createTriangle(ref StringBuilder io_triangleToPrint, int i_sizeToPrint)
        {
            int previousStringLength = 0;
            StringBuilder singleLineOfStars = new StringBuilder();

            string myStr = new string('*', i_sizeToPrint);

            singleLineOfStars.AppendLine(myStr);
            io_triangleToPrint.AppendLine(myStr);
            io_triangleToPrint.AppendLine(myStr);

            for (int i = 1; i <= i_sizeToPrint / 2; i++)
            {

                previousStringLength += singleLineOfStars.Length;
                singleLineOfStars.Remove(0, i + 1);
                singleLineOfStars.Insert(0, k_Space, i);

                io_triangleToPrint.Insert(previousStringLength, singleLineOfStars);

                if (i != i_sizeToPrint / 2) //won't print one too many * in the middle of the triangle
                {
                    io_triangleToPrint.Insert(previousStringLength, singleLineOfStars);
                }
                else if (i_sizeToPrint % 2 == 0) // an even number
                {
                    io_triangleToPrint.Remove(previousStringLength + i - 1, i + 2);
                }

            }
        }

    }
}
