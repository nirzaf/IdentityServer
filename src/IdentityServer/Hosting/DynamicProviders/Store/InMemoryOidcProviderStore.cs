// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer.Storage.Models;
using Duende.IdentityServer.Storage.Stores;

namespace Duende.IdentityServer.Hosting.DynamicProviders.Store;

class InMemoryOidcProviderStore : IIdentityProviderStore
{
    private readonly IEnumerable<OidcProvider> _providers;

    public InMemoryOidcProviderStore(IEnumerable<OidcProvider> providers)
    {
        _providers = providers;
    }

    public Task<IEnumerable<IdentityProviderName>> GetAllSchemeNamesAsync()
    {
        using var activity = Tracing.StoreActivitySource.StartActivity("InMemoryOidcProviderStore.GetAllSchemeNames");
        
        var items = _providers.Select(x => new IdentityProviderName { 
            Enabled = x.Enabled,
            DisplayName = x.DisplayName,
            Scheme = x.Scheme
        });
        
        return Task.FromResult(items);
    }

    public Task<IdentityProvider> GetBySchemeAsync(string scheme)
    {
        using var activity = Tracing.StoreActivitySource.StartActivity("InMemoryOidcProviderStore.GetByScheme");
        
        var item = _providers.FirstOrDefault(x => x.Scheme == scheme);
        return Task.FromResult<IdentityProvider>(item);
    }
}