namespace MVC_Project_Work_02.Migrations.JF
{
    using MVC_Project_Work_02.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC_Project_Work_02.Models.JobDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\JF";
        }

        protected override void Seed(MVC_Project_Work_02.Models.JobDbContext context)
        {
            OnlineJobSite o = new OnlineJobSite { OnlineJobSiteName = "Bdjobs", StartedJourney = DateTime.Parse("2012-02-04").Date, Web = "https://www.bdjobs.com", Response = true };
            o.JobAdvertisements.Add(new JobAdvertisement { JobAdvertisementName = "Future Leader", Post = JobType.MTO});
            o.JobAdvertisements.Add(new JobAdvertisement { JobAdvertisementName = "SPO Cader", Post = JobType.Senior });
            o.RegisterApplicants.Add(new RegisterApplicant { RegisterApplicantName = "Natasha", ContactEmail = "natasha@gmail.com" });
            o.RegisterApplicants.Add(new RegisterApplicant { RegisterApplicantName = "Rouza", ContactEmail = "rouza54@yahoo.com" });

            OnlineJobSite o1 = new OnlineJobSite { OnlineJobSiteName = "Kormo", StartedJourney = DateTime.Parse("2014-03-09").Date, Web = "https://www.kormo.com", Response = false };
            o1.JobAdvertisements.Add(new JobAdvertisement { JobAdvertisementName = "It Consultant", Post = JobType.ItProfessional });
            o1.JobAdvertisements.Add(new JobAdvertisement { JobAdvertisementName = "It Officer", Post = JobType.Junior });
            o1.RegisterApplicants.Add(new RegisterApplicant { RegisterApplicantName = "Ahmed", ContactEmail = "ahmed2015@gmail.com" });
            o1.RegisterApplicants.Add(new RegisterApplicant { RegisterApplicantName = "Masud", ContactEmail = "masud@yahoo.com" });


            context.OnlineJobSites.Add(o);
            context.OnlineJobSites.Add(o1);
            context.SaveChanges();
        }
    }
}
