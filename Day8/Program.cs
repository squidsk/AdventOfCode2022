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
	
	class TreeHeights {
		internal int highestUp;
		internal int highestDown;
		internal int highestLeft;
		internal int highestRight;
		
		public TreeHeights(){ }

		public TreeHeights(int up, int down, int left, int right){
			highestDown = down;
			highestLeft = left;
			highestRight = right;
			highestUp = up;
		}
	}
	
	class Program
	{
		public static void Main(string[] args) {
			//Part1("test.txt");
			Part1("input.txt");
			//Part2("test.txt");
			//Part2("input.txt");

			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}

		static void Part1(string filename) {
			using(StringReader reader = new StringReader(File.ReadAllText(filename))){
				String[] line = File.ReadLines(filename).ToArray();
				int[][] forest = new int[line.Length+2][];
				
				forest[0] = new int[line[0].Length+2];
				forest[forest.Length-1] = new int[line[0].Length+2];
				for(int i = 0; i < forest[0].Length; i += 1) {
					forest[0][i] = -1;
					forest[forest.Length-1][i] = -1;
				}
				for(int i = 0; i < line.Length; i += 1) {
					forest[i+1] = new int[line[i].Length+2];
					forest[i+1][0] = -1;
					forest[i+1][line[i].Length+1] = -1;
					for(int j = 0; j < line[i].Length; j += 1) forest[i+1][j+1] = line[i][j] - '0';
				}
				
				TreeHeights[][] maxHeights = new TreeHeights[forest.Length][];
				maxHeights[0] = new TreeHeights[forest[0].Length];
				maxHeights[forest.Length-1] = new TreeHeights[forest[forest.Length-1].Length];
				for(int i = 0; i< maxHeights[0].Length; i += 1) {
					maxHeights[0][i] = new TreeHeights(-1,-1,-1,-1);
					maxHeights[maxHeights[0].Length - 1][i] = new TreeHeights(-1,-1,-1,-1);
				}
				for(int i = 1; i < maxHeights.Length; i += 1){
					maxHeights[i] = new TreeHeights[maxHeights[0].Length];
					for(int j = 1; j < maxHeights[0].Length; j += 1){
						maxHeights[i][j] = new TreeHeights();
					}
					maxHeights[i][0] = new TreeHeights(-1,-1,-1,-1);
					maxHeights[i][forest[forest.Length-1].Length - 1] = new TreeHeights(-1,-1,-1,-1);
				}
				
				int maxPos = maxHeights.Length - 1;
				for(int i = 1; i < maxPos; i += 1){
					for(int j = 1; j < maxPos; j += 1){
						maxHeights[i][j].highestUp = Math.Max(maxHeights[i-1][j].highestUp, forest[i-1][j]);
						maxHeights[maxPos - i][j].highestDown = Math.Max(maxHeights[maxPos-i+1][j].highestDown, forest[maxPos-i+1][j]);
						maxHeights[j][i].highestLeft = Math.Max(maxHeights[j][i-1].highestLeft, forest[j][i-1]);
						maxHeights[j][maxPos - i].highestRight = Math.Max(maxHeights[j][maxPos-i+1].highestRight, forest[j][maxPos-i+1]);
					}
				}
				
				displayForest(forest);
				//writeToFile(forest);

				int visibleTrees = 0;
				for(int i = 1; i < maxPos; i += 1){
					for(int j = 1; j < maxPos; j += 1){
						if(isVisible(forest[i][j], maxHeights[i][j])) visibleTrees += 1;
					}
				}

				Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, visibleTrees);
			}
		}

		static bool isVisible(int i, TreeHeights t) {
			return i > t.highestDown || i > t.highestLeft || i > t.highestRight || i > t.highestUp;
		}
		
		
		static void displayForest(int[][] forest){
			for(int i = 0; i < forest.Length; i += 1) {
				for(int j = 0; j < forest[i].Length; j += 1) {
					Console.Write(string.Format("{0,3}",forest[i][j]));
				}
				Console.WriteLine();
			}
		}
		
		static void writeToFile(int[][] forest) {
			StreamWriter writer = new StreamWriter("out.txt");
			for(int i = 0; i < forest.Length; i += 1) {
				for(int j = 0; j < forest[i].Length; j += 1) {
					writer.Write(string.Format("{0,3}",forest[i][j]));
				}
				writer.WriteLine();
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