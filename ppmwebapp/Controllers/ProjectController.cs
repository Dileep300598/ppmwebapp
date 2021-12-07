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
using System.Dynamic;

namespace ppmwebapp.Controllers
{

    public class ProjectController : Controller
    {
        private readonly IProjectRepository projectRepository;
        private readonly IConfiguration Configuration;
        public ProjectController(IProjectRepository projectRepository, IConfiguration configuration)
        {
            this.projectRepository = projectRepository;
            Configuration = configuration;

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
            EmployeeToProjectRepository employeeToProjectRepository = new EmployeeToProjectRepository(Configuration);
            IList<EmployeeToProject> Employee = (IList<EmployeeToProject>)employeeToProjectRepository.GetAll();
            IList<EmployeeToProject> Emp = new List<EmployeeToProject>();
            if (Employee.Count > 0)
            {
                foreach (var emp in Employee)
                {
                    if (emp.ProjectId == id)
                    {
                        Emp.Add(emp);
                    }
                }
            }
            return View("List", Emp);
        }


        // GET: ProjectController/Create
        public ActionResult Create()
        {
            IEnumerable<Employee> employees;
            EmployeeRepository employeeRepository = new EmployeeRepository(Configuration);
            employees = employeeRepository.GetAll();
            ViewData["employees"] = new SelectList(employees, "FirstName", "FirstName");
            return View("Create");

        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project pro)
        {
            try
            {
                projectRepository.Create(pro);
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
            IEnumerable<Employee> employees;
            EmployeeRepository employeeRepository = new EmployeeRepository(Configuration);
            employees = employeeRepository.GetAll();
            ViewData["employees"] = new MultiSelectList(employees, "Id", "FirstName");
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
            bool flag = true;
            Project p = projectRepository.Get(id);
            var projects = projectRepository.GetAll();
            EmployeeToProjectRepository employeeToProjectRepository = new EmployeeToProjectRepository(Configuration);
            IList<EmployeeToProject> Employee = (IList<EmployeeToProject>)employeeToProjectRepository.GetAll();
            
            if (Employee.Count > 0)
            {
                foreach (var emp in Employee)
                {
                    if (emp.ProjectId == id)
                    {
                        ViewBag.Message = "This project is mapped to employees please unmap employees to delete";
                        flag = false;
                        break;
                    }

                }
                if (!flag)
                {
                    return View("Index", projects);
                }
                else
                {
                    return View(p);
                }

            }

            else
            {
                return View(p);
            }

        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Project pro)
        {
            int id = pro.id;
            try
            {
                projectRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
