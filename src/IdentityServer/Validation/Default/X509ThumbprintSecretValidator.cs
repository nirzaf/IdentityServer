// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Logging;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Storage.Models;
using Duende.IdentityServer.Validation.Models;
using Microsoft.Extensions.Logging;

namespace Duende.IdentityServer.Validation.Default;

/// <summary>
/// Validator for an X.509 certificate based client secret using the thumbprint
/// </summary>
public class X509ThumbprintSecretValidator : ISecretValidator
{
    private readonly ILogger<X509ThumbprintSecretValidator> _logger;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="logger"></param>
    public X509ThumbprintSecretValidator(ILogger<X509ThumbprintSecretValidator> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret)
    {
        var fail = Task.FromResult(new SecretValidationResult { Success = false });

        if (parsedSecret.Type != IdentityServerConstants.ParsedSecretTypes.X509Certificate)
        {
            _logger.LogDebug("X509 thumbprint secret validator cannot process {type}", parsedSecret.Type ?? "null");
            return fail;
        }

        if (parsedSecret.Credential is not X509Certificate2 cert)
        {
            throw new InvalidOperationException("Credential is not a x509 certificate.");
        }

        var thumbprint = cert.Thumbprint;
        if (thumbprint == null)
        {
            _logger.LogWarning("No thumbprint found in X509 certificate");
            return fail;
        }

        var thumbprintSecrets = secrets.Where(s => s.Type == IdentityServerConstants.SecretTypes.X509CertificateThumbprint);
        var enumerable = thumbprintSecrets.ToList();
        if (!enumerable.Any())
        {
            _logger.LogDebug("No thumbprint secrets configured for client.");
            return fail;
        }

        if (enumerable.Any(thumbprintSecret => thumbprint.Equals(thumbprintSecret.Value, StringComparison.OrdinalIgnoreCase)))
        {
            var result = new SecretValidationResult
            {
                Success = true,
                Confirmation = cert.CreateThumbprintCnf()
            };

            return Task.FromResult(result);
        }

        _logger.LogDebug("No matching x509 thumbprint secret found.");
        return fail;
    }
}