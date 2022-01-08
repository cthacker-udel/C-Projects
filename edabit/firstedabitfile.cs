using System;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
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

        public static Func<int[], int> MinMissPos = (int[] numbers) => {

            List<int> numberList = numbers.Where<int>(e => e > 0).ToList<int>();

            numberList.Sort();

            numberList.ForEach(Console.WriteLine);

            for (int i = 0; i < numberList.Count(); i++) {

                int number = numberList[i];
                if (!numberList.Contains(number - 1) && number != 1) {
                    return number - 1;
                } else if (!numberList.Contains(number + 1)) {
                    return number + 1;
                }

            }
            return 1;

        };

        public static Func<String, String, int> FirstIndex = (String firstString, String lastString) => {

            String[] splitHex = firstString.Split(" ");
            List<String> strList = new List<String>();
            foreach (String eachHex in splitHex) {

                int theNumber = int.Parse(eachHex, System.Globalization.NumberStyles.HexNumber);
                char theChar = (char)theNumber;
                strList.Add(theChar+"");

            }
            String theString = "";
            for (int i = 0; i < strList.Count(); i++) {
                theString += strList[i];
            }
            return theString.IndexOf(lastString);

        };

        public static Func<String, int> NumberOfRepeats = (String theString) => {

            String theSub = "";
            int max = 0;
            for (int i = 0; i < theString.Length; i++) {

                theSub += theString[i];
                if (theString.Contains(theSub)) {
                    int index = theString.IndexOf(theSub);
                    int count = 1;
                    int loopIndex = index;
                    while (loopIndex < theString.Length) {
                        loopIndex += theSub.Length;
                        if ((loopIndex + theSub.Length) < theString.Length && theString.Substring(loopIndex, theSub.Length).Equals(theSub)) {
                            count++;
                        }
                    }
                    max = Math.Max(max, count);
                }

            }
            return max;
            
        };

        public static Func<String, Boolean> IsValidIp = (String ip) => {

            String[] values = ip.Split('.');
            Console.WriteLine("input = {0}", ip);
            if (values.Length != 4) {
                return false;
            } else {
                int[] validValues = values.Where(e => e.ToCharArray().All(f => Char.IsDigit(f))).Where(e => !e.StartsWith("0") || e.Equals("0")).Select(e => int.Parse(e)).ToArray();
                return validValues.Length == 4 && validValues.All(e => e >= 0 && e <= 255);
            }


            // Regex expr = new Regex("(\\d{1,3})\\.(\\d{1,3})\\.(\\d{1,3})\\.(\\d{1,3})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            // MatchCollection matches = expr.Matches(ip);

            // int count = 0;

            // foreach (Match match in matches) {
            //     GroupCollection groups = match.Groups;
            //     for (int i = 0; i < groups.Count; i++) {
            //         Group theGroup = groups[i];
            //         if (int.Parse(theGroup.Name) > 0) {
            //             count++;
            //             int theValue = int.Parse(theGroup.Value);
            //             if (theValue < 0 || theValue > 255) {
            //                 return false;
            //             }
            //         }
            //     }
            // }
            // return count == 4;

        };

        public static Func<int, int, int[][]> PrintGrid = (int x, int y) => {

            int starter = 1;
            List<List<int>> matrix = new List<List<int>>();
            int end = x * y;
            for (int i = 0; i < x; i++) {
                matrix.Add(new List<int>());
            }
            int cycler = 0;
            while (starter <= end) {
                int placement = cycler % x;
                matrix[placement].Add(starter++);
                cycler++;
            }
            List<int[]> returnMatrix = new List<int[]>();
            for (int i = 0; i < matrix.Count(); i++) {
                returnMatrix.Add(matrix[i].ToArray());
            }
            return returnMatrix.ToArray();
        };

        public static Func<String, String, Boolean> Overlap = (String firstWord, String secondWord) => {

            firstWord = firstWord.Replace("*", ".").Replace("+","\\+").ToLower();
            secondWord = secondWord.Replace("*", ".").Replace("+","\\+").ToLower();

            int matchingSequence = 0; // 0 means both lengths are suffice, 1 means just firstword expr, 2 means just secondword expr, 3 means none

            Regex expr1 = new Regex("");
            Regex expr2 = new Regex("");

            if (firstWord.Length <= secondWord.Length) {
                expr1 = new Regex(String.Format("{0}$", secondWord.Substring(secondWord.Length - firstWord.Length)));
                matchingSequence = 1;
            } else {
                matchingSequence = 3;
            }
            if (secondWord.Length <= firstWord.Length) {
                Console.WriteLine("distance = {0}", firstWord.Length - secondWord.Length);
                expr2 = new Regex(String.Format("{0}$", firstWord.Substring(firstWord.Length - secondWord.Length)));
                if (matchingSequence == 1) {
                    matchingSequence = 0;
                } else {
                    matchingSequence = 2;
                }
            } else {
                matchingSequence = 3;
            }

            Console.Write("matchingsequence = {0}", matchingSequence);

            return expr1.Match(secondWord).Success || expr2.Match(firstWord).Success;

        };

        public struct Rational {

            public Rational(int newNumerator, int newDenominator) {
                sign = newNumerator < 0 && newDenominator < 0 ? true : newNumerator < 0 && newDenominator > 0 ? false : newNumerator > 0 && newDenominator < 0 ? false : true;
                numSign = newNumerator > 0;
                denSign = newDenominator > 0;
                if (!denSign && !numSign) {
                    numSign = true;
                    denSign = true;
                }
                // numSign = true = positive, false = negative
                // denSign = true = positive, false = negative
                // sign = true = positive, false = negative
                this.numerator = Math.Abs(newNumerator);
                this.denominator = Math.Abs(newDenominator);
                if (newDenominator == 0) {
                    throw new ArgumentException("Invalid Denominator");
                }
                else if (newNumerator == 0) {
                    this.denominator = 1;
                } else {
                    this.denominator = Math.Abs(newDenominator);
                    // time to reduce
                    bool foundFactor = this.numerator != 0 && this.numerator != 1 && this.denominator != 1;
                    while (foundFactor) {
                        for (int i = 2; i <= Math.Min(this.numerator, this.denominator); i++) {
                            if (this.numerator % i == 0 && this.denominator % i == 0) {
                                this.numerator /= i;
                                this.denominator /= i;
                                foundFactor = true;
                                break;
                            } else {
                                foundFactor = false;
                            }
                        }
                        if (this.numerator == 1 || this.denominator == 1) {
                            break;
                        }
                    }
                }
            }

            public override string ToString() {

                if (this.denominator == 1) {
                    if (sign) {
                        return String.Format("{0}", this.numerator);
                    } else {
                        return String.Format("-{0}", this.numerator);
                    }
                }

                if (sign) {
                    // positive
                    return String.Format("{0}/{1}", this.numerator, this.denominator);
                } else {
                    return String.Format("-{0}/{1}", this.numerator, this.denominator);
                }

            }

            private int numerator;
            private int denominator;
            public int Numerator { get{
                Console.WriteLine("sign = {0}", sign ? "true": "false");
                if (numSign) {
                    return numerator;
                } else {
                    return numerator * -1;
                }
            } }
            public int Denominator { get {
                Console.WriteLine("sign = {0}", sign ? "true": " false");
                if (denSign) {
                    return denominator;
                } else {
                    return denominator * -1;
                }
            } }
            public bool sign { get; }
            public bool numSign { get; }

            public bool denSign { get; }

        }


        public static void Main(string[] args)
        {   

            var r1 = new Rational(-3, 4);
            Console.WriteLine(r1.Numerator);
            Console.WriteLine(r1.Denominator);
            Console.WriteLine(r1.ToString());


        }
    }

}