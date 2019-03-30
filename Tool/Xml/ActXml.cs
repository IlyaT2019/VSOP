using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tool.Xml;

namespace RPA_FLAT.Core.TOOLS
{
    //Проблема: сериализация таблицы в словаре
    public class ActXml<T>:IActXml<T>, IDisposable
    {
        private XmlSerializer _xml;
        private string _path;

        public ActXml(string path)
        {
            _xml = new XmlSerializer(typeof(T));
            _path = path;
        }

        public async Task SerializeAsync(T obj)
        {
            await Task.Run(() => 
            {
                using (var flSm = new FileStream(Path.Combine(_path + ".xml"), FileMode.OpenOrCreate))
                {
                    _xml.Serialize(flSm, obj);
                }
            });           
        }

        public async Task<T> DesirializeAsync()
        {
            return await Task.Run(()=>
            {
                using (var flSm = new FileStream(Path.Combine(_path + ".xml"), FileMode.Open))
                {
                    return (T)_xml.Deserialize(flSm);
                }
            });
        }

        public void Dispose()
        {
            _xml = null;
            _path = null;
        }


        public Task SerializeAsync(T obj, string path)
        {
            throw new NotImplementedException();
        }

        public Task<T> DesirializeAsync(string path)
        {
            throw new NotImplementedException();
        }
    }
}
