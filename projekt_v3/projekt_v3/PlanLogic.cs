using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_v3
{
    public partial class Plan
    {
        public void CreatePlan()
        {
            using (var db = new GradedbEntities1())
            {
                var Sub = db.Subjects.FirstOrDefault(p => p.Id == this.SubjectId);

                bool check = Sub.Avg > WantedAvg;

                if (this.Mode == 1)
                {
                    bool flag = true;
                    

                    for (int i = 1; i <= this.nGrades; i++)
                    {
                        db.Grades.Add(new Grade() { Date = DateTime.Today, GradeValue = 2, ColumnId = this.Pcolumn, Isvirtual = i });
                    }
                    db.SaveChanges();

                    while ((flag && Sub.Avg < this.WantedAvg)||check)
                    {
                        check = false;
                        for (int i = 1; i <= this.nGrades; i++)
                        {
                            var ele = db.Grades.FirstOrDefault(p => p.Isvirtual == i);
                            if (ele.GradeValue < 5)
                            {
                                ele.GradeValue++;
                                db.SaveChanges();
                                Sub.UpdateAverage();
                                db.SaveChanges();
                                if (Sub.Avg >= this.WantedAvg)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                    }

                    if (flag)
                    {
                        this.NeededGrades = string.Join(",", db.Grades.Where(p => p.Isvirtual != null).Select(p => p.GradeValue).ToList());
                    }
                    else
                    {
                        this.NeededGrades = "Nemoguće";
                    }

                    db.Grades.RemoveRange(db.Grades.Where(p => p.Isvirtual != null));
                    db.SaveChanges();
                    Sub.UpdateAverage();
                    db.SaveChanges();
                }

                else if (this.Mode == 2)
                {
                    bool flag = true;
                    

                    for (int i = 1; i <= this.nGrades; i++)
                    {
                        db.Grades.Add(new Grade() { Date = DateTime.Today, GradeValue = (int)Math.Floor(WantedAvg), ColumnId = this.Pcolumn, Isvirtual = i });
                    }
                    db.SaveChanges();


                    for (int i = 1; i <= this.nGrades && flag; i++)
                    {
                        var ele = db.Grades.FirstOrDefault(p => p.Isvirtual == i);
                        while (ele.GradeValue < 5)
                        {
                            ele.GradeValue++;
                            db.SaveChanges();
                            Sub.UpdateAverage();
                            db.SaveChanges();
                            if (Sub.Avg >= this.WantedAvg)
                            {
                                flag = false;
                                break;
                            }

                        }

                    }

                    if (Sub.Avg >= this.WantedAvg)
                    {
                        this.NeededGrades = string.Join(",", db.Grades.Where(p => p.Isvirtual != null).Select(p => p.GradeValue).ToList());
                    }
                    else
                    {
                        this.NeededGrades = "Nemoguće";
                    }

                    db.Grades.RemoveRange(db.Grades.Where(p => p.Isvirtual != null));
                    db.SaveChanges();
                    Sub.UpdateAverage();
                    db.SaveChanges();
                }

                else
                {
                    bool flag = true;
                    

                    for (int i = 1; i <= this.nGrades; i++)
                    {
                        db.Grades.Add(new Grade() { Date = DateTime.Today, GradeValue = 2, ColumnId = this.Pcolumn, Isvirtual = i });
                    }
                    db.SaveChanges();



                    for (int i = 1; i <= this.nGrades && flag; i++)
                    {
                        var ele = db.Grades.FirstOrDefault(p => p.Isvirtual == i);
                        while (ele.GradeValue < 5)
                        {
                            ele.GradeValue++;
                            db.SaveChanges();
                            Sub.UpdateAverage();
                            db.SaveChanges();
                            if (Sub.Avg >= this.WantedAvg)
                            {
                                flag = false;
                                break;
                            }
                        }

                    }


                    if (Sub.Avg >= this.WantedAvg)
                    {
                        var List1 = db.Grades.Where(p => p.Isvirtual != null).Select(p => p.GradeValue).ToList();
                        List1.Sort();
                        this.NeededGrades = string.Join(",", List1);
                    }
                    else
                    {
                        this.NeededGrades = "Nemoguće";
                    }

                    db.Grades.RemoveRange(db.Grades.Where(p => p.Isvirtual != null));
                    db.SaveChanges();
                    Sub.UpdateAverage();
                    db.SaveChanges();
                }
            }
        }
    }
}
