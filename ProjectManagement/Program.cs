using ProjectManagementService.Service;
using System;
using System.IO;
using System.Reflection;

namespace ProjectManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            ProjectService projectService = new ProjectService();
            string path = Path.Combine(Environment.CurrentDirectory, @"File\ProjectRecords.txt");
            var projectList = projectService.GetPairedEmployees(path);
            foreach(var project in projectList)
            {
                Console.WriteLine($"Employee ID#1 ( {project.FirstEmpId} ), Employee ID#2 ( {project.SecondEmpId} ) , Project ID ( {project.ProjectID} ) , Days worked ( {project.DaysOfWorking} )");
            }
            Console.ReadLine();
        }
    }
}
