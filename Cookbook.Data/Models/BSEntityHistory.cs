using System.ComponentModel.DataAnnotations.Schema;
using Cookbook.Data.Core;

namespace Cookbook.Data.Models
{
    
    public class BSEntityHistory : BSCoreEntity
    {
        public string Type { get; set; }
        
        public int ObjectId { get; set; }


        [Column(TypeName = "xml")]
        public string Values { get; set; }
        
    }
}