namespace SLAssginBatch
{
    public class Item
    {
        public Item(string itemCode, decimal numInSale, decimal numInCnt)
        {

            ItemCode = itemCode;
            NumInSale = numInSale;
            NumInCnt = numInCnt;
        }
        public string ItemCode { get; set; }
        public decimal NumInSale { get; set; }
        public decimal NumInCnt { get; set; }
    }
}
