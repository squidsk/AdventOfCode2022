/*
 * User: skalmar
 * Date: 12/5/2022
 * Time: 4:04 PM
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    class Program
    {
        public static void Main(string[] args) {
            Part1("test.txt");
            Part1("test2.txt");
            Part1("input.txt");
            //Part2("test.txt");
            Part2("input.txt");

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static void Part1(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
    			string line = reader.ReadLine();
    			string match = line.Substring(0,1);
    			int positionOfMatch;
    			
    			for(positionOfMatch = 1 ; positionOfMatch < line.Length; positionOfMatch += 1){
    				int index = match.IndexOf(line[positionOfMatch]);
    				if(index != -1) {
    					match = match.Substring(index+1) + line[positionOfMatch];
    				} else {
    					match += line[positionOfMatch];
    					if(match.Length == 4) break;
    				}
    			}

                Console.WriteLine("The start of packet marker in file {0} is completed at character: {1}", filename, positionOfMatch+1);
            }
        }

        static void Part2(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
    			string line = reader.ReadLine();
    			string match = line.Substring(0,1);
    			int positionOfMatch;
    			
    			for(positionOfMatch = 1 ; positionOfMatch < line.Length; positionOfMatch += 1){
    				int index = match.IndexOf(line[positionOfMatch]);
    				if(index != -1) {
    					match = match.Substring(index+1) + line[positionOfMatch];
    				} else {
    					match += line[positionOfMatch];
    					if(match.Length == 14) break;
    				}
    			}

                Console.WriteLine("The start of packet marker in file {0} is completed at character: {1}", filename, positionOfMatch+1);
            }
        }
    }
}