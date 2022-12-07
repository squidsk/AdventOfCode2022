/*
 * Created by SharpDevelop.
 * User: Mom and Dad
 * Date: 2022-12-06
 * Time: 11:25 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day7
{
	class Node {
		internal string name;
		internal int size;
		List<Node> contents;
		
		public Node(string name) {
			this.name = name;
			size = 0;
			contents = new List<Node>();
		}
		
		public Node(string name, int size) {
			this.name = name;
			this.size = size;
		}
		
		public void AddContent(string name, int size=0){
			if(size == 0) contents.Add(new Node(name));
			else contents.Add(new Node(name,size));
		}
		
		private bool isDirectory() { return contents != null; }
		
		
	}
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