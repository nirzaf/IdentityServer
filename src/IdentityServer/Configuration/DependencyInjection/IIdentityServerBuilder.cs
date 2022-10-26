// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.Extensions.DependencyInjection;

namespace Duende.IdentityServer.Configuration.DependencyInjection;

/// <summary>
/// IdentityServer builder Interface
/// </summary>
public interface IIdentityServerBuilder
{
    /// <summary>
    /// Gets the services.
    /// </summary>
    /// <value>
    /// The services.
    /// </value>
    IServiceCollection Services { get; }
}