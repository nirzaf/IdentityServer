﻿// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Threading.Tasks;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Storage.Models;
using Duende.IdentityServer.Validation.Models;

namespace Duende.IdentityServer.Validation;

/// <summary>
/// Validator for an Enumerable List of Secrets
/// </summary>
public interface ISecretsListValidator
{
    /// <summary>
    /// Validates a list of secrets
    /// </summary>
    /// <param name="secrets">The stored secrets.</param>
    /// <param name="parsedSecret">The received secret.</param>
    /// <returns>A validation result</returns>
    Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret);
}