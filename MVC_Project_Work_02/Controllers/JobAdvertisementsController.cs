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
    public class JobAdvertisementsController : ApiController
    {
        private JobDbContext db = new JobDbContext();

        // GET: api/JobAdvertisements
        public IQueryable<JobAdvertisement> GetJobAdvertisements()
        {
            return db.JobAdvertisements;
        }

        // GET: api/JobAdvertisements/5
        [ResponseType(typeof(JobAdvertisement))]
        public IHttpActionResult GetJobAdvertisement(int id)
        {
            JobAdvertisement jobAdvertisement = db.JobAdvertisements.Find(id);
            if (jobAdvertisement == null)
            {
                return NotFound();
            }

            return Ok(jobAdvertisement);
        }

        // PUT: api/JobAdvertisements/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobAdvertisement(int id, JobAdvertisement jobAdvertisement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobAdvertisement.JobAdvertisementId)
            {
                return BadRequest();
            }

            db.Entry(jobAdvertisement).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobAdvertisementExists(id))
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

        // POST: api/JobAdvertisements
        [ResponseType(typeof(JobAdvertisement))]
        public IHttpActionResult PostJobAdvertisement(JobAdvertisement jobAdvertisement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobAdvertisements.Add(jobAdvertisement);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jobAdvertisement.JobAdvertisementId }, jobAdvertisement);
        }

        // DELETE: api/JobAdvertisements/5
        [ResponseType(typeof(JobAdvertisement))]
        public IHttpActionResult DeleteJobAdvertisement(int id)
        {
            JobAdvertisement jobAdvertisement = db.JobAdvertisements.Find(id);
            if (jobAdvertisement == null)
            {
                return NotFound();
            }

            db.JobAdvertisements.Remove(jobAdvertisement);
            db.SaveChanges();

            return Ok(jobAdvertisement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobAdvertisementExists(int id)
        {
            return db.JobAdvertisements.Count(e => e.JobAdvertisementId == id) > 0;
        }
    }
}