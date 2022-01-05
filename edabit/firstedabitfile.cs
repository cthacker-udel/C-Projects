using System;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Globalization;
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

        public static Func<String,String, Int32> HammingDistance = (String str1, String str2) => {

            int index = 0;
            return str1.ToCharArray().Where(e => e != str2[index++]).Count();

        };

        public static Func<String, Int32> CountClaps = (String str1) => {

            return str1.ToCharArray().Where(e => e == 'C').Count();

        };

        public static Func<String, Boolean> SameCase = (String str1) => {

            return str1.ToLower().Equals(str1) || str1.ToUpper().Equals(str1);

        };

        public static Func<String, String> HackerSpeak = (String str1) => {

            String container = "aesio";
            Dictionary<String, String> values = new Dictionary<String, String>();
            values.Add("e", "3");
            values.Add("a", "4");
            values.Add("s", "5");
            values.Add("i", "1");
            values.Add("o", "0");
            String result = str1.ToCharArray().Select<char, String>(e => container.Contains(e.ToString().ToLower()) ? values[e.ToString().ToLower()] : e+"").Aggregate("", (char1, char2) => char1 + "" + char2);
            return result;
            
        };

        public static Func<String, int[]> IndexOfCapitals = (String str1) => {

            int index = 0;
            return str1.ToCharArray().Select<char, int>(e => e.ToString().ToUpper().Equals(str1[index++].ToString()) && Char.IsLetter(e) ? index - 1 : -1 ).Where<int>(e => e != -1).ToArray();
            
        };

        public static Func<String, int[]> Encrypt = (String message) => {

            return message.ToCharArray().Select<char, int>(e => (int)e).ToArray<int>();

        };

        public static Func<int[], String> Decrypt = (int[] input) => {

            return input.Select<int, char>(e => (char)e).Aggregate("", (char1, char2) => char1 + "" + char2);

        };

        public static Func<String, int[]> EncryptV2 = (String message) => {

            List<int> array = new List<int>();
            for (int i = 0; i < message.Length; i++) {

                if (i == 0) {
                    array.Add((int)message[i]);
                } else {
                    int diff = ((int)message[i]) - ((int)message[i - 1]);
                    array.Add(diff);
                }

            }
            return array.ToArray();

        };

        public static Func<int[], String> DecryptV2 = (int[] message) => {

            String result = "";
            int total = 0;
            for (int i = 0; i < message.Length; i++) {

                if (i == 0) {
                    result += (char)message[i];
                } else {

                    total = message[i - 1] + message[i];
                    message[i] = total;
                    result += (char)total;

                }

            }
            return result;

        };

        public static Func<String, String> ConvertTime = (String time) => {

            String[] splitTime = time.Split(":");
            String hour = splitTime[0];
            String minute = splitTime[1].Split(" ")[0];
            if (time.Contains("am") || time.Contains("pm")) {
                if (time.Contains("am")) {
                    // is in am
                    int parseHour = Int32.Parse(hour);
                    switch (parseHour) {
                        case 12:
                            parseHour = 0;
                            break;
                        default:
                            break;
                    }
                    return String.Format("{0}:{1}", parseHour, minute);
                } else {
                    // is in pm
                    int parseHour = Int32.Parse(hour);
                    switch (parseHour) {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                            parseHour += 12;
                            break;
                        case 12:
                            break;
                    }
                    return String.Format("{0}:{1}", parseHour, minute);
                }
            } else {
                int parseHour = Int32.Parse(hour);
                if (parseHour < 12) {
                    return parseHour != 0 ? String.Format("{0}:{1} am", parseHour, minute) : String.Format("12:{0} am", minute);
                } else {
                    return parseHour != 12 ? String.Format("{0}:{1} pm", parseHour - 12, minute) : String.Format("12:{0} pm", minute);
                }
            }

        };

        public static Func<String,Boolean> PossiblePalindrome = (String theString) => {

            int oddCount = 0;
            int evenCount = 0;
            List<char> oddLetters = new List<char>();
            for (int i = 0; i < theString.Length; i++) {

                char theLetter = theString[i];
                int theCount = theString.ToCharArray().Where<char>(e => e == theLetter).Count();
                if (theCount % 2 == 0) {
                    evenCount++;
                } else if (theCount % 2 != 0) {
                    if (oddCount == 0) {
                        oddCount++;
                        oddLetters.Add(theLetter);
                    } else if (!oddLetters.Contains(theLetter)) {
                        return false;
                    }
                }


            }
            return true;

        };

        public static Func<String[], String> LongestAbecedarian = (String[] words) => {

            //words.ToList().ForEach(Console.WriteLine);
            
            List<String> wordsList = new List<String>();
            
            foreach( String eachword in words ) {

                List<char> letters = eachword.ToCharArray().ToList();
                letters.Sort();
                String sortedLetters = letters.Aggregate("", (char1 , char2) => char1 + "" + char2);
                if (sortedLetters.Equals(eachword)) {
                    wordsList.Add(eachword);
                }

            }
            if (wordsList.ToArray().Length == 0) {
                return "";
            }
            wordsList.Sort(delegate(String word1, String word2) {
                if (word1.Length == word2.Length) {
                    return 1;
                }
                return word1.Length - word2.Length;
            });
            return wordsList[wordsList.ToArray().Length - 1];

        };

        public static Func<String, Int32, String> RollingCipher = (String message, int n) => {

            String alphabet = "abcdefghijklmnopqrstuvwxyz";
            String newString = "";

            for (int i = 0; i < message.Length; i++) {

                char letter = message[i];
                int index = alphabet.IndexOf(letter);
                index += n;
                while (index < 0) {
                    index += 26;
                }
                while (index >= 26) {
                    index -= 26;
                }
                newString += alphabet[index];
            }
            return newString;

        };

        public static Func<String, Boolean> IsTimeStampPalindrome = (String timestamp) => {

            return timestamp.Equals(timestamp.ToCharArray().Reverse().Aggregate("", (char1, char2) => char1 + "" + char2));
            
        };

        public static Func<Int32, Int32, Int32, Int32, Int32, Int32, Int32> PalendromeTimestamps = (int hr1, int min1, int sec1, int hr2, int min2, int sec2) => {

            TimeSpan ts = new TimeSpan(hr1, min1, sec1);

            DateTime date1 = new DateTime(2008, 1, 1, new GregorianCalendar());
            DateTime date2 = date1 + new TimeSpan(hr2, min2, sec2);
            date1 = date1 + ts;

            TimeSpan second = new TimeSpan(0, 0, 1);
            int count = 0;

            while (date1.Hour != date2.Hour || date1.Minute != date2.Minute || date1.Second != date2.Second) {

                String timestamp = String.Format("{0}{1}{2}", date1.Hour.ToString().PadLeft(2, '0'), date1.Minute.ToString().PadLeft(2, '0'), date1.Second.ToString().PadLeft(2, '0'));
                if (IsTimeStampPalindrome(timestamp)) {
                    Console.WriteLine(String.Format("palindrome timestamp : {0}", timestamp));
                    count++;
                }
                date1 = date1 + second;

            }
            String newTimestamp = String.Format("{0}{1}{2}", date1.Hour.ToString().PadLeft(2, '0'), date1.Minute.ToString().PadLeft(2, '0'), date1.Second.ToString().PadLeft(2, '0'));
            if (IsTimeStampPalindrome(newTimestamp)) {
                Console.WriteLine(String.Format("palindrome timestamp : {0}", newTimestamp));
                count++;
            }
            return count;

        };


        public static void Main(string[] args)
        {   

            Console.WriteLine(PalendromeTimestamps(2, 12, 22, 4, 35, 10));

        }
    }

}