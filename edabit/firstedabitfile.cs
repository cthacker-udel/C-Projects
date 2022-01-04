using System;
using System.Linq;
using System.Runtime;
namespace Edabit {


    class edabitStorage {

        public static int Sum(int num1, int num2) {
            return num1 + num2;
        }

        public static double[] FindMinMax(double[] numbers) {

            return new double[]{numbers.Min(), numbers.Max()};        

        }

        public static Func<String, Int32> CountDs = (String theString) => {
            char[] res = theString.ToCharArray();
            return res.Where(e => e.ToString().ToLower().Equals("d")).Count();
        };

        

        public static void Main(string[] args)
        {
            Console.WriteLine(CountDs("My friend Dylan got distracted in school."));
        }
    }

}