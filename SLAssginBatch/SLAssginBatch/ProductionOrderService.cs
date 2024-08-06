using System.Text.Json;

namespace SLAssginBatch
{
    internal class ProductionOrderService
    {
        public static ProductionOrder AddProductionOrder(string itemCode, decimal quantity, string warehouse)
        {
            var json = $@"
{{
    ""DueDate"": ""{DateTime.Now:yyyy-MM-dd}"",
    ""ItemNo"": ""{itemCode}"",
    ""PlannedQuantity"": {quantity.ToString("N2", Connection.Culture)},
    {(string.IsNullOrEmpty(warehouse) ? "" : @$"""Warehouse"": ""{warehouse}"",")}
}}
";
            var resultJson = Connection.Post("ProductionOrders", json);
            return JsonSerializer.Deserialize<ProductionOrder>(resultJson);
        }
        public static void ProductionOrderToRelease(int docEntry)
        {
            var json = $@"
{{
    ""AbsoluteEntry"": {docEntry},
    ""ProductionOrderStatus"": ""boposReleased"",
}}
";
            Connection.Patch($"ProductionOrders({docEntry})", json);
        }
        public static string ProductionOrderEntries(int docEntry, string itemCode, decimal quantity, string warehouse, int? bplId)
        {
            var hash = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
            var json = $@"
{{
    ""DueDate"": ""{DateTime.Now:yyyy-MM-dd}"",
    ""Reference2"": ""ArteTrigo"",
    {(bplId is not null ? $@"""BPL_IDAssignedToInvoice"": {bplId}," : "")}
    ""DocumentLines"":[
        {{
            ""DocEntry"": {docEntry},
            ""BaseEntry"": {docEntry},
            ""BaseType"": 202,
            ""Quantity"": {quantity.ToString("N2", Connection.Culture)},
            {(string.IsNullOrEmpty(warehouse) ? "" : @$"""WarehouseCode"": ""{warehouse}""," )}
            ""BatchNumbers"": [
                {{
                  ""BatchNumber"": ""{hash}"",
                  ""ItemCode"": ""{itemCode}"",
                  ""Quantity"": {quantity.ToString("N2", Connection.Culture)}
                }}
            ]
        }}
    ]
}}
";
            Connection.Post("InventoryGenEntries", json, secondConnection: true);
            return hash;
        }
    }
    public class ProductionOrder
    {
        public int AbsoluteEntry { get; set; }
    }
}
