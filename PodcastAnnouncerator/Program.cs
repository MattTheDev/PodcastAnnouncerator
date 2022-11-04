using PodcastAnnouncerator;
using PodcastAnnouncerator.Accessors.Contracts;
using PodcastAnnouncerator.Accessors.Implementations;
using PodcastAnnouncerator.Shared.Models;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddOptions<PodcastConfiguration>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(nameof(PodcastConfiguration)).Bind(settings);
            });

        services.AddOptions<TwitterConfiguration>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(nameof(TwitterConfiguration)).Bind(settings);
            });

        services.AddScoped<IFeedAccessor, FeedAccessor>();
        services.AddScoped<IMessagingAccessor, MessagingAccessor>();
        services.AddScoped<ITwitterAccessor, TwitterAccessor>();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
