using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace Ragged.Hubs
{
	public class EditHub : Hub
	{
		public async Task SendMessage(string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", message);
		}
	}
}
