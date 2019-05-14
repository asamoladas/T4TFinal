﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Tip4Trip_aka.Models;
using Tip4Trip_aka.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace Tip4Trip_aka.Controllers
{
    
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        public ActionResult Index()
        {
           

            return View();
        }
        public ActionResult MyTrip()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var MyHouses = db.Houses.Include(loc => loc.Location).Where(v => v.Owner.Id == user.Id);
            var Images = db.HouseImages.ToList();
            //List<string> paths = new List<string>();
            //foreach(House item in MyHouses) {paths.Add(Images.) }
            //var Reservations = db.Reservations.Where(b=>b.)
            UserHousesViewModel Myall = new UserHousesViewModel() { User = user, Houses = MyHouses.ToList(), HouseImages=Images };

                       
            return View(Myall);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();

        }
        [Authorize]
        public ActionResult MyReservations()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            List<Reservation> reserlist = db.Reservations.Include(mmn => mmn.HouseRes.Location).Where(z => z.RenterId == user.Id).ToList();
            HouseReservationViewModel housereser = new HouseReservationViewModel();
            housereser.Reservations1 = reserlist;

            return View(housereser);

        }
        public ActionResult T4T()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Search(string searching, DateTime? Sstartdate, DateTime? Enddate , int? occupantsearch)
        {
            var Hous = db.Houses.Include(xxx => xxx.Reservations).Include(mmn => mmn.Location).Where(x => x.Location.NameCity.Contains(searching)).Where(l=>l.MaxOccupancy >= occupantsearch);
            if (occupantsearch == null)
            {  Hous = db.Houses.Include(xxx => xxx.Reservations).Include(mmn => mmn.Location).Where(x => x.Location.NameCity.Contains(searching)); }
                var res = db.Reservations.Where(c => (c.StartDate <= Sstartdate.Value) && (c.EndDate >= Sstartdate.Value)).ToList();
            var res2 = db.Reservations.Where(c => (c.StartDate >= Sstartdate.Value) && (c.StartDate <= Enddate.Value)).ToList();
            var res3 = db.Reservations.Where(c => (c.EndDate >= Enddate.Value) && (c.StartDate <= Enddate.Value)).ToList();
            var res12 = res.Concat(res2);
            var res4 = res3.Concat(res12);
            //var res4 = db.Reservations.Where(c => c.StartDate >= Sstartdate.Value && c.EndDate <= Enddate.Value).ToList();
            // var res2 = db.Reservations.Where(f => (f.EndDate >= Enddate.Value)&& (f.StartDate<= Enddate.Value));
            // var res3 = res.Concat(res2);
            if (Sstartdate == null && Enddate == null) { res4 = db.Reservations.ToList(); }

            var ImagesofHouses = db.HouseImages;

            HousesDates to_search_mas = new HousesDates { houses = Hous.ToList(), reservations = res4.Distinct().ToList() , HouseImages=ImagesofHouses.ToList() };
            if (to_search_mas == null) { return View(); }
            return View(to_search_mas);

            // var Hous = db.Houses.Include(xxx => xxx.Reservations).Include(mmn => mmn.Location).Where(x => x.Location.NameCity.Contains(searching) && x.Address.Contains(Address));

            // var res = db.Reservations.Where(c => (c.StartDate <= Sstartdate.Value) && (c.EndDate >= Sstartdate.Value));
            // var res2 = db.Reservations.Where(f => (f.EndDate >= Enddate.Value)&& (f.StartDate<= Enddate.Value));
            // var res4 = db.Reservations.Where(d=>(d.StartDate >= Sstartdate.Value) && (d.StartDate <= Enddate.Value));
            //var res3 = res.Concat(res2).Concat(res4);
            // if (Sstartdate == null && Enddate == null) { res3 = db.Reservations; }


            //  HousesDates to_search_mas = new HousesDates { houses = Hous.ToList(), reservations = res3.ToList() };
            // if (to_search_mas == null) { ViewBag.Message = "T4TripSearCh."; return View(); }

            // ViewBag.Message = "T4TripSearCh.";
            //  return View(to_search_mas);


        }
    }
}