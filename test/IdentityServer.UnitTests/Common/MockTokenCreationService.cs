// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Storage.Models;

namespace IdentityServer.UnitTests.Common;

class MockTokenCreationService : ITokenCreationService
{
    public string TokenResult { get; set; }
    public Token Token { get; set; }

    public Task<string> CreateTokenAsync(Token token)
    {
        Token = token;
        return Task.FromResult(TokenResult);
    }
}