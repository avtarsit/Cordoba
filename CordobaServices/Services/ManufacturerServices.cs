﻿using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class ManufacturerServices : IManufacturerServices
    {

        private GenericRepository<ManufacturersEntity> objGenericRepository = new GenericRepository<ManufacturersEntity>();

        public List<ManufacturersEntity> GetManufacturersList()
        {
            List<ManufacturersEntity> ManufacturersList = new List<ManufacturersEntity>();
            try
            {
                var Result = objGenericRepository.ExecuteSQL<ManufacturersEntity>("GetManufacturersList").ToList<ManufacturersEntity>();

                if (Result != null)
                    ManufacturersList = Result.ToList();

            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
            }
            return ManufacturersList;
        }



        public ManufacturersEntity GetManufaturerDetail(int manufacturer_id)
        {
            ManufacturersEntity Manufacturer = new ManufacturersEntity();
            List<StoreEntity> StoreList = new List<StoreEntity>();
            ManufacturersStoreEntity ManufacturerStoreList = new ManufacturersStoreEntity();

            if (manufacturer_id > 0)
            {
                var parammanufacturer_id = new SqlParameter
                {
                    ParameterName = "manufacturer_id",
                    DbType = DbType.Int32,
                    Value = manufacturer_id
                };
                var Result = objGenericRepository.ExecuteSQL<ManufacturersEntity>("GetManufaturerDetail", parammanufacturer_id).FirstOrDefault();
                if (Result != null)
                    Manufacturer = Result;
            }
            else
            {
                Manufacturer = new ManufacturersEntity();
            }
            var parammanufacturerIdForStore = new SqlParameter
            {
                ParameterName = "manufacturer_id",
                DbType = DbType.Int32,
                Value = manufacturer_id
            };
            var storeResult = objGenericRepository.ExecuteSQL<StoreEntity>("GetManufacturerStoreList", parammanufacturerIdForStore).ToList<StoreEntity>();
            if (storeResult != null)
                StoreList = storeResult.ToList();

            ManufacturerStoreList.manufacturer_id = manufacturer_id;
            ManufacturerStoreList.ManufacturerStore = StoreList;
            Manufacturer.ManufacturerStoreList = ManufacturerStoreList;
            return Manufacturer;

        }


        public int InsertUpdateManufacture(ManufacturersEntity manufacturersEntity)
        {

            try
            {
                var manufacturerIdParam = new SqlParameter { ParameterName = "manufacturer_id", DbType = DbType.Int32, Value = manufacturersEntity.manufacturer_id };
                var nameParam = new SqlParameter { ParameterName = "name", DbType = DbType.String, Value = manufacturersEntity.name };
                var imageUrlParam = new SqlParameter { ParameterName = "image", DbType = DbType.String, Value = manufacturersEntity.image ?? DBNull.Value.ToString() };
                var sortorderParam = new SqlParameter { ParameterName = "sort_order", DbType = DbType.Int32, Value = manufacturersEntity.sort_order };
                var storeIdCSVParam = new SqlParameter { ParameterName = "storeIdCSV", DbType = DbType.String, Value = manufacturersEntity.StoreIdCSV ?? DBNull.Value.ToString() };
                var result = objGenericRepository.ExecuteSQL<int>("InsertUpdateManufacture", manufacturerIdParam, nameParam, imageUrlParam, sortorderParam, storeIdCSVParam).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public int DeleteManufacturer(int manufacturer_id)
        {
            try
            {
                var paramManufacturerId = new SqlParameter { ParameterName = "manufacturer_id", DbType = DbType.Int32, Value = manufacturer_id };
                int result = objGenericRepository.ExecuteSQL<int>("DeleteManufacturer", paramManufacturerId).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}

