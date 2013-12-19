using System;
using System.Data.Entity.Migrations;
using System.Linq;
using AngularLessons.Models;

namespace AngularLessons.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AngularLessonsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AngularLessonsContext context)
        {
            var r = new Random();
            var items = Enumerable.Range(1, 50).Select(o => new TodoItem
            {
                DueDate = new DateTime(2012, r.Next(1, 12), r.Next(1, 28)),
                Priority = (byte)r.Next(10),
                Todo = o.ToString()
            }).ToArray();
            context.TodoItems.AddOrUpdate(item => new { item.Todo }, items);
        }
    }
}
