using Quizzler.Dal.Interfaces.Entities.Base;

namespace Quizzler.Dal.Interfaces.IRepositories.IBase;

public interface IRepository<T> where T : BaseEntity, new()
{

}