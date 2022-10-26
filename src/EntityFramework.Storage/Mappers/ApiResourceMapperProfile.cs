// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;
using AutoMapper;
using Duende.IdentityServer.EntityFramework.Storage.Entities;
using Secret = Duende.IdentityServer.Storage.Models.Secret;

namespace Duende.IdentityServer.EntityFramework.Storage.Mappers;

/// <summary>
/// Defines entity/model mapping for API resources.
/// </summary>
/// <seealso cref="AutoMapper.Profile" />
public class ApiResourceMapperProfile : Profile
{
    /// <summary>
    /// <see cref="ApiResourceMapperProfile"/>
    /// </summary>
    public ApiResourceMapperProfile()
    {
        CreateMap<ApiResourceProperty, KeyValuePair<string, string>>()
            .ReverseMap();

        CreateMap<ApiResource, IdentityServer.Storage.Models.ApiResource>(MemberList.Destination)
            .ConstructUsing(src => new IdentityServer.Storage.Models.ApiResource())
            .ForMember(x => x.ApiSecrets, opts => opts.MapFrom(x => x.Secrets))
            .ForMember(x=>x.AllowedAccessTokenSigningAlgorithms, opts => opts.ConvertUsing(AllowedSigningAlgorithmsConverter.Converter, x=>x.AllowedAccessTokenSigningAlgorithms))
            .ReverseMap()
            .ForMember(x => x.AllowedAccessTokenSigningAlgorithms, opts => opts.ConvertUsing(AllowedSigningAlgorithmsConverter.Converter, x => x.AllowedAccessTokenSigningAlgorithms));

        CreateMap<ApiResourceClaim, string>()
            .ConstructUsing(x => x.Type)
            .ReverseMap()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));

        CreateMap<ApiResourceSecret, Secret>(MemberList.Destination)
            .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null))
            .ReverseMap();

        CreateMap<ApiResourceScope, string>()
            .ConstructUsing(x => x.Scope)
            .ReverseMap()
            .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));
    }
}