using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace ProjetSRT	
{
	public class EncodeSub
	{
		// REGEX And List of SUBS
		Regex TimeReg = new Regex(@"^[0-9]+:+[0-9]+:+[0-9]+,+[0-9]{3}\s-->\s[0-9]+:+[0-9]+:+[0-9]+,+[0-9]{3}$");
		Regex IsANumber = new Regex(@"^[0-9]*$");

		public List<OneSub> ListOfStrs = new List<OneSub>();
		public EncodeSub()
		{

		}


		public async Task ReadStr()
		{
			string mydocpath =
			  Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			try
			{
				using (StreamReader sr = new StreamReader(mydocpath + @"/Broadchurch.S03E06.720p.ORGANiC.FR.NoTAG.txt"))
				{
					string l;
					string dates = "";
					string nextLine = sr.ReadLine();
					while ((l = (await sr.ReadLineAsync())) != null)
					{
						if (TimeReg.Match(l).Success)
						{
							dates = l;
							//string DatecomeIn;
							//0 Frist date 
							// 2 Seconde date 
							string text = "";
							for (int i = 0; i < 2; i++)
							{
								nextLine = sr.ReadLine();
								if (nextLine != "")
								{
									text += nextLine + "\n";
								}
							}
							OneSub SUB = ParsingToOneSub(text, dates);
							ListOfStrs.Add(SUB);
						}
						else if (IsANumber.Match(l).Success)
						{ }
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		public OneSub ParsingToOneSub(string BrutText, string DatesBruts)
		{
			String[] SplitDates = DatesBruts.Split(' ');
			string date1 = SplitDates[0];
			string date2 = SplitDates[2];
			date1 = date1.Replace(',', ':');
			date2 = date2.Replace(',', ':');
			String[] SpliteStart = date1.Split(':');
			String[] SpliteEnd = date2.Split(':');

			TimeSpan Start = new TimeSpan(0, int.Parse(SpliteStart[0]),
										 	 int.Parse(SpliteStart[1]),
											 int.Parse(SpliteStart[2]),
											 int.Parse(SpliteStart[3])
										 );

			TimeSpan End = new TimeSpan(0, int.Parse(SpliteEnd[0]),
										   int.Parse(SpliteEnd[1]),
										   int.Parse(SpliteEnd[2]),
										   int.Parse(SpliteEnd[3])
									   );

			return new OneSub(Start, End, BrutText);
		}

		public async Task PrintSub(List<OneSub> Subs)
		{
			await Task.Delay(Subs[0].ComeIn);
			Console.WriteLine(Subs[0].TextPrint);
			await Task.Delay(Subs[0].ComeOut - Subs[0].ComeIn);

			Console.Clear();

			for (int i = 1; i < Subs.Count; i++)
			{

				await Task.Delay(Subs[i].ComeIn - Subs[i - 1].ComeOut);

				Console.WriteLine(Subs[i].TextPrint);

				await Task.Delay(Subs[i].ComeOut - Subs[i].ComeIn);

				Console.Clear();

			}

		}


		//Test Part 
		//public void PrintSubN(List<OneSub> Subs)
		//{
		//	foreach (OneSub str in Subs)
		//	{
		//		Console.WriteLine(str.ComeIn + " " + str.ComeOut + " " + str.TextPrint + " Delai  " + (str.ComeOut - str.ComeIn));

		//	}
		//	//Console.WriteLine(Subs[0].ComeIn + " " + Subs[0].ComeOut + " " + Subs[0].TextPrint);
		//}

	}
}
