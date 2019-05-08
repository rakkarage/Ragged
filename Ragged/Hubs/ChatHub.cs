using ca.HenrySoftware;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Ragged.Hubs
{
	public static class UserHandler
	{
		public static Dictionary<string, string> ConnectedIds = new Dictionary<string, string>();
	}
	public class ChatHub : Hub
	{
		private Gename _gename = new Gename();
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
			var name = _gename.Name();
			UserHandler.ConnectedIds.Add(Context.ConnectionId, name);
			await Clients.Caller.SendAsync("SetName", name);
			await Clients.All.SendAsync("ReceiveMessage", "System", $"Welcome {name}!");
			await Clients.All.SendAsync("ConnectMessage", UserHandler.ConnectedIds);
			await base.OnConnectedAsync();
		}
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			var name = UserHandler.ConnectedIds[Context.ConnectionId];
			UserHandler.ConnectedIds.Remove(Context.ConnectionId);
			await Clients.All.SendAsync("ReceiveMessage", "System", $"Goodbye {name}!");
			await Clients.All.SendAsync("DisconnectMessage", UserHandler.ConnectedIds);
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
