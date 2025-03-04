// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.EntityFramework.Storage.Mappers;
using Duende.IdentityServer.Storage.Models;
using FluentAssertions;
using Xunit;

namespace EntityFramework.Storage.UnitTests.Mappers;

public class PersistedGrantMappersTests
{
    [Fact]
    public void PersistedGrantAutomapperConfigurationIsValid()
    {
        PersistedGrantMappers.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public void CanMap()
    {
        var model = new PersistedGrant()
        {
            ConsumedTime = new System.DateTime(2020, 02, 03, 4, 5, 6)
        };
            
        var mappedEntity = model.ToEntity();
        mappedEntity.ConsumedTime.Value.Should().Be(new System.DateTime(2020, 02, 03, 4, 5, 6));
            
        var mappedModel = mappedEntity.ToModel();
        mappedModel.ConsumedTime.Value.Should().Be(new System.DateTime(2020, 02, 03, 4, 5, 6));

        Assert.NotNull(mappedModel);
        Assert.NotNull(mappedEntity);
    }
}