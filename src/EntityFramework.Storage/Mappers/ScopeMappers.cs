﻿// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using AutoMapper;
using Duende.IdentityServer.Storage.Models;

namespace Duende.IdentityServer.EntityFramework.Storage.Mappers;

/// <summary>
/// Extension methods to map to/from entity/model for scopes.
/// </summary>
public static class ScopeMappers
{
    static ScopeMappers()
    {
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ScopeMapperProfile>())
            .CreateMapper();
    }

    internal static IMapper Mapper { get; }

    /// <summary>
    /// Maps an entity to a model.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    public static ApiScope ToModel(this Entities.ApiScope entity)
    {
        return entity == null ? null : Mapper.Map<ApiScope>(entity);
    }

    /// <summary>
    /// Maps a model to an entity.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    public static Entities.ApiScope ToEntity(this ApiScope model)
    {
        return model == null ? null : Mapper.Map<Entities.ApiScope>(model);
    }
}