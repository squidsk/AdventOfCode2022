/*
 * User: skalmar
 * Date: 1/23/2023
 * Time: 4:14 PM
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day9
{
    public class Point
    {
        public int X {get;set;}
        public int Y {get;set;}
        
        public Point(){}
        
        public Point(int x, int y) { X = x; Y = y; }
        
        #region Equals and GetHashCode implementation
        public override bool Equals(object obj)
        {
            Point other = obj as Point;
            if (other == null)
                return false;
            return this.X == other.X && this.Y == other.Y;
        }
        
        public override int GetHashCode()
        {
            int hashCode = 0;
            unchecked {
                hashCode += 1000000007 * X.GetHashCode();
                hashCode += 1000000009 * Y.GetHashCode();
            }
            return hashCode;
        }
        #endregion
        
        public bool isAdjacent(Point other) {
            return (Math.Abs(X - other.X) <= 1) && (Math.Abs(Y - other.Y) <= 1);
        }
    }
    
    class Program
    {
        public static void Main(string[] args) {
            Part1("test.txt");
            Part1("input.txt");
            Part2("test.txt");
            Part2("test2.txt");
            Part2("input.txt");

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static void Part1(string filename) {
            Point head = new Point();
            Point tail = new Point();
            HashSet<Point> pointsVisited = new HashSet<Point>();
            
            pointsVisited.Add(new Point());
            
            using(StringReader reader = new StringReader(File.ReadAllText(filename))) {
                while(reader.Peek() != -1) {
                    string[] lineParts = reader.ReadLine().Split();
                    char direction = lineParts[0][0];
                    int distance = int.Parse(lineParts[1]);

                    moveRope(head, tail, direction, distance, pointsVisited);
                }

                Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, pointsVisited.Count);
            }
        }

        static void moveRope(Point head, Point tail, char direction, int distance, HashSet<Point> pointsVisited) {
            for(;distance > 0; distance -= 1) {
                switch (direction) {
                        case 'R': head.Y += 1; break;
                        case 'L': head.Y -= 1; break;
                        case 'D': head.X -= 1; break;
                        case 'U': head.X += 1; break;
                        default: break;
                }
                
                
                if(!head.isAdjacent(tail)) {
                    if(head.X - tail.X != 0){
                        if(head.X > tail.X) tail.X += 1;
                        else tail.X -= 1;
                    }
                    if(head.Y - tail.Y != 0) {
                        if(head.Y > tail.Y) tail.Y += 1;
                        else tail.Y -= 1;
                    }
                    pointsVisited.Add(new Point(tail.X, tail.Y));
                }
            }
        }
        
        static void moveRope(Point[] points, char direction, int distance, HashSet<Point> pointsVisited) {
            for(;distance > 0; distance -= 1) {
                switch (direction) {
                        case 'R': points[0].Y += 1; break;
                        case 'L': points[0].Y -= 1; break;
                        case 'D': points[0].X -= 1; break;
                        case 'U': points[0].X += 1; break;
                        default: break;
                }
                
                Point p1, p2;
                for (int i = 0; i < 9; i++) {
                    p1 = points[i];
                    p2 = points[i+1];
                    
                    if(!p1.isAdjacent(p2)) {
                        if(p1.X - p2.X != 0){
                            if(p1.X > p2.X) p2.X += 1;
                            else p2.X -= 1;
                        }
                        if(p1.Y - p2.Y != 0) {
                            if(p1.Y > p2.Y) p2.Y += 1;
                            else p2.Y -= 1;
                        }
                    }
                }
                pointsVisited.Add(new Point(points[9].X, points[9].Y));
            }
        }
        
        static void Part2(string filename) {
            Point[] points = new Point[10];
            HashSet<Point> pointsVisited = new HashSet<Point>();
            
            pointsVisited.Add(new Point());
            for (int i = 0; i < 10; i++) {
                points[i] = new Point();
            }
            
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
                while(reader.Peek() != -1) {
                    string[] lineParts = reader.ReadLine().Split();
                    char direction = lineParts[0][0];
                    int distance = int.Parse(lineParts[1]);

                    moveRope(points, direction, distance, pointsVisited);
                }

                Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, pointsVisited.Count);
            }
        }
    }
}