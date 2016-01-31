using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_v3
{
    public partial class Column
    {
        public List<double> Group()
        {
            int Count = 0;
            double Sum = 0;
            List<double> grades = new List<double>();
            using(var db = new GradedbEntities1())
            {
                foreach(var Gra in db.Grades.Where(p=>p.ColumnId == Id))
                {
                    if (Count < Coeficient-1)
                    {
                        Sum += Gra.GradeValue;
                        Count++;
                    }
                    else
                    {
                        Sum += Gra.GradeValue;
                        Count++;
                        grades.Add(Sum / Count);
                        Count = 0;
                        Sum = 0;
                    }
                }
                //if (Count == Coeficient)
                //{
                //    grades.Add(Sum / Count);
                //    Count = 0;
                //}
            }
            return grades;
        }
    }
}
