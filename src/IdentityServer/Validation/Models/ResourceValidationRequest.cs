// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;
using Duende.IdentityServer.Storage.Models;

namespace Duende.IdentityServer.Validation.Models;

/// <summary>
/// Models the request to validate scopes and resource indicators for a client.
/// </summary>
public class ResourceValidationRequest
{
    /// <summary>
    /// The client.
    /// </summary>
    public Client Client { get; set; }

    /// <summary>
    /// The requested scope values.
    /// </summary>
    public IEnumerable<string> Scopes { get; set; }

    /// <summary>
    /// The requested resource indicators.
    /// </summary>
    public IEnumerable<string> ResourceIndicators { get; set; }

    /// <summary>
    /// Flag that indicates that validation should allow requested scopes to match non-isolated resources.
    /// If set to false, then only the scopes that match the exact resource indicators requested will be allowed.
    /// </summary>
    public bool IncludeNonIsolatedApiResources { get; set; }
}