using Microsoft.Extensions.Options;
using PodcastAnnouncerator.Accessors.Contracts;
using PodcastAnnouncerator.Shared.Models;
using Tweetinvi;

namespace PodcastAnnouncerator.Accessors.Implementations;

public class TwitterAccessor : ITwitterAccessor
{
    private readonly TwitterConfiguration _twitterConfiguration;

    public TwitterAccessor(IOptions<TwitterConfiguration> twitterConfiguration)
    {
        _twitterConfiguration = twitterConfiguration.Value;
    }

    public async Task SendTweetAsync(string message, string url)
    {
        var tweet = $"{message} - {url}";

        var userClient = new TwitterClient(_twitterConfiguration.ConsumerKey,
            _twitterConfiguration.ConsumerSecret,
            _twitterConfiguration.AccessToken,
            _twitterConfiguration.AccessTokenSecret);

        await userClient.Tweets.PublishTweetAsync(tweet);
    }
}