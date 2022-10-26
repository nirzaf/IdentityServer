// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;
using AutoMapper;
using Duende.IdentityServer.EntityFramework.Storage.Entities;

namespace Duende.IdentityServer.EntityFramework.Storage.Mappers;

/// <summary>
/// Defines entity/model mapping for identity resources.
/// </summary>
/// <seealso cref="AutoMapper.Profile" />
public class IdentityResourceMapperProfile : Profile
{
    /// <summary>
    /// <see cref="IdentityResourceMapperProfile"/>
    /// </summary>
    public IdentityResourceMapperProfile()
    {
        CreateMap<IdentityResourceProperty, KeyValuePair<string, string>>()
            .ReverseMap();

        CreateMap<IdentityResource, IdentityServer.Storage.Models.IdentityResource>(MemberList.Destination)
            .ConstructUsing(src => new IdentityServer.Storage.Models.IdentityResource())
            .ReverseMap();

        CreateMap<IdentityResourceClaim, string>()
            .ConstructUsing(x => x.Type)
            .ReverseMap()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));
    }
}