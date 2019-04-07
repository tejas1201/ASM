using System;
using NHT.ASM.Helpers.ExtensionMethods;
using NHT.ASM.Infrastructure;

namespace NHT.ASM.Dal.Uow
{
    /// <summary>
    /// Defines a Unit of Work using an EF DbContext under the hood.
    /// </summary>
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly IAsmContext _context;

        /// <summary>
        /// Initializes a new instance of the EFUnitOfWork class.
        /// </summary>
        /// <param name="context">Current entry context</param>
        public EfUnitOfWork(IAsmContext context)
        {
            _context = context;
        }

        public void Commit(bool resetAfterCommit)
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Errors occured while saving to the database {ex.ToCustomString()}");
            }
        }
        
        /// <summary>
        /// Undoes changes to the current DbContext
        /// </summary>
        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
