﻿using CordobaModels;
using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces_Layout
{
  public interface ICartServices
    {

      List<CartEntity> GetCartDetailsByCartGroupId(int? StoreID, int cartgroup_id);


    }
}