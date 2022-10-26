// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System;
using Duende.IdentityServer.Infrastructure;
using Duende.IdentityServer.Models.Messages;
using FluentAssertions;
using Xunit;

namespace IdentityServer.UnitTests.Infrastructure;

public class ObjectSerializerTests
{
    public ObjectSerializerTests()
    {
    }

    [Fact]
    public void Can_be_deserialize_message()
    {
        Action a = () => ObjectSerializer.FromString<Message<ErrorMessage>>("{\"created\":0, \"data\": {\"error\": \"error\"}}");
        a.Should().NotThrow();
    }
}