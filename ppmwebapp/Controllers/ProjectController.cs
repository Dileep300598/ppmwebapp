using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ppmwebapp.model;
using ppmwebapp.repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ppmwebapp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
       
        

        // GET: ProjectController
        public ActionResult Index()
        {
            var projectList = projectRepository.GetAll();
            return View(projectList);
        }

        // GET: ProjectController/Details/5
        public ActionResult Details(int id)
        {
            var pro = projectRepository.Get(id);
            return View(pro);
        }

        // GET: ProjectController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project pro)
        {
            try
            {
               Project project = projectRepository.Create(pro);
               return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            var pro1 = projectRepository.Get(id);
            return View(pro1);
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project)
        {
            try
            {
                projectRepository.Edit(project);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Delete/5
        public ActionResult Delete(int id)
        {
            var pro = projectRepository.Delete(id);

            return View(pro);
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Project pro)
        {
            try
            {
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
