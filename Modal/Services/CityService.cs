﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal.Services
{
    class CityService : AllServices<City>
    {
        DbContext _context;


        public CityService(DbContext context) : base(context)
        {
            _context = context;

           
        }
       

    }
}