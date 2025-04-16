using ApiPractice.Interfaces;
using ApiPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPractice.Repositories
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Client> DeleteClient(Client client)
        {
            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<List<Invoice>> DeleteInvoicesFromClient(List<Invoice> invoices)
        {
            _context.Invoices.RemoveRange(invoices);

            await _context.SaveChangesAsync();

            return invoices;
        }

        public async Task<List<Client>> GetAllClients(bool onlyActives = false, bool lastlyActivates = false)
        {
            List<Client> clients = [];

            if (lastlyActivates)
            {
                clients = await _context.Clients
                    .Where(c =>
                    c.IsActive &&
                    c.ActivationDate.HasValue &&
                    (DateTime.Now - c.ActivationDate.Value).TotalDays > 90)
                    .ToListAsync();
            }
            else if (onlyActives)
            {
                clients = await _context.Clients.Where(c => c.IsActive).ToListAsync();
            }
            else
            {
                clients = await _context.Clients.ToListAsync();
            }

            return await Task.Run(() => clients);
        }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            List<Invoice> invoices = [];

            invoices = await _context.Invoices.ToListAsync();

            return await Task.Run(() => invoices);
        }

        public async Task<Client?> GetClientById(int clientId)
        {
            return await _context.Clients.FindAsync(clientId);
        }

        public async Task<List<Client>> GetClientsWithoutInvoices()
        {
            List<Client> clients = await _context.Clients.Where(c => !c.Invoices.Any()).ToListAsync();

            return await Task.Run(() => clients);
        }

        public async Task<List<Client>> GetClientsWithUnpaidInvoices()
        {
            List<Client> clients = await _context.Clients
                .Where(c =>
                    c.Invoices.Any(i => !i.IsPaid))
                .ToListAsync();

            return await Task.Run(() => clients);
        }

        public async Task<Invoice?> GetInvoiceById(int id)
        {
            return await _context.Invoices.FindAsync(id);
        }

        public async Task<List<Invoice>> GetInvoicesFromClient(int id)
        {
            List<Invoice> invoices = [];

            invoices = await _context.Invoices.Where(c => c.ClientId == id).ToListAsync();

            return await Task.Run(() => invoices);
        }

        public async Task<Client> PostNewClient(Client client)
        {
            _context.Clients.Add(client);

            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<Invoice> PostNewInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);

            await _context.SaveChangesAsync();

            return invoice;
        }

        public async Task<Client> PutModifyClient(Client client)
        {
            _context.Clients.Update(client);

            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<Invoice> PutPayInvoice(Invoice invoice)
        {
            _context.Invoices.Update(invoice);

            await _context.SaveChangesAsync();

            return invoice;
        }
    }
}