namespace NHT.ASM.Tests.Unit.Dal
{
    //[TestClass]
    //public class NonDomainEntityRepositoryTests
    //{
    //    [TestMethod]
    //    public void InstantiatingWithDomainEntity_ThrowsError()
    //    {
    //        EvaluatorContext context = InMemoryDatabaseContext.GetEvaluatorContextContext("InstantiatingWithDomainEntity_ThrowsError");
    //        var errors = new List<Exception>();
    //        try
    //        {
    //            var nonDomainEntityRepoFromDomainEntity = new NonDomainEntityRepository<User>(context);
    //        }
    //        catch (Exception e)
    //        {
    //            errors.Add(e);
    //        }
    //        Assert.AreEqual(1,errors.Count);

    //    }
    //    [TestMethod]
    //    public void Add_Single_SavesToContext_EntityCanBeRetrieved()
    //    {
    //        EvaluatorContext context = InMemoryDatabaseContext.GetEvaluatorContextContext("Add_Single_SavesToContext_EntityCanBeRetrieved", false);
    //        IDomainEntityRepository<User> repository = new UserRepository(context);
    //        using (new EfUnitOfWorkFactory().Create(context))
    //        {
    //            repository.Add(new User{GprGebruikersId = 1000});
    //        }

    //        var user = repository.FindAll().FirstOrDefault(u=>u.GprGebruikersId == 1000);

    //        Assert.IsNotNull(user);

    //    }

    //    [TestMethod]
    //    public void Add_Multiple_SavesToContext_EntitiesCanBeRetrieved()
    //    {
    //        EvaluatorContext context = InMemoryDatabaseContext.GetEvaluatorContextContext("Add_Multiple_SavesToContext_EntitiesCanBeRetrieved", false);
    //        IDomainEntityRepository<User> repository = new UserRepository(context);

    //        var user1 = new User{GprGebruikersId = 1001};
    //        var user2 = new User{GprGebruikersId = 1002};
    //        var user3 = new User{GprGebruikersId = 1003};
    //        var user4 = new User{GprGebruikersId = 1004};
    //        using (new EfUnitOfWorkFactory().Create(context))
    //        {
    //            repository.AddRange(new List<User>
    //            {
    //                user1,user2,user3,user4
    //            });
    //        }

    //        List<User> users = repository.FindAll().ToList();

    //        Assert.IsNotNull(users);
    //        Assert.AreEqual(4, users.Count);

    //    }

    //    [TestMethod]
    //    public void Remove_RemovesEntityFromContext_EntityCannotBeRetrieved()
    //    {
    //        EvaluatorContext context = InMemoryDatabaseContext.GetEvaluatorContextContext("Add_Multiple_SavesToContext_EntitiesCanBeRetrieved", false);
    //        IDomainEntityRepository<User> repository = new UserRepository(context);
    //        IUnitOfWork uow = new EfUnitOfWorkFactory().Create(context);

    //        using (uow)
    //        {
    //            repository.Add(new User{GprGebruikersId = 1001});
    //        }

    //        var user = repository.FindAll(u=>u.GprGebruikersId == 1001).Single();
    //        Assert.IsNotNull(user);

    //        using (uow)
    //        {
    //            repository.Remove(user);
    //        }

    //        List<User> deletedUsers = repository.FindAll(u=>u.GprGebruikersId == 1001).ToList();

    //        Assert.IsNotNull(deletedUsers);
    //        Assert.AreEqual(deletedUsers.Count, 0);
    //    }
    //}
}
