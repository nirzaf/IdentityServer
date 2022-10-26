// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using AutoMapper;
using Duende.IdentityServer.EntityFramework.Storage.Entities;

namespace Duende.IdentityServer.EntityFramework.Storage.Mappers;

/// <summary>
/// Defines entity/model mapping for persisted grants.
/// </summary>
/// <seealso cref="AutoMapper.Profile" />
public class PersistedGrantMapperProfile:Profile
{
    /// <summary>
    /// <see cref="PersistedGrantMapperProfile">
    /// </see>
    /// </summary>
    public PersistedGrantMapperProfile()
    {
        CreateMap<PersistedGrant, IdentityServer.Storage.Models.PersistedGrant>(MemberList.Destination)
            .ReverseMap();
    }
}