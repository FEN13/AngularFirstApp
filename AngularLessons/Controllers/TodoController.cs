using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AngularLessons.Models;

namespace AngularLessons.Controllers
{
    public class TodoController : ApiController
    {
        private readonly AngularLessonsContext _db = new AngularLessonsContext();

        // GET api/Todo
        public IEnumerable<TodoItem> GetTodoItems(string q = null, string sort = null, bool desc = false,
                                                        int? limit = null, int offset = 0)
        {
            var list = ((IObjectContextAdapter)_db).ObjectContext.CreateObjectSet<TodoItem>();

            IQueryable<TodoItem> items = string.IsNullOrEmpty(sort) ? list.OrderBy(o => o.Priority)
                : list.OrderBy(String.Format("it.{0} {1}", sort, desc ? "DESC" : "ASC"));

            if (!string.IsNullOrEmpty(q) && q != "undefined") items = items.Where(t => t.Todo.Contains(q));

            if (offset > 0) items = items.Skip(offset);
            if (limit.HasValue) items = items.Take(limit.Value);
            return items;
        }

        // GET api/Todo/5
        public TodoItem GetTodoItem(int id)
        {
            TodoItem todoitem = _db.TodoItems.Find(id);
            if (todoitem == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return todoitem;
        }

        // PUT api/Todo/5
        public HttpResponseMessage PutTodoItem(int id, TodoItem todoitem)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != todoitem.TodoItemId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _db.Entry(todoitem).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Todo
        public HttpResponseMessage PostTodoItem(TodoItem todoitem)
        {
            if (ModelState.IsValid)
            {
                _db.TodoItems.Add(todoitem);
                _db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, todoitem);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = todoitem.TodoItemId }));
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        // DELETE api/Todo/5
        public HttpResponseMessage DeleteTodoItem(int id)
        {
            TodoItem todoitem = _db.TodoItems.Find(id);
            if (todoitem == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _db.TodoItems.Remove(todoitem);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, todoitem);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}