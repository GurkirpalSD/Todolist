using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ToDoListClassLibrary;

namespace ToDoListUnitTests
{
    [TestClass]
    public class ToDoItemTests
    {
       private static ToDoContext _con;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var options = new DbContextOptionsBuilder<ToDoContext>()
                .UseSqlite("Data Source=/workspaces/Todolist/TodoList.API/todolist.db;")
                .Options;

            _con = new ToDoContext(options);
            _con.Database.EnsureCreated();
        }


    [TestMethod]
public void Save_NewToDoItemWithEmptyDescription_ToDatabase()
{
    var newItem = new ToDoItem { Description = "" };
    _con.Add(newItem);
    _con.SaveChanges();

    var itemFromDb = _con.ToDoItems.FirstOrDefault(item => item.Description == "");
    Assert.IsNotNull(itemFromDb);
    Assert.AreEqual("", itemFromDb.Description);
}

[TestMethod]
public void Save_NewToDoItemWithNullDescription_ToDatabase()
{
    var newItem = new ToDoItem { Description = null };
    _con.Add(newItem);
    _con.SaveChanges();

    var itemFromDb = _con.ToDoItems.FirstOrDefault(item => item.Description == null);
    Assert.IsNotNull(itemFromDb);
    Assert.AreEqual(null, itemFromDb.Description);
}

[TestMethod]
public void Save_NewToDoItemWithLongDescription_ToDatabase()
{
    var longDescription = "This is a long description for testing purposes. It should exceed the typical length limit.";
    var newItem = new ToDoItem { Description = longDescription };
    _con.Add(newItem);
    _con.SaveChanges();

    var itemFromDb = _con.ToDoItems.FirstOrDefault(item => item.Description == longDescription);
    Assert.IsNotNull(itemFromDb);
    Assert.AreEqual(longDescription, itemFromDb.Description);
}

[TestMethod]
public void Save_NewToDoItemWithSpecialCharactersInDescription_ToDatabase()
{
    var descriptionWithSpecialChars = "Description with special characters: ~!@#$%^&*()_+{}|:\"<>?`-=[]\\;',./";
    var newItem = new ToDoItem { Description = descriptionWithSpecialChars };
    _con.Add(newItem);
    _con.SaveChanges();

    var itemFromDb = _con.ToDoItems.FirstOrDefault(item => item.Description == descriptionWithSpecialChars);
    Assert.IsNotNull(itemFromDb);
    Assert.AreEqual(descriptionWithSpecialChars, itemFromDb.Description);
}
    }

    public class ToDoContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }
    }
}