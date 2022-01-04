using System;
using System.Linq;
public static class Edabit {


    public static int Sum(int num1, int num2) {
        return num1 + num2;
    }

    public static double[] FindMinMax(double[] numbers) {

        return new double[]{numbers.Min(), numbers.Max()};        

    }

    public static int CountDs = (String theString) => {
        return theString.Split(String.Empty).Where(eachLetter => eachLetter.ToLower().StartsWith("d")).Count();
    };

    public static void Main(string[] args)
    {
        Console.WriteLine(CountDs("My friend Dylan got distracted in school."));
    }

}