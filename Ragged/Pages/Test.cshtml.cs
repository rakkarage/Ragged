using ca.HenrySoftware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Ragged.Pages
{
	[BindProperties(SupportsGet = true)]
	public class TestModel : PageModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public void OnGet()
		{
			ViewData["confirmation"] = $"Welcome {Email}";
		}
		public void OnPost()
		{
			ViewData["confirmation"] = $"{Name}, information will be sent to {Email}";
		}
	}
}
