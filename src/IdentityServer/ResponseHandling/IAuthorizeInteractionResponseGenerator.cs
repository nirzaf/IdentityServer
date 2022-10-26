// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Models.Messages;
using Duende.IdentityServer.ResponseHandling.Models;
using Duende.IdentityServer.Validation;
using Duende.IdentityServer.Validation.Models;

namespace Duende.IdentityServer.ResponseHandling;

/// <summary>
/// Interface for determining if user must login or consent when making requests to the authorization endpoint.
/// </summary>
public interface IAuthorizeInteractionResponseGenerator
{
    /// <summary>
    /// Processes the interaction logic.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="consent">The consent.</param>
    /// <returns></returns>
    Task<InteractionResponse> ProcessInteractionAsync(ValidatedAuthorizeRequest request, ConsentResponse consent = null);
}