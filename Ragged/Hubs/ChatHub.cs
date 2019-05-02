using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Ragged.Hubs
{
	public static class UserHandler
	{
		public static HashSet<string> ConnectedIds = new HashSet<string>();
	}
	public class ChatHub : Hub
	{
		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}
		public async Task EditTopic(string message)
		{
			await Clients.All.SendAsync("EditedTopic", message);
		}
		public override async Task OnConnectedAsync()
		{
			UserHandler.ConnectedIds.Add(Context.ConnectionId);
			await Clients.All.SendAsync("ReceiveMessage", Context.ConnectionId, "connected");
			await base.OnConnectedAsync();
		}
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			UserHandler.ConnectedIds.Remove(Context.ConnectionId);
			await Clients.All.SendAsync("ReceiveMessage", Context.ConnectionId, "disconnected");
			await base.OnDisconnectedAsync(exception);
		}
	}
}
