using Newtonsoft.Json;
using PodcastAnnouncerator.Accessors.Contracts;
using PodcastAnnouncerator.Accessors.Models;

namespace PodcastAnnouncerator.Accessors.Implementations;

public class MessagingAccessor : IMessagingAccessor
{
    public string GetRandomMessage()
    {
        var random = new Random();
        var messages = JsonConvert.DeserializeObject<List<Message>>(File.ReadAllText("Messages.json"));

        return messages[random.Next(0, messages.Count)].Text;
    }
}