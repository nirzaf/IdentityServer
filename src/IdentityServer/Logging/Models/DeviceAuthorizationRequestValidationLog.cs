// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Validation.Models;
using IdentityModel;

namespace Duende.IdentityServer.Logging.Models;

internal class DeviceAuthorizationRequestValidationLog
{
    public string ClientId { get; set; }
    public string ClientName { get; set; }
    public string Scopes { get; set; }

    public Dictionary<string, string> Raw { get; set; }

    private static readonly string[] SensitiveValuesFilter = {
        OidcConstants.TokenRequest.ClientSecret,
        OidcConstants.TokenRequest.ClientAssertion
    };

    public DeviceAuthorizationRequestValidationLog(ValidatedDeviceAuthorizationRequest request)
    {
        Raw = request.Raw.ToScrubbedDictionary(SensitiveValuesFilter);

        if (request.Client != null)
        {
            ClientId = request.Client.ClientId;
            ClientName = request.Client.ClientName;
        }

        if (request.RequestedScopes != null)
        {
            Scopes = request.RequestedScopes.ToSpaceSeparatedString();
        }
    }

    public override string ToString()
    {
        return LogSerializer.Serialize(this);
    }
}