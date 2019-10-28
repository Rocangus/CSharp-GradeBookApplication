using System;
using System.Collections.Generic;
using System.Text;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }
            List<double> grades = new List<double>();
            foreach (Student s in Students)
            {
                grades.Add(s.AverageGrade);
            }
            grades.Sort();
            var index = grades.BinarySearch(averageGrade);
            var position = index < 0? ~index: index;
            if (index >= 0.8 * Students.Count)
            {
                return 'A';
            }
            else if (index >= 0.6 * Students.Count)
            {
                return 'B';
            }
            else if (index >= 0.4 * Students.Count)
            {
                return 'C';
            }
            else if (index >= 0.2 * Students.Count)
            {
                return 'D';
            }
            else
            {
                return 'F';
            }
        }
    }
}
