// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.Linq;
using Duende.IdentityServer.EntityFramework.Services;
using Duende.IdentityServer.EntityFramework.Storage.DbContexts;
using Duende.IdentityServer.EntityFramework.Storage.Interfaces;
using Duende.IdentityServer.EntityFramework.Storage.Mappers;
using Duende.IdentityServer.EntityFramework.Storage.Options;
using Duende.IdentityServer.Storage.Models;
using Duende.IdentityServer.Storage.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EntityFramework.Tests.Services;

public class CorsPolicyServiceTests : IntegrationTest<CorsPolicyServiceTests, ConfigurationDbContext, ConfigurationStoreOptions>
{
    public CorsPolicyServiceTests(DatabaseProviderFixture<ConfigurationDbContext> fixture) : base(fixture)
    {
        foreach (var options in TestDatabaseProviders.SelectMany(x => x.Select(y => (DbContextOptions<ConfigurationDbContext>) y)).ToList())
        {
            using (var context = new ConfigurationDbContext(options))
                context.Database.EnsureCreated();
        }
    }

    [Theory, MemberData(nameof(TestDatabaseProviders))]
    public void IsOriginAllowedAsync_WhenOriginIsAllowed_ExpectTrue(DbContextOptions<ConfigurationDbContext> options)
    {
        const string testCorsOrigin = "https://identityserver.io/";

        using (var context = new ConfigurationDbContext(options))
        {
            context.Clients.Add(new Client
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientName = Guid.NewGuid().ToString(),
                AllowedCorsOrigins = new List<string> { "https://www.identityserver.com" }
            }.ToEntity());
            context.Clients.Add(new Client
            {
                ClientId = "2",
                ClientName = "2",
                AllowedCorsOrigins = new List<string> { "https://www.identityserver.com", testCorsOrigin }
            }.ToEntity());
            context.SaveChanges();
        }

        bool result;
        using (var context = new ConfigurationDbContext(options))
        {
            var svcs = new ServiceCollection();
            svcs.AddSingleton<IConfigurationDbContext>(context);
            var provider = svcs.BuildServiceProvider();

            var service = new CorsPolicyService(provider, FakeLogger<CorsPolicyService>.Create(), new NoneCancellationTokenProvider());
            result = service.IsOriginAllowedAsync(testCorsOrigin).Result;
        }

        Assert.True(result);
    }

    [Theory, MemberData(nameof(TestDatabaseProviders))]
    public void IsOriginAllowedAsync_WhenOriginIsNotAllowed_ExpectFalse(DbContextOptions<ConfigurationDbContext> options)
    {
        using (var context = new ConfigurationDbContext(options))
        {
            context.Clients.Add(new Client
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientName = Guid.NewGuid().ToString(),
                AllowedCorsOrigins = new List<string> { "https://www.identityserver.com" }
            }.ToEntity());
            context.SaveChanges();
        }

        bool result;
        using (var context = new ConfigurationDbContext(options))
        {
            var svcs = new ServiceCollection();
            svcs.AddSingleton<IConfigurationDbContext>(context);
            var provider = svcs.BuildServiceProvider();

            var service = new CorsPolicyService(provider, FakeLogger<CorsPolicyService>.Create(), new NoneCancellationTokenProvider());
            result = service.IsOriginAllowedAsync("InvalidOrigin").Result;
        }

        Assert.False(result);
    }
}