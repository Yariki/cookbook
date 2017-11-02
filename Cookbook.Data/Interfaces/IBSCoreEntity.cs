using System;

namespace Cookbook.Data.Interfaces
{
    public interface IBSCoreEntity
    {
        int Id { get; set; }
        DateTime Created { get; set; }
    }
}