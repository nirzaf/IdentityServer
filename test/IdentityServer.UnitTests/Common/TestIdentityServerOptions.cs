// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.Configuration.DependencyInjection.Options;

namespace IdentityServer.UnitTests.Common;

internal class TestIdentityServerOptions
{
    public static IdentityServerOptions Create()
    {
        var options = new IdentityServerOptions
        {
            IssuerUri = "https://idsvr.com"
        };

        return options;
    }
}