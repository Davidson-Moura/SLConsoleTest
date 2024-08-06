using SLAssginBatch;
using System.Text.Json;

string url = "https://imaginehdb:50000/b1s/v2";
string company = "SBO_ARTETRIGO_PRD";
string userName = "manager";
string password = "sap@123";

string userName2 = "davidson";
string password2 = "Sap123";

string bpCode = "C006772";// "BatchTest";

bool createPartner = false;
bool createItem = false;

string warehouse = "01-005";


int? bplId = null; //1;

/*
Items by Bug
PA000130
PA000119
PA000140
PA000093
PA000094
 */
List<Item> items = new List<Item>() {
    new Item("PA000130", 1m, 1m),
    new Item("PA000119", 1m, 1m),
    new Item("PA000140", 1m, 1m),
    new Item("PA000093", 1m, 1m),
    new Item("PA000094", 1m, 1m)
};
var item = items[2];
string itemCode = item.ItemCode;
var quantity = 12;

decimal numInSale = item.NumInSale;
decimal numInStock = item.NumInCnt;

Console.WriteLine("Starting connection...");
Connection.SetConnection(url, company, userName, password);
Connection.SetConnection2(url, company, userName2, password2);
Console.WriteLine("Connection started!");

Console.WriteLine(); Console.WriteLine(); Console.WriteLine();

#region Creation
if (createPartner)
{
    Console.WriteLine("Creating Partner..");
    try
    {
        string partner = $@"
        {{
            ""CardCode"": ""{bpCode}"",
            ""CardName"": ""SL Batch Test"",
            ""CardType"": ""cCustomer"",
        }}
        ";
        Connection.Post("BusinessPartners", partner);
    }catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
        throw ex;
    }
    Console.WriteLine("Partner created!");
    Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
}


if (createItem)
{
    Console.WriteLine("Creating Item..");
    try
    {
        string itemJson = $@"
        {{
            ""ItemCode"": ""{itemCode}"",
            ""ItemName"": ""Item Batch Test"",
            ""InventoryItem"": ""tYES"",
            ""PurchaseItem"": ""tYES"",
            ""SalesItem"": ""tYES"",
            ""ManageBatchNumbers"": ""tYES"",
            ""Valid"": ""tYES"",
            ""SalesItemsPerUnit"": {numInSale.ToString("N2", Connection.Culture)}, 
            ""CountingItemsPerUnit"": {numInStock.ToString("N2", Connection.Culture)}
        }}
        ";
        Connection.Post("Items", itemJson);
    }catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
        throw ex;
    }
    Console.WriteLine("Item created!");
    Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
}
#endregion

string hash = null;
Console.WriteLine("Creating Production Order...");
try
{
    var production = ProductionOrderService.AddProductionOrder(itemCode, quantity, warehouse);
    ProductionOrderService.ProductionOrderToRelease(production.AbsoluteEntry);
    var batchNumber = ProductionOrderService.ProductionOrderEntries(production.AbsoluteEntry, itemCode, quantity, warehouse, bplId);
    hash = batchNumber;
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.ReadKey();
    throw ex;
}
Console.WriteLine("Production Order created!");

Console.WriteLine(); Console.WriteLine(); Console.WriteLine();

#region Create Batch
//Console.WriteLine("Creating Batch...");

//string hash = null;
//try
//{
//    hash = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
//    string entryBatch = $@"
//    {{
//        {(bplId is not null ? $@"""BPL_IDAssignedToInvoice"": {bplId}," : "") }
//        ""DocumentLines"": [
//            {{
//                ""ItemCode"": ""ItemBatchTest"",
//                ""Quantity"": ""{quantity.ToString("N2", Connection.Culture)}"",
//                ""UnitPrice"": ""1"",
//                ""BatchNumbers"": [
//                    {{
//                      ""BatchNumber"": ""{hash}"",
//                      ""ItemCode"": ""ItemBatchTest"",
//                      ""Quantity"": {quantity.ToString("N2", Connection.Culture)}
//                    }}
//                ]
//            }}
//        ]
//    }}
//    ";
//    Connection.Post("InventoryGenEntries", entryBatch);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//    Console.ReadKey();
//    throw ex;
//}
//Console.WriteLine("Batch created!");

//Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
#endregion

List<Order> orders = new List<Order>();

Random random = new Random();
int qtdStock = 3;// random.Next(1, 4);
var quantitySale = qtdStock * numInSale;

Console.WriteLine("Creating Orders..");
try
{
    var numberOfTasks = 4;
    Task<string>[] tasks = new Task<string>[numberOfTasks];

    for (int i = 0; i < numberOfTasks; i++)
    {
        tasks[i] = Task.Run(() =>
        {
            var result =  OrderService.InsertOrder(bpCode, itemCode, quantitySale, qtdStock, hash,
                warehouse,
                bplId,
                i % 2 == 0);
            Console.WriteLine(result);
            Console.WriteLine("__________________________________________________________________");
            return result;
        });
    }
    string[] results = await Task.WhenAll(tasks);

    foreach (var r in results)
    {
        var obj = JsonSerializer.Deserialize<Order>(r);
        if (obj is not null)
            orders.Add(obj);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.ReadKey();
    throw ex;
}
Console.WriteLine("End process!");

Console.WriteLine("Creating Invoice..");
try
{
    var numberOfTasks = 4;
    Task<string>[] tasks = new Task<string>[numberOfTasks];

    for (int i = 0; i < numberOfTasks; i++)
    {
        var index = i;
        tasks[i] = Task.Run(() =>
        {
            var order = orders[index];
            var result = InvoiceService.CreateInvoice(order, hash, itemCode, qtdStock, i % 2 == 0);
            Console.WriteLine(result);
            Console.WriteLine("__________________________________________________________________");
            return result;
        });
    }
    string[] results = await Task.WhenAll(tasks);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.ReadKey();
    throw ex;
}
Console.WriteLine("End process!");

Console.ReadKey();