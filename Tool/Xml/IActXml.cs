using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Xml
{
    public interface IActXml<T>
    {
        Task SerializeAsync(T obj);
        Task SerializeAsync(T obj, string path);
        Task<T> DesirializeAsync();
        Task<T> DesirializeAsync(string path);
    }
}
