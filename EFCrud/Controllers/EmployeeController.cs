using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCrud.Models;
namespace EFCrud.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmployeeList()
        {
            try
            {
                var employeeList = from a in _employeeContext.Employees
                                   join b in _employeeContext.Departments
                                   on a.DepartmentId equals b.DepartmentId
                                   into Dep //giving aliase 
                                   from b in Dep.DefaultIfEmpty()

                                   select new Employee
                                   {
                                       EmployeeId = a.EmployeeId,
                                       FirstName = a.FirstName,
                                       LastName = a.LastName,
                                       Age = a.Age,
                                       MaritalStatus = a.MaritalStatus,
                                       Gender = a.Gender,
                                       DepartmentId = a.DepartmentId,

                                       Department = b == null ? "" : b.DepartmentName
                                   };
                return View(employeeList);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public IActionResult Create(Employee obj)
        {
            loadDropdownMenu();
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (obj.EmployeeId == 0)
                    {
                        _employeeContext.Employees.Add(obj);
                        await _employeeContext.SaveChangesAsync();

                    }
                    else
                    {
                        _employeeContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        await _employeeContext.SaveChangesAsync();
                    }
                    return RedirectToAction("EmployeeList");
                }
                return View(obj);
            }
            catch (Exception e)
            {
                return RedirectToAction("EmployeeList");
            }
        }

        private void loadDropdownMenu()
        {
            try
            {
                List<Department> departmentlist = new List<Department>();
                departmentlist = _employeeContext.Departments.ToList();
                departmentlist.Insert(0, new Department { DepartmentId = 0, DepartmentName = "Please Select" });

                ViewBag.DepartmentList = departmentlist;
            }
            catch (Exception e)
            {

            }
        }

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {

                var employee =await _employeeContext.Employees.FindAsync(id);
                if(employee!=null)
                {
                    _employeeContext.Employees.Remove(employee);
                    await _employeeContext.SaveChangesAsync();
                }
                    return RedirectToAction("EmployeeList");
                
             
            }
            catch (Exception e)
            {
                return RedirectToAction("EmployeeList");
            }
        }



    }
}
