﻿// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


namespace Duende.IdentityServer.Validation.Models;

/// <summary>
/// Validation result for end session requests
/// </summary>
/// <seealso cref="ValidationResult" />
public class EndSessionValidationResult : ValidationResult
{
    /// <summary>
    /// Gets or sets the validated request.
    /// </summary>
    /// <value>
    /// The validated request.
    /// </value>
    public ValidatedEndSessionRequest ValidatedRequest { get; set; }
}