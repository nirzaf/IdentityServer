// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System;
using System.Threading.Tasks;
using Duende.IdentityServer;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Storage.Stores;
using Duende.IdentityServer.Stores.InMemory;
using Duende.IdentityServer.Validation;
using Duende.IdentityServer.Validation.Default;
using FluentAssertions;
using IdentityServer.UnitTests.Common;
using IdentityServer.UnitTests.Validation.Setup;
using Microsoft.Extensions.Logging;
using Xunit;

namespace IdentityServer.UnitTests.Validation.Secrets;

public class MutualTlsSecretValidation
{
    private const string Category = "Secrets - MutualTls Secret Validation";
       
    private IClientStore _clients = new InMemoryClientStore(ClientValidationTestClients.Get());

    ///////////////////
    // thumbprints
    ///////////////////

    [Fact]
    [Trait("Category", Category)]
    public async Task Thumbprint_invalid_secret_type_should_not_match()
    {
        ISecretValidator validator = new X509ThumbprintSecretValidator(new Logger<X509ThumbprintSecretValidator>(new LoggerFactory()));

        var clientId = "mtls_client_invalid";
        var client = await _clients.FindEnabledClientByIdAsync(clientId);

        var secret = new ParsedSecret
        {
            Id = clientId,
            Credential = "secret",
            Type = IdentityServerConstants.ParsedSecretTypes.SharedSecret
        };

        var result = await validator.ValidateAsync(client.ClientSecrets, secret);

        result.Success.Should().BeFalse();
    }

    [Fact]
    [Trait("Category", Category)]
    public async Task Thumbprint_missing_cert_should_throw()
    {
        ISecretValidator validator = new X509ThumbprintSecretValidator(new Logger<X509ThumbprintSecretValidator>(new LoggerFactory()));

        var clientId = "mtls_client_invalid";
        var client = await _clients.FindEnabledClientByIdAsync(clientId);

        var secret = new ParsedSecret
        {
            Id = clientId,
            Credential = "secret",
            Type = IdentityServerConstants.ParsedSecretTypes.X509Certificate
        };

        Func<Task> act = async () => await validator.ValidateAsync(client.ClientSecrets, secret);
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    [Trait("Category", Category)]
    public async Task Thumbprint_invalid_secret_should_not_match()
    {
        ISecretValidator validator = new X509ThumbprintSecretValidator(new Logger<X509ThumbprintSecretValidator>(new LoggerFactory()));

        var clientId = "mtls_client_invalid";
        var client = await _clients.FindEnabledClientByIdAsync(clientId);

        var secret = new ParsedSecret
        {
            Id = clientId,
            Credential = TestCert.Load(),
            Type = IdentityServerConstants.ParsedSecretTypes.X509Certificate
        };

        var result = await validator.ValidateAsync(client.ClientSecrets, secret);

        result.Success.Should().BeFalse();
    }

    [Fact]
    [Trait("Category", Category)]
    public async Task Thumbprint_valid_secret_should_match()
    {
        ISecretValidator validator = new X509ThumbprintSecretValidator(new Logger<X509ThumbprintSecretValidator>(new LoggerFactory()));

        var clientId = "mtls_client_valid";
        var client = await _clients.FindEnabledClientByIdAsync(clientId);

        var secret = new ParsedSecret
        {
            Id = clientId,
            Credential = TestCert.Load(),
            Type = IdentityServerConstants.ParsedSecretTypes.X509Certificate
        };

        var result = await validator.ValidateAsync(client.ClientSecrets, secret);

        result.Success.Should().BeTrue();
    }

    ///////////////////
    // names
    ///////////////////

    [Fact]
    [Trait("Category", Category)]
    public async Task Name_invalid_secret_type_should_not_match()
    {
        ISecretValidator validator = new X509NameSecretValidator(new Logger<X509NameSecretValidator>(new LoggerFactory()));

        var clientId = "mtls_client_invalid";
        var client = await _clients.FindEnabledClientByIdAsync(clientId);

        var secret = new ParsedSecret
        {
            Id = clientId,
            Credential = "secret",
            Type = IdentityServerConstants.ParsedSecretTypes.SharedSecret
        };

        var result = await validator.ValidateAsync(client.ClientSecrets, secret);

        result.Success.Should().BeFalse();
    }

    [Fact]
    [Trait("Category", Category)]
    public async Task Name_missing_cert_should_throw()
    {
        ISecretValidator validator = new X509NameSecretValidator(new Logger<X509NameSecretValidator>(new LoggerFactory()));

        var clientId = "mtls_client_invalid";
        var client = await _clients.FindEnabledClientByIdAsync(clientId);

        var secret = new ParsedSecret
        {
            Id = clientId,
            Credential = "secret",
            Type = IdentityServerConstants.ParsedSecretTypes.X509Certificate
        };

        Func<Task> act = async () => await validator.ValidateAsync(client.ClientSecrets, secret);
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    [Trait("Category", Category)]
    public async Task Name_invalid_secret_should_not_match()
    {
        ISecretValidator validator = new X509NameSecretValidator(new Logger<X509NameSecretValidator>(new LoggerFactory()));

        var clientId = "mtls_client_invalid";
        var client = await _clients.FindEnabledClientByIdAsync(clientId);

        var secret = new ParsedSecret
        {
            Id = clientId,
            Credential = TestCert.Load(),
            Type = IdentityServerConstants.ParsedSecretTypes.X509Certificate
        };

        var result = await validator.ValidateAsync(client.ClientSecrets, secret);

        result.Success.Should().BeFalse();
    }

    [Fact]
    [Trait("Category", Category)]
    public async Task Name_valid_secret_should_match()
    {
        ISecretValidator validator = new X509NameSecretValidator(new Logger<X509NameSecretValidator>(new LoggerFactory()));

        var clientId = "mtls_client_valid";
        var client = await _clients.FindEnabledClientByIdAsync(clientId);

        var secret = new ParsedSecret
        {
            Id = clientId,
            Credential = TestCert.Load(),
            Type = IdentityServerConstants.ParsedSecretTypes.X509Certificate
        };

        var result = await validator.ValidateAsync(client.ClientSecrets, secret);

        result.Success.Should().BeTrue();
    }
}