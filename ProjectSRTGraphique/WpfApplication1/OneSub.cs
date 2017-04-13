using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
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
