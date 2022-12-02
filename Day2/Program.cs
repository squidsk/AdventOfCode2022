/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/2/2022
 * Time: 11:53 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace Day2
{
	class Program
	{
		const int ROCK = 'A';
		const int PAPER = 'B';
		const int SCISSORS = 'C';
		const int DRAW = 'Y';
		const int LOSE = 'X';
		const int WIN = 'Z';
		
		public static void Main(string[] args)
		{
			using(StringReader reader = new StringReader(File.ReadAllText("input.txt"))) {
				int gameScore = 0;
				
				while(reader.Peek() != -1) {
					string line = reader.ReadLine();
					char oppChar = line[0];
					char myChar = getMyChar(line[0], line[2]); // = line[2] - 'X' + ROCK;  //PART 1 code
					int roundScore = myChar - ROCK + 1;
					
					if(myChar == oppChar) roundScore += 3;
					if((myChar == ROCK && oppChar == SCISSORS) || (myChar == PAPER && oppChar == ROCK) || (myChar == SCISSORS && oppChar == PAPER)) roundScore += 6;
					else roundScore += 0;
					
					gameScore+=roundScore;
				}
				Console.WriteLine("The game score is: {0}.", gameScore);
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}

		static char getMyChar(char oppChar, char gameResult) {
			if(gameResult == DRAW) return oppChar;
			if(gameResult == WIN) return (char)((oppChar - ROCK + 1)%3 + ROCK);
			return (char)((oppChar - ROCK + 2)%3 + ROCK);
		}
	}
}