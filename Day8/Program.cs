/*
 * Created by SharpDevelop.
 * User: Mom and Dad
 * Date: 2022-12-08
 * Time: 1:13 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day8
{
	class Program
	{
		public static void Main(string[] args) {
			Part1("test.txt");
			//Part1("input.txt");
			//Part2("test.txt");
			//Part2("input.txt");

			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}

		static void Part1(string filename) {
			using(StringReader reader = new StringReader(File.ReadAllText(filename))){
				String[] line = File.ReadLines(filename).ToArray();
				int[][] forest = new int[line.Length+2][];

				Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, "Put solution here!");
			}
		}

		static void Part2(string filename) {
			using(StringReader reader = new StringReader(File.ReadAllText(filename))){
				// TODO: Implement Functionality Here

				Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, "Put solution here!");
			}
		}
	}
}