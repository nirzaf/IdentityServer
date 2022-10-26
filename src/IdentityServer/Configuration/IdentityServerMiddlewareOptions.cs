// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System;
using Microsoft.AspNetCore.Builder;

namespace Duende.IdentityServer.Configuration;

/// <summary>
/// Options for the IdentityServer middleware
/// </summary>
public class IdentityServerMiddlewareOptions
{
    /// <summary>
    /// Callback to wire up an authentication middleware
    /// </summary>
    public Action<IApplicationBuilder> AuthenticationMiddleware { get; set; } = (app) => app.UseAuthentication();
}