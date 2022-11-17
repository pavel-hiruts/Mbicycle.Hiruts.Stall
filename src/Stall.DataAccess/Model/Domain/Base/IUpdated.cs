using Stall.DataAccess.Model.Identity;

namespace Stall.DataAccess.Model.Domain.Base;

public interface IUpdated
{
    User? UpdatedBy { get; set;}

    DateTime? UpdatedDate { get; }
}