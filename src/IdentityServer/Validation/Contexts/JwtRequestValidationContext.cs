// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.Storage.Models;

namespace Duende.IdentityServer.Validation.Contexts;

/// <summary>
/// Context for validating a JWT sent as a request parameter.
/// </summary>
public class JwtRequestValidationContext
{
    /// <summary>
    /// The Client for which the validation is being performed.
    /// </summary>
    public Client Client { get; set; }

    /// <summary>
    /// The JWT request object string.
    /// </summary>
    public string JwtTokenString { get; set; }

    /// <summary>
    /// Specifies whether the JWT typ and content-type for JWT secured authorization requests is checked according to IETF spec.
    /// This might break older OIDC conformant request objects.
    /// </summary>
    public bool? StrictJarValidation { get; set; }

    /// <summary>
    /// Indicates if the JTI claim is expected in the result.
    /// </summary>
    public bool IncludeJti { get; internal set; }
}