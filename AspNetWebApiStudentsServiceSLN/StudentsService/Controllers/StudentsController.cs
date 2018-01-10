using StudentsServiceDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentsService.Controllers
{
    public class StudentsController : ApiController
    {
        StudentsServiceDAManager daManager = new StudentsServiceDAManager();

        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                        daManager.GetAllStudents());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Get(int id)
        {
            try
            {
                Student student = daManager.GetStudentById(id);

                if (student.Id != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, student);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Search failed. Student with Id = " +
                        id.ToString() + " not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post([FromBody]Student student)
        {
            try
            {
                int newId = daManager.AddStudent(student);
                student.Id = newId;

                HttpResponseMessage message = Request.CreateResponse(
                    HttpStatusCode.Created, student);
                string requestUriString = Request.RequestUri.ToString();
                message.Headers.Location = new Uri(requestUriString +
                    ((requestUriString.EndsWith("/")) ? "" : "/") + student.Id.ToString());

                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]Student student)
        {
            try
            {
                student.Id = id;
                Student testStudent = daManager.GetStudentById(id);

                if (testStudent.Id != 0)
                {
                    daManager.UpdateStudent(student);
                    return Request.CreateResponse(HttpStatusCode.OK, student);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Update failed. Student with Id = " +
                        id.ToString() + " not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Student testStudent = daManager.GetStudentById(id);

                if (testStudent.Id != 0)
                {
                    daManager.DeleteStudent(id);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Delete failed. Student with Id = " +
                        id.ToString() + " not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
