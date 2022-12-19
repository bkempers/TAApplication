/**
*Author:      Trevor Koenig
 * Partner:     Ben Kempers
 * Date:        9 / 27 / 2022
 * Course:      CS 4540, University of Utah, School of Computing
 * Copyright:   CS 4540 and[Ben Kempers and Trevor Koenig] - This work may not be copied for use in Academic Coursework.
 *
 * I, Trevor Koenig and Ben Kempers, certify that I wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 *
 *    This file initializes the database completely, including migrations and seeding user data
 *    
 */

// comment this out to prevent deleting the database on startup
#define DELTABLE

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TAApplication.Areas.Data;
using TAApplication.Models;

namespace TAApplication.Data
{
    public class TAContext : IdentityDbContext<TAUser>
    {
        public IHttpContextAccessor _httpContextAccessor;

        // See lecture recording to finish - so close
        //public TAContextModelSnapshot( TAContext( DbContextOptions<TAContext> option, IHttpContextAccessor _httpContextAccessor);
        public TAContext(DbContextOptions<TAContext> options, IHttpContextAccessor http)
            : base(options)
        {
            _httpContextAccessor = http;
        }

        public async Task InitializeUsers(UserManager<TAUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            #if (DEBUG && DELTABLE)
                Database.EnsureDeleted();
            #endif

            Database.Migrate();

            if(!await roleManager.RoleExistsAsync("Applicant"))
                await roleManager.CreateAsync(new IdentityRole("Applicant"));

            if (!await roleManager.RoleExistsAsync("Professor"))
                await roleManager.CreateAsync(new IdentityRole("Professor"));

            if (!await roleManager.RoleExistsAsync("Administrator"))
                await roleManager.CreateAsync(new IdentityRole("Administrator"));

            if(!await userManager.Users.AnyAsync())
            {
                TAUser admin = new TAUser { Uid = "u1234567", Name = "Administrator", ReferredTo = "Admin" };
                Add(admin);
                await userManager.AddToRoleAsync(admin, "Administrator");
                String adminEmail = "admin@utah.edu";
                admin.Email = adminEmail;
                admin.UserName = adminEmail;
                admin.NormalizedEmail = adminEmail.Normalize();
                admin.NormalizedUserName = adminEmail.Normalize();
                admin.EmailConfirmed = true;
                await userManager.CreateAsync(admin, "123ABC!@#def");

                TAUser prof = new TAUser { Uid = "u7654321", Name = "Professor", ReferredTo = "Prof" };
                Add(prof);
                await userManager.AddToRoleAsync(prof, "Professor");
                string profEmail = "professor@utah.edu";
                prof.Email = profEmail;
                prof.UserName = profEmail;
                prof.NormalizedEmail = profEmail.Normalize();
                prof.NormalizedUserName = profEmail.Normalize();
                prof.EmailConfirmed = true;
                await userManager.CreateAsync(prof, "123ABC!@#def");

                TAUser one = new TAUser { Uid = "u0000000", Name = "student0", ReferredTo = "0" };
                Add(one);
                await userManager.AddToRoleAsync(one, "Applicant");
                string oneEmail = "u0000000@utah.edu";
                one.Email = oneEmail;
                one.UserName = oneEmail;
                one.NormalizedEmail = oneEmail.Normalize();
                one.NormalizedUserName = oneEmail.Normalize();
                one.EmailConfirmed = true;
                await userManager.CreateAsync(one, "123ABC!@#def");

                TAUser two = new TAUser { Uid = "u0000001", Name = "student1", ReferredTo = "1" };
                Add(two);
                await userManager.AddToRoleAsync(two, "Applicant");
                string twoEmail = "u0000001@utah.edu";
                two.Email = twoEmail;
                two.UserName = twoEmail;
                two.NormalizedEmail = twoEmail.Normalize();
                two.NormalizedUserName = twoEmail.Normalize();
                two.EmailConfirmed = true;
                await userManager.CreateAsync(two, "123ABC!@#def");

                TAUser three = new TAUser { Uid = "u0000002", Name = "student2", ReferredTo = "2" };
                Add(three);
                await userManager.AddToRoleAsync(three, "Applicant");
                string threeEmail = "u0000002@utah.edu";
                three.Email = threeEmail;
                three.UserName = threeEmail;
                three.NormalizedEmail = threeEmail.Normalize();
                three.NormalizedUserName = threeEmail.Normalize();
                three.EmailConfirmed = true;
                await userManager.CreateAsync(three, "123ABC!@#def");
            }

            SaveChanges();
        }

