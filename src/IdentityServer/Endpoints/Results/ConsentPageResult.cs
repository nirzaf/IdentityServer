// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Configuration.DependencyInjection.Options;
using Duende.IdentityServer.Hosting;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Http;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models.Messages;
using Microsoft.Extensions.DependencyInjection;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation.Models;

namespace Duende.IdentityServer.Endpoints.Results;

/// <summary>
/// Result for consent page
/// </summary>
/// <seealso cref="IEndpointResult" />
public class ConsentPageResult : IEndpointResult
{
    private readonly ValidatedAuthorizeRequest _request;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsentPageResult"/> class.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <exception cref="System.ArgumentNullException">request</exception>
    public ConsentPageResult(ValidatedAuthorizeRequest request)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));
    }

    internal ConsentPageResult(
        ValidatedAuthorizeRequest request,
        IdentityServerOptions options,
        IServerUrls urls,
        IAuthorizationParametersMessageStore authorizationParametersMessageStore = null) 
        : this(request)
    {
        _options = options;
        _urls = urls;
        _authorizationParametersMessageStore = authorizationParametersMessageStore;
    }

    private IdentityServerOptions _options;
    private IServerUrls _urls;
    private IAuthorizationParametersMessageStore _authorizationParametersMessageStore;

    private void Init(HttpContext context)
    {
        _options = _options ?? context.RequestServices.GetRequiredService<IdentityServerOptions>();
        _urls = _urls ?? context.RequestServices.GetRequiredService<IServerUrls>();
        _authorizationParametersMessageStore = _authorizationParametersMessageStore ?? context.RequestServices.GetService<IAuthorizationParametersMessageStore>();
    }

    /// <summary>
    /// Executes the result.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns></returns>
    public async Task ExecuteAsync(HttpContext context)
    {
        Init(context);

        var returnUrl = _urls.BasePath.EnsureTrailingSlash() + Constants.ProtocolRoutePaths.AuthorizeCallback;
        if (_authorizationParametersMessageStore != null)
        {
            var msg = new Message<IDictionary<string, string[]>>(_request.ToOptimizedFullDictionary());
            var id = await _authorizationParametersMessageStore.WriteAsync(msg);
            returnUrl = returnUrl.AddQueryString(Constants.AuthorizationParamsStore.MessageStoreIdParameterName, id);
        }
        else
        {
            returnUrl = returnUrl.AddQueryString(_request.ToOptimizedQueryString());
        }

        var consentUrl = _options.UserInteraction.ConsentUrl;
        if (!consentUrl.IsLocalUrl())
        {
            // this converts the relative redirect path to an absolute one if we're 
            // redirecting to a different server
            returnUrl = _urls.Origin + returnUrl;
        }

        var url = consentUrl.AddQueryString(_options.UserInteraction.ConsentReturnUrlParameter, returnUrl);
        context.Response.Redirect(_urls.GetAbsoluteUrl(url));
    }
}