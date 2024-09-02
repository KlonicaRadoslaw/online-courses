using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Interfaces;

namespace OnlineCourses.Controllers
{
    public class ContentController : Controller
    {
        public readonly IContentRepository _contentRepository;
        public ContentController(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public async Task<IActionResult> Index(int categoryItemId)
        {
            var content = await _contentRepository.GetContentByCategoryItemId(categoryItemId);

            return View(content);
        }
    }
}
