using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_FLAT.Core.Model
{
    public class InformationDialogService
    {
        public string NameFile { get; set; }

        public string Path { get; set; }

        public ExtensionFile ExpansionFile { get; set; }

        public string Size { get; set; }
    }

    public enum ExtensionFile
    {
        /// <summary>
        /// Расширение csv. Текстовый файл, с разделителями
        /// </summary>
        csv = 0,
        /// <summary>
        /// Расширение txt. Текстовый файл
        /// </summary>
        txt,
        /// <summary>
        /// Расширение xlsx. Файл Excel
        /// </summary>
        xlsx
    }
}
