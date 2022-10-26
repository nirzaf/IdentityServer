// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.Services.Default;
using Duende.IdentityServer.Storage.Models;
using Duende.IdentityServer.Storage.Stores;
using Duende.IdentityServer.Storage.Stores.Serialization;
using Duende.IdentityServer.Stores.Default;
using Duende.IdentityServer.Stores.InMemory;

namespace IdentityServer.UnitTests.Common;

public class TestUserConsentStore : IUserConsentStore
{
    private DefaultUserConsentStore _userConsentStore;
    private InMemoryPersistedGrantStore _grantStore = new InMemoryPersistedGrantStore();

    public TestUserConsentStore()
    {
        _userConsentStore = new DefaultUserConsentStore(
            _grantStore,
            new PersistentGrantSerializer(),
            new DefaultHandleGenerationService(),
            TestLogger.Create<DefaultUserConsentStore>());
    }

    public Task StoreUserConsentAsync(Consent consent)
    {
        return _userConsentStore.StoreUserConsentAsync(consent);
    }

    public Task<Consent> GetUserConsentAsync(string subjectId, string clientId)
    {
        return _userConsentStore.GetUserConsentAsync(subjectId, clientId);
    }

    public Task RemoveUserConsentAsync(string subjectId, string clientId)
    {
        return _userConsentStore.RemoveUserConsentAsync(subjectId, clientId);
    }
}