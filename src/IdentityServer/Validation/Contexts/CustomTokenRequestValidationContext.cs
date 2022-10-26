// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.Validation.Models;

namespace Duende.IdentityServer.Validation.Contexts;

/// <summary>
/// Context class for custom token request validation
/// </summary>
public class CustomTokenRequestValidationContext
{
    /// <summary>
    /// Gets or sets the result.
    /// </summary>
    /// <value>
    /// The result.
    /// </value>
    public TokenRequestValidationResult Result { get; set; }
}