using PodcastAnnouncerator.Accessors.Contracts;

namespace PodcastAnnouncerator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IFeedAccessor _feedAccessor;
        private readonly IMessagingAccessor _messagingAccessor;
        private readonly ITwitterAccessor _twitterAccessor;

        public Worker(ILogger<Worker> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;

            var scope = serviceScopeFactory.CreateScope();
            _feedAccessor = scope.ServiceProvider.GetRequiredService<IFeedAccessor>();
            _messagingAccessor = scope.ServiceProvider.GetRequiredService<IMessagingAccessor>();
            _twitterAccessor = scope.ServiceProvider.GetRequiredService<ITwitterAccessor>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var randomMessage = _messagingAccessor.GetRandomMessage();
                // TODO - Store "latest" so that we don't announce it again.
                var latest = _feedAccessor.GetLatest();
                await _twitterAccessor.SendTweetAsync(randomMessage, latest.Links.FirstOrDefault().Uri.ToString());

                // TODO - I just put a random long interval here. Gotta change that too.
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}