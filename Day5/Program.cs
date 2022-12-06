/*
 * Created by SharpDevelop.
 * User: Mom and Dad
 * Date: 2022-12-04
 * Time: 5:02 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day5
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
		
		static Stack<char>[] GetCratesFromReader(StringReader reader) {
			Stack<char>[] stacksOfCrates = new Stack<char>[9];
			for(int i = 0; i < stacksOfCrates.Length; i += 1) stacksOfCrates[i] = new Stack<char>();
			
			string line = reader.ReadLine();
			while(line.Contains("[")){
				var lineByStack = line.SplitBy(4);
				
				int count = 0;
				foreach(var crate in lineByStack) {
					if(!String.IsNullOrWhiteSpace(crate)) {
						stacksOfCrates[count].Push(crate[1]);
					}
					count += 1;
				}
				line = reader.ReadLine();
			}
			line=reader.ReadLine();
			
			//reorder stacks
			for(int i=0; i<9; i+=1){
				Stack<char> temp = new Stack<char>();
				while(stacksOfCrates[i].Count > 0) temp.Push(stacksOfCrates[i].Pop());
				stacksOfCrates[i] = temp;
			}
			return stacksOfCrates;
		}
		
		static void Part1(string filename) {
			using(StringReader reader = new StringReader(File.ReadAllText(filename))){
				Stack<char>[] stacksOfCrates = GetCratesFromReader(reader);
				string[] separators = {"move", "from", "to"};
				string line;
				
				while(reader.Peek() != -1){
					line = reader.ReadLine();
					Match m = Regex.Match(line, "move (\\d+) from (\\d+) to (\\d+)");
					int numCratesToMove = int.Parse(m.Groups[1].Value);
					for(int i = 0; i < numCratesToMove; i += 1) {
						int fromCrate = int.Parse(m.Groups[2].Value) - 1;
						int toCrate = int.Parse(m.Groups[3].Value) - 1;
						char charToMove = stacksOfCrates[fromCrate].Pop();
						stacksOfCrates[toCrate].Push(charToMove);
					}
				}
				
				string result = "";
				foreach(var crate in stacksOfCrates)
					if(crate.Count > 0) result += crate.Peek();
				
				Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, result);
			}
		}

		static void Part2(string filename) {
			using(StringReader reader = new StringReader(File.ReadAllText(filename))){
				Stack<char>[] stacksOfCrates = GetCratesFromReader(reader);
				string[] separators = {"move", "from", "to"};
				string line;

				while(reader.Peek() != -1){
					line = reader.ReadLine();
					Match m = Regex.Match(line, "move (\\d+) from (\\d+) to (\\d+)");
					int numCratesToMove = int.Parse(m.Groups[1].Value);
					int fromCrate = int.Parse(m.Groups[2].Value) - 1;
					int toCrate = int.Parse(m.Groups[3].Value) - 1;
					Stack<char> charsToMove = new Stack<char>();
					for(int i = 0; i < numCratesToMove; i += 1) {
						charsToMove.Push(stacksOfCrates[fromCrate].Pop());
					}
					while(charsToMove.Count > 0) stacksOfCrates[toCrate].Push(charsToMove.Pop());
				}
				
				string result = "";
				foreach(var crate in stacksOfCrates)
					if(crate.Count > 0) result += crate.Peek();

				Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, result);
			}
		}
	}
}