﻿// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace IdentityServer.UnitTests.Extensions;

public class HttpRequestExtensionsTests
{
    [Fact]
    public void GetCorsOrigin_valid_cors_request_should_return_cors_origin()
    {
        var ctx = new DefaultHttpContext();
        ctx.Request.Scheme = "http";
        ctx.Request.Host = new HostString("foo");
        ctx.Request.Headers.Add("Origin", "http://bar");

        ctx.Request.GetCorsOrigin().Should().Be("http://bar");
    }

    [Fact]
    public void GetCorsOrigin_origin_from_same_host_should_not_return_cors_origin()
    {
        var ctx = new DefaultHttpContext();
        ctx.Request.Scheme = "http";
        ctx.Request.Host = new HostString("foo");
        ctx.Request.Headers.Add("Origin", "http://foo");

        ctx.Request.GetCorsOrigin().Should().BeNull();
    }

    [Fact]
    public void GetCorsOrigin_no_origin_should_not_return_cors_origin()
    {
        var ctx = new DefaultHttpContext();
        ctx.Request.Scheme = "http";
        ctx.Request.Host = new HostString("foo");

        ctx.Request.GetCorsOrigin().Should().BeNull();
    }
}