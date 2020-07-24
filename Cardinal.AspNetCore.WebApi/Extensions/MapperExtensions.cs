using AutoMapper;
using Cardinal.AspNetCore;
using Cardinal.AspNetCore.WebApi.DTOs;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="IMapper"/>.
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="mapper"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDTO Map<TEntity, TDTO>(this IMapper mapper, TEntity source) where TEntity : Entity where TDTO : BaseDTO
        {
            try
            {
                var dto = mapper.Map<TDTO>(source);
                return dto;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="mapper"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TEntity Map<TEntity, TDTO>(this IMapper mapper, TDTO source) where TEntity : Entity where TDTO : BaseDTO
        {
            try
            {
                var dto = mapper.Map<TEntity>(source);
                return dto;
            }
            catch
            {
                return null;
            }
        }
    }
}
