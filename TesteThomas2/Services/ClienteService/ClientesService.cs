using Microsoft.EntityFrameworkCore;
using TesteThomas2.Data;
using TesteThomas2.Entities;
using TesteThomas2.Models;
using TesteThomas2.Services.ClienteService.Interfaces;

namespace TesteThomas2.Service.ClienteService
{
    public class ClientesService : IClientesService
    {
        private readonly LojaDbContext _db;

        public ClientesService(LojaDbContext db)
        {
            _db = db;
        }

        public async Task<Cliente> BuscarPorId(int id)
        {
            return await _db.Clientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Cliente> CadastrarNovoCliente(ClienteCreateModel clienteCadastro)
        {
            // Validar campos
            if (string.IsNullOrEmpty(clienteCadastro.Nome) || string.IsNullOrEmpty(clienteCadastro.Email))
                throw new Exception("Por favor, preencha todos os campos.");

            // Cadastrar na base de dados
            var novoCliente = new Cliente
            {
                Nome = clienteCadastro.Nome,
                Email = clienteCadastro.Email ?? "Sem categoria",
                Logotipo = clienteCadastro.Logotipo ,
                Logradouros = (ICollection<Logradouro>)clienteCadastro.Logradouros.ToList()
            };
            await _db.Clientes.AddAsync(novoCliente);
            await _db.SaveChangesAsync();

            return novoCliente;
        }

        public async Task<Cliente> EditarCliente(ClienteEditModel clienteEditar)
        {
            // Validar campos
            if (string.IsNullOrEmpty(clienteEditar.Nome) || string.IsNullOrEmpty(clienteEditar.Email))
                throw new Exception("Por favor, preencha todos os campos.");

            // buscar cliente
            var cliente = await _db.Clientes.FirstOrDefaultAsync(x => x.Id == clienteEditar.Id);
            if (cliente == null)
                throw new Exception("O produto informado não foi encontrado.");
             
            // atualizar
            cliente.Nome = clienteEditar.Nome == cliente.Nome ? cliente.Nome : clienteEditar.Nome;
            cliente.Email = clienteEditar.Email == cliente.Email ? cliente.Email : clienteEditar.Email;
            cliente.Logotipo = clienteEditar.Logotipo == cliente.Logotipo ? cliente.Logotipo : clienteEditar.Logotipo;
            cliente.Logradouros = clienteEditar.Logradouros == cliente.Logradouros ? cliente.Logradouros : clienteEditar.Logradouros;

            // save changes
            _db.Clientes.Update(cliente);
            await _db.SaveChangesAsync();
            return cliente;
        }

        public async Task<List<Cliente>> ListarClientes()
        {
            return await _db.Clientes.ToListAsync();
        }

        public async Task Excluir(Cliente ClienteEditar, int Id)
        {
            // buscar produto
            var cliente = await _db.Clientes.FirstOrDefaultAsync(x => x.Id == ClienteEditar.Id);
            if (cliente == null)
                throw new Exception("O produto informado não foi encontrado.");

            // att
            cliente.Ativo = false;

            // save changes
            _db.Clientes.Update(cliente);
            await _db.SaveChangesAsync();
        }
    }
}