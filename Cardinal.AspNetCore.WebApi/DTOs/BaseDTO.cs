using System;
using System.Xml.Serialization;

namespace Cardinal.AspNetCore.WebApi.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
    }
}
