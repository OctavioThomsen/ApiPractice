using ApiPractice.DTOs;
using ApiPractice.Interfaces;
using ApiPractice.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiPractice.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly IService Service;

        public ClientController(IService service)
        {
            Service = service;
        }

        [HttpDelete("DeleteClient")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            Client? client = await Service.GetClientById(id);

            if (client == null)
                return NotFound("Client doesn't exist.");

            if (client.Invoices != null && client.Invoices.Any())
            {
                return BadRequest("Client have invoices related.");
            }

            client = await Service.DeleteClient(client);

            return Ok($"Client '{client.Name}' eliminated succesfully.");
        }

        [HttpDelete("DeleteInvoicesFromClient")]
        public async Task<IActionResult> DeleteInvoicesFromClient(int id)
        {
            List<Invoice> invoices = await Service.GetInvoicesFromClient(id);

            if (!invoices.Any())
                return BadRequest("Client doesn't have invoices related.");

            invoices = await Service.DeleteInvoicesFromClient(invoices);

            List<InvoiceDto> invoiceDtos = invoices.Select(invoice => new InvoiceDto
            {
                Id = invoice.Id,
                ClientId = invoice.ClientId,
                Date = invoice.Date,
                TotalAmount = invoice.TotalAmount,
                IsPaid = invoice.IsPaid
            }).ToList();

            return Ok(invoices);
        }

        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetAllClients(bool onlyActives = false, bool lastlyActivates = false)
        {
            List<Client> clients = await Service.GetAllClients(onlyActives, lastlyActivates);

            return Ok(clients);
        }

        [HttpGet("GetClientsWithoutInvoices")]
        public async Task<IActionResult> GetClientsWithoutInvoices()
        {
            List<Client> clients = await Service.GetClientsWithoutInvoices();

            return Ok(clients);
        }

        [HttpGet("GetInvoicesFromClient")]
        public async Task<IActionResult> GetInvoicesFromClient(int id)
        {
            List<Invoice> invoices = await Service.GetInvoicesFromClient(id);

            return Ok(invoices);
        }

        [HttpGet("GetSumarizedInvoicesFromAClient")]
        public async Task<IActionResult> GetSumarizedInvoicesFromAClient(int id)
        {
            decimal amount = await Service.GetSumarizedInvoicesFromAClient(id);

            return Ok(amount);
        }

        [HttpGet("GetClientsWithUnpaidInvoices")]
        public async Task<IActionResult> GetUnpaidInvoices()
        {
            List<Client> clients = await Service.GetClientsWithUnpaidInvoices();

            return Ok(clients);
        }

        [HttpPost("PostNewClient")]
        public async Task<IActionResult> PostNewClient([FromBody] ClientDto newClientDto)
        {
            Client client = new()
            {
                Name = newClientDto.Name,
                BusinessName = newClientDto.BusinessName,
                CUIT = newClientDto.CUIT,
                IsActive = newClientDto.IsActive,
                ActivationDate = newClientDto.IsActive ? DateTime.UtcNow.AddHours(-3) : null,
                Email = newClientDto.Email,
                Phone = newClientDto.Phone,
                Address = newClientDto.Address,
                City = newClientDto.City,
                Province = newClientDto.Province
            };

            client = await Service.PostNewClient(client);

            ClientDto responseClientDto = new()
            {
                Id = client.Id,
                Name = client.Name,
                BusinessName = client.BusinessName,
                CUIT = client.CUIT,
                IsActive = client.IsActive,
                ActivationDate = client.ActivationDate,
                Email = client.Email,
                Phone = client.Phone,
                Address = client.Address,
                City = client.City,
                Province = client.Province
            };

            return Ok(responseClientDto);
        }

        [HttpPost("PostNewInvoice")]
        public async Task<IActionResult> PostNewInvoice([FromBody] InvoiceDto newInvoiceDto)
        {
            Client? client = await Service.GetClientById(newInvoiceDto.ClientId);

            if (client == null)
            {
                return NotFound("Client doesn't exist.");
            }

            Invoice invoice = new()
            {
                ClientId = newInvoiceDto.ClientId,
                Date = DateTime.UtcNow.AddHours(-3),
                TotalAmount = newInvoiceDto.TotalAmount,
                IsPaid = newInvoiceDto.IsPaid
            };

            invoice = await Service.PostNewInvoice(invoice);

            InvoiceDto responseInvoiceDto = new()
            {
                Id = invoice.Id,
                ClientId = invoice.ClientId,
                Date = invoice.Date,
                TotalAmount = invoice.TotalAmount,
                IsPaid = invoice.IsPaid
            };

            return Ok(responseInvoiceDto);
        }

        [HttpPut("PutModifyClient")]
        public async Task<IActionResult> PutModifyClient([FromBody] ClientModifyDto clientModifyDto)
        {
            Client? client = await Service.GetClientById(clientModifyDto.Id);

            if (client == null)
                return NotFound("Client doesn't exist.");

            client.Name = string.IsNullOrWhiteSpace(clientModifyDto.Name) ? client.Name : clientModifyDto.Name;
            client.BusinessName = string.IsNullOrWhiteSpace(clientModifyDto.BusinessName) ? client.BusinessName : clientModifyDto.BusinessName;
            client.CUIT = string.IsNullOrWhiteSpace(clientModifyDto.CUIT) ? client.CUIT : clientModifyDto.CUIT;
            client.Email = string.IsNullOrWhiteSpace(clientModifyDto.Email) ? client.Email : clientModifyDto.Email;
            client.Phone = string.IsNullOrWhiteSpace(clientModifyDto.Phone) ? client.Phone : clientModifyDto.Phone;
            client.Address = string.IsNullOrWhiteSpace(clientModifyDto.Address) ? client.Address : clientModifyDto.Address;
            client.City = string.IsNullOrWhiteSpace(clientModifyDto.City) ? client.City : clientModifyDto.City;
            client.Province = string.IsNullOrWhiteSpace(clientModifyDto.Province) ? client.Province : clientModifyDto.Province;
            client.IsActive = !clientModifyDto.IsActive.HasValue ? client.IsActive : clientModifyDto.IsActive.Value;

            if (client.IsActive && client.ActivationDate == null)
            {
                client.ActivationDate = DateTime.UtcNow.AddHours(-3);
            }

            client = await Service.PutModifyClient(client);

            ClientDto responseClientDto = new()
            {
                Id = client.Id,
                Name = client.Name,
                BusinessName = client.BusinessName,
                CUIT = client.CUIT,
                IsActive = client.IsActive,
                ActivationDate = client.ActivationDate,
                Email = client.Email,
                Phone = client.Phone,
                Address = client.Address,
                City = client.City,
                Province = client.Province
            };

            return Ok(responseClientDto);
        }

        [HttpPut("PutPayInvoice")]
        public async Task<IActionResult> PutPayInvoice(int id)
        {
            Invoice? invoice = await Service.GetInvoiceById(id);

            if (invoice == null)
                return NotFound("Invoice doesn't exist.");

            if (invoice.IsPaid)
            {
                return BadRequest("Invoice already paid.");
            }

            invoice.IsPaid = true;

            invoice = await Service.PutPayInvoice(invoice);

            InvoiceDto invoiceDto = new()
            {
                Id = invoice.Id,
                ClientId = invoice.ClientId,
                Date = invoice.Date,
                TotalAmount = invoice.TotalAmount,
                IsPaid = invoice.IsPaid
            };

            return Ok(invoiceDto);
        }
    }
}