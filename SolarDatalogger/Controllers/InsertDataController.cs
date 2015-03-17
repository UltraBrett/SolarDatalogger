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
using SolarDatalogger.Models;

namespace SolarDatalogger.Controllers
{
    public class InsertDataController : ApiController
    {
        private SolarPanelDataEntities db = new SolarPanelDataEntities();

        // GET: api/InsertData
        public IQueryable<SolarData> GetSolarDatas()
        {
            return db.SolarDatas;
        }

        // GET: api/InsertData/5
        [ResponseType(typeof(SolarData))]
        public IHttpActionResult GetSolarData(int id)
        {
            SolarData solarData = db.SolarDatas.Find(id);
            if (solarData == null)
            {
                return NotFound();
            }

            return Ok(solarData);
        }

        // PUT: api/InsertData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSolarData(int id, SolarData solarData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != solarData.Id)
            {
                return BadRequest();
            }

            db.Entry(solarData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolarDataExists(id))
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

        // POST: api/InsertData
        [ResponseType(typeof(SolarData))]
        public IHttpActionResult PostSolarData(SolarData solarData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SolarDatas.Add(solarData);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = solarData.Id }, solarData);
        }

        // DELETE: api/InsertData/5
        [ResponseType(typeof(SolarData))]
        public IHttpActionResult DeleteSolarData(int id)
        {
            SolarData solarData = db.SolarDatas.Find(id);
            if (solarData == null)
            {
                return NotFound();
            }

            db.SolarDatas.Remove(solarData);
            db.SaveChanges();

            return Ok(solarData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SolarDataExists(int id)
        {
            return db.SolarDatas.Count(e => e.Id == id) > 0;
        }
    }
}