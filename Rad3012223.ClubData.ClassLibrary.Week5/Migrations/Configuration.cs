namespace Rad3012223.ClubData.ClassLibrary.Week5.Migrations
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using Newtonsoft.Json.Schema;
    using Rad3012223.ClubData.ClassLibrary.Week5.DTOs;
    using Rad3012223.ClubData.ClassLibrary.Week5.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Tracker.DataLayer;
    using Tracker.WebAPIClient;

    internal sealed class Configuration : DbMigrationsConfiguration<Rad3012223.ClubData.ClassLibrary.Week5.Models.Week5ClubContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rad3012223.ClubData.ClassLibrary.Week5.Models.Week5ClubContext context)
        {
            ActivityAPIClient.Track(StudentID: "S00211628", StudentName: "Martin Melody", activityName: "RAD301 Week5Lab 2223", Task: "Seeding Clubs and Members Data");

            Seed_Members(context);
            

        }


        public void Seed_Members(Week5ClubContext context)
        {
            List<StudentDTO> sdto = Get<StudentDTO>("Rad3012223.ClubData.ClassLibrary.Week5.StudentList1.csv");
            List<Student> students = new List<Student>();
            List<Member> members = new List<Member>();

            sdto.ForEach(rec =>
            {
                students.Add(
                    new Student
                    {
                        FirstName = rec.FirstName,
                        SecondName = rec.SecondName,
                        StudentID =  rec.StudentID,
                    });
            });
            context.Student.AddOrUpdate(s => s.StudentID, students.ToArray());
            context.SaveChanges();

            addClubs(context);


            //for (int i = 0; i < 10; i++)
            //{

            //    if (i == 0)
            //    {
            //        members.Add(
            //            new Member
            //            {
            //                MemberID = i,
            //                approved = true,
            //                StudentID = students[i].StudentID,
            //                AssociatedClub = 1,
            //            });
            //        i++;
            //    }
            //        members.Add(
            //            new Member
            //            {
            //                MemberID = i,
            //                approved = false,
            //                StudentID = students[i].StudentID,
            //                AssociatedClub = 1,
            //            });
            //}

                
            
            //context.Members.AddOrUpdate(m => m.MemberID, members.ToArray());
            //context.SaveChanges();

            //context.Clubs.AddOrUpdate(c => c.ClubId, new Models.Club[]
            //    {
            //        new Models.Club{ClubId = 1, ClubName = "Swimming Club", CreationDate = new DateTime(year:2019, month:12,day:9), adminID=93, clubMembers = members }
            //    });

            //context.SaveChanges();



        }

        private void addClubs(Week5ClubContext context)
        {
            List<Member> members = new List<Member>();
            members = get_members(context);

            //List<Member> club1 = new List<Member>();
            //List<Member> club2 = new List<Member>();
            //List<Member> club3 = new List<Member>();

            //members.ForEach(rec =>
            //{
            //    if (rec.AssociatedClub == 1)
            //    {
            //        club1.Add(rec);
            //    }
            //    else if (rec.AssociatedClub == 2)
            //    {
            //        club2.Add(rec);
            //    }
            //    else if(rec.AssociatedClub == 3)
            //    {
            //        club3.Add(rec);
            //    }
            //});


            Console.WriteLine("here");
            for (int i = 0; i < members.Count; i++)
            {
                Console.WriteLine(members[i].StudentID);
            }

            context.Member.AddOrUpdate(m => new { m.AssociatedClub, m.approved, m.StudentID }, members.ToArray());
            context.Club.AddOrUpdate(club => club.ClubName, new Club[]
            {
                new Club
                {
                    ClubName = "The Chess Club",
                    CreationDate = new DateTime(day:23, month:10, year:2020),
                    adminID = 1,
                },
                new Club
               {
                   ClubName = "Volley ball Club",
                   CreationDate = new DateTime(day:01, month: 01, year: 2018),
                   adminID = 1,
               },
                 new Club
               {
                   ClubName = "Martial Arts Club",
                   CreationDate = new DateTime(day:15, month: 08, year: 2020),
                   adminID = 1,
               }

            } // End of Clubs array
                 );// End of Add or Update


            context.SaveChanges();
        }


        private List<Member> get_members(Week5ClubContext context)
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

        private List<Student> GetStudents(Week5ClubContext context, int count)
        {
            // Create a random list of studnets ids
            var randomSetStudent = context.Student.Select(s => new { s.StudentID, r = Guid.NewGuid() });
            // Sort them and take 10
            List<string> subset = randomSetStudent.OrderBy(s => s.r)
                .Select(s => s.StudentID.ToString()).Take(count).ToList();
            // Return the selected st students as a realized list
            return context.Student.Where(s => subset.Contains(s.StudentID)).ToList();
        }

        public static List<T> Get<T>(string resourceName)
        {
            // Get the current Assembly
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            { // Create a stream reader

                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                    { HasHeaderRecord = false };
                    // Create a csv reader for the stream
                    CsvReader csvReader = new CsvReader(reader, configuration);
                    return csvReader.GetRecords<T>().ToList();
                }
            }
        }

    }
}
