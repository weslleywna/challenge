using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product : BaseEntity, IProduct
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
