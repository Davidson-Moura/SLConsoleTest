using SLAssginBatch;

string url = "https://localhost:50000/b1s/v2";
string company = "SBODemoBR";
string userName = "manager";
string password = "sap@123";

string userName2 = "Bruno";
string password2 = "sap@123";

string bpCode = "BatchTest";

bool createPartner = false;
bool createItem = false;

decimal NumInSale = 0.5m;
decimal NumInStock = 1m;
string ItemCode = "ItemBatchTest";

int? bplId = 1; //null;

/*
Console.WriteLine("Enter with Service Layer Login:");

Console.WriteLine("Service Layer URL:");
company = Console.ReadLine();

Console.WriteLine("CompanyDB:");
company = Console.ReadLine();

Console.WriteLine("UserName:");
userName = Console.ReadLine();

Console.WriteLine("Password:");
password = Console.ReadLine();
*/

Console.WriteLine("Starting connection...");
Connection.SetConnection(url, company, userName, password);
Console.WriteLine("Connection started!");

Console.WriteLine(); Console.WriteLine(); Console.WriteLine();

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
        string item = $@"
        {{
            ""ItemCode"": ""{ItemCode}"",
            ""ItemName"": ""Item Batch Test"",
            ""InventoryItem"": ""tYES"",
            ""PurchaseItem"": ""tYES"",
            ""SalesItem"": ""tYES"",
            ""ManageBatchNumbers"": ""tYES"",
            ""Valid"": ""tYES"",
            ""SalesItemsPerUnit"": {NumInSale.ToString("N2", Connection.Culture)}, 
            ""CountingItemsPerUnit"": {NumInStock.ToString("N2", Connection.Culture)}
        }}
        ";
        Connection.Post("Items", item);
    }catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
        throw ex;
    }
    Console.WriteLine("Item created!");
    Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
}


Console.WriteLine("Creating Batch..");
var quantity = 40;

string hash = null;
try
{
    hash = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
    string entryBatch = $@"
    {{
        {(bplId is not null ? $@"""BPL_IDAssignedToInvoice"": {bplId}," : "") }
        ""DocumentLines"": [
            {{
                ""ItemCode"": ""ItemBatchTest"",
                ""Quantity"": ""{quantity.ToString("N2", Connection.Culture)}"",
                ""UnitPrice"": ""1"",
                ""BatchNumbers"": [
                    {{
                      ""BatchNumber"": ""{hash}"",
                      ""ItemCode"": ""ItemBatchTest"",
                      ""Quantity"": {quantity.ToString("N2", Connection.Culture)}
                    }}
                ]
            }}
        ]
    }}
    ";
    Connection.Post("InventoryGenEntries", entryBatch);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.ReadKey();
    throw ex;
}
Console.WriteLine("Batch created!");

Console.WriteLine(); Console.WriteLine(); Console.WriteLine();

Console.WriteLine("Creating Orders..");
try
{
    var numberOfTasks = 100;
    Task<string>[] tasks = new Task<string>[numberOfTasks];

    for (int i = 0; i < numberOfTasks; i++)
    {
        tasks[i] = Task.Run(() =>
        {
            Random random = new Random();
            int qtdStock = random.Next(1, 4);
            var quantitySale = qtdStock * NumInSale;
            Connection.SetConnection(url, company,
                i % 2 == 0 ? userName : userName2,
                i % 2 == 0 ? password : password2);
            return OrderService.InsertOrder(bpCode, ItemCode, quantitySale, qtdStock, hash, bplId);
        });
    }
    string[] results = await Task.WhenAll(tasks);

    foreach (var result in results)
    {
        Console.WriteLine(result);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.ReadKey();
    throw ex;
}
Console.WriteLine("Orders created!");
Console.ReadKey();