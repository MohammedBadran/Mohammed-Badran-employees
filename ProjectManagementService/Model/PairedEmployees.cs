using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementService.Model
{
    public class PairedEmployees
    {
        public int FirstEmpId { get; set; }
        public int SecondEmpId { get; set; }
        public int ProjectID { get; set; }
        public int DaysOfWorking { get; set; }

    }
}
