using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelper.Domain.Users
{
    public class Developer
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string CompanyId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public List<DeveloperSkills> Skills { get; set; } = new List<DeveloperSkills>();

        public int Experience { get; set; }

        public int DailyCapacity { get; set; }  //занятость

        public Dictionary<DateTime, float> Schedule { get; set; } = new();  //расписание дата-колво часов

        public List<string> ProjectIds { get; set; } = new();


    }
}
