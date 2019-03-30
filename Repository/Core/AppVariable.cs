using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core
{
    public class AppVariable <T>: Variable
    {
        public NameAppVariable NameAppVariable { get; set; }

        public TypeVariable TypeVariable { get; set; }

        public T Value { get; set; }

        public override string Description { get => base.Description; set => base.Description = value; }

        public override int Number { get => base.Number; set => base.Number = value; }

        public override bool IsChecked { get => base.IsChecked; set => base.IsChecked = value; }
    }

}
