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
        internal int up;
        internal int down;
        internal int left;
        internal int right;
        
        public TreeHeights(){ }

        public TreeHeights(int up, int down, int left, int right){
            this.down = down;
            this.left = left;
            this.right = right;
            this.up = up;
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
                    maxHeights[maxHeights.Length - 1][i] = new TreeHeights(-1,-1,-1,-1);
                }
                for(int i = 1; i < maxHeights.Length - 1; i += 1){
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
                        maxHeights[i][j].up = Math.Max(maxHeights[i-1][j].up, forest[i-1][j]);
                        maxHeights[maxPos - i][j].down = Math.Max(maxHeights[maxPos-i+1][j].down, forest[maxPos-i+1][j]);
                        maxHeights[j][i].left = Math.Max(maxHeights[j][i-1].left, forest[j][i-1]);
                        maxHeights[j][maxPos - i].right = Math.Max(maxHeights[j][maxPos-i+1].right, forest[j][maxPos-i+1]);
                    }
                }
                
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
            return i > t.down || i > t.left || i > t.right || i > t.up;
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
                String[] line = File.ReadLines(filename).ToArray();
                int[][] forest = new int[line.Length][];
                TreeHeights[][] treesVisible = new TreeHeights[line.Length][];
                
                for(int i = 0; i < line.Length; i += 1) {
                    forest[i] = new int[line.Length];
                    treesVisible[i] = new TreeHeights[line.Length];
                    for(int j = 0; j < line.Length; j += 1) {
                        forest[i][j] = line[i][j] - '0';
                        treesVisible[i][j] = new TreeHeights();
                    }
                }
                
                int maxPos = forest.Length - 1;
                for(int i = 1; i < maxPos; i += 1) {
                    //treesVisible[i][1].left = treesVisible[1][i].up = treesVisible[i][maxPos-1].right = treesVisible[maxPos-1][i].down = 1;
                    for(int j = 1; j < maxPos; j += 1) {
                        //treesVisible[i][j].left = forest[i][j-1] > forest[i][j-2] ? 1 : treesVisible[i][j-1].left + 1;
                        //treesVisible[j][i].up = forest[j-1][i] > forest[j-2][i] ? 1 : treesVisible[j-1][i].up + 1;
                        //treesVisible[i][maxPos-j].right = forest[i][maxPos-j+1] > forest[i][maxPos-j+2] ? 1 : treesVisible[i][maxPos-j+1].right + 1;
                        //treesVisible[maxPos-j][i].down = forest[maxPos-j+1][i] > forest[maxPos-j+2][i] ? 1 : treesVisible[maxPos-j+1][i].down + 1;
                        int k = j-2;
                        treesVisible[i][j].left = 1;
                        while(k >= 0 && forest[i][k+1] < forest[i][j]){
                            if(forest[i][k] >= forest[i][k+1]) treesVisible[i][j].left += 1;
                            k -= 1;
                        }
                        k = j-2;
                        treesVisible[j][i].up = 1;
                        while(k >= 0 && forest[k+1][i] < forest[j][i]){
                        	if(forest[k][i] >= forest[k+1][i]) treesVisible[j][i].up += 1;
                        	k -= 1;
                        }
                        k = 2;
                        treesVisible[i][maxPos-j].right = 1;
                        while(k <= j && forest[i][maxPos-j+k-1] < forest[i][maxPos-j]){
                        	if(forest[i][maxPos-j+k] >= forest[i][maxPos-j+k-1]) treesVisible[i][maxPos-j].right += 1;
                        	k += 1;
                        }
                        k = 2;
                        treesVisible[maxPos-j][i].down = 1;
                        while(k <= j && forest[maxPos-j+k-1][i] < forest[maxPos-j][i]){
                        	if(forest[maxPos-j+k][i] >= forest[maxPos-j+k-1][i]) treesVisible[maxPos-j][i].down += 1;
                        	k += 1;
                        }
                    }
                }
                
                int maxViewability = 0;
                int maxI=0, maxJ=0;
                for(int i = 1; i < maxPos; i += 1) {
                    for(int j = 1; j < maxPos; j += 1) {
                        int viewability = treesVisible[i][j].left * treesVisible[i][j].up * treesVisible[i][j].down * treesVisible[i][j].right;
                        if(viewability > maxViewability) {
                            //Console.WriteLine("New maximum ({2}) found at {0}, {1}",i, j, viewability);
                            maxViewability = viewability;
                            maxI = i;
                            maxJ = j;
                        }
                    }
                }
                
                Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1} at position ({2},{3})", filename, maxViewability, maxI, maxJ);
            }
        }
    }
}