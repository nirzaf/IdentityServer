// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Duende.IdentityServer.EntityFramework.Storage.Entities;
using Secret = Duende.IdentityServer.Storage.Models.Secret;

namespace Duende.IdentityServer.EntityFramework.Storage.Mappers;

/// <summary>
/// Defines entity/model mapping for clients.
/// </summary>
/// <seealso cref="AutoMapper.Profile" />
public class ClientMapperProfile : Profile
{
    /// <summary>
    /// <see>
    ///     <cref>{ClientMapperProfile}</cref>
    /// </see>
    /// </summary>
    public ClientMapperProfile()
    {
        CreateMap<ClientProperty, KeyValuePair<string, string>>()
            .ReverseMap();

        CreateMap<Client, IdentityServer.Storage.Models.Client>()
            .ForMember(dest => dest.ProtocolType, opt => opt.Condition(srs => srs != null))
            .ForMember(x => x.AllowedIdentityTokenSigningAlgorithms, opts => opts.ConvertUsing(AllowedSigningAlgorithmsConverter.Converter, x => x.AllowedIdentityTokenSigningAlgorithms))
            .ReverseMap()
            .ForMember(x => x.AllowedIdentityTokenSigningAlgorithms, opts => opts.ConvertUsing(AllowedSigningAlgorithmsConverter.Converter, x => x.AllowedIdentityTokenSigningAlgorithms));

        CreateMap<ClientCorsOrigin, string>()
            .ConstructUsing(src => src.Origin)
            .ReverseMap()
            .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src));

        CreateMap<ClientIdPRestriction, string>()
            .ConstructUsing(src => src.Provider)
            .ReverseMap()
            .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src));

        CreateMap<ClientClaim, IdentityServer.Storage.Models.ClientClaim>(MemberList.None)
            .ConstructUsing(src => new IdentityServer.Storage.Models.ClientClaim(src.Type, src.Value, ClaimValueTypes.String))
            .ReverseMap();

        CreateMap<ClientScope, string>()
            .ConstructUsing(src => src.Scope)
            .ReverseMap()
            .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));

        CreateMap<ClientPostLogoutRedirectUri, string>()
            .ConstructUsing(src => src.PostLogoutRedirectUri)
            .ReverseMap()
            .ForMember(dest => dest.PostLogoutRedirectUri, opt => opt.MapFrom(src => src));

        CreateMap<ClientRedirectUri, string>()
            .ConstructUsing(src => src.RedirectUri)
            .ReverseMap()
            .ForMember(dest => dest.RedirectUri, opt => opt.MapFrom(src => src));

        CreateMap<ClientGrantType, string>()
            .ConstructUsing(src => src.GrantType)
            .ReverseMap()
            .ForMember(dest => dest.GrantType, opt => opt.MapFrom(src => src));

        CreateMap<ClientSecret, Secret>(MemberList.Destination)
            .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null))
            .ReverseMap();
    }
}