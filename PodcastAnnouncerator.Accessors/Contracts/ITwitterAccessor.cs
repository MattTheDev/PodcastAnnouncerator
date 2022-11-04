namespace PodcastAnnouncerator.Accessors.Contracts;

public interface ITwitterAccessor
{
    Task SendTweetAsync(string message, string url);
}