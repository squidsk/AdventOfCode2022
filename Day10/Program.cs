/*
 * User: skalmar
 * Date: 1/24/2023
 * Time: 11:04 AM
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day10
{
    class Program
    {
        public static void Main(string[] args) {
            Part1("test.txt");
            Part1("input.txt");
            Part2("test.txt");
            Part2("input.txt");

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static void Part1(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
                int register = 1;
                int cycleNumber = 0;
                int sumOfSignalStrength = 0;
                
                while(reader.Peek() != -1) {
                    string[] lineParts = reader.ReadLine().Split();
                    string operation = lineParts[0];
                    
                    cycleNumber += 1;
                    addToSumOfSignalStrength(register, cycleNumber, ref sumOfSignalStrength);
                    
                    if(operation.Equals("addx")) {
                        cycleNumber += 1;
                        addToSumOfSignalStrength(register, cycleNumber, ref sumOfSignalStrength);
                        register += int.Parse(lineParts[1]);
                    }
                }

                Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, sumOfSignalStrength);
            }
        }

        static void addToSumOfSignalStrength(int register, int cycleNumber, ref int sumOfSignalStrength) {
            if((cycleNumber-20)%40 == 0)
                sumOfSignalStrength += cycleNumber*register;
        }

        static void Part2(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
                int register = 1;
                int cycleNumber = 0;
                char[,] screen = new char[6,40];
                
                while(reader.Peek() != -1) {
                    string[] lineParts = reader.ReadLine().Split();
                    string operation = lineParts[0];
                    
                    cycleNumber += 1;
                    if(cycleNumber >= 40)
                        cycleNumber = cycleNumber;
                    writeToCycle(cycleNumber - 1, register, screen);
                    if(operation.Equals("addx")) {
                        cycleNumber += 1;
                        writeToCycle(cycleNumber - 1, register, screen);
                        register += int.Parse(lineParts[1]);
                    }
                }
                
                Console.WriteLine("The solution to Part 2 with inputfile: {0} is:", filename);
                Console.WriteLine("------------------------------------------");
                for (int i = 0; i < 6; i++) {
                    string output = "";
                    for (int j = 0; j < 40; j++) {
                        output += screen[i,j];
                    }
                    Console.WriteLine("|" + output + "|");
                }
                Console.WriteLine("------------------------------------------");
            }
        }

        static void writeToCycle(int cycleNumber, int register, char[,] screen) {
            int row = cycleNumber / 40;
            int col = cycleNumber % 40;
            //int pixel = register % 40;
            
            if(register >= col - 1 && register <= col + 1) {
                screen[row,col] = '#';
            } else {
                screen[row,col] = '.';
            }
        }
    }
}