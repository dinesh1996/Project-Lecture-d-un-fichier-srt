using System;
namespace ProjetSRT
{
	public class OneSub
	{

		public TimeSpan ComeIn;
		public TimeSpan ComeOut;
		public string TextPrint;

		public OneSub()
		{
		}


		public OneSub(TimeSpan ComeInp, TimeSpan ComeOutp, string TextPrintp)
		{

			ComeIn = ComeInp;
			ComeOut = ComeOutp;
			TextPrint = TextPrintp;
		}

	}
}
