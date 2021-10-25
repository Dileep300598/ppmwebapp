using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ppmwebapp.Models;
using ppmwebapp.repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ppmwebapp.Controllers
{
    public class RoleController : Controller
    {
        
        private readonly IRoleRepository roleRepository;

       
        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        // GET: RoleController
        public ActionResult Index()
        {
            var roleList = roleRepository.GetAll();
            return View(roleList);
        }

        // GET: RoleController/Details/5
        public ActionResult Details(int id)
        {
            var role = roleRepository.Get(id);

            return View(role);
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role role)
        {
            try
            {
                roleRepository.Create(role);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController/Edit/5
        public ActionResult Edit(int id)
        {
            var role1 = roleRepository.Get(id);
            return View(role1);
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role)
        {
            try
            {
                roleRepository.Edit(role);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController/Delete/5
        public ActionResult Delete(int id)
        {
            var role1 = roleRepository.Delete(id);
            return View(role1);

        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Role role)
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
