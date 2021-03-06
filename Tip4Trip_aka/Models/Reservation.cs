﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Tip4Trip_aka.Models
{
    public class Reservation
    {

       private string renterId = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).Id;
        public int Id { get; set; }
        public House HouseRes { get; set; }
        public int HouseId { get; set; }
        //public string renter { get; set; }
        public ApplicationUser Renter { get; set; }
        public string RenterId { get { return renterId; } set { renterId = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).Id; } }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DateGreaterThanAttribute(otherPropertyName = "StartDate", ErrorMessage = "End date must be greater than start date")]
        [Display(Name = "End Date")]
        //[Range(typeof(DateTime),"Reservation.StartDate","??",ErrorMessage ="End Date is earlier than Start Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

       
        
        public int Occupants { get; set; }

        public DateTime DateOfBooking { get; set; }
        public string CustommerComments { get; set; }
        public double PricePerNightCharged { get; set; }


        int Status = 1; // 1 means that the status is Active

    }
}