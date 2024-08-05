
namespace SLAssginBatch
{
    public class OrderService
    {
        internal static string InsertOrder(string bpCode, string itemCode, decimal quantityStock, decimal quantity, string hash, int? bplId = null)
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
            return Connection.Post("Orders", order, false);
        }
    }
}
