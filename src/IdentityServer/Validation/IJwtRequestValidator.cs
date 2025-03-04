// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.Validation.Contexts;
using Duende.IdentityServer.Validation.Models;

namespace Duende.IdentityServer.Validation;

/// <summary>
/// Interface for request object validator
/// </summary>
public interface IJwtRequestValidator
{
    /// <summary>
    /// Validates a JWT request object
    /// </summary>
    Task<JwtRequestValidationResult> ValidateAsync(JwtRequestValidationContext context);
}