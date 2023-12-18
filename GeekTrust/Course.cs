
using System;
using System.Collections.Generic;

namespace GeekTrust
{
    class Course(string name, string instructor, DateTime date, int minEmployees, int maxEmployees)
    {
        public string Name { get; } = name;
        public string Instructor { get; } = instructor;
        public DateTime Date { get; } = date;
        public int MinEmployees { get; } = minEmployees;
        public int MaxEmployees { get; } = maxEmployees;
        public string Id { get; } = $"OFFERING-{name}-{instructor}";
        public readonly List<Employee> Users = [];
        private bool Alloted = false;

        public void Print()
        {
            Console.WriteLine($"Course Name: {Name}");
            Console.WriteLine($"Instructor: {Instructor}");
            Console.WriteLine($"Date: {Date:d/M/yyyy}");
            Console.WriteLine($"Min Employees: {MinEmployees}");
            Console.WriteLine($"Max Employees: {MaxEmployees}");
        }

        public bool RegisterUser(Employee employee)
        {
            if (Users.Count + 1 <= MaxEmployees)
            {
                Users.Add(employee);
                Users.Sort((x, y) => x.Name.CompareTo(y.Name));
                return true;
            }
            return false;
        }

        public string Allot()
        {
            List<string> results = [];
            foreach (var user in Users)
            {
                string result = GetCourseRegisterationId(user.Name, user.Email);
                results.Add(result);
            };
            Alloted = true;
            return string.Join(Environment.NewLine, results);
        }

        public string GetCourseRegisterationId(string userName, string userEmail)
        {
            return $"REG-COURSE-{userName}-{Name} {userEmail} {Id} {Name} {userName} {Date:ddMMyyyy} CONFIRMED";
        }

        public string Cancel(string registerationId, string userName)
        {
            if (Alloted)
            {
                return $"{registerationId} CANCEL_REJECTED";
            }
            var user = Users.RemoveAll(user => user.Name == userName);
            return $"{registerationId} CANCEL_ACCEPTED";
        }
    }
}