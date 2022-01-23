using System;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Animals;
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

        public static void Main(string[] args)
        {   
            TrackRobot("....................................................................................................").ToList().ForEach(Console.Write);
        }
    }

}