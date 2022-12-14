namespace Rad3012223.ClubData.ClassLibrary.Week5.Migrations
{
    using CsvHelper.Configuration;
    using CsvHelper;
    using Rad3012223.ClubData.ClassLibrary.Week5.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Tracker.WebAPIClient;
    using ad3012223.ClubData.ClassLibrary.Week5.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Rad3012223.ClubData.ClassLibrary.Week5.Models.Week5ClubContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rad3012223.ClubData.ClassLibrary.Week5.Models.Week5ClubContext context)
        {
            
            ActivityAPIClient.Track(StudentID: "S00210326", StudentName: "Paul Mc Gonigle", activityName: "RAD301 Week5 Lab 2223"
            , Task: "Seeding Clubs and Member Data");

            //addClubs(context);
            //get_students();
            //context.SaveChanges();




            List<Student> students = get_students();
            context.SaveChanges();
            context.Students.AddOrUpdate(s => s.StudentID, students.ToArray());
            context.SaveChanges();
            context.SaveChanges();




            addClubs(context);
            context.SaveChanges();


        }
        //private static void addClubs(Rad3012223.ClubData.ClassLibrary.Week5.Models.Week5ClubContext context)
        //{
        //    context.Clubs.AddOrUpdate(club => club.ClubName, new Club[] {
        //       new Club
        //       {

        //           ClubName = "The Chess Club",
        //           CreationDate = new DateTime(day:25, month:01, year:2017),


        //       } // End of First club added other clubs can be added next
               
               

        //    } // End of Clubs array
        //         );// End of Add or Update
        //    context.SaveChanges();
        //}
        private static List<Student> get_students()
        {
            // Get the list of DTO records from the resource
            List<StudentDTO> sdto = Get<StudentDTO>("Rad3012223.ClubData.ClassLibrary.Week5.StudentList1.csv");
            List<Student> Students = new List<Student>();
            // iterate over the course DTO records and create course records for each one making the course year an intiger in the process
            // Dummy val

            sdto.ForEach(rec => {
                Students.Add(
                  new Student
                  {
                      StudentID = rec.StudentID,
                      FirstName = rec.FirstName,
                      SecondName = rec.SecondName

                  }
                  );
            });

            return Students;

        }

        private static List<Member> get_members(Week5ClubContext context)
        {

            List<Student> students1 = GetStudents(context, 10);
            List<Student> students2 = GetStudents(context, 10);
            List<Student> students3 = GetStudents(context, 10);

            List<Member> members = new List<Member>();

            students1.ForEach(rec =>
            {
                members.Add(
                    new Member
                    {
                        StudentID = rec.StudentID,
                        approved = true,
                        AssociatedClub = 1
                    });
            });

            students2.ForEach(rec =>
            {
                members.Add(
                    new Member
                    {
                        StudentID = rec.StudentID,
                        approved = true,
                        AssociatedClub = 2
                    });
            });

            students3.ForEach(rec =>
            {
                members.Add(
                    new Member
                    {
                        StudentID = rec.StudentID,
                        approved = true,
                        AssociatedClub = 3
                    });
            });

            return members;
        }

        public static List<Student> GetStudents(Week5ClubContext context, int count)
        {
            // Create a random list of studnets ids
            var randomSetStudent = context.Students.Select(s => new { s.StudentID, r = Guid.NewGuid() });
            // Sort them and take 10
            List<string> subset = randomSetStudent.OrderBy(s => s.r)
                .Select(s => s.StudentID.ToString()).Take(count).ToList();
            // Return the selected st students as a realized list
            return context.Students.Where(s => subset.Contains(s.StudentID)).ToList();
        }

        public static List<T> Get<T>(string resourceName)
        {
            // Get the current assembly
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {   // create a stream reader
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                    { HasHeaderRecord = false };
                    // create a csv reader dor the stream
                    CsvReader csvReader = new CsvReader(reader, configuration);
                    return csvReader.GetRecords<T>().ToList();
                }
            }
        }

        public static void addClubs(Week5ClubContext context)
        {
            List<Member> members = new List<Member>();

            members = context.Members.ToList();

            Console.WriteLine("here");
            for (int i = 0; i < members.Count; i++)
            {
                Console.WriteLine(members[i].MemberID);
            }

            List<Member> club1 = new List<Member>();
            List<Member> club2 = new List<Member>();
            List<Member> club3 = new List<Member>();


            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].AssociatedClub == 1)
                {
                    club1.Add(members[i]);
                }
                else if(members[i].AssociatedClub == 2)
                {
                    club2.Add(members[i]);
                }
                else
                {
                    club3.Add(members[i]);
                }
            }


            for (int i = 0; i < club1.Count; i++)
            {
                Console.WriteLine(club1[i].MemberID);
            }


            context.Members.AddOrUpdate(m => new { m.AssociatedClub, m.approved, m.StudentID }, members.ToArray());
            context.Clubs.AddOrUpdate(club => club.ClubName, new Club[]
            {
                new Club
                {
                    ClubName = "The Chess Club",
                    CreationDate = new DateTime(day:23, month:10, year:2020),
                    adminID = club1[0].MemberID,
                },
                new Club
               {
                   ClubName = "Volley ball Club",
                   CreationDate = new DateTime(day:01, month: 01, year: 2018),
                   adminID = club2[0].MemberID,
               },
                 new Club
               {
                   ClubName = "Martial Arts Club",
                   CreationDate = new DateTime(day:15, month: 08, year: 2020),
                   adminID = club3[0].MemberID,
               }

            } // End of Clubs array
                 );// End of Add or Update


            context.SaveChanges();
        }
    }
}
