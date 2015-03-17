using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolarDatalogger.Models;

namespace SolarDatalogger.Controllers
{
    public class SolarDatasController : Controller
    {
        private SolarPanelDataEntities db = new SolarPanelDataEntities();

        // GET: SolarDatas
        public ActionResult Index()
        {
            return View(db.SolarDatas.ToList());
        }

        // GET: SolarDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolarData solarData = db.SolarDatas.Find(id);
            if (solarData == null)
            {
                return HttpNotFound();
            }
            return View(solarData);
        }

        // GET: SolarDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SolarDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TimeInserted,VoltageOne,VoltageTwo,VoltageThree,Temperature")] SolarData solarData)
        {
            if (ModelState.IsValid)
            {
                db.SolarDatas.Add(solarData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(solarData);
        }

        // GET: SolarDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolarData solarData = db.SolarDatas.Find(id);
            if (solarData == null)
            {
                return HttpNotFound();
            }
            return View(solarData);
        }

        // POST: SolarDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeInserted,VoltageOne,VoltageTwo,VoltageThree,Temperature")] SolarData solarData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solarData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(solarData);
        }

        // GET: SolarDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolarData solarData = db.SolarDatas.Find(id);
            if (solarData == null)
            {
                return HttpNotFound();
            }
            return View(solarData);
        }

        // POST: SolarDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolarData solarData = db.SolarDatas.Find(id);
            db.SolarDatas.Remove(solarData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
