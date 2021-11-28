using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementService.Model
{
    public class Project
    {
        public int ProjectID { get; set; }
        public int EmpId { get; set; }
        public DateTime DateFrom { get; set; } 
        public DateTime DateTo { get; set; } 
        public int DaysOfWorking { get; set; }

    }
}
