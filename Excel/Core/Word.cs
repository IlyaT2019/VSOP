using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Core
{
    public class Word : OfficeApp
    {
        protected override Filter filter { get; set; }

        public Word(Filter filter = null)
        {

        }
    }
}