        public async Task InitializeApplications(UserManager<TAUser> userManager) {
            IEnumerable<TAApplication.Models.Application> applications = await Application.ToListAsync();
            if(applications.Count() == 0) {
                TAUser one = await userManager.FindByEmailAsync("u0000000@utah.edu");

                //PS5 application database seeding for u0000000 & u0000001
                Application application_one = new Application();
                //application_one.ID = 1;
                application_one.User = one;
                application_one.currentDegree = Degree.BS;
                application_one.currentDepartment = "CS";
                application_one.UofU_GPA = (float)4.0;
                application_one.desiredHours = 15;
                application_one.availableBefore = true;
                application_one.numSemesters = 2;
                application_one.personalStatement = "I have always been a huge fan of learning and teaching, especially in the" +
                    "computer science field. My goal with hopefully becoming a TA is being able to grow my knowledge base within" +
                    "this program, as well as develop personal relationships with the professors of the classes I would be a teaching" +
                    "assistant.";
                application_one.transferSchool = "N/A";
                application_one.linkedInURL = "https://www.linkedin.com/student_zero";
                application_one.resumeFileName = "student_zero_resume.pdf";

                Add(application_one);
                await SaveChangesAsync();

                TAUser two = await userManager.FindByEmailAsync("u0000001@utah.edu");

                Application application_two = new Application();
                //application_two.ID = 2;
                application_two.User = two;
                application_two.currentDegree = Degree.BA;
                application_two.currentDepartment = "EAE";
                application_two.UofU_GPA = (float)3.0;
                application_two.desiredHours = 8;
                application_two.availableBefore = false;
                application_two.numSemesters = 4;

                Add(application_two);
                await SaveChangesAsync();
            }
        }

