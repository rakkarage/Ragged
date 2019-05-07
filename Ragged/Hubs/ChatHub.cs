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
			await Clients.Caller.SendAsync("ReceiveMessage", "Ragged", "Welcome");
			await Clients.All.SendAsync("ConnectMessage", Context.ConnectionId, UserHandler.ConnectedIds);
			await base.OnConnectedAsync();
		}
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			UserHandler.ConnectedIds.Remove(Context.ConnectionId);
			await Clients.All.SendAsync("DisconnectMessage", Context.ConnectionId, UserHandler.ConnectedIds);
			await base.OnDisconnectedAsync(exception);
		}
		// public async Task AddToGroup(string groupName)
		// {
		// 	await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
		// 	await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
		// }
		// public async Task RemoveFromGroup(string groupName)
		// {
		// 	await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
		// 	await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
		// }
	}
}
