namespace proyectoprogra6_api.ModelsDTOs
{
    public class ReservationCount
    {

        public string? ItemName { get; set; }
        public int Count { get; set; }

        public ReservationCount(string? itemName, int count)
        {
            ItemName = itemName;
            Count = count;
        }
    }
}
