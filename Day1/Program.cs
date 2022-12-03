/*
 * User: Steve
 * Date: 12/9/2019
 * Time: 11:29 PM
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day1
{
	class Program
	{
		public static void Main(string[] args)
		{
			using(StringReader reader = new StringReader(File.ReadAllText("input.txt"))) {
				int maxFood = -1;
				int maxFood2 = -1;
				int maxFood3 = -1;
				int currentFood = 0;
				while(reader.Peek() != -1) {
					String line = reader.ReadLine();
					if(!line.Trim().Equals("")) {
						currentFood += int.Parse(line);
					} else {
						if(currentFood > maxFood) {
							int tempFood = maxFood;
							maxFood = currentFood;
							currentFood = tempFood;
						}
						if(currentFood > maxFood2) {
							int tempFood = maxFood2;
							maxFood2 = currentFood;
							currentFood = tempFood;
						}
						if(currentFood > maxFood3) {
							int tempFood = maxFood3;
							maxFood3 = currentFood;
							currentFood = tempFood;
						}
						currentFood = 0;
					}
				}

				Console.WriteLine(string.Format("The amount of food carried by the top three elves is: {0}", maxFood+maxFood2+maxFood3));
			}
			
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}