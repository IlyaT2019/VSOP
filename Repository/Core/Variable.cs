using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core
{
    public abstract class Variable
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int Number { get; set; }

        public virtual bool IsChecked { get; set; }

        //public virtual TypeVariable TypeVariable { get; set; } 
        
        public virtual string Description { get; set; }
    }
}
