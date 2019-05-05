using ca.HenrySoftware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Ragged.Pages
{
	[BindProperties]
	public class IndexModel : PageModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Group { get; set; }
		public string Topic { get; set; }
		public string Status { get; set; }
		private Gename _gename = new Gename();
		public void OnGet()
		{
			Name = _gename.Name();
		}
		public void OnPost()
		{
		}
	}
}
