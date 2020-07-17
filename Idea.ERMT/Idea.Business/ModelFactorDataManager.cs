using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Migrations;
using System.Linq;
using Idea.DAL;
using Idea.Entities;

namespace Idea.Business
{
    public class ModelFactorDataManager
    {
        /// <summary>
        /// Returns a new ModelFactorDatas.
        /// </summary>
        /// <returns></returns>
        public static ModelFactorData GetNew()
        {
            return new ModelFactorData();
        }

        /// <summary>
        /// Saves the ModelFactorDatas.
        /// </summary>
        /// <param name="modelFactorData"></param>
        /// <returns></returns>
        public static ModelFactorData Save(ModelFactorData modelFactorData)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                List<ModelFactorData> mfds = context.ModelFactorDatas.Where(
                        mfd2 => mfd2.Date == modelFactorData.Date
                            && mfd2.IDModelFactor == modelFactorData.IDModelFactor
                            && mfd2.IDRegion == modelFactorData.IDRegion
                            && mfd2.IDModelFactorData != modelFactorData.IDModelFactorData).ToList();

                foreach (ModelFactorData factorData in mfds)
                {

                    context.ModelFactorDatas.Remove(factorData);

                }

                context.ModelFactorDatas.AddOrUpdate(modelFactorData);

                context.SaveChanges();
                return modelFactorData;
            }
        }

        /// <summary>
        /// Returns the first available date to be used when pasting data for a factor.
        /// </summary>
        /// <param name="idModelFactor"></param>
        /// <returns></returns>
        public static DateTime GetAvailableDateForPastedDataByModelFactor(int idModelFactor)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                List<DateTime> usedDates = context.ModelFactorDatas.Where(mfd => mfd.IDModelFactor == idModelFactor).OrderBy(mfd => mfd.Date).Select(mfd => mfd.Date).ToList();
                if (usedDates.Count > 0)
                {
                    return usedDates[usedDates.Count - 1].AddDays(1);
                }

                return DateTime.Now;
            }
        }

        /// <summary>
        /// Returns the first available date to be used when pasting data for a region.
        /// </summary>
        /// <param name="idRegion"></param>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static DateTime GetAvailableDateForPastedDataByRegionAndModel(int idRegion, int idModel)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                List<int> idModelFactors =
                    context.ModelFactors.Where(mf => mf.IDModel == idModel).Select(mf => mf.IDModelFactor).ToList();

                List<DateTime> usedDates = context.ModelFactorDatas.Where(mfd => mfd.IDRegion == idRegion && idModelFactors.Contains(mfd.IDModelFactor)).OrderBy(mfd => mfd.Date).Select(mfd => mfd.Date).ToList();
                if (usedDates.Count > 0)
                {
                    return usedDates[usedDates.Count - 1].AddDays(1);
                }

                return DateTime.Now;
            }
        }

        /// <summary>
        /// Returns the list of all the ModelFactorDatas.
        /// </summary>
        /// <returns></returns>
        public static List<ModelFactorData> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.ModelFactorDatas.ToList();
            }
        }

        /// <summary>
        /// Gets and Returns the ModelFactorData with IDModelFactorData == idModelFactorData
        /// </summary>
        /// <param name="idModelFactorData"></param>
        /// <returns></returns>
        public static ModelFactorData GetById(int idModelFactorData)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.ModelFactorDatas.FirstOrDefault(mfd => mfd.IDModelFactorData == idModelFactorData);
            }
        }

        /// <summary>
        /// Gets and  Returns the ModelFactorDatas by idModelFactor.
        /// </summary>
        /// <param name="idModelFactor"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetByModelFactor(int idModelFactor)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.ModelFactorDatas.Where(mfd => mfd.IDModelFactor == idModelFactor).OrderBy(mfd => mfd.Date).ToList();
            }
        }


        /// <summary>
        /// Gets and Returns the ModelFactorDatas by date, model and region.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="idModel"></param>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetByDateModelRegion(DateTime date, int idModel, int idRegion)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (from mfd in context.ModelFactorDatas
                        join mf in context.ModelFactors on mfd.IDModelFactor equals mf.IDModelFactor
                        where mf.IDModel == idModel
                              && mfd.Date == date
                              && mfd.IDRegion == idRegion
                        select mfd).ToList();
            }
        }

        /// <summary>
        /// Gets and Returns the list of map data with parameters: idModelFactor, regionsIds and DateTime. 
        /// </summary>
        /// <param name="idModelFactor"></param>
        /// <param name="regionsIds"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetMapData(int idModelFactor, string regionsIds, DateTime from, DateTime to)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                var o = (context.spModelFactorData_GetMapData(idModelFactor, regionsIds, from, to));
                var aux = from o2 in o
                          let dtModified = o2.dt_modified
                          where dtModified != null
                          let data = o2.Data
                          where data != null
                          let dateTime = o2.Date
                          where dateTime != null
                          let dtCreated = o2.dt_created
                          where dtCreated != null
                          let idModelFactorData = o2.IDModelFactorData
                          where idModelFactorData != null
                          select new ModelFactorData
                                  {
                                      IDModelFactor = o2.IDModelFactor,
                                      Data = (Decimal)data,
                                      Date = (DateTime)dateTime,
                                      DateModified = (DateTime)dtModified,
                                      DateCreated = (DateTime)dtCreated,
                                      IDModelFactorData = (int)idModelFactorData,
                                      IDRegion = o2.IDRegion
                                  };
                return aux.ToList();
            }
        }

        /// <summary>
        /// Returns the ModelFactorDatas for the model factor, regions and between from and to.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="idModelFactor"></param>
        /// <param name="regionIds"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetBetweenDatesModelFactorRegion(DateTime from, DateTime to, int idModelFactor, string regionIds)
        {
            List<int> regionList = regionIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(region => Convert.ToInt32(region)).ToList(); ;
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return
                    context.ModelFactorDatas.Where(
                        mfd => mfd.Date >= from && mfd.Date <= to && mfd.IDModelFactor == idModelFactor && regionList.Contains(mfd.IDRegion)).ToList();
            }
        }

        /// <summary>
        /// Returns a list of regions containing data for the provided model factors and dates.
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="modelFactors"></param>
        /// <returns></returns>
        public static List<Region> GetRegionsWithData(DateTime dateFrom, DateTime dateTo, IEnumerable<ModelFactor> modelFactors)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                IEnumerable<int> modelFactorIDs = modelFactors.Select(mf2 => mf2.IDModelFactor);
                return (from mfd in context.ModelFactorDatas
                        join mf in context.ModelFactors on mfd.IDModelFactor equals mf.IDModelFactor
                        join r in context.Regions on mfd.IDRegion equals r.IDRegion
                        where modelFactorIDs.Contains(mf.IDModelFactor)
                              && mfd.Date >= dateFrom && mfd.Date <= dateTo
                        select r).Distinct().ToList();
            }
        }

        /// <summary>
        /// Returns the minor date with data for the model.
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static DateTime? GetMinorDate(int idModel)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                ModelFactorData aux = (from mfd in context.ModelFactorDatas
                                       join mf in context.ModelFactors
                                           on mfd.IDModelFactor equals mf.IDModelFactor
                                       where mf.IDModel == idModel
                                       select mfd).OrderBy(mfd => mfd.Date).FirstOrDefault();

                if (aux != null)
                {
                    return aux.Date;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the major date with data for the model.
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static DateTime? GetMajorDate(int idModel)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                ModelFactorData aux = (from mfd in context.ModelFactorDatas
                                       join mf in context.ModelFactors
                                           on mfd.IDModelFactor equals mf.IDModelFactor
                                       where mf.IDModel == idModel
                                       select mfd).OrderByDescending(mfd => mfd.Date).FirstOrDefault();

                if (aux != null)
                {
                    return aux.Date;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the max level that has data for the model factor and region, between the from and to dates.
        /// </summary>
        /// <param name="idModelFactor"></param>
        /// <param name="idRegion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static int GetMaxLevelWithData(int idModelFactor, int idRegion, DateTime from, DateTime to)
        {

            List<int> childRegionIDs = RegionManager.GetAllChilds(idRegion).Select(r => r.IDRegion).ToList();

            int maxLevel = -1;
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                List<ModelFactorData> modelFactorDatas =
                    context.ModelFactorDatas.Where(
                        mfd =>
                            mfd.IDModelFactor == idModelFactor && childRegionIDs.Contains(mfd.IDRegion) &&
                            mfd.Date >= from && mfd.Date <= to).ToList();


                maxLevel = modelFactorDatas.Select(modelFactorData => RegionManager.GetRegionLevel(modelFactorData.IDRegion)).Concat(new[] { maxLevel }).Max();
            }
            return maxLevel;
        }


        /// <summary>
        /// Return the ModelFactorData with the most recent data.
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static ModelFactorData GetModelFactorLastDate(int idModel)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (from mf in context.ModelFactors
                        join mfd in context.ModelFactorDatas
                            on mf.IDModelFactor equals mfd.IDModelFactor
                        where mf.IDModel == idModel
                        select mfd).OrderByDescending(mfd => mfd.Date).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the ModelFactorDatas with data available.
        /// </summary>
        /// <param name="idModel"></param>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetModelFactorWithDataAvailable(int idModel, int idRegion)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (from mfd in context.ModelFactorDatas
                        join mf in context.ModelFactors
                            on mfd.IDModelFactor equals mf.IDModelFactor
                        where mf.IDModel == idModel && mfd.IDRegion == idRegion
                        select mfd).OrderBy(mfd => mfd.Date).ToList();
            }
        }

        /// <summary>
        /// Returns the ModelFactorDatas by region.
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetByRegion(int idRegion)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.ModelFactorDatas.Where(mfd => mfd.IDRegion == idRegion).ToList();
            }
        }

        /// <summary>
        /// Returns the ModelFactorDatas for the idmodelfactor and date.
        /// </summary>
        /// <param name="idModelFactor"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetByModelFactorIdAndDate(int idModelFactor, DateTime date)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return
                    context.ModelFactorDatas.Where(mfd => mfd.IDModelFactor == idModelFactor && mfd.Date == date)
                        .ToList();
            }
        }

        /// <summary>
        /// Returns the ModelFactorDatas by date, idmodelfactor and idregion.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="idModelFactor"></param>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetByDateModelFactorRegion(DateTime date, int idModelFactor, int idRegion)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return
                    context.ModelFactorDatas.Where(
                        mfd => mfd.Date == date && mfd.IDModelFactor == idModelFactor && mfd.IDRegion == idRegion)
                        .OrderBy(mfd => mfd.Date)
                        .ToList();
            }

        }

        /// <summary>
        /// Deletes the ModelFactorData.
        /// </summary>
        /// <param name="modelfactorData"></param>
        public static void Delete(ModelFactorData modelfactorData)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                ModelFactorData aux = context.ModelFactorDatas.FirstOrDefault(mfd => mfd.IDModelFactorData == modelfactorData.IDModelFactorData);
                context.ModelFactorDatas.Remove(aux);
                context.SaveChanges();
            }
        }
    }
}
