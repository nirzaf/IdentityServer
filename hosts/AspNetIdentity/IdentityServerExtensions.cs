// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Configuration.DependencyInjection;
using Duende.IdentityServer.Configuration.DependencyInjection.BuilderExtensions;
using IdentityServerHost.Models;

namespace IdentityServerHost;

internal static class IdentityServerExtensions
{
    internal static WebApplicationBuilder ConfigureIdentityServer(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityServer()
            .AddInMemoryIdentityResources(Resources.IdentityResources)
            .AddInMemoryApiResources(Resources.ApiResources)
            .AddInMemoryApiScopes(Resources.ApiScopes)
            .AddInMemoryClients(Clients.Get())
            .AddAspNetIdentity<ApplicationUser>();

        return builder;
    }
}