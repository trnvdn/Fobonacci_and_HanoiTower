using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace HanoiTower;

internal class Program
{
    static void Main(string[] args)
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine();
            Console.WriteLine("Enter num of operation:\n1 for Hanoi tower\n2 for Fibonacci num via recursion\n3 for Fibonacci num via iterations\n4 for Fibonacci num via closed form formula\n0 for exit");
            Console.WriteLine();
            int num = 0;
            string? choose = Console.ReadLine();
            while (string.IsNullOrEmpty(choose) || string.IsNullOrWhiteSpace(choose))
            {
                Console.WriteLine("Error!Try again.");
                choose = Console.ReadLine();
            }
            while (int.TryParse(choose, out num) == false)
            {
                Console.WriteLine("Error!Enter num.");
                choose = Console.ReadLine();
            }

            switch (num)
            {
                case 1:
                    Hanoi();
                    break;
                case 2:
                    int fibIndexRecursion = IndexValidation();
                    Stopwatch recursionWatch = new Stopwatch();
                    recursionWatch.Start();
                    Console.WriteLine($"Answer : {FibonacciRecursion(fibIndexRecursion)}");
                    recursionWatch.Stop();
                    Console.WriteLine($"Time : {recursionWatch.Elapsed}");
                    break;
                case 3:
                    int fibIndexIterations = IndexValidation();
                    Stopwatch iterativelyWatch = new Stopwatch();
                    iterativelyWatch.Start();
                    Console.WriteLine($"Answer : {FibonacciIteratively(fibIndexIterations)}");
                    iterativelyWatch.Stop();
                    Console.WriteLine($"Time : {iterativelyWatch.Elapsed}");
                    break;
                case 4:
                    int fibIndexExpression = IndexValidation();
                    Stopwatch expressionWatch = new Stopwatch();
                    expressionWatch.Start();
                    Console.WriteLine($"Answer : {FibonacciIteratively(fibIndexExpression)}");
                    expressionWatch.Stop();
                    Console.WriteLine($"Time : {expressionWatch.Elapsed}");
                    break;
                case 0:
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Unknown request!");
                    break;
            }
        }

        int IndexValidation()
        {
            Console.WriteLine("Enter index");
            string line = Console.ReadLine();
            int index = 0;
            while (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine("Error.Line must contain num");
                line = Console.ReadLine();
            }
            while (int.TryParse(line, out index) == false)
            {
                Console.WriteLine("Error!.Enter num.");
                line = Console.ReadLine();
            }

            while (index < 0)
            {
                Console.WriteLine("Index can be only positive.Try again.");
                line = Console.ReadLine();
                while (int.TryParse(line, out index) == false)
                {
                    Console.WriteLine("Error!Enter num.");
                    line = Console.ReadLine();
                }
            }

            return index;
        }
        void Hanoi()
        {
            Stopwatch stopwatch = new Stopwatch();
            var pin1 = new List<string>();
            var pin2 = new List<string>();
            var pin3 = new List<string>();
            string slice = "";
            bool success = int.TryParse(Console.ReadLine(), out int s);
            while (!success)
            {
                Console.WriteLine("Enter num!");
                success = int.TryParse(Console.ReadLine(), out s);
            }
            stopwatch.Start();
            string downborder = string.Concat(Enumerable.Repeat("IIII" ,s+2));
            int stepnumber = 0;
            DrawHanoi(s,pin1);
            PrintTower();
            Solving(s,1, 2);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
             void Solving(int n, int i, int k)
        {
            if (n == 1)
            {
                Lifting(i,k);
                PrintTower();
                return;
            }
            int temp = 6 - i - k;
            Solving(n - 1, i, temp);
            Lifting(i,k);
            PrintTower();
            Solving(n-1,temp,k);
        }
        void PrintTower()
        {
            if (stepnumber != 0)
            {
                Console.WriteLine($"Step № {stepnumber}");
            }
            Console.WriteLine();
            if (pin1.Count != 0)
            {
                Console.WriteLine("1ST PIN");
                Console.WriteLine();
                Console.WriteLine();
                for (int index = pin1.Count - 1; index >= 0; index--)
                {
                    Console.WriteLine(pin1[index]);
                }
                Console.WriteLine(downborder);
                Console.WriteLine();
            }
            if (pin2.Count != 0)
            {
                Console.WriteLine("2ND PIN");
                Console.WriteLine();
                Console.WriteLine();
                for (int index = pin2.Count - 1; index >= 0; index--)
                {
                    Console.WriteLine(pin2[index]);
                }
                Console.WriteLine(downborder);
                Console.WriteLine();
            }
            if(pin3.Count != 0)
            {
                Console.WriteLine("3RD PIN");
                Console.WriteLine();
                Console.WriteLine();
                for (int index = pin3.Count - 1; index >= 0; index--)
                {
                    Console.WriteLine(pin3[index]);
                }
                Console.WriteLine();
                Console.WriteLine(downborder);
                Console.WriteLine();
            }

            stepnumber++;
            Console.WriteLine();
            Console.WriteLine();
        }

        void Lifting(int fst, int sec)
        {
            switch (fst)
                {
                    case 1:
                        slice = pin1.Last();
                        pin1.Remove(slice);
                        break;
                    case 2:
                        slice = pin2.Last();
                        pin2.Remove(slice);
                        break;
                    case 3:
                        slice = pin3.Last();
                        pin3.Remove(slice);
                        break;
                }

                switch (sec)
                {
                    case 1:
                        pin1.Add(slice);
                        break;
                    case 2:
                        pin2.Add(slice);
                        break;
                    case 3:
                        pin3.Add(slice);
                        break;
                }
        }
        
        void DrawHanoi(int n,List<string> pin)
        {
            string elems = "||||";
            
            for (int index = n; index > 0; index--)
            {
                string indent = string.Concat(Enumerable.Repeat("  " ,n-index + 2));
                string result = string.Concat(Enumerable.Repeat(elems , index));
                pin.Add(indent + result);
            }
        }
        }
        
        long FibonacciRecursion(long currentnum)
        {
            if (currentnum == 1 || currentnum == 0)
            {
                return currentnum;
            }

            return FibonacciRecursion(currentnum - 1) + FibonacciRecursion(currentnum - 2);
        }

        long FibonacciIteratively(int index)
        {
            long answer = 0;
            long temp;
            long num = 1;
            int counter = 0;
            while (counter != index)
            {
                temp = answer;
                answer = num;
                num += temp;
                counter++;
            }
            return answer;
        }

        double FibonacciMath(int index)
        {
            return (Math.Pow((1 + Math.Sqrt(5)), index) - Math.Pow((1 - Math.Sqrt(5)), index)) /
                   (Math.Pow(2, index) * Math.Sqrt(5));
        }
       
    }
}