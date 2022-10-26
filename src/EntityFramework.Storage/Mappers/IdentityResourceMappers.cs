// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using AutoMapper;
using Duende.IdentityServer.Storage.Models;

namespace Duende.IdentityServer.EntityFramework.Storage.Mappers;

/// <summary>
/// Extension methods to map to/from entity/model for identity resources.
/// </summary>
public static class IdentityResourceMappers
{
    static IdentityResourceMappers()
    {
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<IdentityResourceMapperProfile>())
            .CreateMapper();
    }

    internal static IMapper Mapper { get; }

    /// <summary>
    /// Maps an entity to a model.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    public static IdentityResource ToModel(this Entities.IdentityResource entity)
    {
        return entity == null ? null : Mapper.Map<IdentityResource>(entity);
    }

    /// <summary>
    /// Maps a model to an entity.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    public static Entities.IdentityResource ToEntity(this IdentityResource model)
    {
        return model == null ? null : Mapper.Map<Entities.IdentityResource>(model);
    }
}