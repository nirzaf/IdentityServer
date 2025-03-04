// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;
using Duende.IdentityServer.Configuration.DependencyInjection;
using Duende.IdentityServer.Configuration.DependencyInjection.BuilderExtensions;
using Microsoft.Extensions.DependencyInjection;

namespace Duende.IdentityServer.Test;

/// <summary>
/// Extension methods for the IdentityServer builder
/// </summary>
public static class IdentityServerBuilderExtensions
{
    /// <summary>
    /// Adds test users.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="users">The users.</param>
    /// <returns></returns>
    public static IIdentityServerBuilder AddTestUsers(this IIdentityServerBuilder builder, List<TestUser> users)
    {
        builder.Services.AddSingleton(new TestUserStore(users));
        builder.AddProfileService<TestUserProfileService>();
        builder.AddResourceOwnerValidator<TestUserResourceOwnerPasswordValidator>();
            
        builder.AddBackchannelAuthenticationUserValidator<TestBackchannelLoginUserValidator>();

        return builder;
    }
}