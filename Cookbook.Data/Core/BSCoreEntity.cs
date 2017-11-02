using System;
using Cookbook.Data.Interfaces;

namespace Cookbook.Data.Core
{
    public class BSCoreEntity : IBSCoreEntity
    {
        public int Id { get; set; }
        
        public DateTime Created { get; set; }
        
    }
}