using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.Extensions.Options;
using PodcastAnnouncerator.Accessors.Contracts;
using PodcastAnnouncerator.Shared.Models;

namespace PodcastAnnouncerator.Accessors.Implementations;

public class FeedAccessor : IFeedAccessor
{
    private readonly PodcastConfiguration _podcastConfiguration;

    public FeedAccessor(IOptions<PodcastConfiguration> podcastConfiguration)
    {
        _podcastConfiguration = podcastConfiguration.Value;
    }

    public List<SyndicationItem> List()
    {
        try
        {
            var reader = XmlReader.Create(_podcastConfiguration.Url);
            var feed = SyndicationFeed.Load(reader);

            return feed.Items.ToList();
        }
        catch (Exception)
        {
            return new List<SyndicationItem>();
        }
    }

    public SyndicationItem GetLatest()
    {
        var latest = List();
        return latest.FirstOrDefault();
    }
}