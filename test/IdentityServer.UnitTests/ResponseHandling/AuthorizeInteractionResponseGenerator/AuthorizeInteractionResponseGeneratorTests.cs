// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Duende.IdentityServer;
using Duende.IdentityServer.Configuration.DependencyInjection.Options;
using Duende.IdentityServer.Storage.Models;
using Duende.IdentityServer.Validation.Models;
using FluentAssertions;
using IdentityServer.UnitTests.Common;
using Xunit;
using static IdentityModel.OidcConstants;

namespace IdentityServer.UnitTests.ResponseHandling.AuthorizeInteractionResponseGenerator;

public class AuthorizeInteractionResponseGeneratorTests
{
    private IdentityServerOptions _options = new IdentityServerOptions();
    private Duende.IdentityServer.ResponseHandling.Default.AuthorizeInteractionResponseGenerator _subject;
    private MockConsentService _mockConsentService = new MockConsentService();
    private StubClock _clock = new StubClock();

    public AuthorizeInteractionResponseGeneratorTests()
    {
        _subject = new Duende.IdentityServer.ResponseHandling.Default.AuthorizeInteractionResponseGenerator(
            _options,
            _clock,
            TestLogger.Create<Duende.IdentityServer.ResponseHandling.Default.AuthorizeInteractionResponseGenerator>(),
            _mockConsentService,
            new MockProfileService());
    }


    [Fact]
    public async Task Authenticated_User_with_restricted_current_Idp_with_prompt_none_must_error()
    {
        var request = new ValidatedAuthorizeRequest
        {
            ClientId = "foo",
            Subject = new IdentityServerUser("123")
            {
                IdentityProvider = IdentityServerConstants.LocalIdentityProvider
            }.CreatePrincipal(),
            Client = new Client
            {
                EnableLocalLogin = false,
                IdentityProviderRestrictions = new List<string>
                {
                    "some_idp"
                }
            },
            PromptModes = new[] { PromptModes.None },
        };

        var result = await _subject.ProcessInteractionAsync(request);

        result.IsError.Should().BeTrue();
        result.IsLogin.Should().BeFalse();
    }

    [Fact]
    public async Task Authenticated_User_with_maxage_with_prompt_none_must_error()
    {
        _clock.UtcNowFunc = () => new DateTime(2020, 02, 03, 9, 0, 0);

        var request = new ValidatedAuthorizeRequest
        {
            ClientId = "foo",
            Subject = new IdentityServerUser("123")
            {
                AuthenticationTime = new DateTime(2020, 02, 01, 9, 0, 0),
                IdentityProvider = IdentityServerConstants.LocalIdentityProvider
            }.CreatePrincipal(),
            Client = new Client
            {
                EnableLocalLogin = true,
            },
            PromptModes = new[] { PromptModes.None },
            MaxAge = 3600
        };

        var result = await _subject.ProcessInteractionAsync(request);

        result.IsError.Should().BeTrue();
        result.IsLogin.Should().BeFalse();
    }

    [Fact]
    public async Task Authenticated_User_with_different_requested_Idp_with_prompt_none_must_error()
    {
        var request = new ValidatedAuthorizeRequest
        {
            ClientId = "foo",
            Client = new Client(),
            AuthenticationContextReferenceClasses = new List<string>{
                "idp:some_idp"
            },
            Subject = new IdentityServerUser("123")
            {
                IdentityProvider = IdentityServerConstants.LocalIdentityProvider
            }.CreatePrincipal(),
            PromptModes = new[] { PromptModes.None }
        };

        var result = await _subject.ProcessInteractionAsync(request);

        result.IsError.Should().BeTrue();
        result.IsLogin.Should().BeFalse();
    }

    [Fact]
    public async Task Authenticated_User_beyond_client_user_sso_lifetime_with_prompt_none_should_error()
    {
        var request = new ValidatedAuthorizeRequest
        {
            ClientId = "foo",
            Client = new Client()
            {
                UserSsoLifetime = 3600 // 1h
            },
            Subject = new IdentityServerUser("123")
            {
                IdentityProvider = "local",
                AuthenticationTime = _clock.UtcNow.UtcDateTime.Subtract(TimeSpan.FromSeconds(3700))
            }.CreatePrincipal(),
            PromptModes = new[] { PromptModes.None }
        };

        var result = await _subject.ProcessInteractionAsync(request);

        result.IsError.Should().BeTrue();
        result.IsLogin.Should().BeFalse();
    }

    [Fact]
    public async Task locally_authenticated_user_but_client_does_not_allow_local_with_prompt_none_should_error()
    {
        var request = new ValidatedAuthorizeRequest
        {
            ClientId = "foo",
            Client = new Client()
            {
                EnableLocalLogin = false
            },
            Subject = new IdentityServerUser("123")
            {
                IdentityProvider = IdentityServerConstants.LocalIdentityProvider
            }.CreatePrincipal(),
            PromptModes = new[] { PromptModes.None }
        };

        var result = await _subject.ProcessInteractionAsync(request);

        result.IsError.Should().BeTrue();
        result.IsLogin.Should().BeFalse();
    }
}