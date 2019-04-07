using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHT.ASM.Tests.Unit.BLL
{
    [TestClass]
    public class BaseLogicTests
    {
        //[TestMethod]
        //public void Post_SavesToContext_EntityCanBeRetrieved()
        //{
        //    string testName = "Post_SavesToContext_EntityCanBeRetrieved";
        //    var logic = GetLogic(testName);

        //    logic.Post(new UserDto {GprGebruikersId = 10});

        //    var john = logic.GetAll(x => x.GprGebruikersId == 10).Single();

        //    Assert.IsNotNull(john);
        //}

        //[TestMethod]
        //public void Put_ChangesEntityInContext_ChangesCanBeRetrieved()
        //{
        //    string testName = "Put_ChangesEntityInContext_ChangesCanBeRetrieved";
        //    var logic = GetLogic(testName);

        //    logic.Post(new UserDto { GprGebruikersId = 10});

        //    var john = logic.GetAll(x => x.GprGebruikersId == 10).Single();
        //    var oldDateModified = john.DateModified;
        //    var oldDateCreated = john.DateCreated;
        //    Assert.AreEqual(10, john.GprGebruikersId);

        //    logic.Put(john.Id,new UserDto { GprGebruikersId = 12});
            
        //    var john2 = logic.GetAll(x => x.GprGebruikersId == 12).Single();
        //    Assert.AreEqual(12, john2.GprGebruikersId);
        //    Assert.AreEqual(oldDateCreated, john2.DateCreated);
        //    Assert.AreNotEqual(oldDateModified,john2.DateModified);
        //}

        
        //[TestMethod]
        //public void Delete_RemovesEntityFromContext_EntityCannotBeRetrieved()
        //{
        //    string testName = "Delete_RemovesEntityFromContext_EntityCannotBeRetrieved";
        //    var logic = GetLogic(testName);

        //    logic.Post(new UserDto { GprGebruikersId = 10});

        //    var john = logic.GetAll(x => x.GprGebruikersId == 10).Single();
        //    var johnId = john.Id;
        //    Assert.IsNotNull(john);

        //    logic.Delete(john.Id);

        //    var john2 = logic.GetById(johnId);
            
        //    Assert.IsNull(john2);
        //}
        
        //private static IUserLogic GetLogic(string testName)
        //{
        //    var options = new DbContextOptionsBuilder<EvaluatorContext>()
        //        .UseInMemoryDatabase(testName)
        //        .Options;
        //    var context = new EvaluatorContext(options);

        //    var userLogic = new UserLogic(context, new UserRepository(context));
            
        //    return userLogic;
        //}
    }
}