        public async Task InitializeCourses(UserManager<TAUser> userManager)
        {
            IEnumerable<TAApplication.Models.Course> courses = await Course.ToListAsync();
            if (courses.Count() == 0)
            {
                //PS6 course database seeing for ~5 courses
                //Course One
                Course course_one = new Course();
                course_one.User = await userManager.FindByEmailAsync("professor@utah.edu");
                course_one.semesterOffered = Semester.Spring;
                course_one.yearOffered = 2023;
                course_one.courseTitle = "Intro Comp Programming";
                course_one.courseDepartment = "CS";
                course_one.courseNumber = 1400;
                course_one.courseSection = 001;
                course_one.courseDescription = "This course is an introduction to the engineering and mathematical " +
                    "skills required to effectively program computers and is designed for students with no prior programming experience.";
                course_one.profUNID = 1234567;
                course_one.profName = "David Johnson";
                //course_one.daysOffered = "Monday Wednesday";
                //course_one.timeOffered = "01:25PM-02:45PM"
                course_one.courseLocation = "S BEH AUD";
                course_one.creditHours = 4;
                course_one.courseEnrollment = 100;
                course_one.Note = "More freshmen that expected taking this class, need a lot more TA's that also have availability on weekends!";
                Add(course_one);
                await SaveChangesAsync();

                //Course Two
                Course course_two = new Course();
                course_two.User = await userManager.FindByEmailAsync("professor@utah.edu");
                course_two.semesterOffered = Semester.Spring;
                course_two.yearOffered = 2023;
                course_two.courseTitle = "Intro Alg & Data Struct";
                course_two.courseDepartment = "CS";
                course_two.courseNumber = 2420;
                course_two.courseSection = 001;
                course_two.courseDescription = "This course provides an introduction to the problem of engineering computational efficiency " +
                    "into programs. Students will learn about classical algorithms (including sorting, searching, and graph traversal), " +
                    "data structures (including stacks, queues, linked lists, trees, hash tables, and graphs), and analysis of " +
                    "program space and time requirements. Students will complete extensive programming exercises that require the " +
                    "application of elementary techniques from software engineering.";
                course_two.profUNID = 3333333;
                course_two.profName = "Ben Jones";
                //course_two.daysOffered = "Monday Wednesday";
                //course_two.timeOffered = "01:25PM-02:45PM"
                course_two.courseLocation = "S BEH AUD";
                course_two.creditHours = 4;
                course_two.courseEnrollment = 150;
                Add(course_two);
                await SaveChangesAsync();

                //Course Three
                Course course_three = new Course();
                course_three.User = await userManager.FindByEmailAsync("professor@utah.edu");
                course_three.semesterOffered = Semester.Spring;
                course_three.yearOffered = 2023;
                course_three.courseTitle = "Software Practice";
                course_three.courseDepartment = "CS";
                course_three.courseNumber = 3500;
                course_three.courseSection = 001;
                course_three.courseDescription = "Practical exposure to the process of creating large software systems, including requirements " +
                    "specifications, design, implementation, testing, and maintenance. Emphasis on software process, software tools " +
                    "(debuggers, profilers, source code repositories, test harnesses), software engineering techniques (time management, code, " +
                    "and documentation standards, source code management, object-oriented analysis and design), and team development practice. " +
                    "Much of the work will be in groups and will involve modifying preexisting software systems.";
                course_three.profUNID = 4444444;
                course_three.profName = "H. James de St. Germain";
                //course_three.daysOffered = "Monday Wednesday";
                //course_three.timeOffered = "01:25PM-02:45PM"
                course_three.courseLocation = "HEB 2008";
                course_three.creditHours = 4;
                course_three.courseEnrollment = 150;
                Add(course_three);
                await SaveChangesAsync();

                //Course Four
                Course course_four = new Course();
                course_four.User = await userManager.FindByEmailAsync("professor@utah.edu");
                course_four.semesterOffered = Semester.Spring;
                course_four.yearOffered = 2023;
                course_four.courseTitle = "Computer Organization";
                course_four.courseDepartment = "CS";
                course_four.courseNumber = 3810;
                course_four.courseSection = 001;
                course_four.courseDescription = "An in-depth study of computer architecture and design, including topics such as RISC and " +
                    "CISC instruction set architectures, CPU organizations, pipelining, memory systems, input/output, and parallel machines. " +
                    "Emphasis is placed on performance measures and compilation issues.";
                course_four.profUNID = 5555555;
                course_four.profName = "Rajeev Balasubramonian";
                //course_four.daysOffered = "Monday Wednesday";
                //course_four.timeOffered = "01:25PM-02:45PM"
                course_four.courseLocation = "WEB L104";
                course_four.creditHours = 4;
                course_four.courseEnrollment = 100;
                Add(course_four);
                await SaveChangesAsync();

                //Course Five
                Course course_five = new Course();
                course_five.User = await userManager.FindByEmailAsync("professor@utah.edu");
                course_five.semesterOffered = Semester.Spring;
                course_five.yearOffered = 2023;
                course_five.courseTitle = "Computer Systems";
                course_five.courseDepartment = "CS";
                course_five.courseNumber = 4400;
                course_five.courseSection = 001;
                course_five.courseDescription = "Introduction to computer systems from a programmer's point of view. " +
                    "Machine level representations of programs, optimizing program performance, memory hierarchy, linking, " +
                    "exceptional control flow, measuring program performance, virtual memory, concurrent programming with threads, " +
                    "network programming.";
                course_five.profUNID = 7654321;
                course_five.profName = "Daniel Kopta";
                //course_five.daysOffered = "Monday Wednesday";
                //course_five.timeOffered = "01:25PM-02:45PM"
                course_five.courseLocation = "HEB 2004";
                course_five.creditHours = 3;
                course_five.courseEnrollment = 250;
                course_five.Note = "More students enrolled this semester than excpected - need capable TA's confident in low level programming & C.";
                Add(course_five);
                await SaveChangesAsync();
            }
        }

        public async Task InitializeAvailability(UserManager<TAUser> userManager) {
            IEnumerable<TAApplication.Models.Availability> availabilities = await Availability.ToListAsync();
            if (availabilities.Count() == 0) {
                TAUser one = await userManager.FindByEmailAsync("u0000000@utah.edu");
                Availability user1Availabilities = new Availability();
                user1Availabilities.User = one;
                user1Availabilities.ID = one.Id;
                //user1Availabilities.mondayAvailability = "111111111111111100000000000000000000000000000000";
                //user1Availabilities.tuesdayAvailability = "000000000000111111111111111111111111000000000000";
                //user1Availabilities.wednesdayAvailability = "000000000000000000000000000000000000000000000000";
                //user1Availabilities.thursdayAvailability = "000000000000111111111111111111111111000000000000";
                //user1Availabilities.fridayAvailability = "111111111111111100000000000000000000000000000000";
                user1Availabilities.WeekAvailability = "111111111111111100000000000000000000000000000000000000000000000011111111111111111111000000000000000000000000000000000000000000000000000000000000000000000000000011111111111111111111000000000000111111111111111100000000000000000000000000000000";

                Add(user1Availabilities);
                await SaveChangesAsync();
            }
        }

