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
        // GET: Employee
        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";
            IList<Employee> employees;

            using (NHibernate.ISession session = NhibernateSession.OpenSession())  // Open a session to conect to the database
            {
                employees = session.Query<Employee>().ToList(); //  Querying to get all the employees
            }

            return View(employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            Employee employee = new Employee();
            using (NHibernate.ISession session = NhibernateSession.OpenSession())
            {
                employee = session.Query<Employee>().Where(b => b.EmployeeId == id).FirstOrDefault();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Employee employee = new Employee();     //  Creating a new instance of the Employee
                employee.FirstName = collection["FirstName"].ToString();
               employee.LastName = collection["LastName"].ToString();
                employee.Age = Convert.ToInt32(collection["Age"]);
                employee.MaritalStatus = (collection["MaritalStatus"]).ToString();
                employee.Gender = (collection["Gender"]).ToString();
                employee.Department = collection["Gender"].ToString();

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

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = new Employee();
            using (NHibernate.ISession session = NhibernateSession.OpenSession())
            {
                employee = session.Query<Employee>().Where(b => b.EmployeeId == id).FirstOrDefault();
            }

            ViewBag.SubmitAction = "Save";
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Employee employee = new Employee();     //  Updating a new instance of the Employee
                employee.FirstName = collection["FirstName"].ToString();
                employee.LastName = collection["LastName"].ToString();
                employee.Age = Convert.ToInt32(collection["Age"]);
                employee.MaritalStatus = (collection["MaritalStatus"]).ToString();
                employee.Gender = (collection["Gender"]).ToString();
                employee.Department = collection["Gender"].ToString();


                // TODO: Add insert logic here
                using (NHibernate.ISession session = NhibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(employee);
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            // Delete the employee
            Employee employee = new Employee();
            using (NHibernate.ISession session = NhibernateSession.OpenSession())
            {
                employee = session.Query<Employee>().Where(b => b.EmployeeId == id).FirstOrDefault();
            }
            ViewBag.SubmitAction = "Confirm delete";
            return View("Edit", employee);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(long id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (NHibernate.ISession session = NhibernateSession.OpenSession())
                {
                    Employee employee = session.Get<Employee>(id);

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(employee);
                        trans.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}
