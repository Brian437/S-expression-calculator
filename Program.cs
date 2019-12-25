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
using S_ExpressionFramework;

namespace S_Expression
{
	class Program
	{
		const bool DEBUG_MODE = false;
		static bool continueLoop = true;
		static S_ExpressionCalculatorBase sExpression = new S_ExpressionCalculatorBase();

		static void Main(string[] args)
		{
			String commandLine;
			String result;
			sExpression.DebugMode = false;

			do
			{
				commandLine = Console.ReadLine();
				if (commandLine.ToUpper() == "EXIT")
				{
					continueLoop = false;
					Console.WriteLine("Closing program");
				}
				else
				{
					result = sExpression.ReadCommandLine(commandLine);
					Console.WriteLine(result);
				}
				
			} while (continueLoop);
		}
	}
}
