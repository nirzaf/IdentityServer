// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.EntityFramework.Storage.Mappers;
using Duende.IdentityServer.Storage.Models;
using Xunit;

namespace EntityFramework.Storage.UnitTests.Mappers;

public class IdentityResourcesMappersTests
{
    [Fact]
    public void IdentityResourceAutomapperConfigurationIsValid()
    {
        IdentityResourceMappers.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public void CanMapIdentityResources()
    {
        var model = new IdentityResource();
        var mappedEntity = model.ToEntity();
        var mappedModel = mappedEntity.ToModel();

        Assert.NotNull(mappedModel);
        Assert.NotNull(mappedEntity);
    }
}