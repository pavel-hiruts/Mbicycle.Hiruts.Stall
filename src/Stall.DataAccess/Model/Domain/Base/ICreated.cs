using Stall.DataAccess.Model.Identity;

namespace Stall.DataAccess.Model.Domain.Base;

public interface ICreated
{
    User? CreatedBy { get; set; }
    DateTime? CreatedDate { get; }
}