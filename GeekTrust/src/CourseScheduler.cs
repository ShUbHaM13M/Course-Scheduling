using System;
using System.Collections.Generic;
using System.Linq;

namespace GeekTrust
{
    public class CourseScheduler
    {
        public static readonly Dictionary<string, Course> courses = [];

        public string ProcessCommand((string, Dictionary<string, string>) data)
        {
            var (command, args) = data;
            string result = "";
            switch (command)
            {
                case "ADD-COURSE-OFFERING":
                    result = AddCourse(args);
                    break;
                case "REGISTER":
                    result = RegisterUser(args);
                    break;
                case "ALLOT":
                    result = AllotCourse(args);
                    break;
                case "CANCEL":
                    result = CancelCourse(args);
                    break;
                default: break;
            }
            return result;
        }


        private static string AddCourse(Dictionary<string, string> args)
        {
            args.TryGetValue("COURSE-NAME", out string name);
            args.TryGetValue("INSTRUCTOR", out string instructor);
            args.TryGetValue("DATE", out string dateString);
            DateTime date = DateTime.ParseExact(dateString, "dMyyyy", System.Globalization.CultureInfo.InvariantCulture);
            args.TryGetValue("MIN-EMPLOYEES", out string minEmployees);
            args.TryGetValue("MAX-EMPLOYEES", out string maxEmployees);
            Course course = new(name, instructor, date, int.Parse(minEmployees), int.Parse(maxEmployees));
            courses.Add(course.Id, course);
            return course.Id;
        }

        private static string RegisterUser(Dictionary<string, string> args)
        {
            args.TryGetValue("EMAIL-ID", out string email);
            args.TryGetValue("COURSE-OFFERING-ID", out string courseId);
            var employee = new Employee(email);
            courses.TryGetValue(courseId, out var course);
            bool added = course.RegisterUser(employee);
            return added ? $"REG-COURSE-{employee.Name}-{course.Name} ACCEPTED" : "COURSE_FULL_ERROR";
        }

        private static string AllotCourse(Dictionary<string, string> args)
        {
            args.TryGetValue("COURSE-OFFERING-ID", out string courseId);
            if (courses.TryGetValue(courseId, out var course))
            {
                return course.Allot();
            }
            return "INVALID COURSE-OFFERING-ID";
        }

        private static string CancelCourse(Dictionary<string, string> args)
        {
            args.TryGetValue("COURSE-REGISTERATION-ID", out string registerationId);
            string[] tokens = registerationId.Split("-");
            string userName = tokens[2];
            string courseName = tokens[3];
            var course = courses.FirstOrDefault(course => course.Value.Name == courseName).Value;
            if (course != null)
                return course.Cancel(registerationId, userName);
            return "INVALID COURSE-OFFERING-ID";
        }
    }
}

