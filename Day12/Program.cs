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
    class Point {
        internal int Row;
        internal int Col;
        
        public Point(int x, int y) {
            Row = x;
            Col = y;
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
            string[] lines = File.ReadAllText(filename).Split('\n');
            int[,] maze = new int[lines.Length,lines[0].Length];
            int[,] mazePath = new int[lines.Length,lines[0].Length];
            Point startPoint = null;
            Point endPoint = null;
            
            ReadMaze(lines, maze, ref startPoint, ref endPoint);
            InitializeMazePath(mazePath);
            recSolve(maze, mazePath, startPoint, 0, -1);

            Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, mazePath[endPoint.Row,endPoint.Col]);
            
        }

        static void Part2(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
                string[] lines = File.ReadAllText(filename).Split('\n');
                int[,] maze = new int[lines.Length,lines[0].Length];
                int[,] mazePath = new int[lines.Length,lines[0].Length];
                List<Point> startPoints = new List<Point>();
                Point endPoint = null;
                int shortestPath = int.MaxValue;
                
                ReadMaze2(lines, maze, ref startPoints, ref endPoint);
                foreach (var point in startPoints) {
                    InitializeMazePath(mazePath);
                    recSolve(maze, mazePath, point, 0, -1);
                    if(shortestPath > mazePath[endPoint.Row, endPoint.Col]) {
                        shortestPath = mazePath[endPoint.Row, endPoint.Col];
                    }
                }
                

                Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, shortestPath);
            }
        }

        static void ReadMaze(string[] lines, int[,] maze, ref Point startPoint, ref Point endPoint) {
            for (int i = 0; i < lines.Length; i++) {
                for (int j = 0; j < lines[0].Length; j++) {
                    if (lines[i][j] == 'E') {
                        maze[i, j] = 27;
                        endPoint = new Point(i,j);
                    } else if (lines[i][j] == 'S') {
                        maze[i, j] = 0;
                        startPoint = new Point(i,j);
                    } else {
                        maze[i, j] = lines[i][j] - 'a' + 1;
                    }
                }
            }
        }
        
        static void ReadMaze2(string[] lines, int[,] maze, ref List<Point> startPoints, ref Point endPoint) {
            for (int i = 0; i < lines.Length; i++) {
                for (int j = 0; j < lines[0].Length; j++) {
                    if (lines[i][j] == 'E') {
                        maze[i, j] = 26;
                        endPoint = new Point(i,j);
                    } else if (lines[i][j] == 'S') {
                        maze[i, j] = 0;
                    } else {
                        maze[i, j] = lines[i][j] - 'a';
                    }
                    
                    if(maze[i,j] == 0)
                        startPoints.Add(new Point(i,j));

                }
            }
        }
        static void InitializeMazePath(int[,] mazePath){
            int rows = mazePath.GetLength(0);
            int cols = mazePath.GetLength(1);
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    mazePath[i,j] = int.MaxValue;
                }
            }
        }
        
        static void recSolve(int[,] maze, int[,] mazePath, Point startPoint, int curDistance, int previousHeight){
            int i = startPoint.Row;
            int j = startPoint.Col;
            if(validLocation(maze.GetLength(0), maze.GetLength(1), i, j) && maze[i,j] - previousHeight <= 1){
                int lastDistance = mazePath[i,j];
                if(lastDistance > curDistance){
                    mazePath[i,j] = curDistance;
                    curDistance += 1;
                    recSolve(maze, mazePath, new Point(i-1,j), curDistance, maze[i,j]);
                    recSolve(maze, mazePath, new Point(i+1,j), curDistance, maze[i,j]);
                    recSolve(maze, mazePath, new Point(i,j-1), curDistance, maze[i,j]);
                    recSolve(maze, mazePath, new Point(i,j+1), curDistance, maze[i,j]);
                }
            }
        }

        static bool validLocation(int length, int width, int i, int j){
            return (i >=0 && j>= 0 && i<length && j<width);
        }

    }
}