using System;

namespace MFL.Data.Entities
{
    public interface IUpdatable
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
