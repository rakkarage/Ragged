using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace Ragged.Hubs
{
	public class EditHub : Hub
	{
		public async Task EditMessage1(string message)
		{
			await Clients.All.SendAsync("EditedMessage1", message);
		}
		public async Task EditMessage2(string message)
		{
			await Clients.All.SendAsync("EditedMessage2", message);
		}
		public async Task EditMessage3(string message)
		{
			await Clients.All.SendAsync("EditedMessage3", message);
		}
	}
}
