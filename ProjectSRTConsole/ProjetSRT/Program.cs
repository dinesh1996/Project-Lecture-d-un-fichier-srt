using System;

namespace ProjetSRT
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			EncodeSub Test = new EncodeSub();

			Test.ReadStr().Wait();

			Test.PrintSub(Test.ListOfStrs).Wait();

		}
	}
}
