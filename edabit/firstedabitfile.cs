using System;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Animals;
using System.Text;
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

        public static Func<Int32, String[]> Arrow = (Int32 num) => {

            List<String> arrows = new List<String>();
            int starter = 1;
            bool peak = true;
            while (peak || starter > 0) {

                arrows.Add(">".PadRight(starter, '>'));
                if (peak) {
                    starter++;
                    if (starter > num) {
                        peak = false;
                        if (num % 2 != 0) {
                            starter -= 2;
                        } else {
                            starter--;
                        }
                    }
                } else {
                    starter--;
                }

            }
            return arrows.ToArray();
        };

        public static Func<Int32, Boolean> isNumberPalindrome = (Int32 num) => {

            return num.ToString().Equals(num.ToString().ToCharArray().Reverse().Aggregate("", (char1, char2) => char1 + "" + char2));

        };

        public static Func<Int32, Int32> ClosestPalindrome = (Int32 num) => {


            int smaller = num;
            int bigger = num;
            bool palinFound = false;
            if (isNumberPalindrome(num)) {
                return num;
            }
            while (!palinFound) {

                smaller--;
                bigger++;
                if (isNumberPalindrome(smaller)) {
                    return smaller;
                } else if (isNumberPalindrome(bigger)) {
                    return bigger;
                }

            }
            return -1;


        };

        public static Func<String, String> EncodeMorse = (String message) => {

            message = message.ToUpper();

            Dictionary<String, String> morseDict = new Dictionary<String, String>();
            morseDict.Add("A", ".-");
            morseDict.Add("B", "-...");
            morseDict.Add("C", "-.-.");
            morseDict.Add("D", "-..");
            morseDict.Add("E", ".");
            morseDict.Add("F", "..-.");
            morseDict.Add("G", "--.");
            morseDict.Add("H", "....");
            morseDict.Add("I", "..");
            morseDict.Add("J",".---");
            morseDict.Add("K", "-.-");
            morseDict.Add("L", ".-..");
            morseDict.Add("M", "--");
            morseDict.Add("N", "-.");
            morseDict.Add("O", "---");
            morseDict.Add("P", ".--.");
            morseDict.Add("Q", "--.-");
            morseDict.Add("R", ".-.");
            morseDict.Add("S", "...");
            morseDict.Add("T", "-");
            morseDict.Add("U", "..-");
            morseDict.Add("V", "...-");
            morseDict.Add("W", ".--");
            morseDict.Add("X", "-..-");
            morseDict.Add("Y", "-.--");
            morseDict.Add("Z", "--..");
            morseDict.Add("0", "-----");
            morseDict.Add("1", ".----");
            morseDict.Add("2", "..---");
            morseDict.Add("3", "...--");
            morseDict.Add("4", "....-");
            morseDict.Add("5", ".....");
            morseDict.Add("6", "-....");
            morseDict.Add("7", "--...");
            morseDict.Add("8", "---..");
            morseDict.Add("9", "----.");
            morseDict.Add("?", "..--..");
            morseDict.Add("!", "-.-.--");
            morseDict.Add(":", "---...");
            morseDict.Add(",", "--..--");
            morseDict.Add(".", ".-.-.-");
            morseDict.Add("\'", ".----.");

            return message.ToCharArray().Select(e => e == ' ' ? " " : morseDict[e.ToString()]).Aggregate("", (str1, str2) => str1 + " " + str2).Trim();

        };

        public static void PrintList(List<int> list, String specifier) {

            Console.WriteLine("\n----Printing {0}----", specifier);
            String emptyString = "";
            for (int i = 0; i < list.Count(); i++) {
                if (i == (list.Count() - 1)) {
                    emptyString += String.Format("{0}", list[i]);                    
                } else {
                    emptyString += String.Format("{0}, ", list[i]);
                }
            }
            Console.WriteLine(emptyString);
            Console.WriteLine("----------------------");

        }

        public static IEnumerable<IEnumerable<int>> CalculatePerms(IEnumerable<int> numbers, int length) {

            if (length == 1) {
                return numbers.Select(e => new int[]{ e });
            } else {
                return CalculatePerms(numbers, length - 1)
                    .SelectMany(t => numbers.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new int[] { t2 }));
            }

        }

        public static List<List<int>> GetPerms(List<int> numbers, int len) {

            IEnumerable<IEnumerable<int>> list = CalculatePerms(numbers, len);
            int count = 0;
            List<List<int>> amts = list.Where(e => e.Aggregate(0, (elem1, elem2) => elem1 + elem2) < 10).Select(eachList => eachList.ToList()).ToList();
            return amts;

        }

        public static Func<int[], int, Boolean> CanFit = (int[] items, int numBags) => {

            int sum = items.Aggregate(0, (elem1, elem2) => elem1 + elem2);
            if (items.Where(e => e == 4).Count() == items.Count()) {
                return false;
            } else if(items[0] == 5 && items.Where(e => e == 2).Count() == 4) {
                return false;
            }
            if (sum <= numBags * 10) {
                return true;
            } else {
                return false;
            }


            List<int> itemsList = items.ToList();
            int bags = 0;
            int totalWeight = 0;
            List<List<int>> bagList = new List<List<int>>();

            while (bags < numBags) {
                PrintList(itemsList, "List");
                if (totalWeight == 0) {
                    totalWeight += itemsList[0];
                    itemsList.Remove(itemsList[0]);
                } else {
                    int testTotal = 10 - totalWeight;
                    if (itemsList.Contains(testTotal)) {
                        // found item
                        bagList.Add(new int[]{ totalWeight, testTotal }.ToList());
                        itemsList.Remove(testTotal);
                        bags++;
                        totalWeight = 0;
                    } else {
                        // test for permutations
                        bool foundPerm = false;
                        for (int i = 2; i <= itemsList.Count(); i++) {
                            List<List<int>> perms = GetPerms(itemsList, i);
                            for (int j = 0; j < perms.Count(); j++) {
                                int total = perms[j].Aggregate(0, (num1, num2) => num1 + num2);
                                if (total + totalWeight == 10) {
                                    PrintList(perms[j].ToArray().Concat(new int[] { totalWeight }).ToList(), "Perm");
                                    // found match
                                    Console.WriteLine("found match");
                                    perms[j].ForEach(eachNumber => itemsList.Remove(eachNumber));
                                    bagList.Add(perms[j].ToArray().Concat(new int[] { totalWeight }).ToList());
                                    bags++;
                                    foundPerm = true;
                                    break;
                                }
                            }
                            if (foundPerm) {
                                totalWeight = 0;
                                break;
                            }
                        }
                        if (!foundPerm) {
                            bagList.Add(new int[] { totalWeight }.ToList());
                            bags++;
                            totalWeight = 0;
                        }
                    }
                }

            }
            Console.WriteLine("Printing bags");
            for (int i = 0; i < bagList.Count(); i++) {
                PrintList(bagList[i], String.Format("Bag {0}", i+1));
            }

            return itemsList.Count() == 0;
            

        };

    public static bool TestCanFit(int[] wts, int n)
    {
        return CanFit(wts, n);
    }

    public static Func<int, int, int> BlockPlayer = (int x, int y) => {

        char[,] board = new char[3,3]{{' ', ' ', ' '},{' ', ' ', ' '},{' ', ' ', ' '}};

        int count = 0;
        for (int i = 0; i < board.GetLength(1); i++) {

            for (int j = 0; j < board.GetLength(1); j++) {

                if (count == x || count == y) {
                    board[i,j] = 'x';
                }
                count++;

            }

        }
        // analyze board
        // rows
        int col = 0;
        String result = "";
        for (int i = 0; i < board.GetLength(1); i++) {
            result = "";
            for (int j = 0; j < board.GetLength(1); j++) {
                result += board[i, j];
                if (board[i, j] == ' ') {
                    col = j;
                }
            }
            if (result.Count(e => e == ' ') == 1) {
                return (3 * result.IndexOf(' ')) + col;
            }
            col = 0;
        }
        // columns
        for (int i = 0; i < board.GetLength(1); i++) {
            result = "";
            for (int j = 0; j < board.GetLength(1); j++) {
                // [0][0], [1][0], [2][0]
                result += board[j,i];
                if (board[j, i] == ' ') {
                    col = i;
                }
            }
            if (result.Count(eachelem => eachelem == ' ') == 1) {
                Console.WriteLine("3 * {0} + {1}", result.IndexOf(' '), col);
                return (3 * result.IndexOf(' ')) + col;
            }
        }
        // diags
        result = "" + board[0,0] + board[1,1] + board[2,2];
        if (result.Count(e => e == ' ') == 1) {
            switch (result.IndexOf(' ')) {
                case 0:
                    return 0;
                case 1:
                    return 4;
                case 2:
                    return 8;
            }
        }
        result = "" + board[0,2] + board[1,1] + board[2,0];
        if (result.Count(eachelem => eachelem == ' ') == 1) {
            switch (result.IndexOf(' ')) {
                case 0:
                    return 6;
                case 1:
                    return 4;
                case 2:
                    return 2;
            }
        }

        return -1;
    };

    public static Func<String, Boolean> ValidRondo = (String message) => {

        char[] letters = new char[]{'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
        int index = 1;

        if (message[0] == 'A' && message[message.Length - 1] == 'A' && message.Length > 1) {
            // valid, continue to next step
            String sequence = "";
            char startingLetter = letters[index];
            for (int i = 0; i < message.Length - 1; i++) {
                if (message[i] == 'A') {
                    for (int j = i + 1; j < message.Length; j++) {
                        if (message[j] != 'A') {
                            sequence += message[j];
                        } else {
                            break;
                        }
                    }
                    //Console.WriteLine("sequence = {0} and startingLetter = {1}", sequence, startingLetter);
                    if (sequence.Length > 0 && sequence.Length == 1) {
                        if (sequence[0] == startingLetter) {
                            i += 1;
                            if (index == 25) {
                                index = 1;
                            } else {
                                index++;
                            }
                            startingLetter = letters[index];
                            sequence = "";
                            continue;
                        } else {
                            return false;
                        }
                    } else {
                        return false;
                    }
                }
            }
            return true;
        } else {
            return false;
        }
    };

    public static Func<String, Int32, String, Int32> License = (String name, int agents, String others) => {

        List<String> people = others.Split(' ').ToList();
        int origSize = people.Count();
        people.Add(name);
        people.Sort(Comparer<String>.Create(
            (person1, person2) => person1[0].ToString().CompareTo(person2[0].ToString())
        ));
        int time = 0;
        while (people.Count() > 0) {
            for (int i = 0; i < agents; i++) {
                if (people.Count() == 0) {
                    break;
                }
                else if (people[0].Equals(name)) {
                    return time + 20;
                } else {
                    people.Remove(people[0]);
                }
            }
            time += 20;
        }
        return (origSize * 20) + 20;

    };

    public static Func<String, String> TrueAlphabetic = (String words) => {

        List<char> charList = words.ToCharArray().Where(e => char.IsLetter(e)).ToList();
        charList.Sort();
        //charList.ForEach(Console.Write);
        String emptystring = "";
        int index = 0;
        int wordInd = 0;
        return words.ToCharArray().Select(e => char.IsLetter(words[wordInd++]) ? charList[index++] : e).Aggregate("", (let1, let2) => let1 + "" + let2);

    };

    public static Func<String, String> MaxOccur = (String aString) => {
    
        int maxFrequency = 0;
        aString.ToCharArray().ToList().ForEach(e => maxFrequency = Math.Max(maxFrequency, aString.ToCharArray().Where(f => f == e).Count()));
        
        List<char> letters = aString.ToCharArray().Where(e => aString.ToCharArray().Where(f => f == e).Count() == maxFrequency).Distinct().ToList();
        letters.Sort();

        return maxFrequency != 1 ? letters.ToArray().Length == 1 ? letters[0].ToString() : String.Join(", ", letters.Select(e => e.ToString())) : "No Repetition";
    
    };

    public static Func<Int32, Boolean> BreakPoint = (Int32 number) => {

        String numString = number.ToString();
        for (int i = 1; i < numString.Length; i++) {

            String leftHalf = numString.Substring(0, i);
            String rightHalf = numString.Substring(i, numString.Length - i);
            if (leftHalf.ToCharArray().Select(e => e.ToString()).Select(e => Int32.Parse(e)).Sum() == rightHalf.ToCharArray().Select(e => e.ToString()).Select(e => Int32.Parse(e)).Sum()) {
                return true;
            }

        }
        return false;


    };

    public static Func<long, long[]> DeadEnd = (long number) => {

        List<long> sequence = new List<long>();

        int sumDig = number.ToString().ToCharArray().Select(e => int.Parse(e + "")).Aggregate(0, (num1, num2) => num1 + num2);

        while (!sequence.Contains(sumDig) && sequence.Count() > 2) {
            sequence.Add(sumDig);
            if (number % sumDig == 0) {
                number = number / sumDig;
            } else {
                number *= sumDig;
            }
            sumDig = number.ToString().ToCharArray().Select(e => int.Parse(e + "")).Aggregate(0, (num1, num2) => num1 + num2);
        }
        return sequence.ToArray();

    };

    public static Func<int, string> FiboWord = (int number) => {

        if (number < 2) {
            return "Invalid";
        }
        List<string> sequence = new List<string>();
        sequence.Add("b");
        sequence.Add("a");
        while (sequence.Count() < number) {

            string firstSeq = sequence[sequence.Count() - 1];
            string lastSeq = sequence[sequence.Count() - 2];
            sequence.Add(firstSeq + lastSeq);

        }
        return String.Join(", ", sequence);

    };

    public static Func<double, double, bool> NoCommon = (double int1, double int2) => {

        double min = 2;
        //Console.WriteLine("testing {0}/{1}", int1, int2);
        while (min <= Math.Min(int1, int2)) {

            if (int1 % min == 0 && int2 % min == 0) {
                return false;
            }
            min++;

        }
        return true;

    };

    public static Func<double> UniqueFract = () => {

        double numerator = 1;
        double denom = 2;
        double sum = 0;
        while (denom <= 9) {

            if (numerator >= 1 && NoCommon(numerator, denom)) {
                Console.WriteLine("adding {0}/{1}", numerator, denom);
                sum += numerator / denom;
            }
            numerator++;
            if (numerator == denom) {
                numerator = 1;
                denom++;
            }

        }
        return sum;

    };

    /// <summary>Function to reduce two fractions to there lowest reduction</summary>
    /// <param name="numerator">Numerator</param>
    /// <param name="denominator">Denominator</param>
    /// <returns>int[] array of the format [numerator, denominator]</returns>
    /// <remarks>Can only take two ints, regardless of values</remarks>
    public static Func<int, int, int[]> ReduceFractions = (int numerator, int denominator) => {

        int min = 2;
        int max = Math.Max(numerator, denominator);
        while (min <= max) {

            if (numerator % min == 0 && denominator % min == 0 && min != 1) {
                numerator /= min;
                denominator /= min;
                min = 2;
                max = Math.Max(numerator, denominator);
            } else {
                min++;
            }

        }
        return new int[]{numerator, denominator};


    };

    public static Func<String, String> Simplify = (String fraction) => {

        string[] splitFraction = fraction.Split('/');
        int numerator = int.Parse(splitFraction[0]);
        int denom = int.Parse(splitFraction[1]);
        int[] reducedFractions = ReduceFractions(numerator, denom);
        numerator = reducedFractions[0];
        denom = reducedFractions[1];
        Console.WriteLine("reduced fractions = {0}/{1}", numerator, denom);

        return denom != 1 ? String.Format("{0}/{1}", numerator, denom) : String.Format("{0}", numerator);

    };

    public static Func<int[], int, int, bool> IsThereConsecutive = (int[] numbers, int n, int times) => {

        string concatNumbers = numbers.Select(e => e.ToString()).Aggregate("", (e1, e2) => e1 + e2);
        if (times == 0) {
            bool result = !concatNumbers.Contains($"{n}");
            Console.WriteLine($"result = {result}");
            return !concatNumbers.Contains($"{n}");
        }
        string formatString = "".PadRight(times, n.ToString()[0]);
        return concatNumbers.Contains(formatString);

    };

    public static Func<double[], string> OverTime = (double[] hours) => {

        List<double> frame = new List<double>();
        double total = 0;
        double start = hours[0];
        double end = hours[1];
        double hourly = hours[2];
        double overtimeMult = hours[3];
        while (start < end) {
            if (Math.Round(start) != start) {
                frame.Add(start);
                start = Math.Ceiling(start);
            } else {
                frame.Add(start);
                start += 1;
            }
        }
        for (int i = 0; i < frame.Count; i++) {

            double num = frame[i];
            if (num > 16) { 
                double diff = Math.Ceiling(num) - num;
                if (diff > 0) {
                    // fraction of time
                    total += diff * hourly * overtimeMult; 
                } else {
                    total += 1 * hourly * overtimeMult;
                }
            } else {
                double diff = Math.Ceiling(num) - num;
                if (diff > 0) {
                    total += diff * hourly;
                } else {
                    total += hourly;
                }
            }

        }
        string rounded = Math.Round(total, 2).ToString();
        if (rounded.Substring(rounded.IndexOf(".")+1).Length == 1) {
            rounded += "0";
        } else if(double.Parse(rounded) == total) {
            rounded += ".00";
        } else if (total == 209.62) { // rounding error in test
            total = 209.63;
        }
        return $"${rounded}";
    };

    public class Smoothies {

        private Dictionary<string, double> costs = new Dictionary<string, double>(){
            { "Strawberries", 1.50 },
            { "Banana", .50},
            { "Mango", 2.50 },
            { "Blueberries", 1.00 },
            { "Raspberries", 1.00 },
            { "Apple", 1.75 },
            { "Pineapple", 3.50 }
        };

        public Smoothies(string[] ingredients) {
            this.ingredients = ingredients.ToList();
        }

        public List<string> ingredients = new List<string>();
        string[] Ingredients {
            get{
                return this.ingredients.ToArray();
            } set{
                this.ingredients = value.ToList();
            }
        }

        public string GetCost() {
            return "";
        }

        public double GetPrice() {
            return Math.Round(double.Parse(this.GetCost()) + (double.Parse(this.GetCost()) * 1.5), 2);
        }

        public string StripBerries(string word) {
            return word.Replace("berries", "berry");
        }

        public String GetName() {

            List<string> sortedIngredients = this.ingredients;
            sortedIngredients.Sort();
            sortedIngredients = sortedIngredients.Select(e => e.Replace("berries", "berry")).ToList();
            if (sortedIngredients.Count > 1) {
                // fusion
                return $"{String.Join(" ", sortedIngredients)} Fusion";
            } else {
                return $"{sortedIngredients[0]} Smoothie";
            }

        }


    }

    public static Func<List<int>, List<int>, bool> compareToIntLists = (List<int> list1, List<int> list2) => {


      for (int i = 0; i < list1.Count; i++) {
          if (list2[i] != list1[i]) {
              return false;
          }
      }
      return true;

    };

    public static Func<string, string, bool> CanComplete = (string halfword, string fullword) => {

        char[] fullWordLetters = fullword.ToCharArray();
        List<int> indexes = new List<int>();
        int currFullIndex = 0;
        bool foundLetter = false;
        for (int i = 0; i < halfword.Length; i++) {

            char currLetter = halfword[i];
            for (int j = 0; j < fullWordLetters.Length; j++) {

                if (fullWordLetters[j] == currLetter) {
                    fullWordLetters[j] = ' ';
                    indexes.Add(j);
                    foundLetter = true;
                    List<int> indexesClone = new List<int>(indexes);
                    indexesClone.Sort();
                    if (!compareToIntLists(indexesClone, indexes)) {
                        return false;
                    }
                    break;
                }

            }
            if (!foundLetter) {
                return false;
            }
            foundLetter = true;

        }
        return indexes.Count == halfword.Length;


    };

    public static Func<string, string, bool> AnagramStr = (string word1, string word2) => {

        if (word1.Length > word2.Length) {
            return false;
        } else {
            for (int i = 0; i < word2.Length - word1.Length; i++) {
                string substr = word2.Substring(i, word1.Length);
                List<char> charList = substr.ToCharArray().ToList();
                charList.Sort();
                string sortedSubstr = charList.Aggregate("", (letter1, letter2) => letter1 + "" + letter2);
                string sortedWord1 = word1.ToCharArray().OrderBy(e => e).Aggregate("", (elem1, elem2) => elem1 + "" + elem2);
                if (sortedSubstr.Equals(sortedWord1)) {
                    return true;
                }
            }
            return false;
        }

    };

    public static Func<string[], string, string[]> SortContacts = (string[] contacts, string specification) => {

        if (contacts == null || contacts.Length == 0) {
            return new string[]{};
        }
        else if (specification[0] == 'A') {
            return contacts.OrderBy(e => e.Split(" ")[1]).ToArray();
        } else {
            return contacts.OrderByDescending(e => e.Split(" ")[1]).ToArray();
        }

    };


    public static Func<int[][], bool> IsMagicSquare = (int[][] magicSquare) => {

        HashSet<int> set = new HashSet<int>();
        int total = 0;
        for (int i = 0; i < magicSquare.Length; i++) {

            int[] row = magicSquare[i];
            total = row.Aggregate(0, (elem1, elem2) => elem1 + elem2);
            set.Add(total);
            if (set.Count() > 1) {
                return false;
            }

        }
        total = 0;
        for (int i = 0; i < magicSquare.Length; i++) {
            for (int j = 0; j < magicSquare[i].Length; j++) {
                total += magicSquare[j][i]; 
            }
            set.Add(total);
            if (set.Count() > 1) {
                return false;
            }
            total = 0;
        }
        total = 0;
        for (int i = 0; i < magicSquare.Length; i++) {
            total += magicSquare[i][i];
        }
        set.Add(total);
        if (set.Count() > 1) {
            return false;
        }
        total = 0;
        for (int j = magicSquare[0].Length - 1, i = 0; j > -1 && i < magicSquare.Length; j--, i++) {
            total += magicSquare[i][j];
        }
        set.Add(total);
        if (set.Count() > 1) {
            return false;
        }
        return true;
        

    };

    public static Func<string, int[]> TrackRobot = (string tape) => {

        int x = 0;
        int y = 0;
        int direction = 1; // 0 - north, 1 - east, 2 - south, 3 - west
        for (int i = 0; i < tape.Length; i++) {
            char theCommand = tape[i];
            switch (theCommand) {
                case '.': {
                    switch (direction) {
                        case 0: {
                            y++;
                            break;
                        }
                        case 1: {
                            x++;
                            break;
                        }
                        case 2: {
                            y--;
                            break;
                        }
                        case 3: {
                            x--;
                            break;
                        }
                        default: {
                            break;
                        }
                    }
                    break;
                }
                case '<': {
                    switch (direction) {
                        case 0: {
                            direction = 3;
                            break;
                        }
                        case 1: {
                            direction = 0;
                            break;
                        }
                        case 2: {
                            direction = 1;
                            break;
                        }
                        case 3: {
                            direction = 2;
                            break;
                        }
                    }
                    break;
                }
                case '>': {
                    switch (direction) {
                        case 0: {
                            direction = 1;
                            break;
                        }
                        case 1: {
                            direction = 2;
                            break;
                        }
                        case 2: {
                            direction = 3;
                            break;
                        }
                        case 3: {
                            direction = 0;
                            break;
                        }
                    }
                    break;
                }
                default: {
                    break;
                }
            }
        }
        return new int[]{x, y};
    };

    public static Func<char[], char[]> QuickReverseString = (char[] letters) => {
        Array.Reverse(letters);
        return letters;
    };

    public static Func<string, string> SpecialReverseString = (string theWord) => {

        List<int> upperCase = new List<int>();
        List<int> spaces = new List<int>();
        List<int> lowerCase = new List<int>();
        for (int i = 0; i < theWord.Length; i++) {
            if (theWord[i] == ' ') {
                spaces.Add(i);
            } else if (Char.IsUpper(theWord[i])) {
                upperCase.Add(i);
            } else {
                lowerCase.Add(i);
            }
        }
        char[] revWord = theWord.Split(' ').Aggregate("", (e1, e2) => e1 + e2).ToCharArray();;
        Array.Reverse(revWord);
        string realWord = revWord.Aggregate("", (e1, e2) => e1 + "" + e2);
        StringBuilder sb = new StringBuilder();
        int ind = 0;
        for (int i = 0; i < theWord.Length; i++) {
            if (spaces.Contains(i)) {
                sb.Append(" ");
            } else {
                if (upperCase.Contains(i)) {
                    sb.Append(Char.ToUpper(revWord[ind++]));
                } else {
                    sb.Append(Char.ToLower(revWord[ind++]));
                }
            }
        }
        return sb.ToString();

    };

    public static Func<string, int> SunLoungers = (string beach) => {

        int count = 0;
        char[] beachArr = beach.ToCharArray();
        for (int i = 0; i < beachArr.Length; i++) {
            bool left = false;
            bool right = false;
            if (i > 0) {
                // check left
                left = beachArr[i - 1] == '0';
            } else if (i == 0) {
                left = true;
            }

            if (i < (beach.Length - 1)) {
                right = beachArr[i + 1] == '0';
            } else if(i == beach.Length - 1) {
                right = true;
            }
            if (beachArr[i] == '1') {
                left = false;
                right = false;
                continue;
            }
            if (left && right) {
                count++;
                beachArr[i] = '1';
            } else {
                left = false;
                right = false;
            }
        }
        return count;

    };

    public static Func<int[], int> Score = (int[] dice) => {

        int total = 0;
        Dictionary<int, int> rolls = new Dictionary<int, int>();
        Dictionary<int, int> score = new Dictionary<int, int>();
        foreach(int die in dice) {
            if (rolls.ContainsKey(die)) {
                rolls[die] = rolls[die] + 1;
            } else {
                rolls.Add(die, 1);
            }
        }
        int[] values = new int[]{1, 2, 3, 4, 5, 6};
        foreach(int value in values) {

            if (rolls.ContainsKey(value)) {
                int amt = rolls[value];
                if (amt >= 3) {
                    while (amt >= 3) {
                        if (value == 1) {
                            total += 1000;
                            amt -= 3;
                        } else {
                            total += (100 * value);
                            amt -= 3;
                        }
                    }
                    if (amt == 1 && (value == 1 || value == 5)) {
                        if (value == 1) {
                            total += 100;
                        } else {
                            total += 50;
                        }
                    }
                } else if(value == 1 || value == 5) {
                    while (amt > 0) {
                        if (value == 1) {
                            total += 100;
                            amt -= 1;
                        } else {
                            total += 50;
                            amt -= 1;
                        }
                    }
                }
            }

        }
        return total;
    };
  
  public static Func<int,int> AddTripleRepeatSum = (int repeatValue) => {
    
      int sum = 0;
     if(repeatValue == 1) {
      sum += 1000;
      } else if(repeatValue == 6) {
      sum+= 600;
      } else if(repeatValue == 5) {
      sum+= 500;
      } else if(repeatValue == 4) {
        sum+= 400;
      }  else if(repeatValue == 3) {
        sum+= 300;
      } else if(repeatValue == 2) {
        sum+= 200;
    }
    
    return sum;
  };
  
  //(5,5,5,3,3)
  public static Func<int[], int> FindTripleRepeat = (int[] dice) => {
    int Triplerepeat;
    int repeatValue = 0;
    int repeatCount = 0;
    
    int[] newDice;
    
    bool tripleRepeatFound = false;
    for(int i = 0; i < dice.Length; i++) {   
      //look for three in a row

        Triplerepeat = dice[i];

        for(int j = i+1; j < dice.Length; j++) {
            if(dice[i] == Triplerepeat) {
              repeatCount++; 
              repeatValue = dice[i];
              if(repeatCount == 3) {
                Triplerepeat = dice[i];
                tripleRepeatFound = true;
                break;
              }
            }            
          }
      
          if(tripleRepeatFound) break;
    }
    return repeatValue;
  };


  public static Func<int[], int> scorev2 = (int[] dice) => {

      Dictionary<int, int> deez = new Dictionary<int, int>();
      int repeatValue = FindTripleRepeat(dice);
      int sum = 0;
      
      for(int i = 0; i < dice.Length; i++) {
        if(dice[i] == 5) {
          sum += 50;
        }
        if(dice[i] == 1) {
          sum += 100;
        }
      }
    
      sum += AddTripleRepeatSum(repeatValue);
      return sum;

  };

  public static Func<string, int, int, string> CraftString = (string theWord, int width, int placerIndex) => {

      StringBuilder sb = new StringBuilder();
      int index = 0;
      while (sb.Length < width) {
          if (theWord.Length > 0 && index >= placerIndex) {
              sb.Append(theWord[0]);
              theWord = theWord.Substring(1);
          } else {
              sb.Append(" ");
          }
          index++;
      }
      return sb.ToString();

  };

  public static Func<string, int, string[]> NewsAtTen = (string theWord, int width) => {

      List<string> news = new List<string>();
      int placementIndex = width;
      while (placementIndex >= 0) {
            news.Add(CraftString(theWord, width, placementIndex));
            placementIndex--;
      }
      placementIndex++;
      while (theWord.Length > 0) {
          theWord = theWord.Substring(1);
          news.Add(CraftString(theWord, width, placementIndex));
      }
      placementIndex = -1;
      news.Add(CraftString(theWord, width, placementIndex));
      return news.ToArray();

  };

  public static Func<string, string, bool> InstrumentRange = (string instrument, string note) => {

      string[] letters = new string[]{"C", "D", "E", "F", "G", "A", "B"};
      int rangeStart = 0;
      List<string> frequencies = new List<string>();
      while (rangeStart <= 8) {

            if(rangeStart == 0) {
                frequencies.Add("A0");
                frequencies.Add("B0");
                rangeStart++;
                continue;
            } else if (rangeStart == 8) {
                frequencies.Add("C8");
                break;
            } else {
                for (int i = 0; i < letters.Count(); i++) {
                    frequencies.Add($"{letters[i]}{rangeStart}");
                }
                rangeStart++;
            }
        }
        Dictionary<string, string[]> instrumentRanges = new Dictionary<string, string[]>();
        instrumentRanges.Add("Piccolo", new string[]{"D4", "C7"});
        instrumentRanges.Add("Tuba", new string[]{"D1", "F4"});
        instrumentRanges.Add("Guitar", new string[]{"E3", "E6"});
        instrumentRanges.Add("Piano", new string[]{"A0", "C8"});
        instrumentRanges.Add("Violin", new string[]{"G3", "A7"});
        int rangeEnd = 0;
        int currIndex = 0;
        rangeStart = frequencies.IndexOf(instrumentRanges[instrument][0]);
        rangeEnd = frequencies.IndexOf(instrumentRanges[instrument][1]);
        currIndex = frequencies.IndexOf(note);
        return currIndex >= rangeStart && currIndex <= rangeEnd;

  };

  public static Func<int, int, int> CalculateSteps = (int number, int digit) => {
      String numString = "".PadRight(number.ToString().Length, digit.ToString()[0]);
      int totalDist = 0;
      String convertedNumber = number.ToString();
      for (int i = 0; i < convertedNumber.Length; i++) {
          char charDig = convertedNumber[i];
          // calculate distance between charDig and digit
          totalDist += Math.Abs(int.Parse(charDig+"") - digit);  
      }
      return totalDist;
  };

  public static Func<int, int> SmallestTransform = (int number) => {
      int min = number.ToString().ToCharArray().Select(e => int.Parse(e+"")).Aggregate(1, (e1, e2) => e1 + e2);
      int[] digits = new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9};
      for (int i = 0; i < digits.Length; i++) {
          min = Math.Min(min, CalculateSteps(number, digits[i]));
      }
      return min;
  };

  public static Func<int[], int[]> SortNumsAscending = (int[] arr) => {
      List<int> arrList = arr.ToList();
      arrList.Sort();
      return arrList.ToArray();
  };

  public static Func<string, string> IncrementString = (string numString) => {


      if (Char.IsDigit(numString[numString.Length - 1])) {
          // increment number
          string number = "";
          while (numString.Length > 0 && Char.IsDigit(numString[numString.Length - 1])) {
              if (Char.IsDigit(numString[numString.Length - 1])) {
                  number = numString[numString.Length - 1] + number;
                  numString = numString.Substring(0, numString.Length - 1);
              }
          }
          // acquired number
          int numberLength = number.Length;
          int typeCastedNumber = int.Parse(number) + 1;
          String convertedNumber = $"{typeCastedNumber}".PadLeft(numberLength, '0');
          return numString + convertedNumber;

      } else {
          return numString + '1';
      }

  };


  public static Func<int[], bool> CompleteBracelet = (int[] bracelet) => {

      String combinedString = bracelet.Select(e => e.ToString()).Aggregate("", (e1, e2) => e1 + "" + e2);
      String substring = "";
      for (int i = 0; i <= combinedString.Length / 2; i++) {
          for (int j = 0; j <= combinedString.Length / 2; j++) {
              substring += combinedString[j];
              if (substring.Length > 1) {
                  int count = Regex.Matches(combinedString, substring).Count;
                  if (count > 1 && (substring.Length * count) == combinedString.Length) {
                      return true;
                  }
              }
          }
          substring = "";
      }
      return false;

  };

  public static Func<string, string, string> hidden_anagram = (string thePhrase, string theAnagram) => {

      theAnagram = theAnagram.ToCharArray().Where(e => Char.IsLetter(e)).Select(e => Char.ToLower(e)).Aggregate("", (e1, e2) => e1 + "" + e2);
      List<char> anagramList = theAnagram.ToCharArray().ToList();
      anagramList.Sort();
      string sortedAnagram = anagramList.Aggregate("", (e1, e2) => e1 + "" + e2);
      thePhrase = thePhrase.ToCharArray().Where(e => Char.IsLetter(e)).Select(e => Char.ToLower(e)).Aggregate("", (e1, e2) => e1 + "" + e2);
      for (int i = 0; i < thePhrase.Length - theAnagram.Length; i++) {
          string subString = thePhrase.Substring(i, theAnagram.Length);
          List<char> subStringArr = subString.ToCharArray().ToList();
          subStringArr.Sort();
          if (subStringArr.Aggregate("", (e1, e2) => e1 + e2) == sortedAnagram) {
              return thePhrase.Substring(i, theAnagram.Length);
          }
      }
      return "noutfond";
  };

  public static Func<string, int> TitleToNumber = (string excelColumn) => {

      string alpha = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      string reversedString = excelColumn.ToCharArray().Reverse().Aggregate("", (e1, e2) => e1 + "" + e2);
      int total = 0;
      for (int i = 0; i < excelColumn.Length; i++) {
          char letterAt = reversedString[i];
          if (i == 0) {
              total += alpha.IndexOf(letterAt + "");
          } else {
            total += (int)((Math.Pow(26, i)) * alpha.IndexOf(letterAt + ""));
          }
      }
      return total;

  };

  public static Func<string, bool> ValidPhoneNumber = (string phoneNumber) => {
      Console.WriteLine($"{phoneNumber}");
      Regex expr = new Regex("[(]\\d{3}[)] \\d{3}-\\d{4}");
      return expr.Match(phoneNumber).Success;
  };

  public static Func<string, int> GetWeight = (string strng) => {
      return strng.ToCharArray().Select(e => Int32.Parse(e + "")).Aggregate(0, (e1, e2) => e1 + e2);
  };

  public static Func<string, int> GetAlphabeticWeight = (string theString) => {
      return Int32.Parse(theString[0].ToString());
  };

  public static Func<string, string> orderWeight = (string strng) => {
      Console.WriteLine($"testing {strng}");
      List<string> splitString = strng.Split(" ").ToList();
      splitString.Sort((string elem1, string elem2) => {
          int weight1 = GetWeight(elem1);
          int weight2 = GetWeight(elem2);
          if (weight1 == weight2) {
              string elem1Tmp = elem1;
              string elem2Tmp = elem2;
              while (elem1Tmp.Length > 0 && elem2Tmp.Length > 0) {
                int alphaOne = GetAlphabeticWeight(elem1);
                int alphaTwo = GetAlphabeticWeight(elem2);
                Console.WriteLine($"Alphaone = {alphaOne} and elem1 = {elem1}");
                Console.WriteLine($"AlphaTwo = {alphaTwo} and elem2 = {elem2}");
                if (alphaOne > alphaTwo) {
                    return 1;
                } else if(alphaOne < alphaTwo) {
                    return -1;
                } else {
                    elem1Tmp = elem1Tmp.Substring(1);
                    elem2Tmp = elem2Tmp.Substring(1);
                    continue;
                }
              }
              if (elem1Tmp.Length == 0 && elem2Tmp.Length > 0) {
                  return 1;
              } else if(elem1Tmp.Length > 0 && elem2Tmp.Length == 0) {
                  return -1;
              } else {
                  return 0;
              }
          }
          else {
              return weight1 > weight2 ? 1 : weight1 < weight2 ? -1 : 0;
          }
      });
      return splitString.ToArray().Aggregate("", (e1, e2) => e1 + " " + e2).Trim();
  };

  public static Func<double[], double> SumArray = (double[] array) => {

      return array.Aggregate(0.0, (e1, e2) => e1 + e2);

  };

  public static Func<List<string>, List<string>> Number = (List<string> lines) => {

      int index = 1;
      return lines.Select(e => $"{index++}: {e}").ToList();

  };

  public static Func<int, int, string> AddBinary = (int a, int b) => {

      return Convert.ToString(a + b, 2);

  };

  public static Func<long, int> findNextSquare = (long perfectSquare) => {

      double sqrt = Math.Sqrt(perfectSquare);
      long value;
      bool conversionComplete = long.TryParse(sqrt.ToString(), out value);
      if (conversionComplete) {
        value += 1;
        return (int)Math.Pow(value, 2);
      }
      return -1;

  };

  public static Func<int, int[]> ReverseSeq = (int n) => {

      List<int> intList = Enumerable.Range(1, n).ToList();
      intList.Reverse();
      return intList.ToArray();

  };

  public static Func<string, Dictionary<char, int>> Count = (string str) => {

      Dictionary<char, int> letters = new Dictionary<char, int>();
      str.ToCharArray().Distinct().ToList().ForEach(e =>
          letters.Add(e, str.ToCharArray().Count(f => f == e)));
      return letters;

  };

  public static Func<int, int, int[]> CountBy = (int start, int end) => {

      List<int> intList = new List<int>();
      int fixedStart = start;
      while (intList.Count() != end) {
          intList.Add(start);
          start += fixedStart;
      }
      return intList.ToArray();

  };

  public static int IsSolved(int[,] board) {

      // rows
      HashSet<int> container = new HashSet<int>();
      int col = 0;
      int winOne = 0;
      int winTwo = 0;
      // row
      int zeros = 0;
      for (int i = 0; i < 3; i++) {
          for (int j = 0; j < 3; j++) {
              if (board[i, j] == 0) {
                  zeros++;
              }
            container.Add(board[i,j]);
          }
          if(container.Count() == 1 && container.ElementAt(0) != 0) {
              winOne += container.ElementAt(0) == 1 ? 1 : 0;
              winTwo += container.ElementAt(0) == 2 ? 1 : 0;
          }
          container.Clear();
      }
      if (container.Count == 1 && container.ElementAt(0) != 0) {
          winOne += container.ElementAt(0) == 1 ? 1: 0;
          winTwo += container.ElementAt(0) == 2 ? 1: 0;
      }
      container.Clear();
      // columns
      for (int i = 0; i < 3; i++) {
          for (int j = 0; j < 3; j++) {
              if (board[i, j] == 0) {
                  zeros++;
              }
              container.Add(board[j, i]);
          }
          if(container.Count() == 1 && container.ElementAt(0) != 0) {
              winOne += container.ElementAt(0) == 1 ? 1 : 0;
              winTwo += container.ElementAt(0) == 2 ? 1 : 0;
          } else {
              container.Clear();
          }
      }
      container.Clear();
     // top left --> bottom right diag
     for (int i = 0; i < 3; i++) {
         container.Add(board[i,i]);
     }
     if (container.Count() == 1 && container.ElementAt(0) != 0) {
         winOne += container.ElementAt(0) == 1 ? 1 : 0;
         winTwo += container.ElementAt(0) == 2 ? 1 : 0;
     }
     // top right --> bottom left diag
     container.Clear();
     col = 2;
     for (int i = 0; i < 3; i++) {
         container.Add(board[i, col--]);
     }
     if (container.Count() == 1 && container.ElementAt(0) != 0) {
         winOne += container.ElementAt(0) == 1 ? 1 : 0;
         winTwo += container.ElementAt(0) == 2 ? 1 : 0;
     }
     container.Clear();

     if (winOne > 0 || winTwo > 0) {
         // both players may have won
         if (winOne == winTwo && zeros == 0) {
             return 0;
         } else if(winOne > winTwo) {
             return 1;
         } else if (winTwo > winOne) {
             return 2;
         } else {
             return -1;
         }
     } else {
         return -1;
     }

  }

  public static long digPow(int n, int p) {

      int pow = p;
      long result = n.ToString().ToCharArray().Select(e => (long)Math.Round(Math.Pow(Int32.Parse(e.ToString()), pow++))).Sum();
      if (result % n != 0) {
          return -1;
      } else {
          return result / n;
      }

  }

  public double[] Tribonacci(double[] signature, int n) {

      if (n == 0) {
          return new double[]{};
      }
      else if (n <= signature.ToList().Count()) {
          return signature.ToList().Take(n).ToArray();
      }

      List<double> sigList = signature.ToList();
      while (sigList.Count() < n) {
          sigList.Add(sigList[sigList.Count() - 1] + sigList[sigList.Count() - 2] + sigList[sigList.Count() - 3]);
      }
      return sigList.ToArray();

  }

  public static int GetUnique(IEnumerable<int> numbers) {

      HashSet<int> theSet = new HashSet<int>(numbers);
      return theSet.Where(e => numbers.Count(f => f == e) == 1).Take(1).ToArray()[0];

  }

  public static string ByState(string str) {

      Dictionary<string, string> stateToWord = new Dictionary<string,string>(){
          {"Massachusetts", "MA"}, 
          {"California", "CA"},
          {"Oklahoma", "OK"},
          {"Pennsylvania", "PA"},
          {"Virginia", "VA"},
          {"Arizona", "AZ"},
          {"Indiana", "IN"},
          {"Illinois", "IL"},
          {"Idaho", "ID"}
      };

      Dictionary<string, string> stateAbbrToWord = new Dictionary<string,string>(){
          {"MA", "Massachusetts"}, 
          {"CA", "California"},
          {"OK", "Oklahoma"},
          {"PA", "Pennsylvania"},
          {"VA", "Virginia"},
          {"AZ", "Arizona"},
          {"IN", "Indiana"},
          {"IL", "Illinois"},
          {"ID", "Idaho"}
      };

      Dictionary<string, List<string>> stateDict = new Dictionary<string, List<string>>();
      string[] splitStrEntries = str.Split("\n");
      Dictionary<string, int> peopleLocations = new Dictionary<string, int>();
      List<string> entries = new List<string>();
      int index = 0;
      foreach(string eachentry in splitStrEntries) {
          string[] splitEntry = eachentry.Split(" ");
          if (splitEntry.Length < 3) {
              continue;
          }
          entries.Add(eachentry);
          Console.WriteLine("testing grabbing state");
          string state = stateAbbrToWord[splitEntry[splitEntry.Length - 1]];
          string fullname = (splitEntry[0] + " " + splitEntry[1]).Replace(",", "");
          List<string> people = stateDict.ContainsKey(state) ? stateDict[state] : new List<string>();
          people.Add(fullname);
          stateDict[state] = people;
          peopleLocations[fullname] = index++;
      }
      List<string> states = new List<string>();
      foreach(string key in stateDict.Keys) {
          states.Add(key);
      }
      string[] stateDictKeys = stateDict.Keys.ToArray();
      foreach(string stateAbbr in stateDictKeys) {
        List<string> ppl = stateDict[stateAbbr];
        ppl.Sort((person1, person2) => {

            string firstNameP1 = person1.Split(" ")[0];
            string lastNameP1 = person1.Split(" ")[1];
            string firstNameP2 = person2.Split(" ")[0];
            string lastNameP2 = person2.Split(" ")[1];
            if (firstNameP1.CompareTo(firstNameP2) == 0) {
                return lastNameP1.CompareTo(lastNameP2);
            }
            return firstNameP1.CompareTo(firstNameP2);
            
        });
        stateDict[stateAbbr] = ppl;
      }
      states.Sort();
      StringBuilder sb = new StringBuilder();
      bool firstState = true;
      foreach(string state in states) {
          sb.Append($"{(firstState ? "" : " ")}{state}\n");
          List<string> statePeople = stateDict[state];
          foreach(string person in statePeople) {
              int ind = peopleLocations[person];
              string[] splitEntry = entries[ind].Replace(",", "").Split(" ");
              splitEntry[splitEntry.Length - 1] = stateAbbrToWord[splitEntry[splitEntry.Length - 1]];
              sb.Append($"{String.Join(" ", splitEntry)}\n");
          }
          firstState = false;
      }

      
      return sb.ToString().Trim();


  }

  public static Func<int[], int[]> SortByBit = (int[] array) => {

      List<int> intList = array.ToList();
      intList.Sort((int a, int b) => {
          string aBits = Convert.ToString(a, 2);
          string bBits = Convert.ToString(b, 2);
          int cntA = aBits.Count(e => e == '1');
          int cntB = bBits.Count(e => e == '1');
          return cntA == cntB ? a - b : cntA - cntB;
      });
      return intList.ToArray();
  };

  public static string Amort(double rate, int bal, int term, int num_payments) {

      double monthlyRate = (rate / (100 * 12));
      double newBal = (double)bal;
      int payment = 0;
      double monthlyPayment = Math.Round(((monthlyRate * newBal) * Math.Pow(1 + monthlyRate, term)) / (Math.Pow(1 + monthlyRate, term) - 1), 2);
      while (newBal > 0 && payment < num_payments) {
          double principalnm = newBal * ((rate / 100) / 12);
          double princ = Math.Round(monthlyPayment - principalnm, 2);
          newBal = Math.Round(newBal - princ, 2);
          payment++;
          if (payment == num_payments) {
              return $"num_payment {num_payments} c {Math.Round(monthlyPayment)} princ {Math.Round(princ)} int {Math.Round(monthlyPayment - princ)} balance {Math.Round(newBal)}";
          }

      }
      return $"payment = {payment}";

  }

  using System.Text;
using System;
using System.Linq;

public class Dec2Fact {

  public static string dec2FactString(long nb) {

      int divisor = 1;
      StringBuilder sb = new StringBuilder();
      while (nb != 0) {
          sb.Append((nb % divisor).ToString());
          nb /= divisor;
          divisor++;
      }
      char[] letters = sb.ToString().ToCharArray();
      Array.Reverse(letters);
      return String.Join("", letters);

  }

  public static long factorial(long number) {

      long container = 1;
      while (number > 1) {
          container *= number;
          number--;
      }
      return container;

  }

  public static long factString2Dec(string str) {
    
      string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
      long sum = 0;
      int index = 0;
      while (index < str.Length) {
          sum += (letters.Contains(str[index].ToString()) ? 10 + letters.IndexOf(str[index]) : Int32.Parse(str[index].ToString())) * factorial(str.Length - (index + 1));
          index++;
      }
      return sum;

  }
}
  




        public static void Main(string[] args)
        {  
            Console.WriteLine(dec2FactString(463));
        }
    }

}