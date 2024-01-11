using MVC2.CONTEXT;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;

namespace MVC2.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        CRUDMVCDBEntities dbObj=new CRUDMVCDBEntities();
        public ActionResult Student(STUDENT_TBL obj)
        {
            //if (obj != null)
            //{
            //    return View(obj);
            //}    
            //else
            //{
            //    obj = new STUDENT_TBL();
               
            //    return View(obj);
            //}
            ModelState.Clear();
            return View(obj);
        }
        [HttpPost]
        public ActionResult AddStudent(STUDENT_TBL model)
        {
            if(ModelState.IsValid)
            {
                STUDENT_TBL obj=new STUDENT_TBL();
                obj.ID = model.ID;
                obj.NAME = model.NAME;
                obj.FNAME=model.FNAME;
                obj.EMAIL=model.EMAIL;
                obj.MOBILE=model.MOBILE;
                obj.DESCRIPTION=model.DESCRIPTION;
                if(model.ID==0)
                {
                    dbObj.STUDENT_TBL.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    var objStudent = dbObj.STUDENT_TBL.Where(x => x.ID == model.ID).FirstOrDefault();
                    objStudent.NAME = model.NAME;
                    objStudent.FNAME = model.FNAME;
                    objStudent.EMAIL = model.EMAIL;
                    objStudent.MOBILE = model.MOBILE;
                    objStudent.DESCRIPTION = model.DESCRIPTION;

                    //dbObj.Entry(obj).State=EntityState.Modified; 
                    dbObj.SaveChanges();   
                }
            }
            ModelState.Clear();
            return View("Student");
        }
        public ActionResult StudentList()
        {
            var res=dbObj.STUDENT_TBL.ToList();
            return View(res);
        }
        public ActionResult Delete(int id)
        {
            var res = dbObj.STUDENT_TBL.Where(x=>x.ID==id).First();
            dbObj.STUDENT_TBL.Remove(res);
            dbObj.SaveChanges() ;
            //Display Updated List
            var list = dbObj.STUDENT_TBL.ToList();
            return View("StudentList",list);
        }
    }
}