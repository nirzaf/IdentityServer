﻿// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Events.Infrastructure;

namespace Duende.IdentityServer.Services;

/// <summary>
/// Models persistence of events
/// </summary>
public interface IEventSink
{
    /// <summary>
    /// Raises the specified event.
    /// </summary>
    /// <param name="evt">The event.</param>
    Task PersistAsync(Event evt);
}