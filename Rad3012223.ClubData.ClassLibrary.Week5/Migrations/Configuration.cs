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


            for (int i = 0; i < 10; i++)
            {

                if (i == 0)
                {
                    members.Add(
                        new Member
                        {
                            MemberID = i,
                            approved = true,
                            StudentID = students[i].StudentID,
                            AssociatedClub = 1,
                        });
                    i++;
                }
                    members.Add(
                        new Member
                        {
                            MemberID = i,
                            approved = false,
                            StudentID = students[i].StudentID,
                            AssociatedClub = 1,
                        });
            }

                
            
            context.Member.AddOrUpdate(m => m.MemberID, members.ToArray());
            context.SaveChanges();

            context.Club.AddOrUpdate(c => c.ClubId, new Models.Club[]
                {
                    new Models.Club{ClubId = 1, ClubName = "Swimming Club", CreationDate = new DateTime(year:2019, month:12,day:9), adminID=93, clubMembers = members }
                });

            context.SaveChanges();



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


// add clubs look at week 4 think I got it working there.