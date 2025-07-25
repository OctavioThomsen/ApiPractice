﻿using ApiPractice.Models;

namespace ApiPractice.Interfaces
{
    public interface IService
    {
        Task<Client> DeleteClient(Client client);
        Task<List<Invoice>> DeleteInvoicesFromClient(List<Invoice> invoices);
        Task<List<Client>> GetAllClients(bool onlyActives, bool lastlyActivates);
        Task<Client?> GetClientById(int clientId);
        Task<List<Client>> GetClientsWithoutInvoices();
        Task<List<Client>> GetClientsWithUnpaidInvoices();
        Task<Invoice?> GetInvoiceById(int id);
        Task<List<Invoice>> GetInvoicesFromClient(int id);
        Task<decimal> GetSumarizedInvoicesFromAClient(int id);
        Task<Client> PostNewClient(Client client);
        Task<Invoice> PostNewInvoice(Invoice invoice);
        Task<Client> PutModifyClient(Client client);
        Task<Invoice> PutPayInvoice(Invoice invoice);
    }
}