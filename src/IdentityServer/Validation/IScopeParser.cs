// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;
using Duende.IdentityServer.Validation.Models;

namespace Duende.IdentityServer.Validation;

/// <summary>
/// Allows parsing raw scopes values into structured scope values.
/// </summary>
public interface IScopeParser
{
    // todo: test return no error, and no parsed scopes. how do callers behave?
    /// <summary>
    /// Parses the requested scopes.
    /// </summary>
    ParsedScopesResult ParseScopeValues(IEnumerable<string> scopeValues);
}