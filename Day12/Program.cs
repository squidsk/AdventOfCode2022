/*
 * User: skalmar
 * Date: 2/1/2023
 * Time: 4:58 PM
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day12
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
            string[] lines = File.ReadAllText(filename).Split('\n');
            int[,] maze = new int[lines.Length,lines[0].Length];
            int[,] mazePath = new int[lines.Length,lines[0].Length];
            int startRow = 0;
            int startCol = 0;
            int endRow = 0;
            int endCol = 0;
            
            for (int i = 0; i < lines.Length; i++) {
                for (int j = 0; j < lines[0].Length; j++) {
                    mazePath[i,j] = -1;
                    if(lines[i][j] == 'E'){
                        maze[i,j] = 27;
                        endRow = i;
                        endCol = j;
                    }
                    else if(lines[i][j] == 'S'){
                        maze[i,j] = 0;
                        mazePath[i,j] = int.MaxValue;
                        startRow = i;
                        startCol = j;
                    }
                    else maze[i,j] = lines[i][j] - 'a' + 1;
                }
            }
            recSolve(maze, mazePath, startRow, startCol, -1);

            Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, mazePath[endRow,endCol]);
            
        }

        static void Part2(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
                // TODO: Implement Functionality Here

                Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, "Put solution here!");
            }
        }
        
        static void recSolve(int[,] maze, int[,] mazePath, int i, int j, int curDistance){
            if(validLocation(maze.GetLength(0), maze.GetLength(1), i, j)){
                if(mazePath[i,j] > curDistance){
                    mazePath[i,j] = curDistance;
                    curDistance += 1;
                    recSolve(maze, mazePath, i-1,j, curDistance);
                    recSolve(maze, mazePath, i+1,j, curDistance);
                    recSolve(maze, mazePath, i,j-1, curDistance);
                    recSolve(maze, mazePath, i,j+1, curDistance);
                }
            }
        }

        static bool validLocation(int length, int width, int i, int j){
            return (i >=0 && j>= 0 && i<length && j<width);
        }

    }
}