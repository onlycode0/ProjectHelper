using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelper.Domain.Projects
{
    public class Step
    {
        public string Id { get; set; }

        public List<string> TaskIds { get; set; } = new List<string>();
    }
}
