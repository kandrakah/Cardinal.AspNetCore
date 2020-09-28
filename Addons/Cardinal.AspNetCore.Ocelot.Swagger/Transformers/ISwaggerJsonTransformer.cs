﻿using System.Collections.Generic;

namespace Cardinal.AspNetCore.Ocelot
{
    /// <summary>
    /// Interface which describe class for transformation downstream service swagger json into upstream format.
    /// </summary>
    public interface ISwaggerJsonTransformer
    {
        /// <summary>
        /// Transforms downstream swagger json into upstream format.
        /// </summary>
        /// <param name="swaggerJson">The swagger json.</param>
        /// <param name="reRoutes">The re routes.</param>
        /// <returns>
        /// Transformed swagger json.
        /// </returns>
        string Transform(string swaggerJson, IEnumerable<ReRouteOptions> reRoutes);
    }
}
