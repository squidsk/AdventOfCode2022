/*
 * Created by SharpDevelop.
 * User: Mom and Dad
 * Date: 2022-12-03
 * Time: 9:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day4
{
	class Program
	{
		public static void Main(string[] args)
		{
			Part1();
			Part2();
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		static void Part1() {
			using(StringReader reader = new StringReader(File.ReadAllText("input.txt"))){
				int fullyContainsCount = 0;
				
				while(reader.Peek() != -1) {
					string[] elves = reader.ReadLine().Split(',');
					string[] firstElfRange = elves[0].Split('-');
					string[] secondElfRange = elves[1].Split('-');
					int e1Lower = int.Parse(firstElfRange[0]);
					int e1Upper = int.Parse(firstElfRange[1]);
					int e2Lower = int.Parse(secondElfRange[0]);
					int e2Upper = int.Parse(secondElfRange[1]);
					
					if((e1Lower >= e2Lower && e1Upper <= e2Upper) || (e2Lower >= e1Lower && e2Upper <= e1Upper)) fullyContainsCount += 1;
				}
				Console.WriteLine("Assignment pairs where one range fully contains the other: {0}", fullyContainsCount);
			}
		}
		
		static void Part2() {
			using(StringReader reader = new StringReader(File.ReadAllText("input.txt"))){
				int fullyContainsCount = 0;
				
				while(reader.Peek() != -1) {
					string[] elves = reader.ReadLine().Split(',');
					string[] firstElfRange = elves[0].Split('-');
					string[] secondElfRange = elves[1].Split('-');
					int e1Lower = int.Parse(firstElfRange[0]);
					int e1Upper = int.Parse(firstElfRange[1]);
					int e2Lower = int.Parse(secondElfRange[0]);
					int e2Upper = int.Parse(secondElfRange[1]);
					
					if((e1Lower >= e2Lower && e1Lower <= e2Upper) || (e1Upper >= e2Lower && e1Upper <= e2Upper) || (e2Lower >= e1Lower && e2Lower <= e1Upper) || (e2Upper >= e1Lower && e2Upper <= e1Upper)) fullyContainsCount += 1;
				}
				Console.WriteLine("Assignment pairs where one range fully contains the other: {0}", fullyContainsCount);
			}
		}
	}
}