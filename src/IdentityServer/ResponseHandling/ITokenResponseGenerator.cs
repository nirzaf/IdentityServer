// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.ResponseHandling.Models;
using Duende.IdentityServer.Validation;
using Duende.IdentityServer.Validation.Models;

namespace Duende.IdentityServer.ResponseHandling;

/// <summary>
/// Interface the token response generator
/// </summary>
public interface ITokenResponseGenerator
{
    /// <summary>
    /// Processes the response.
    /// </summary>
    /// <param name="validationResult">The validation result.</param>
    /// <returns></returns>
    Task<TokenResponse> ProcessAsync(TokenRequestValidationResult validationResult);
}