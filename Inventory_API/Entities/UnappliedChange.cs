using Inventory_API.Tools;
using System;
using Inventory_API.Models;

namespace Inventory_API.Entities
{
    public class UnappliedChange
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public InvEnums.Table TableCode { get; set; }
        public UnappliedGODModel ChangedObject { get; set; }
        public InvEnums.OperationCode OperationType { get; set; }
    }
}
