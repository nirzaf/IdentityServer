// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.ResponseHandling.Models;
using Duende.IdentityServer.Validation;
using Duende.IdentityServer.Validation.Models;

namespace Duende.IdentityServer.ResponseHandling;

/// <summary>
/// Interface the backchannel authentication response generator
/// </summary>
public interface IBackchannelAuthenticationResponseGenerator
{
    /// <summary>
    /// Processes the response.
    /// </summary>
    /// <param name="validationResult">The validation result.</param>
    /// <returns></returns>
    Task<BackchannelAuthenticationResponse> ProcessAsync(BackchannelAuthenticationRequestValidationResult validationResult);
}