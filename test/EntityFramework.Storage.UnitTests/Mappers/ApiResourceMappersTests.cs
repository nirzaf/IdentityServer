// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Linq;
using Duende.IdentityServer.EntityFramework.Storage.Entities;
using Duende.IdentityServer.EntityFramework.Storage.Mappers;
using FluentAssertions;
using Xunit;
using ApiResource = Duende.IdentityServer.Storage.Models.ApiResource;
using Secret = Duende.IdentityServer.Storage.Models.Secret;

namespace EntityFramework.Storage.UnitTests.Mappers;

public class ApiResourceMappersTests
{
    [Fact]
    public void AutomapperConfigurationIsValid()
    {
        ApiResourceMappers.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public void Can_Map()
    {
        var model = new ApiResource();
        var mappedEntity = model.ToEntity();
        var mappedModel = mappedEntity.ToModel();

        Assert.NotNull(mappedModel);
        Assert.NotNull(mappedEntity);
    }

    [Fact]
    public void Properties_Map()
    {
        var model = new ApiResource()
        {
            Description = "description",
            DisplayName = "displayname",
            Name = "foo",
            Scopes = { "foo1", "foo2" },
            Enabled = false
        };


        var mappedEntity = model.ToEntity();

        mappedEntity.Scopes.Count.Should().Be(2);
        var foo1 = mappedEntity.Scopes.FirstOrDefault(x => x.Scope == "foo1");
        foo1.Should().NotBeNull();
        var foo2 = mappedEntity.Scopes.FirstOrDefault(x => x.Scope == "foo2");
        foo2.Should().NotBeNull();
            

        var mappedModel = mappedEntity.ToModel();
            
        mappedModel.Description.Should().Be("description");
        mappedModel.DisplayName.Should().Be("displayname");
        mappedModel.Enabled.Should().BeFalse();
        mappedModel.Name.Should().Be("foo");
    }

    [Fact]
    public void missing_values_should_use_defaults()
    {
        var entity = new Duende.IdentityServer.EntityFramework.Storage.Entities.ApiResource
        {
            Secrets = new System.Collections.Generic.List<ApiResourceSecret>
            {
                new ApiResourceSecret
                {
                }
            }
        };

        var def = new ApiResource
        {
            ApiSecrets = { new Secret("foo") }
        };

        var model = entity.ToModel();
        model.ApiSecrets.First().Type.Should().Be(def.ApiSecrets.First().Type);
    }
}