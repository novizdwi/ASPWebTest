using Microsoft.AspNetCore.Mvc;
using ASPWebTest.Services;
using System.Threading.Tasks;
using ASPWebTest.Models;

namespace ASPWebTest.ViewComponents
{
	public class NavigationMenuViewComponent : ViewComponent
	{
		private MenuService menuService;

		public NavigationMenuViewComponent(MenuService menuService)
		{
            this.menuService = menuService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var items = await menuService.GetMenu();
			return View(items);
		}
	}
}