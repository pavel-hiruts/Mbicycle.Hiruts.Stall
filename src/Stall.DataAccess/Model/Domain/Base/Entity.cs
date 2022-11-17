using Stall.DataAccess.Model.Identity;

namespace Stall.DataAccess.Model.Domain.Base;

public abstract class Entity : ICreated, IUpdated
{
    public int Id { get; set; }
    private User? _createdBy;
    private User? _updatedBy;

    public User? CreatedBy
    {
        get => _createdBy;
        set
        {
            _createdBy = value; 
            CreatedDate = DateTime.Now;
        }
    }

    public DateTime? CreatedDate { get; private set; }

    public User? UpdatedBy
    {
        get => _updatedBy;
        set
        {
            _updatedBy = value; 
            UpdatedDate = DateTime.Now;
        }
    }

    public DateTime? UpdatedDate { get; private set; }
}