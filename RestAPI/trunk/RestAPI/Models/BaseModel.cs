using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestAPI.Models
{
    public abstract class BaseModel
    {
        public int id { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }

        public BaseModel()
        {
            created = DateTime.Now;
            updated = DateTime.Now;
        }

        public abstract Dictionary<string, object> GetDetails();

        protected Dictionary<string, object> InitializeDictionary()
        {
            Dictionary<string, object> rv = new Dictionary<string, object>();
            rv.Add("id", id);
            rv.Add("created", Assistant.ConvertDateTime(created));
            rv.Add("updated", Assistant.ConvertDateTime(updated));

            return rv;
        }
    }
}