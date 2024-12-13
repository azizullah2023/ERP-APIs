using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Inspire.Erp.Web.Common
{
    public class NotificationHub : Hub, INotificationHub
    {
        private readonly IHubContext<NotificationHub> _hub;
        public NotificationHub(IHubContext<NotificationHub> hub)
        {
            _hub = hub;
        }
        public async Task BroadcastToUser(string data, string userId) => await _hub.Clients.All.SendAsync("ReceiveMessage", userId, data);
        //    public async Task AddToGroup(string groupName)
        //=> await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        //    public async Task RemoveFromGroup(string groupName)
        //        => await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        //    public async Task BroadcastToGroup(string groupName)
        //=> await Clients.Group(groupName).SendAsync("broadcasttogroup", $"{Context.ConnectionId} has joined the group {groupName}.");

        // public async Task BroadcastChartData(List<ChartModel> data, string connectionId)
        //=> await Clients.Client(connectionId).SendAsync("broadcastchartdata", data);
        public string GetConnectionId() => Context.ConnectionId;

        public string GetUserId() => Context.User.Identity.Name;

        public async Task SendMessage(string user, string message)
        {
            await _hub.Clients.Client(user).SendAsync("ReceiveMessage", user, message);
        }

        public async Task Send(string user, ApiMethod client)
        {
            await _hub.Clients.Client(user).SendAsync("ReceiveClient", user, client);
        }
    }

    public interface INotificationHub
    {
        public Task BroadcastToUser(string data, string userId);
        public Task SendMessage(string user, string message);
        public Task Send(string user, ApiMethod client);
    }
}
