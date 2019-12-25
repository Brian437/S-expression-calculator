/*
 * S Expressino
 * Created by Brian Chaves
 * Created around December 20, 2019
 * Updated on December 24, 2019
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace S_Expression
{
	class Program
	{
		const bool DEBUG_MODE = false;
		static bool continueLoop = true;

		static void Main(string[] args)
		{
			String commandLine;
			String result;
			do
			{
				commandLine = Console.ReadLine();
				result = ReadCommandLine(commandLine);
				Console.WriteLine(result);
			} while (continueLoop);
		}
		public static String ReadCommandLine(String originalCommandLine)
		{
			if (originalCommandLine.ToUpper() == "EXIT")
			{
				continueLoop = false;
				return "Closing program";
			}
			try
			{
				String newCommandLine;
				String oldCommandLine;
				newCommandLine = TrueTrim(originalCommandLine);
				do
				{
					if (DEBUG_MODE)
					{
						Console.WriteLine(newCommandLine);
					}
					oldCommandLine = newCommandLine;
					ShortenBracket(ref newCommandLine);
				} while (oldCommandLine != newCommandLine);
				return newCommandLine;
			}
			catch(Exception ex)
			{
				return "Error - "+ex.Message;
			}
		}
		public static String TrueTrim(String word)
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
		public static void ShortenBracket(ref String commandLine)
		{
			int start;
			int end;
			String prefix;
			String suffix;
			String bracket;
			String result;

			start = commandLine.LastIndexOf('(');

			if (start == -1)
			{
				return;
			}

			end = commandLine.IndexOf(')', start);

			if (end == -1)
			{
				throw new Exception("No ending brackets");
			}

			prefix = commandLine.Substring(0, start);
			suffix = commandLine.Substring(end+1);
			bracket = commandLine.Substring(start+1, (end-start-1));
			result = SolveBracket(bracket);
			commandLine = prefix + result + suffix;
		}
		public static String SolveBracket(String commandFunction)
		{
			int total;
			commandFunction = TrueTrim(commandFunction);
			String[] commandLineWords = commandFunction.Split(' ');
			if (commandLineWords.Length < 3)
			{
				throw new Exception("No arguments assigned");
			}
			else if (commandLineWords[0].ToUpper() == "EXPONENT")
			{
				int theBase;
				int power;
				int exp;
				theBase = int.Parse(commandLineWords[1]);
				power = int.Parse(commandLineWords[2]);
				exp = (int)Math.Pow(theBase, power);
				return exp.ToString();
			}
			else if (commandLineWords.Length >= 3)
			{
				total = 0;
				if (commandLineWords[0].ToUpper() == "ADD")
				{
					total = 0;
				}
				else if (commandLineWords[0].ToUpper() == "MULTIPLY")
				{
					total = 1;
				}
				else
				{
					throw new Exception("Invalid function");
				}

				for (int x = 1; x < commandLineWords.Length; x++)
				{
					if (commandLineWords[0].ToUpper() == "ADD")
					{
						total += int.Parse(commandLineWords[x]);
					}
					else if (commandLineWords[0].ToUpper() == "MULTIPLY")
					{
						total *= int.Parse(commandLineWords[x]);
					}
				}
				return total.ToString();
			}
			return "";
		}
	}
}
