// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.Models.Messages;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.ResponseHandling.Models;
using Duende.IdentityServer.Validation.Models;

namespace IdentityServer.UnitTests.Endpoints.Authorize;

internal class StubAuthorizeInteractionResponseGenerator : IAuthorizeInteractionResponseGenerator
{
    internal InteractionResponse Response { get; set; } = new InteractionResponse();

    public Task<InteractionResponse> ProcessInteractionAsync(ValidatedAuthorizeRequest request, ConsentResponse consent = null)
    {
        return Task.FromResult(Response);
    }
}