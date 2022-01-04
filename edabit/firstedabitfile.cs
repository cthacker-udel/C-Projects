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

        static Func<String, Int32> CountDs = (String theString) => {
            String[] res = theString.Split(String.Empty)//.Where(eachLetter => eachLetter.ToLower().StartsWith("d")).Count();
            for (int i = 0; i < res.Length; i++) {
                Console.WriteLine(res[i]);
            }
            return -1;
        };

        public static void Main(string[] args)
        {
            Console.WriteLine(CountDs("My friend Dylan got distracted in school."));
        }
    }

}