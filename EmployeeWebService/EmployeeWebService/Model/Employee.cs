using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebService.Model
{
    public class Employee
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public string SkillSet { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfJoining { get; set; }
        public bool IsActive { get; set; }
    }
}
