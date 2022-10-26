// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Duende.IdentityServer.Storage.Models;

namespace Duende.IdentityServer.Storage.Stores;

/// <summary>
/// Interface to model storage of identity providers.
/// </summary>
public interface IIdentityProviderStore
{
    /// <summary>
    /// Gets all identity providers name.
    /// </summary>
    Task<IEnumerable<IdentityProviderName>> GetAllSchemeNamesAsync();
        
    /// <summary>
    /// Gets the identity provider by scheme name.
    /// </summary>
    /// <param name="scheme"></param>
    /// <returns></returns>
    Task<IdentityProvider> GetBySchemeAsync(string scheme);
}