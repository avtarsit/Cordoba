﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class ManufacturersStoreEntity
    {
       public int? ManufacturerID { get; set; }
       public List<StoreEntity> ManufacturerStore { get; set; }

    }
}