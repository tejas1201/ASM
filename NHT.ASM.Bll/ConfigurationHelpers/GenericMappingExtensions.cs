using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace NHT.ASM.Bll.ConfigurationHelpers
{
    /// <summary>
    /// Generic mapping extension
    /// </summary>
    public static class GenericMappingExtensions
    {
        private static readonly IMapper Mapper = GetMapper();

        private static IMapper GetMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfileConfiguration());
            });

            var mapper = mappingConfig.CreateMapper();

            return mapper;
        }

        #region public methods
        /// <summary>
        /// Maps a model to another (mapping must be defined in <see cref="AutoMapperProfileConfiguration"/>)
        /// </summary>
        /// <typeparam name="T">Target model</typeparam>
        /// <param name="item">The input model</param>
        public static T MapTo<T>(this object item)
        {
            var model = Mapper.Map<T>(item);

            return model;
        }

        /// <summary>
        /// Maps a queryable of one model to a list of another (mapping must be defined in <see cref="AutoMapperProfileConfiguration"/>)
        /// </summary>
        /// <typeparam name="T">Target model</typeparam>
        /// <param name="items">The input model</param>
        public static IEnumerable<T> MapIQueryableToListOf<T>(this IQueryable<object> items)
        {
            var list = new List<T>();
            foreach (object item in items)
            {
                var model = Mapper.Map<T>(item);
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// Maps a list of one model to a list of another (mapping must be defined in <see cref="AutoMapperProfileConfiguration"/>)
        /// </summary>
        /// <typeparam name="T">Target model</typeparam>
        /// <param name="items">The input model</param>
        public static IEnumerable<T> MapListToListOf<T>(this IEnumerable<object> items)
        {
            return items.Select(Mapper.Map<T>).ToList();
        }

        #endregion
    }
}