        public async Task InitializeEnrollments(String filename, UserManager<TAUser> userManager)
        {
            IEnumerable<TAApplication.Models.CourseEnrollment> enrollments = await Enrollments.ToListAsync();
            if (enrollments.Count() == 0)
            {
                // get default professor incase a course does not exist for a class enrollment
                TAUser admin = await userManager.FindByEmailAsync("admin@utah.edu");

                // source: https://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array
                using (var reader = new StreamReader(File.OpenRead(@filename)))
                {
                    // convert date lines into storeable string of comma-seperated-values
                    var dateLine = reader.ReadLine();
                    var datesList = new List<string>(dateLine.Split(','));
                    datesList = datesList.GetRange(1, datesList.Count - 1);
                    var dateData = String.Join(',', datesList.ToArray());
                    System.Diagnostics.Debug.WriteLine(dateData);


                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        // split by space as well to seperate course dept and num
                        var values = line.Split(',', ' ');
                        string dept = values[0];
                        string num = values[1];
                        System.Diagnostics.Debug.WriteLine("\n\n\n\n", dept, " ", num);

                        // cut off class information and re-combine into storeable string of comma-seperated-values
                        string[] result = new string[values.Length - 2];
                        Array.Copy(values, 2, result, 0, values.Length - 2);
                        var enrollValues = String.Join(',', result);
                        System.Diagnostics.Debug.WriteLine("\n\n\n\ndata: ", enrollValues, "for course: ", dept, " ", num);

                        // assign values to new enrollment entry
                        CourseEnrollment enrollment = new CourseEnrollment();
                        Course? course = await Course.FirstOrDefaultAsync(m => m.courseDepartment == dept && m.courseNumber == Int32.Parse(num));
                        // attempt to add the course if there is no course matching that description - fill with default data
                        if (course == null)
                        {
                            course = new Course();
                            course.User = admin;
                            course.semesterOffered = Semester.Fall;
                            course.yearOffered = DateTime.Now.Year;
                            course.courseTitle = "Unknown";
                            course.courseDepartment = dept;
                            course.courseNumber = Int32.Parse(num);
                            course.courseSection = 001;
                            course.courseDescription = "Default Course creation. Please correct.";
                            course.profUNID = 0000000;
                            course.profName = "TBD";
                            course.courseLocation = "TBD";
                            course.creditHours = 1;
                            course.courseEnrollment = 1;
                            Add(course);
                            await SaveChangesAsync();
                        }
                        enrollment.Course = course;
                        enrollment.dates = dateData;
                        enrollment.enrollments = enrollValues;

                        Add(enrollment);
                        await SaveChangesAsync();
                    }
                }
            }
        }



        /// <summary>
        /// Every time Save Changes is called, add timestamps
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()  // JIM: Override save changes to add timestamps
        {
            AddTimestamps();
            return base.SaveChanges();
        }
        /// <summary>
        /// Every time Save Changes (Async) is called, add timestamps
        /// </summary>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            AddTimestamps();   // JIM: Override save changes async to add timestamps
            return await base.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is ModificationTracking
                        && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = "";

            if (_httpContextAccessor.HttpContext == null) // happens during startup/initialization code
            {
                currentUsername = "DBSeeder";
            }
            else
            {
                currentUsername = _httpContextAccessor.HttpContext.User.Identity?.Name;
            }

            //currentUsername ??= "Sadness"; // JIM: compound assignment magic... test for null, and if so, assign value

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((ModificationTracking)entity.Entity).CreationDate = DateTime.UtcNow;
                    ((ModificationTracking)entity.Entity).CreatedBy = currentUsername;
                }
                ((ModificationTracking)entity.Entity).ModificationDate = DateTime.UtcNow;
                ((ModificationTracking)entity.Entity).ModifiedBy = currentUsername;
            }
        }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        public DbSet<TAApplication.Models.Application> Application { get; set; }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        public DbSet<TAApplication.Models.Course> Course { get; set; }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        public DbSet<TAApplication.Models.Availability> Availability { get; set; }
        public DbSet<TAApplication.Models.CourseEnrollment> Enrollments { get; set; }
    }
}