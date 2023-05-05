using CommunityToolkit.Mvvm.Messaging;
using Trackerino.App.Services.Interfaces;

namespace Trackerino.App.Services;

public class MessengerService : IMessengerService
{
    public IMessenger Messenger { get; }

    public MessengerService(IMessenger messenger)
    {
        Messenger = messenger;
    }

    public void Send<TMessage>(TMessage message)
        where TMessage : class
    {
        Messenger.Send(message);
    }
}