// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System;
using Microsoft.AspNetCore.Authentication;

namespace IdentityServer.UnitTests.Common;

class MockSystemClock : ISystemClock
{
    public DateTimeOffset Now { get; set; }

    public DateTimeOffset UtcNow
    {
        get
        {
            return Now;
        }
    }
}