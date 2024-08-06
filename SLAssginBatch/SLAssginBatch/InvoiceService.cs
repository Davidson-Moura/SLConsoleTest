namespace SLAssginBatch
{
    public class InvoiceService
    {
        public static string CreateInvoice(Order o, string hash, string itemCode, decimal quantityStock, bool secondConnection = false)
        {
            var lines = "";
            o.DocumentLines.ForEach(l =>
            {
                lines += @$"
{{
    ""BaseType"": 17,
    ""BaseEntry"": {o.DocEntry},
    ""BaseLine"": {l.LineNum},
    ""BatchNumbers"": [
        {{
            ""BatchNumber"": ""{hash}"",
            ""ItemCode"": ""{itemCode}"",
            ""Quantity"": {quantityStock.ToString("N2", Connection.Culture)}
        }}
    ]
}}
";
            });
            

            var json = $@"
{{  
    ""DocumentLines"": [{lines}]
}}
";
            return Connection.Post("Invoices", json, secondConnection: secondConnection, throwExceptions: false);
        }
    }
}
