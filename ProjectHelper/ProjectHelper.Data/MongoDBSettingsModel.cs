using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelper.Data
{
    public class MongoDBSettingsModel
    {
        public string ConnectionURI { get; set; } = null!;

        public string DataBaseName { get; set; } = null!;

        public string ProductManagersCollection { get; set; } = null!;
    }
}
