using System;
using System.Text;

namespace B18_Ex01_02
{
    class Program
    {

        public static void Main()
        {

           string charsToPrint= String.Format(
@"  *****
   ***  
    *  
   *** 
  *****");
            Console.WriteLine(charsToPrint);
            StringBuilder myStr = new StringBuilder("Hello");
            
        }
            
    }
}
