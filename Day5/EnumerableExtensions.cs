/*
 * Created by SharpDevelop.
 * User: Mom and Dad
 * Date: 2022-12-04
 * Time: 10:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Day5
{
	/// <summary>
	/// Description of EnumerableExtensions.
	/// </summary>
	public static class EnumerableExtensions
	{
		public static IEnumerable<string> SplitBy(this string str, int chunkLength)
		{
			if (String.IsNullOrEmpty(str)) throw new ArgumentException();
			if (chunkLength < 1) throw new ArgumentException();

			for (int i = 0; i < str.Length; i += chunkLength)
			{
				if (chunkLength + i > str.Length)
					chunkLength = str.Length - i;

				yield return str.Substring(i, chunkLength);
			}
		}
	}
}
