﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entity
{
    public  class AccommodationPackage
    {
        public int ID { get; set; }

        public int AccommodationTypeID { get; set; }

        public virtual AccommodationType AccommodationType { get; set; }

        public string Name { get; set; }

        public int NoOfRoom { get; set; }

        public decimal FeePerNight { get; set; }

    }
}
