﻿// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using Duende.IdentityServer.Storage.Models;
using Duende.IdentityServer.Stores.InMemory;
using FluentAssertions;
using Xunit;

namespace IdentityServer.UnitTests.Stores;

public class InMemoryClientStoreTests
{
    [Fact]
    public void InMemoryClient_should_throw_if_contain_duplicate_client_ids()
    {
        List<Client> clients = new List<Client>
        {
            new Client { ClientId = "1"},
            new Client { ClientId = "1"},
            new Client { ClientId = "3"}
        };

        Action act = () => new InMemoryClientStore(clients);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void InMemoryClient_should_not_throw_if_does_not_contain_duplicate_client_ids()
    {
        List<Client> clients = new List<Client>
        {
            new Client { ClientId = "1"},
            new Client { ClientId = "2"},
            new Client { ClientId = "3"}
        };

        new InMemoryClientStore(clients);
    }
}