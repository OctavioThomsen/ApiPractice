namespace ApiPractice.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BusinessName { get; set; }
        public string CUIT { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ActivationDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}
