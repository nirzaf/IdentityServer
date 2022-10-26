// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;
using AutoMapper;
using Duende.IdentityServer.EntityFramework.Storage.Entities;

namespace Duende.IdentityServer.EntityFramework.Storage.Mappers;

/// <summary>
/// Defines entity/model mapping for scopes.
/// </summary>
/// <seealso cref="AutoMapper.Profile" />
public class ScopeMapperProfile : Profile
{
    /// <summary>
    /// <see cref="ScopeMapperProfile"/>
    /// </summary>
    public ScopeMapperProfile()
    {
        CreateMap<ApiScopeProperty, KeyValuePair<string, string>>()
            .ReverseMap();

        CreateMap<ApiScopeClaim, string>()
            .ConstructUsing(x => x.Type)
            .ReverseMap()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));

        CreateMap<ApiScope, IdentityServer.Storage.Models.ApiScope>(MemberList.Destination)
            .ConstructUsing(src => new IdentityServer.Storage.Models.ApiScope())
            .ForMember(x => x.Properties, opts => opts.MapFrom(x => x.Properties))
            .ForMember(x => x.UserClaims, opts => opts.MapFrom(x => x.UserClaims))
            .ReverseMap();
    }
}