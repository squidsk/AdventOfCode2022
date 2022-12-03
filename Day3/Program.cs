/*
 * Created by SharpDevelop.
 * User: Mom and Dad
 * Date: 2022-12-02
 * Time: 10:02 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day3
{
	class Program
	{
		public static void Main(string[] args)
		{
			//Part1();
			Part2();
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		static void Part1() {
			using(StringReader reader = new StringReader(File.ReadAllText("input.txt"))){
				HashSet<char> foundLetters = new HashSet<char>();
				HashSet<char> duplicatedLetters = new HashSet<char>();
				int sumOfLetters = 0;
				
				while(reader.Peek() != -1){
					string line = reader.ReadLine();
					for(int i =0; i<line.Length/2; i+=1){
						foundLetters.Add(line[i]);
					}
					for(int i=line.Length/2; i<line.Length; i+=1){
						char currentLetter = line[i];
						if(foundLetters.Contains(currentLetter) && !duplicatedLetters.Contains(currentLetter)){
							int currentLetterValue;
							
							duplicatedLetters.Add(currentLetter);
							if(currentLetter <= 'Z') currentLetterValue = 26 + currentLetter - 'A' + 1;
							else currentLetterValue = currentLetter - 'a' + 1;
							sumOfLetters += currentLetterValue;
						}
					}
					foundLetters.Clear();
					duplicatedLetters.Clear();
				}
				Console.WriteLine("Part 1 - The sum of the letters found in both sides is: {0}", sumOfLetters);
			}
		}
		
		static void Part2(){
			using(StringReader reader = new StringReader(File.ReadAllText("input.txt"))){
				HashSet<char> firstBagLetters = new HashSet<char>();
				HashSet<char> secondBagDuplicates = new HashSet<char>();
				int sumOfLetters = 0;
				
				while(reader.Peek() != -1){
					string line = reader.ReadLine();
					for(int i =0; i<line.Length; i+=1){
						firstBagLetters.Add(line[i]);
					}
					line = reader.ReadLine();
					for(int i=0; i<line.Length; i+=1){
						char currentLetter = line[i];
						if(firstBagLetters.Contains(currentLetter)) secondBagDuplicates.Add(currentLetter);
					}
					line = reader.ReadLine();
					for(int i=0; i<line.Length; i+=1) {
						char currentLetter = line[i];
						if(secondBagDuplicates.Contains(currentLetter)){
							int currentLetterValue;
							
							if(currentLetter <= 'Z') currentLetterValue = 26 + currentLetter - 'A' + 1;
							else currentLetterValue = currentLetter - 'a' + 1;
							sumOfLetters += currentLetterValue;
							break;
						}
					}
					firstBagLetters.Clear();
					secondBagDuplicates.Clear();
				}
				Console.WriteLine("Part 2 - The sum of the letters found in both sides is: {0}", sumOfLetters);
			}
		}
	}
}