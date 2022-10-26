// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Threading.Tasks;
using Duende.IdentityServer.Models.Messages;
using Duende.IdentityServer.Services.Default;
using Duende.IdentityServer.Stores.Default;
using FluentAssertions;
using IdentityServer.UnitTests.Common;
using Xunit;

namespace IdentityServer.UnitTests.Stores.Default;

public class DistributedCacheAuthorizationParametersMessageStoreTests
{
    MockDistributedCache _mockCache = new MockDistributedCache();
    DistributedCacheAuthorizationParametersMessageStore _subject;
    public DistributedCacheAuthorizationParametersMessageStoreTests()
    {
        _subject = new DistributedCacheAuthorizationParametersMessageStore(_mockCache, new DefaultHandleGenerationService());
    }

    [Fact]
    public async Task DeleteAsync_should_remove_item()
    {
        _mockCache.Items.Count.Should().Be(0);

        var msg = new Message<IDictionary<string, string[]>>(new Dictionary<string, string[]>());
        var id = await _subject.WriteAsync(msg);

        _mockCache.Items.Count.Should().Be(1);

        await _subject.DeleteAsync(id);

        _mockCache.Items.Count.Should().Be(0);
    }
}