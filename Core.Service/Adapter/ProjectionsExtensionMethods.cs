using AutoMapper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Adapter
{
    public static class ProjectionsExtensionMethods
    {
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <typeparam name="TProjection">对象类型</typeparam>
        /// <returns></returns>
        public static TProjection ProjectedAs<TProjection,TKey>(this BaseEntity<TKey> item)
            where TProjection : class, new()
        {
            return Mapper.Map<TProjection>(item);
        }

        /// <summary>
        /// 对象集合转换
        /// </summary>
        /// <typeparam name="TProjection">对象类型</typeparam>
        /// <returns></returns>
        public static List<TProjection> ProjectedAsCollection<TProjection, TKey>(this IEnumerable<BaseEntity<TKey>> items)
            where TProjection : class, new()
        {
            return Mapper.Map<List<TProjection>>(items);
        }

        public static TProjection ProjectedAs<TProjection>(this object item)
          where TProjection : class, new()
        {
            return Mapper.Map<TProjection>(item);
        }
    }
}
