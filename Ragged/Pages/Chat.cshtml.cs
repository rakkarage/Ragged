using ca.HenrySoftware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Ragged.Pages
{
	public class ChatModel : PageModel
	{
		private Gename _gename = new Gename();
		[ViewData]
		public string Name => _gename.Name();
	}
}