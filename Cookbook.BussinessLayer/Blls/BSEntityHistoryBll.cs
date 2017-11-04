using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Cookbook.BussinessLayer.Core;
using Cookbook.BussinessLayer.Interfaces;
using Cookbook.Data.Interfaces;
using Cookbook.Data.Models;

namespace Cookbook.BussinessLayer.Blls
{
    public class BSEntityHistoryBll : BSBussinessEntityBll<BSEntityHistory>, IBSEntityHistoryBll
    {
        public BSEntityHistoryBll()
        {
        }

        public BSEntityHistoryBll(IBSContextFactory contextFactory) : base(contextFactory)
        {
        }

        public IEnumerable<T> GetHistoryForEntity<T>(int id) where T: class, IBSCoreEntity
        {
            Type t = typeof(T);
            var histories = GetFiltered(h => h.ObjectId == id && h.Type == t.Name);
            if (histories == null || !histories.Any())
            {
                return null;
            }
            var list = new List<T>();
            foreach (var bsEntityHistory in histories)
            {
                list.Add(DesirializeObject<T>(bsEntityHistory.Values));
            }
            return list;
        }
        
        public void AddHistory<T>(T obj) where T : class, IBSCoreEntity
        {
            var history = new BSEntityHistory();
            history.ObjectId = obj.Id;
            history.Type = typeof(T).Name;
            history.Values = SerializeObject(obj, typeof(T));
            this.Insert(history);
        }

        private string SerializeObject(object obj, Type t)
        {
            var serializer = new XmlSerializer(t);
            var text = new StringWriter();
            serializer.Serialize(text,obj);
            return text.ToString();
        }

        private T DesirializeObject<T>(string obj) where T : class, IBSCoreEntity
        {
            var serializer = new XmlSerializer(typeof(T));
            return serializer.Deserialize(new StringReader(obj)) as T;
        }
        
    }
}