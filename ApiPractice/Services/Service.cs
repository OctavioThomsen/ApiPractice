using ApiPractice.Interfaces;
using ApiPractice.Models;

namespace ApiPractice.Services
{
    public class Service : IService
    {
        private readonly IRepository _repository;

        public Service(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Client> DeleteClient(Client client)
        {
            return await _repository.DeleteClient(client);
        }

        public async Task<List<Invoice>> DeleteInvoicesFromClient(List<Invoice> invoices)
        {
            return await _repository.DeleteInvoicesFromClient(invoices);
        }

        public async Task<List<Client>> GetAllClients(bool onlyActives, bool lastlyActivates)
        {
            return await _repository.GetAllClients(onlyActives, lastlyActivates);
        }

        public async Task<Client?> GetClientById(int clientId)
        {
            return await _repository.GetClientById(clientId);
        }

        public async Task<List<Client>> GetClientsWithoutInvoices()
        {
            return await _repository.GetClientsWithoutInvoices();
        }

        public async Task<List<Client>> GetClientsWithUnpaidInvoices()
        {
            return await _repository.GetClientsWithUnpaidInvoices();
        }

        public async Task<Invoice?> GetInvoiceById(int id)
        {
            return await _repository.GetInvoiceById(id);
        }

        public async Task<List<Invoice>> GetInvoicesFromClient(int id)
        {
            return await _repository.GetInvoicesFromClient(id);
        }

        public async Task<decimal> GetSumarizedInvoicesFromAClient(int id)
        {
            List<Invoice> invoices = await _repository.GetInvoicesFromClient(id);

            decimal amount = invoices.Sum(c => c.TotalAmount);

            return amount;
        }

        public async Task<Client> PostNewClient(Client client)
        {
            return await _repository.PostNewClient(client);
        }

        public async Task<Invoice> PostNewInvoice(Invoice invoice)
        {
            return await _repository.PostNewInvoice(invoice);
        }

        public async Task<Client> PutModifyClient(Client client)
        {
            return await _repository.PutModifyClient(client);
        }

        public async Task<Invoice> PutPayInvoice(Invoice invoice)
        {
            return await _repository.PutPayInvoice(invoice);
        }
    }
}
