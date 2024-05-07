using FacultetApi.Data;
using FacultetApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FacultyWebApi.ExtensionMethods
{

    public static class ExtensionMethodcs
    {
        //public static int GetStudentId(this Student student)
        //{
        //    return student.Id;
        //}

        //public static string GetStudentNamee(this Student student)
        //{
        //    return $"{student.Name},{student.Surname}";
        //}

        public static string GetStudentName(this DbSet<Student> students, int id)
        {
            using (var db = new FacultyDbContext())
            {
                var student = db.Students.FirstOrDefault(s => s.Id == id);
                if (student != null)
                {
                    return student.Name + "" + student.Surname;
                }
                return "";

            }
        }

        public static string GetStudentYear(this DbSet<Student> students, int age)
        {
            using (var db = new FacultyDbContext())
            {
                var student = db.Students.Where(x => x.Years == age).FirstOrDefault();

                if (student != null)
                {
                    return student.Name + student.Surname + student.Place;

                }
                return "";

            }
        }

        public static string GetCollegeName(this DbSet<College> college, int id)
        {
            using (var db = new FacultyDbContext())
            {
                var colleges = db.Colleges.FirstOrDefault(s => s.Id == id);

                if (colleges != null)
                {
                    return $"{colleges.Name} EmployerId {colleges.EmployerId}";

                }
                return "";

            }
        }

        public static string GetCountyName(this DbSet<County> county, int id)
        {
            using (var db = new FacultyDbContext())
            {
                var countys = db.Colleges.FirstOrDefault(s => s.Id == id);

                if (countys != null)
                {
                    return $"{countys.Name} EmployerId: {countys.EmployerId}";

                }
                return "";

            }
        }
        public static ICollection<Student> GetStudentsByStudyId(this DbSet<Student> student, int studyId)
        {
            using (var db = new FacultyDbContext())
            {
                return db.Students.Where(s => s.StudyId == studyId).ToList();

            }

        }

        public static ICollection<Employer> GetEmployersByFacultyId(this DbSet<Employer> employer, int facultyId)
        {
            using (var db = new FacultyDbContext())
            {
                return db.Employers.Where(i => i.FacultyId == facultyId).ToList();

            }

        }

      
        public static int GetStudentsCount(this DbSet<Student> students)
        {
            using (var db = new FacultyDbContext())
            {
                return db.Students.Count();
            }
        }

        //public static IQueryable<Faculty> GetByName(this DbSet<Faculty> faculty, string name)
        //{
        //    using (var db = new FacultyDbContext())
        //    {
        //        return db.Facultys.Where(i=>i.Name == name);
        //    }
        //}
        //public static IQueryable<Faculty> GetStudyFaculty(this DbSet<Faculty> facultys)
        //{
        //    using (var db = new FacultyDbContext())
        //    {
        //        var query = from faculty in db.Facultys
        //                    join place in db.Places
        //                    on faculty.PlaceId equals place.Id
        //                    join facstudy in db.FacultyStudys
        //                    on faculty.Id equals facstudy.FacultyId
        //                    select faculty;
        //        return query;

        //    }
        //}

        //public static IQueryable<FacultyStudy> GetByStudyId(this DbSet<FacultyStudy> facultyStudy, int id)
        //{
        //    using (var db = new FacultyDbContext())
        //    {
        //        return db.FacultyStudys.Where(i => i.StudyId==id);
        //    }
        //}
        //public static IQueryable<FacultyStudy> GetByFacultyId(this DbSet<FacultyStudy> facultyStudy, int id)
        //{
        //    using (var db = new FacultyDbContext())
        //    {
        //        return db.FacultyStudys.Where(i => i.FacultyId == id);
        //    }
        //}
        //public static IQueryable<FacultyStudy> GetById(this DbSet<FacultyStudy> facultyStudy, int id)
        //{
        //    using (var db = new FacultyDbContext())
        //    {
        //        return db.FacultyStudys.Where(i => i.Id == id);
        //    }
        //}
        //public static IQueryable<Employer> GetEmployereByPlaceName(this IQueryable<Employer> employer, string placeName)
        //{
        //    return employer.Where(e => e.Place.Name.ToLower() == placeName.ToLower()).Include(s => s.Place);

        //}
        public static IQueryable<Student> GetStudyFaculty(this DbSet<Student> student, int id)
        {
            using (var db = new FacultyDbContext())
            {
                return db.Students.Where(i => i.StudyId==id);
            }
        }

        public static IQueryable<Faculty> GetFakultetiByGrad(this IQueryable<Faculty> fakulteti, string grad)
        {
            return fakulteti.Where(f => f.Place.Name.ToLower() == grad.ToLower());
        }
        public static IQueryable<College> GetCollegeByEmployerName(this IQueryable<College> college, string employerName)
        {
            return college.Where(f=>f.Employer.Name==employerName);
        }
       
        public static IQueryable<Employer> GetEmployereByFacultyName(this IQueryable<Employer> employer, string facultyName)
        {
            return employer.Where(e=>e.Faculty.Name.ToLower() == facultyName);
        }
        public static IQueryable<FacultyStudy> GetFacultyStudyByStudyName(this IQueryable<FacultyStudy> studies, string studyName)
        {
            return studies.Where(s=>s.Study.Name.ToLower() == studyName.ToLower()).Include(s=>s.Study);
        }
        public static IQueryable<FacultyStudy> GetFacultyStudyByFacultyName(this IQueryable<FacultyStudy> studies, string facultyName)
        {
            return studies.Where(s => s.Faculty.Name.ToLower() == facultyName).Include(f=>f.Faculty);
        }
        public static IQueryable<Student> GetStudentBystudyName(this IQueryable<Student> student, string studyName)
        {
            return student.Where(s => s.Study.Name.ToLower() == studyName.ToLower()).Include(s => s.Study);
        }
        public static IQueryable<Student> GetStudentByPlaceName(this IQueryable<Student> student, string placeName)
        {
            return student.Where(e => e.Place.Name.ToLower() == placeName.ToLower()).Include(s => s.Place); 

        }
        public static IQueryable<StudentCollege> GetStudentCollegeByStudentName(this IQueryable<StudentCollege> studentcol, string studentName)
        {
            return studentcol.Where(e => e.Student.Name==studentName);

        }
        public static IQueryable<StudentCollege> GetStudentCollegeByCollegeName(this IQueryable<StudentCollege> studentcol, string colegeName)
        {
            return studentcol.Where(e => e.College.Name == colegeName);

        }
        public static IQueryable<StudentCollege> GetStudentCollegeByCollegeEmployerName(this IQueryable<StudentCollege> studentcol, string colegeName)
        {
            return studentcol.Where(e => e.College.Employer.Name == colegeName).Include(s=>s.College);

        }

    }
}
