using System.ServiceModel.Syndication;

namespace PodcastAnnouncerator.Accessors.Contracts;

public interface IFeedAccessor
{
    List<SyndicationItem> List();
    SyndicationItem GetLatest();
}