﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelManager.VIewModal;

namespace TravelManager.Modal.Services
{

    public class CountryService : AllServices<Country>
    {
        public CountryService(TravelContext context) : base(context)
        {
        }
           
    }
}

