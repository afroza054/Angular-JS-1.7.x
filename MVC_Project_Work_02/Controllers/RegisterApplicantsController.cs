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
    public class RegisterApplicantsController : ApiController
    {
        private JobDbContext db = new JobDbContext();

        // GET: api/RegisterApplicants
        public IQueryable<RegisterApplicant> GetRegisterApplicants()
        {
            return db.RegisterApplicants;
        }

        // GET: api/RegisterApplicants/5
        [ResponseType(typeof(RegisterApplicant))]
        public IHttpActionResult GetRegisterApplicant(int id)
        {
            RegisterApplicant registerApplicant = db.RegisterApplicants.Find(id);
            if (registerApplicant == null)
            {
                return NotFound();
            }

            return Ok(registerApplicant);
        }

        // PUT: api/RegisterApplicants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegisterApplicant(int id, RegisterApplicant registerApplicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registerApplicant.RegisterApplicantId)
            {
                return BadRequest();
            }

            db.Entry(registerApplicant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisterApplicantExists(id))
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

        // POST: api/RegisterApplicants
        [ResponseType(typeof(RegisterApplicant))]
        public IHttpActionResult PostRegisterApplicant(RegisterApplicant registerApplicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RegisterApplicants.Add(registerApplicant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = registerApplicant.RegisterApplicantId }, registerApplicant);
        }

        // DELETE: api/RegisterApplicants/5
        [ResponseType(typeof(RegisterApplicant))]
        public IHttpActionResult DeleteRegisterApplicant(int id)
        {
            RegisterApplicant registerApplicant = db.RegisterApplicants.Find(id);
            if (registerApplicant == null)
            {
                return NotFound();
            }

            db.RegisterApplicants.Remove(registerApplicant);
            db.SaveChanges();

            return Ok(registerApplicant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegisterApplicantExists(int id)
        {
            return db.RegisterApplicants.Count(e => e.RegisterApplicantId == id) > 0;
        }
    }
}