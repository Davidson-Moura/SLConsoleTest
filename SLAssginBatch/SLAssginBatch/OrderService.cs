
namespace SLAssginBatch
{
    public class OrderService
    {
        internal static string InsertOrder(string bpCode, string itemCode, decimal quantityStock, decimal quantity, string hash,
            string warehouse,
            int? bplId = null,
            bool secondConnection = false)
        {
            string order = $@"
            {{
                ""CardCode"": ""{bpCode}"",
                ""DocDueDate"": ""{DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")}"",
                {(bplId is not null ? $@"""BPL_IDAssignedToInvoice"": {bplId}," : "")}
                ""DocumentLines"": [
                    {{
                        ""ItemCode"": ""{itemCode}"",
                        ""Quantity"": {quantity.ToString("N2", Connection.Culture)},
                        ""UnitPrice"": ""1"",
                        ""Usage"": 29,
                        ""TaxCode"":""6913-002"",
                        {(string.IsNullOrEmpty(warehouse) ? "" : @$"""WarehouseCode"": ""{warehouse}"",")}
                        ""BatchNumbers"": [
                            {{
                              ""BatchNumber"": ""{hash}"",
                              ""ItemCode"": ""{itemCode}"",
                              ""Quantity"": {quantityStock.ToString("N2", Connection.Culture)}
                            }}
                        ]
                    }}
                ]
            }}
            ";
            return Connection.Post("Orders", order, secondConnection,false);
        }
    }
    public class Order
    {
        public int DocEntry { get; set; }
        public List<OrderLine> DocumentLines { get; set; }
    }
    public class OrderLine
    {
        public int LineNum { get; set; }
    }
}
