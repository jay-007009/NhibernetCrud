using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NhibernetCrud.Models;
using NHibernate;
using NhibernetCrud;
using Microsoft.AspNetCore.Http;

namespace NhibernetCrud.Controllers
{
    public class EmployeeController : Controller
    {
       
        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";
            IList<Employee> employees;

            using (NHibernate.ISession session = NhibernateSession.OpenSession())  // Open a session to conect to the database
            {
                employees = session.Query<Employee>().ToList(); //  Querying to get all the employees
                return View(employees);
            }


        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            using (NHibernate.ISession session = NhibernateSession.OpenSession())
            {
                var employee = session.Get<Employee>(id);
                return View(employee);
            }
        }


        // POST: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create/5
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                // Employee employee = new Employee();     //  Creating a new instance of the Employee
                // employee.FirstName = collection["FirstName"].ToString();
                //employee.LastName = collection["LastName"].ToString();
                // employee.Age = Convert.ToInt32(collection["Age"]);
                // employee.MaritalStatus = Convert.ToBoolean(collection["MaritalStatus"]);
                // employee.Gender = (collection["Gender"]).ToString();
                // employee.Department = (collection["Department"]).ToString();

                // TODO: Add insert logic here
                using (NHibernate.ISession session = NhibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                        session.Save(employee); //  Save the employee in session
                        transaction.Commit();   //  Commit the changes to the database
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }




        // PUT: Employee/Edit
        public ActionResult Edit(int id)
        {
            //Employee employee = new Employee();
            //using (NHibernate.ISession session = NhibernateSession.OpenSession())
            //{
            //    employee = session.Query<Employee>().Where(b => b.EmployeeId == id).FirstOrDefault();
            //}

            //ViewBag.SubmitAction = "Save";
            //return View(employee);

            using (NHibernate.ISession session = NhibernateSession.OpenSession())
            {
                var employee = session.Get<Employee>(id);
                return View(employee);
            }

        }

        // PUT: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee /*,FormCollection collection*/)
        {
            //try
            //{
            //    Employee employee = new Employee();     //  Updating a new instance of the Employee
            //    employee.FirstName = collection["FirstName"].ToString();
            //    employee.LastName = collection["LastName"].ToString();
            //    employee.Age = Convert.ToInt32(collection["Age"]);
            //    employee.MaritalStatus = Convert.ToBoolean (collection["MaritalStatus"]);
            //    employee.Gender = (collection["Gender"]).ToString();
            //    employee.Department = (collection["Department"]).ToString();


            //    // TODO: Add insert logic here
            //    using (NHibernate.ISession session = NhibernateSession.OpenSession())
            //    {
            //        using (ITransaction transaction = session.BeginTransaction())
            //        {
            //            session.SaveOrUpdate(employee);
            //            transaction.Commit();
            //        }
            //    }
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}

            try
            {
                using (NHibernate.ISession session = NhibernateSession.OpenSession())
                {
                    var employeetoUpdate = session.Get<Employee>(id);

                    employeetoUpdate.FirstName = employee.FirstName;
                    employeetoUpdate.LastName = employee.LastName;
                    employeetoUpdate.Age = employee.Age;
                    employeetoUpdate.MaritalStatus = employee.MaritalStatus;
                    employeetoUpdate.Gender = employee.Gender;
                    employeetoUpdate.Department = employee.Department;
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(employeetoUpdate);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

      





        // DELETE: Employee/Delete/
        public ActionResult Delete(int id)
        {
            using (NHibernate.ISession session = NhibernateSession.OpenSession())
            {
                var employee = session.Get<Employee>(id);
                return View(employee);
            }
        }

        // DELETE: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee employee /*,FormCollection collection*/)
        {
            //try
            //{
            //    // TODO: Add delete logic here
            //    using (NHibernate.ISession session = NhibernateSession.OpenSession())
            //    {
            //        Employee employee = session.Get<Employee>(id);

            //        using (ITransaction trans = session.BeginTransaction())
            //        {
            //            session.Delete(employee);
            //            trans.Commit();
            //        }
            //    }
            //    return RedirectToAction("Index");
            //}
            //catch (Exception e)
            //{
            //    return View();
            //}

            try
            {
                using (NHibernate.ISession session = NhibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        var employe = session.Get<Employee>(id);
                        session.Delete(employe);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }            
        }

        public IActionResult Department(List<Department> departments)
        {
            departments = new List<Department>()
            {
                new Department()
                { DepartmentId=1001,DepartmentName="FrontEnd" },
                 new Department()
                { DepartmentId=1002,DepartmentName="HR" },
                  new Department()
                { DepartmentId=1003,DepartmentName="Senior Manager" },
                   new Department()
                { DepartmentId=1004,DepartmentName="Backend" },
                    new Department()
                { DepartmentId=1005,DepartmentName="Devops" },
            };

            return View(departments);
        }
    }
}
