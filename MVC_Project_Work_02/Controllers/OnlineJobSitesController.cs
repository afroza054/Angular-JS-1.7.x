using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MVC_Project_Work_02.Models;

namespace MVC_Project_Work_02.Controllers
{
    public class OnlineJobSitesController : ApiController
    {
        private JobDbContext db = new JobDbContext();

        // GET: api/OnlineJobSites
        public IQueryable<OnlineJobSite> GetOnlineJobSites()
        {
            return db.OnlineJobSites;
        }

        // GET: api/OnlineJobSites/5
        [ResponseType(typeof(OnlineJobSite))]
        public IHttpActionResult GetOnlineJobSite(int id)
        {
            OnlineJobSite onlineJobSite = db.OnlineJobSites.Find(id);
            if (onlineJobSite == null)
            {
                return NotFound();
            }

            return Ok(onlineJobSite);
        }

        // PUT: api/OnlineJobSites/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOnlineJobSite(int id, OnlineJobSite onlineJobSite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != onlineJobSite.OnlineJobSiteId)
            {
                return BadRequest();
            }

            var ext = db.OnlineJobSites.Include(x => x.JobAdvertisements).Include(y => y.RegisterApplicants).First(x => x.OnlineJobSiteId == onlineJobSite.OnlineJobSiteId);
            ext.OnlineJobSiteName = onlineJobSite.OnlineJobSiteName;
            ext.StartedJourney = onlineJobSite.StartedJourney;
            ext.Web = onlineJobSite.Web;
            ext.Response = onlineJobSite.Response;

            if (onlineJobSite.JobAdvertisements != null && onlineJobSite.JobAdvertisements.Count > 0)
            {
                var prs = onlineJobSite.JobAdvertisements.ToArray();
                for (var i = 0; i < prs.Length; i++)
                {
                    var temp = ext.JobAdvertisements.FirstOrDefault(c => c.JobAdvertisementId == prs[i].JobAdvertisementId);
                    if (temp != null)
                    {
                        temp.JobAdvertisementName = prs[i].JobAdvertisementName;
                        temp.Post = prs[i].Post;
                    }
                    else
                    {
                        ext.JobAdvertisements.Add(prs[i]);
                    }
                }
                prs = ext.JobAdvertisements.ToArray();
                for (var i = 0; i < prs.Length; i++)
                {
                    var temp = onlineJobSite.JobAdvertisements.FirstOrDefault(x => x.JobAdvertisementId == prs[i].JobAdvertisementId);
                    if (temp == null)
                        db.Entry(prs[i]).State = EntityState.Deleted;
                }
            }
            if (onlineJobSite.RegisterApplicants != null && onlineJobSite.RegisterApplicants.Count > 0)
            {
                var srvs = onlineJobSite.RegisterApplicants.ToArray();
                for (var i = 0; i < srvs.Length; i++)
                {
                    var temp = ext.RegisterApplicants.FirstOrDefault(c => c.RegisterApplicantId == srvs[i].RegisterApplicantId);
                    if (temp != null)
                    {
                        temp.RegisterApplicantName = srvs[i].RegisterApplicantName;
                        temp.ContactEmail = srvs[i].ContactEmail;
                    }
                    else
                    {
                        ext.RegisterApplicants.Add(srvs[i]);
                    }
                }
                srvs = ext.RegisterApplicants.ToArray();
                for (var i = 0; i < srvs.Length; i++)
                {
                    var temp = onlineJobSite.RegisterApplicants.FirstOrDefault(x => x.RegisterApplicantId == srvs[i].RegisterApplicantId);
                    if (temp == null)
                        db.Entry(srvs[i]).State = EntityState.Deleted;
                }
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OnlineJobSiteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OnlineJobSites
        [ResponseType(typeof(OnlineJobSite))]
        public IHttpActionResult PostOnlineJobSite(OnlineJobSite onlineJobSite)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.OnlineJobSites.Add(onlineJobSite);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = onlineJobSite.OnlineJobSiteId }, onlineJobSite);
        }

        // DELETE: api/OnlineJobSites/5
        [ResponseType(typeof(OnlineJobSite))]
        public IHttpActionResult DeleteOnlineJobSite(int id)
        {
            OnlineJobSite onlineJobSite = db.OnlineJobSites.Find(id);
            if (onlineJobSite == null)
            {
                return NotFound();
            }

            db.OnlineJobSites.Remove(onlineJobSite);
            db.SaveChanges();

            return Ok(onlineJobSite);
        }
        /// <summary>
        /// /////////sir
        ///
        /// </summary>
        /// <returns></returns>
        [Route("custom/OnlineJobSites")]
        public IQueryable<OnlineJobSite> GetOnlineJobSitesWithRelated()
        {
            return db.OnlineJobSites
                .Include("JobAdvertisements")
                .Include("RegisterApplicants");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("custom/OnlineJobSites/{id}")]
        public IHttpActionResult GetOnlineJobSiteWithRelated(int id)
        {
            return Ok(db.OnlineJobSites
                    .Include(x => x.JobAdvertisements)
                    .Include(x => x.RegisterApplicants)
                    .FirstOrDefault(x => x.OnlineJobSiteId == id));
        }
        private bool OnlineJobSiteExists(int id)
        {
            return db.OnlineJobSites.Count(e => e.OnlineJobSiteId == id) > 0;
        }
    }
}