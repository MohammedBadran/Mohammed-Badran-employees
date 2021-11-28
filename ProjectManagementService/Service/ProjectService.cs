using ProjectManagementService.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementService.Service
{
    public class ProjectService
    {
        public List<PairedEmployees> GetPairedEmployees(string filePath)
        {
            List<PairedEmployees> pairedEmployeesList = new List<PairedEmployees>();
            List<Project> projectList = LoadFile(filePath);
            if (projectList.Count > 0)
            {
                foreach (IGrouping<int, Project> project in projectList.GroupBy(x => x.ProjectID).ToList())
                {
                    List<Project> projectGroup = project.Take(2).ToList();
                    if (projectGroup.Count == 2)
                    {
                        PairedEmployees pairedEmployees = new PairedEmployees()
                        {
                            // Calc the intersection between two employees (as a team in one project)
                            DaysOfWorking = projectGroup.Min(x => x.DaysOfWorking),
                            FirstEmpId = projectGroup[0].EmpId,
                            SecondEmpId = projectGroup[1].EmpId,
                            ProjectID = projectGroup[0].ProjectID

                        };
                        pairedEmployeesList.Add(pairedEmployees);
                    }
                }
            }
            return pairedEmployeesList;
        }
        private List<Project> LoadFile(string filePath)
        {
            List<Project> projectService = new List<Project>();
            if (!string.IsNullOrEmpty(filePath))
            {
                foreach (string line in System.IO.File.ReadLines(filePath))
                {
                    var isValidline = CheckFileFormate(line);
                    if (isValidline.Item1)
                    {
                        var project = projectService.FirstOrDefault(x => x.EmpId == isValidline.Item2.EmpId && x.ProjectID == isValidline.Item2.ProjectID);
                        if (project==null)
                        {
                            projectService.Add(isValidline.Item2);
                        }
                        
                    }
                }
                projectService.OrderBy(x => x.ProjectID);
            }

            return projectService;
        }
        /// <summary>
        /// check if line match pattern 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private Tuple<bool, Project> CheckFileFormate(string line)
        {
            bool lineCheck = false;
            Project project = null;
            string[] parts = line.Split(',');
            ///Check for number of fileds
            if (parts.Count() == 4)
            {
                int projectId = 0;
                int empId = 0;
                if (int.TryParse(parts[0], out empId) && int.TryParse(parts[1], out projectId))
                {
                    Tuple<bool, DateTime> checkForDateFrom = CheckForDate(parts[2]);
                    Tuple<bool, DateTime> checkForDateTo = CheckForDate(parts[3], true);
                    if (checkForDateFrom.Item1 && checkForDateTo.Item1)
                    {
                        project = new Project()
                        {
                            EmpId = empId,
                            ProjectID = projectId,
                            DateFrom = checkForDateFrom.Item2,
                            DateTo = checkForDateTo.Item2,
                            DaysOfWorking = (checkForDateTo.Item2 - checkForDateFrom.Item2).Days
                        };
                        lineCheck = true;
                    }
                }
            }
            return Tuple.Create(lineCheck, project);
        }
        private Tuple<bool, DateTime> CheckForDate(string date, bool isDateTo = false)
        {
            bool isValid = false;
            DateTime dateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(date))
            {
                date = date.Replace(" ", string.Empty);
                if (date.ToLower() == "null" && isDateTo)
                {
                    isValid = true;
                }
                else
                {
                    if (DateTime.TryParse(date, out dateTime))
                    {
                        isValid = true;
                    }
                }

            }
            return Tuple.Create(isValid, dateTime);
        }
    }
}
