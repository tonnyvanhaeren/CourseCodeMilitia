using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using web.Demo.Filters;
using web.Mappings;
using web.Models;

namespace web.Controllers
{

    [Route("groups")]
    public class GroupsController : Controller
    {
        private static List<GroupViewModel> groups = new List<GroupViewModel>{
            new GroupViewModel { Id= 1, Name= "sample group" }
        };

        public IGroupsService _groupsService { get; }

        public GroupsController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(CancellationToken ct)
        {
            // throw new ArgumentException("Arg exception");
            var result = await _groupsService.GetAllAsync(ct);
            return View(result.ToViewModel());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> DetailsAsync(long id, CancellationToken ct)
        {
            var group = await _groupsService.GetByIdAsync(id, ct);

            if(group == null)
            {
                return NotFound();
            }

            return View(group.ToViewModel());
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(GroupViewModel model, CancellationToken ct)
        {
            await _groupsService.AddAsync(model.ToServiceModel(), ct);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(long id, GroupViewModel model, CancellationToken ct)
        {
            var group = await _groupsService.UpdateAsync(model.ToServiceModel(), ct);

            if (group == null)
            {
                return NotFound();
            }

            return RedirectToAction("index");
        }
    }
}
