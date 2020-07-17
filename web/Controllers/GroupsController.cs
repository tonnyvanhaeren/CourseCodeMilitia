using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using web.Demo;
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
        public SomeRootConfiguration _config { get; }

        private readonly DemoSecretsConfiguration _secrets;

        public GroupsController(DemoSecretsConfiguration secrets,  SomeRootConfiguration config, IGroupsService groupsService)
        {
            _groupsService = groupsService;
            _config = config;
            _secrets = secrets;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_groupsService.GetAll().ToViewModel());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Details(long id)
        {
            var group = _groupsService.GetById(id);

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
        public IActionResult Create(GroupViewModel model)
        {
            _groupsService.Add(model.ToServiceModel());
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, GroupViewModel model)
        {
            var group = _groupsService.Update(model.ToServiceModel());

            if (group == null)
            {
                return NotFound();
            }

            return RedirectToAction("index");
        }
    }
}
