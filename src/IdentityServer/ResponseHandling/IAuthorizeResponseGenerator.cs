// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.ResponseHandling.Models;
using Duende.IdentityServer.Validation;
using Duende.IdentityServer.Validation.Models;

namespace Duende.IdentityServer.ResponseHandling;

/// <summary>
/// Interface for the authorize response generator
/// </summary>
public interface IAuthorizeResponseGenerator
{
    /// <summary>
    /// Creates the response
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    Task<AuthorizeResponse> CreateResponseAsync(ValidatedAuthorizeRequest request);
}