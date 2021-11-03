
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using ppmwebapp.model;
using ppmwebapp.repo.Concrete;
using ppmwebapp.repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ppmwebapp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration Configuration;

        public EmployeeController(IEmployeeRepository employeeRepo, IConfiguration configuration)
        {
            _employeeRepository = employeeRepo;
            Configuration = configuration;

        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            var employeeList = _employeeRepository.GetAll();
            return View(employeeList);
        }

        // GET: EmployeeController/Details/5
        // [Route("Employee/Details/{Id}")]
        public ActionResult Details(int id)
        {
            var emp = _employeeRepository.Get(id);
            return View(emp);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            IEnumerable<Role> roles;
            RoleRepository roleRepository = new RoleRepository(Configuration);
            roles = roleRepository.GetAll();
            ViewData["roles"] = new SelectList(roles, "Id", "RoleName");
            return View("Create");
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee emp)
        {
            try
            { 
                _employeeRepository.Create(emp);
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            IEnumerable<Role> roles;
            RoleRepository roleRepository = new RoleRepository(Configuration);
            roles = roleRepository.GetAll();
            ViewData["roles"] = new SelectList(roles, "Id", "RoleName");
            var emp = _employeeRepository.Get(id);

            return View(emp);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            try
            {
                 _employeeRepository.Edit(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var empl = _employeeRepository.Get(id);
            return View(empl);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Employee emp)
        {
            int id = emp.Id;
            try
            {
                _employeeRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}