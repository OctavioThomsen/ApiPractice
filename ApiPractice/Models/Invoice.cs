namespace ApiPractice.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }

        public Client Client { get; set; }
    }
}
