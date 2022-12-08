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
using System.Linq;

namespace Day7
{
	class Node {
		internal string name;
		internal int size;
		internal readonly HashSet<Node> contents;
		internal bool isDirectory;
		
		public Node(string name, int size = 0) {
			this.name = name;
			this.size = size;
			isDirectory = size == 0;
			contents = new HashSet<Node>();
		}
		
		public void AddContent(Node n){
			contents.Add(n);
		}
		
		
		public Node GetSubDirectory(string nodeName) {
			return contents.First(n => n.name.Equals(nodeName));
		}
		
		public void CalculateSize() {
			size = 0;
			foreach(Node n in contents){
				size += n.size;
			}
		}
	}
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

		static Node GetDirectoryTree(string filename)
		{
			Node root = new Node("/");
			Stack<Node> directoryStack = new Stack<Node>();
			directoryStack.Push(root);
			
			using(StringReader reader = new StringReader(File.ReadAllText(filename))){
				reader.ReadLine();
				while (reader.Peek() != -1) {
					String[] tokens = reader.ReadLine().Split(' ');
					if (tokens[1] == "cd") {
						if (tokens[2].Equals("..")) {
							directoryStack.Pop().CalculateSize();
						} else {
							Node n = directoryStack.Peek().GetSubDirectory(tokens[2]);
							directoryStack.Push(n);
						}
					} else {
						//ls case
						while (reader.Peek() != -1 && reader.Peek() != '$') {
							tokens = reader.ReadLine().Split(' ');
							if (tokens[0].Equals("dir")) {
								directoryStack.Peek().AddContent(new Node(tokens[1]));
							} else {
								directoryStack.Peek().AddContent(new Node(tokens[1], int.Parse(tokens[0])));
							}
						}
					}
				}
			}
			while (directoryStack.Count > 0) directoryStack.Pop().CalculateSize();
				
			return root;
		}
		static void Part1(string filename) {
			Node root = GetDirectoryTree(filename);
			Queue<Node> calcSize = new Queue<Node>();
			calcSize.Enqueue(root);
			
			int size = 0;
			while(calcSize.Count > 0) {
				if(calcSize.Peek().size < 100000) {
					size += calcSize.Peek().size;
				}
				foreach(Node n in calcSize.Peek().contents) {
					if(n.isDirectory) calcSize.Enqueue(n);
				}
				calcSize.Dequeue();
			}
			
			Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, size);
			
		}

		static void Part2(string filename) {
			Node root = GetDirectoryTree(filename);
			Queue<Node> calcSize = new Queue<Node>();
			int sizeRequired = 30000000 - (70000000 - root.size);
			calcSize.Enqueue(root);
			
			int minSize = root.size;
			while(calcSize.Count > 0) {
				int nextDirSize = calcSize.Peek().size;
				if(nextDirSize >= sizeRequired && nextDirSize < minSize) {
					minSize = nextDirSize;
				}
				foreach(Node n in calcSize.Peek().contents) {
					if(n.isDirectory) calcSize.Enqueue(n);
				}
				calcSize.Dequeue();
			}
			Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, minSize);
		}
	}
}