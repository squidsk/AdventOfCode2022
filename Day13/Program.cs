/*
 * User: skalmar
 * Date: 2/2/2023
 * Time: 10:25 AM
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day13
{
    class Program
    {
        public static void Main(string[] args) {
            Part1("test.txt");
            Part1("test2.txt");
            Part1("input.txt");
            //Part2("test.txt");
            //Part2("input.txt");

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static void Part1(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
                int sum = 0;
                int numberOfPairs = 1;
                while(reader.Peek() != -1 ) {
                    String packet1 = reader.ReadLine();
                    String packet2 = reader.ReadLine();
                    int p1Index = 0;
                    int p2Index = 0;
                    bool orderedDetermined = false;
                    
                    if(AreListsInOrder(ref packet1, ref packet2, ref p1Index, ref p2Index, ref orderedDetermined)) {
                        sum += numberOfPairs;
                        Console.WriteLine("Packet pair {0} is in order",numberOfPairs);
                    } else {
                        Console.WriteLine("Packet pair {0} is NOT in order",numberOfPairs);
                    }
                    
                    
                    numberOfPairs += 1;
                    reader.ReadLine();
                }

                Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, sum);
            }
        }

        static void Part2(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
                // TODO: Implement Functionality Here

                Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, "Put solution here!");
            }
        }
        
        static bool AreListsInOrder(ref string packet1, ref string packet2, ref int p1Index, ref int p2Index, ref bool orderDetermined) {
            orderDetermined = false;
            p1Index += 1;
            p2Index += 1;

            while(p1Index < packet1.Length && p2Index < packet2.Length) {
                if(Char.IsDigit(packet1[p1Index]) && Char.IsDigit(packet2[p2Index])){
                    if(!AreDigitsInOrder(packet1, packet2, ref p1Index, ref p2Index, out orderDetermined))
                        return false;
                    if(orderDetermined)
                        return true;
                } else if (packet1[p1Index] != ']' && packet2[p2Index] == ']') {
                    //while(packet1[p1Index] != ']') p1Index += 1;
                    if(orderDetermined) return true;
                    return false;
                } else if (packet1[p1Index] == ']') {
                    int closeBracketCount = 1;
                    //if(Char.IsDigit(packet2[p2Index])) orderDetermined = true;
                    if(packet2[p2Index] != ']') orderDetermined = true;
                    while(closeBracketCount > 0) {
                        if(packet2[p2Index] == ']') 
                            closeBracketCount -=1;
                        else {
                            if (packet2[p2Index] == '[')
                                closeBracketCount += 1;
                            p2Index += 1;
                        }
                    }
                    return true;
                } else if (packet1[p1Index] == '[' && /*Char.IsDigit(packet2[p2Index])){ //*/packet2[p2Index] != '[') {
                    if(!Char.IsDigit(packet2[p2Index]))
                        p2Index = p2Index;
                    packet2 = ConverNumberToList(packet2, p2Index);
                    if(!AreListsInOrder(ref packet1, ref packet2, ref p1Index, ref p2Index, ref orderDetermined))
                        return false;
                    if(orderDetermined)
                        return true;
                } else if (packet1[p1Index] != '[' && packet2[p2Index] == '[') {
                    packet1 = ConverNumberToList(packet1, p1Index);
                    if(!AreListsInOrder(ref packet1,ref packet2, ref p1Index, ref p2Index, ref orderDetermined))
                        return false;
                    if(orderDetermined)
                        return true;
                } else if (packet1[p1Index] == '[' && packet2[p2Index] == '[') {
                    if(!AreListsInOrder(ref packet1,ref  packet2, ref p1Index, ref p2Index, ref orderDetermined))
                        return false;
                    if(orderDetermined)
                        return true;
                } else {
                    //when you get a comma just do nothing
                }
                p1Index += 1;
                p2Index += 1;
            }
            
            if(p1Index < packet1.Length) return false;
            return true;
        }

        static bool AreDigitsInOrder(string packet1, string packet2, ref int p1, ref int p2, out bool orderDetermined) {
            orderDetermined = false;
            if (Char.IsDigit(packet1[p1 + 1]) && Char.IsDigit(packet2[p2 + 1])) {
                p1 += 1;
                p2 += 1;
            } else if (Char.IsDigit(packet1[p1 + 1])) {
                orderDetermined = true;
                return false;
            } else if (Char.IsDigit(packet2[p2 + 1])) {
                orderDetermined = true;
                p2 += 1;
            } else {
                //both single digits, if packet2 comes before packet1 it is an error
                if (packet2[p2] < packet1[p1]){
                    orderDetermined = true;
                    return false;
                }
                if (packet1[p1] < packet2[p2])
                    orderDetermined = true;
            }
            return true;
        }

        static string ConverNumberToList(string packet, int index) {
            if(index + 1 >= packet.Length) return packet;
            if(Char.IsDigit(packet[index+1]))
                return packet.Substring(0, index) + "[" + packet.Substring(index,2) + "]" + packet.Substring(index+2);
            return packet.Substring(0, index) + "[" + packet[index] + "]" + packet.Substring(index+1);
        }
    }
}