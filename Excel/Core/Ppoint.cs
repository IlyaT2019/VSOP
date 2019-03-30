using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Core
{
    public class Ppoint : OfficeApp
    {
        protected override Filter filter { get; set ; }

        public Ppoint(Filter filter = null)
        { }
    }
}
