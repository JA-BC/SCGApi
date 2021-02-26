using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebApi.Utilities.Filters;
using WebApi.Utilities.Paginations;

namespace WebApi.Utilities.IQueryableExtensions
{
    public static class IQueryableExtensions
    {
        public static List<TModel> AddPagination<TModel>(
            this IQueryable<TModel> collection, Pagination pagination)
        {
            var query = new List<TModel>();
            query = collection.Skip((pagination.Page - 1) * pagination.Limit).Take(pagination.Limit).ToList();
            // "Capacity" contiene el total de registros despues de haber sido filtrado
            query.Capacity = collection.Count();
            return query;
        }

        public static IQueryable<TModel> AddFilter<TModel>(this IQueryable<TModel> collection, Filter[] filters)
        {

            try
            {
                if (filters.Count() < 1)
                {
                    return collection;
                }

                var query = new List<TModel>();

                for (int i = 0; i < filters.Length; i++)
                {
                    var index = i; // index alterated by the linq

                    Func<TModel, object> getPropValue = (model) =>
                    {
                        PropertyInfo prop = typeof(TModel).GetProperty(filters[index].PropertyName);
                        var value = prop.GetValue(model, null);

                        return value ?? "null";
                    };

                    switch (filters[index].Operator)
                    {
                        #region Contains
                        case EFilterOperator.Contains:
                            if (index == 0)
                            {
                                query = (from model in collection
                                         where getPropValue(model).ToString().ToLower()
                                               .Contains(filters[index].Value.ToLower())
                                         select model).ToList();
                            } else
                            {
                                query =  (from model in query
                                          where getPropValue(model).ToString().ToLower()
                                                .Contains(filters[index].Value.ToLower())
                                          select model).ToList();
                            }
                            break;
                        #endregion

                        #region Equals
                        case EFilterOperator.Equals:

                            if (index == 0)
                            {
                                query = (from model in collection
                                         where getPropValue(model).ToString()
                                               .ToLower() == filters[index].Value.ToLower()
                                         select model).ToList();
                            }
                            else
                            {
                                query = (from model in query
                                         where getPropValue(model).ToString()
                                               .ToLower() == filters[index].Value.ToLower()
                                         select model).ToList();
                            }
                            break;
                        #endregion

                        #region NotEquals
                        case EFilterOperator.NotEquals:

                            if (index == 0)
                            {
                                query = (from model in collection
                                         where getPropValue(model).ToString()
                                               .ToLower() != filters[index].Value.ToLower()
                                         select model).ToList();
                            }
                            else
                            {
                                query = (from model in query
                                         where getPropValue(model).ToString()
                                               .ToLower() != filters[index].Value.ToLower()
                                         select model).ToList();
                            }
                            break;
                        #endregion

                        #region Between
                        case EFilterOperator.Between:
                            string[] splitValue = filters[index].Value.Split('&');

                            if (splitValue.Length != 2) throw new Exception("Debe enviar dos valores válidos");

                            if (int.TryParse(splitValue[0], out int startValue) && int.TryParse(splitValue[1], out int endValue))
                            {

                                if (startValue > endValue) throw new Exception("El valor inicial no puede ser mayor");

                                if (index == 0)
                                {
                                    query = (from model in collection
                                             where int.Parse(getPropValue(model).ToString()) >= startValue &&
                                                   int.Parse(getPropValue(model).ToString()) <= endValue
                                             select model).ToList();
                                }
                                else
                                {
                                    query = (from model in query
                                             where int.Parse(getPropValue(model).ToString()) >= startValue &&
                                                   int.Parse(getPropValue(model).ToString()) <= endValue
                                             select model).ToList();
                                }

                            }
                            else if (DateTime.TryParse(splitValue[0], out DateTime startDateValue) && DateTime.TryParse(splitValue[1], out DateTime endDateValue))
                            {

                                if (index == 0)
                                {
                                    query = (from model in collection
                                             where DateTime.Parse(getPropValue(model).ToString()) >= startDateValue &&
                                                   DateTime.Parse(getPropValue(model).ToString()) <= endDateValue
                                             select model).ToList();
                                }
                                else
                                {
                                    query = (from model in query
                                             where DateTime.Parse(getPropValue(model).ToString()) >= startDateValue &&
                                                   DateTime.Parse(getPropValue(model).ToString()) <= endDateValue
                                             select model).ToList();
                                }

                            }
                            else
                            {
                                throw new Exception($"Los valores {splitValue[0]} y {splitValue[1]} no son válidos");
                            }

                            break;

                        #endregion

                        #region GreaterThen
                        case EFilterOperator.GreaterThan:

                            if (int.TryParse(filters[index].Value, out int intValue))
                            {

                                if (index == 0)
                                {
                                    query = (from model in collection
                                             where int.Parse(getPropValue(model).ToString()) >= intValue
                                             select model).ToList();
                                }
                                else
                                {
                                    query = (from model in query
                                             where int.Parse(getPropValue(model).ToString()) >= intValue
                                             select model).ToList();
                                }

                            }
                            else if (DateTime.TryParse(filters[index].Value, out DateTime dateValue))
                            {

                                if (index == 0)
                                {
                                    query = (from model in collection
                                             where DateTime.Parse(getPropValue(model).ToString()) >= dateValue
                                             select model).ToList();
                                }
                                else
                                {
                                    query = (from model in query
                                             where DateTime.Parse(getPropValue(model).ToString()) >= dateValue
                                             select model).ToList();
                                }
                            }
                            else
                            {
                                throw new Exception($"El valor {filters[index].Value} no es válido");
                            }

                            break;
                        #endregion

                        #region LessThan
                        case EFilterOperator.LessThan:

                            if (int.TryParse(filters[index].Value, out intValue))
                            {

                                if (index == 0)
                                {
                                    query = (from model in collection
                                             where int.Parse(getPropValue(model).ToString()) <= intValue
                                             select model).ToList();
                                }
                                else
                                {
                                    query = (from model in query
                                             where int.Parse(getPropValue(model).ToString()) <= intValue
                                             select model).ToList();
                                }

                            }
                            else if (DateTime.TryParse(filters[index].Value, out DateTime dateValue))
                            {

                                if (index == 0)
                                {
                                    query = (from model in collection
                                             where DateTime.Parse(getPropValue(model).ToString()) <= dateValue
                                             select model).ToList();
                                }
                                else
                                {
                                    query = (from model in query
                                             where DateTime.Parse(getPropValue(model).ToString()) <= dateValue
                                             select model).ToList();
                                }
                            }
                            else
                            {
                                throw new Exception($"El valor {filters[index].Value} no es válido");
                            }

                            break;
                        #endregion

                        #region StartWith
                        case EFilterOperator.StartWith:

                            if (index == 0)
                            {
                                query = (from model in collection
                                         where getPropValue(model).ToString().ToLower()
                                               .StartsWith(filters[index].Value.ToLower())
                                         select model).ToList();
                            }
                            else
                            {
                                query = (from model in query
                                         where getPropValue(model).ToString().ToLower()
                                               .StartsWith(filters[index].Value.ToLower())
                                         select model).ToList();
                            }
                            break;
                        #endregion

                        #region EndWith
                        case EFilterOperator.EndWith:

                            if (index == 0)
                            {
                                query = (from model in collection
                                         where getPropValue(model).ToString().ToLower()
                                               .EndsWith(filters[index].Value.ToLower())
                                         select model).ToList();
                            }
                            else
                            {
                                query = (from model in query
                                         where getPropValue(model).ToString().ToLower()
                                               .EndsWith(filters[index].Value.ToLower())
                                         select model).ToList();
                            }
                            break;
                        #endregion

                        #region Default
                        default:
                            throw new Exception("Se debe elegir un operador válido");
                            #endregion
                    }
                }


               return query.Distinct().AsQueryable();

            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static IQueryable<TModel> AddSortBy<TModel>(this IQueryable<TModel> collection, SortFilter[] sorts)
        {

            try
            {
                if (sorts.Count() < 1)
                {
                    return collection;
                }

                IOrderedEnumerable<TModel> orderedQuery = null;

                for (int i = 0; i < sorts.Length; i++)
                {
                    var index = i; // index alterated by the linq
                    Func<TModel, object> func = m => typeof(TModel) // m <=> model (TModel)
                                                     .GetProperty(sorts[index].PropertyName)
                                                     .GetValue(m, null);

                    switch (sorts[index].Operator)
                    {
                        #region Ascendent
                        case ESortOperator.Ascendent:
                            orderedQuery = (index == 0)
                                            ? collection.OrderBy(func)
                                            : orderedQuery.ThenBy(func);
                            break;
                        #endregion

                        #region Descendet
                        case ESortOperator.Descendent:
                            orderedQuery = (index == 0)
                                            ? collection.OrderByDescending(func)
                                            : orderedQuery.ThenByDescending(func);
                            break;
                        #endregion

                        #region Default
                        default:
                            throw new Exception("Se debe elegir un operador válido");
                        #endregion
                    }
                }

                return orderedQuery.AsQueryable();

            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        

    }

}
