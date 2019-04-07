using NHT.ASM.Infrastructure;

namespace NHT.ASM.Dal.Uow
{
  /// <summary>
  /// Creates new instances of an EF unit of Work.
  /// </summary>
  public class EfUnitOfWorkFactory : IUnitOfWorkFactory
  {
    /// <inheritdoc />
    public IUnitOfWork Create(IAsmContext context)
    {
      return new EfUnitOfWork(context);
    }
  }
}
