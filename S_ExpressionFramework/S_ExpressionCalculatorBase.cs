/*
 * S Expressino
 * Created by Brian Chaves
 * Created around December 24, 2019
 * Updated on December 24, 2019
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_ExpressionFramework
{
	/// <summary>
	/// This is S-expression calculator base class. adding virtual functions incase someone wants the ability to override them in the future
	/// </summary>
    public class S_ExpressionCalculatorBase
    {
		/// <summary>
		/// If debug mode is turned on, Console will display the soultion one bracket at a time for each line.
		/// </summary>
		private bool debugMode;
		public bool DebugMode
		{
			get { return debugMode; } 
			set { debugMode = value; }
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// 
		public S_ExpressionCalculatorBase()
		{

		}
		/// <summary>
		/// Read command line
		/// </summary>
		/// <param name="originalCommandLine"></param>
		/// <returns></returns>
		public virtual String ReadCommandLine(String originalCommandLine)
		{
			try
			{
				String newCommandLine;
				String oldCommandLine;
				newCommandLine = TrueTrim(originalCommandLine);
				do
				{
					if (DebugMode)
					{
						Console.WriteLine(newCommandLine);
					}
					oldCommandLine = newCommandLine;
					ShortenBracket(ref newCommandLine);
				} while (oldCommandLine != newCommandLine);
				return newCommandLine;
			}
			catch (Exception ex)
			{
				return "Error - " + ex.Message;
			}
		}
		/// <summary>
		/// This function will remove spaces before, after, and any extra spaces between words, making sure that only one space is between words.
		/// </summary>
		/// <param name="word"></param>
		/// <returns></returns>
		public virtual String TrueTrim(String word)
		{
			word = word.Trim();
			bool doubleSpaceFound = false;
			do
			{
				word = word.Replace("  ", " ");
				doubleSpaceFound = (word.IndexOf("  ") != -1);
			} while (doubleSpaceFound);
			return word;
		}
		/// <summary>
		/// gets the start and end of bracket one at a time
		/// </summary>
		/// <param name="commandLine">Command line</param>
		public virtual void ShortenBracket(ref String commandLine)
		{
			int start;
			int end;
			String prefix;
			String suffix;
			String bracket;
			String result;

			start = commandLine.LastIndexOf('(');

			if (start != -1)
			{
				end = commandLine.IndexOf(')', start);

				if (end == -1)
				{
					throw new Exception("No ending brackets");
				}

				prefix = commandLine.Substring(0, start);
				suffix = commandLine.Substring(end + 1);
				bracket = commandLine.Substring(start + 1, (end - start - 1));
				result = SolveBracket(bracket);
				commandLine = prefix + result + suffix;
			}
		}
		/// <summary>
		/// Calculates Bracket
		/// </summary>
		/// <param name="commandFunction">Command Line</param>
		/// <returns></returns>
		public virtual String SolveBracket(String commandFunction)
		{
			int total;
			commandFunction = TrueTrim(commandFunction);
			String[] commandLineWords = commandFunction.Split(' ');
			if (commandLineWords.Length < 3)
			{
				throw new Exception("At least two arguments required");
			}
			else if (commandLineWords[0].ToUpper() == "EXPONENT")
			{
				total = Exponent(commandLineWords);
			}
			else if (commandLineWords[0].ToUpper() == "ADD")
			{
				total = AddNumbers(commandLineWords);
			}
			else if (commandLineWords[0].ToUpper() == "MULTIPLY")
			{
				total = MultiplyNumbers(commandLineWords);
			}
			else
			{
				throw new Exception("Invalid function");
			}
			return total.ToString();
		}
		/// <summary>
		/// Sums the total of numbers
		/// </summary>
		/// <param name="numbers">numbers</param>
		/// <returns></returns>
		public virtual int AddNumbers(string[] numbers)
		{
			int total = 0;
			for (int x = 1; x < numbers.Length; x++)
			{
				total += int.Parse(numbers[x]);
			}
			return total;
		}
		/// <summary>
		/// Multiplys the numbers
		/// </summary>
		/// <param name="numbers">Numbers</param>
		/// <returns></returns>
		public virtual int MultiplyNumbers(string[] numbers)
		{
			int total = 1;
			for (int x = 1; x < numbers.Length; x++)
			{
				total *= int.Parse(numbers[x]);
			}
			return total;
		}
		/// <summary>
		/// Exponent
		/// </summary>
		/// <param name="numbers">numbers</param>
		/// <returns></returns>
		public virtual int Exponent(string[] numbers)
		{
			int theBase;
			int power;
			int exp;
			theBase = int.Parse(numbers[1]);
			power = int.Parse(numbers[2]);
			exp = (int)Math.Pow(theBase, power);
			return exp;
		}
	}
}
