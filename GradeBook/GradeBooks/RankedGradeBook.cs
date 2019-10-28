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

        // Override of GetLetterGrade from BaseGradeBook. Returns a grade based on the passed average grade compared to the class peers rather than absolute numbers; an average in the top 20% gives an A,
        // between top 20 and 40 gives a B and so forth. Throws InvalidOperationException if the number of students is too small (less than 5).
        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }
            var grades = PrepareGradeList();
            return DetermineRankedLetterGrade(averageGrade, grades);
        }

        // Determines which letter grade to give
        private char DetermineRankedLetterGrade(double averageGrade, List<double> grades)
        {
            var index = grades.BinarySearch(averageGrade);
            var position = index < 0 ? ~index : index;
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

        // Prepares the list of grades to be passed by the override of GetLetterGrade to DetermineRankedLetterGrade.
        private List<double> PrepareGradeList()
        {
            List<double> grades = new List<double>();
            foreach (Student s in Students)
            {
                grades.Add(s.AverageGrade);
            }
            grades.Sort();
            return grades;
        }

        // Overrides BaseGradeBook's CalculateStatistics to avoid exception handling if the number of students is too small.
        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            var count = 0;
            foreach (Student s in Students)
            {
                if (s.Grades.Count > 0)
                {
                    count++;
                }
            }
            if (count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
