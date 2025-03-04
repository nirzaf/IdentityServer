﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AspNetIdentityDb;

class Program
{
    public static void Main(string[] args)
    {
        var host = BuildWebHost(args);
        SeedData.EnsureSeedData(host.Services);
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
}