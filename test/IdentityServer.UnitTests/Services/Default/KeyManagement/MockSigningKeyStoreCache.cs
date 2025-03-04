using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer.Services.Default.KeyManagement;

namespace IdentityServer.UnitTests.Services.Default.KeyManagement;

class MockSigningKeyStoreCache : ISigningKeyStoreCache
{
    public List<KeyContainer> Cache { get; set; } = new List<KeyContainer>();

    public bool GetKeysAsyncWasCalled { get; set; }
    public bool StoreKeysAsyncWasCalled { get; set; }
    public TimeSpan StoreKeysAsyncDuration { get; set; }

    public Task<IEnumerable<KeyContainer>> GetKeysAsync()
    {
        GetKeysAsyncWasCalled = true;
        return Task.FromResult(Cache.AsEnumerable());
    }

    public Task StoreKeysAsync(IEnumerable<KeyContainer> keys, TimeSpan duration)
    {
        StoreKeysAsyncWasCalled = true;
        StoreKeysAsyncDuration = duration;

        Cache = keys.ToList();
        return Task.CompletedTask;
    }
}