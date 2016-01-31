using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_v3
{
    public partial class Subject
    {
        public void UpdateAverage()
        {
            {
                if (this.Rule == 1)
                {
                    this.Avg = ArithmeticAvg();
                }
                else if (this.Rule == 2)
                {
                    this.Avg = GeometricAvg();
                }
                else
                {
                    this.Avg = HarmonicAvg();
                }
            }
        }

        private double? ArithmeticAvg()
        {
            Double Sum = 0, Divider = 0;
            using (var db = new GradedbEntities1())
            {
                foreach (var Col in db.Columns.Where(p => p.SubjectId == this.Id))
                {
                    if (Col.Rule == 1)
                    {
                        foreach (var Gra in db.Grades.Where(p => p.ColumnId == Col.Id))
                        {
                            Sum += (double) Gra.GradeValue;
                            Divider++;
                        }
                    }
                    else if (Col.Rule == 2)
                    {
                        foreach (var Gra in db.Grades.Where(p => p.ColumnId == Col.Id))
                        {
                            Sum += Gra.GradeValue * (double)Col.Coeficient;
                            Divider += (double)Col.Coeficient;
                        }
                    }
                    else
                    {
                        foreach (var Gra in Col.Group())
                        {
                            Sum += Gra;
                            Divider++;
                        }
                    }
                }
                if (Divider == 0)
                {
                    return null;
                }
                else
                {
                    return Math.Round(Sum / Divider, 2);
                }
            }

        }

        private double? GeometricAvg()
        {
            double Product = 1, Root = 0;
            using (var db = new GradedbEntities1())
            {
                foreach (var Col in db.Columns.Where(p => p.SubjectId == this.Id))
                {
                    if (Col.Rule == 1)
                    {
                        foreach (var Gra in db.Grades.Where(p => p.ColumnId == Col.Id))
                        {
                            Product *= (double)Gra.GradeValue;
                            Root++;
                        }
                    }
                    else if (Col.Rule == 2)
                    {
                        foreach (var Gra in db.Grades.Where(p => p.ColumnId == Col.Id))
                        {
                            Product *= Math.Pow(Gra.GradeValue, (double)Col.Coeficient);
                            Root += (double)Col.Coeficient;
                        }
                    }
                    else
                    {
                        foreach (var Gra in Col.Group())
                        {
                            Product *= Gra;
                            Root++;
                        }
                    }
                }

            }
            if (Root == 0)
            {
                return null;
            }
            else
            {
                return Math.Round(Math.Pow(Product, 1 / Root), 2);
            }
        }

        private double? HarmonicAvg()
        {
            Double Sum = 0, Num = 0;
            using (var db = new GradedbEntities1())
            {
                foreach (var Col in db.Columns.Where(p => p.SubjectId == this.Id))
                {
                    if (Col.Rule == 1)
                    {
                        foreach (var Gra in db.Grades.Where(p => p.ColumnId == Col.Id))
                        {
                            Sum += 1 / (double)Gra.GradeValue;
                            Num++;
                        }
                    }
                    else if (Col.Rule == 2)
                    {
                        foreach (var Gra in db.Grades.Where(p => p.ColumnId == Col.Id))
                        {
                            Sum += (double)Col.Coeficient / Gra.GradeValue;
                            Num += (double)Col.Coeficient;
                        }
                    }
                    else
                    {
                        foreach (var Gra in Col.Group())
                        {
                            Sum += 1 / Gra;
                            Num++;
                        }
                    }
                }
                if (Num == 0)
                {
                    return null;
                }
                else
                {
                    return Math.Round(Num / Sum, 2);
                }
            }
        }
    }
}
