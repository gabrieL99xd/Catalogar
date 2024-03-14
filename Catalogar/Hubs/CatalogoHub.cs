using Microsoft.AspNetCore.SignalR;

namespace Catalogar.Hubs
{
    public class CatalogoHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
