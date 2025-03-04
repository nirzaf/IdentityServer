﻿// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Duende.IdentityServer.Models.Contexts;
using Duende.IdentityServer.Services;

namespace IdentityServer.UnitTests.Common;

public class MockProfileService : IProfileService
{
    public ICollection<Claim> ProfileClaims { get; set; } = new HashSet<Claim>();
    public bool IsActive { get; set; } = true;

    public bool GetProfileWasCalled => ProfileContext != null;
    public ProfileDataRequestContext ProfileContext { get; set; }

    public bool IsActiveWasCalled => ActiveContext != null;
    public IsActiveContext ActiveContext { get; set; }

    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        ProfileContext = context;
        context.IssuedClaims = ProfileClaims.ToList();
        return Task.CompletedTask;
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        ActiveContext = context;
        context.IsActive = IsActive;
        return Task.CompletedTask;
    }
}