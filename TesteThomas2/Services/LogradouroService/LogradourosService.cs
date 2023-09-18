using Microsoft.EntityFrameworkCore;
using TesteThomas2.Data;
using TesteThomas2.Entities;
using TesteThomas2.Models;
using TesteThomas2.Services.LogradouroService.Interfaces;

namespace TesteThomas2.Service.LogradouroService
{
    public class LogradourosService : ILogradourosService
    {
        private readonly LojaDbContext _db;

        public LogradourosService(LojaDbContext db)
        {
            _db = db;
        }

        public async Task<Logradouro> BuscarPorId(int id)
        {
            return await _db.Logradouros.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Logradouro> CadastrarNovoEndereco(LogradouroDetailModel enderecoCadastro)
        {
            // Cadastrar na base de dados
            var novoEndereco = new Logradouro
            {
                Endereco = enderecoCadastro.Endereco,
                Cidade = enderecoCadastro.Cidade,
                Estado = enderecoCadastro.Estado,
                CEP = enderecoCadastro.CEP,
                ClienteId = enderecoCadastro.ClienteId,
                cliente = enderecoCadastro.Cliente
            };
            await _db.Logradouros.AddAsync(novoEndereco);
            await _db.SaveChangesAsync();

            return novoEndereco;
        }

        public async Task<Logradouro> EditarEndereco(LogradouroEditModel enderecoEditar)
        {
            // buscar cliente
            var endereco = await _db.Logradouros.FirstOrDefaultAsync(x => x.Id == enderecoEditar.Id);
            if (endereco == null)
                throw new Exception("O endereco informado não foi encontrado.");

            // atualizar
            endereco.Endereco = enderecoEditar.Endereco == endereco.Endereco ? endereco.Endereco : enderecoEditar.Endereco;
            endereco.Cidade = enderecoEditar.Cidade == endereco.Cidade ? endereco.Cidade : enderecoEditar.Cidade;
            endereco.Estado = enderecoEditar.Estado == endereco.Estado ? endereco.Estado : enderecoEditar.Estado;
            endereco.CEP = enderecoEditar.CEP == endereco.CEP ? endereco.CEP : enderecoEditar.CEP;
            endereco.ClienteId = enderecoEditar.ClienteId == endereco.ClienteId ? endereco.ClienteId : enderecoEditar.ClienteId;
            endereco.cliente = enderecoEditar.cliente == endereco.cliente ? endereco.cliente : enderecoEditar.cliente;
            
            // save changes
            _db.Logradouros.Update(endereco);
            await _db.SaveChangesAsync();
            return endereco;
        }

        public async Task<List<Logradouro>> ListarEnderecos()
        {
            return await _db.Logradouros.ToListAsync();
        }

        public async Task Excluir(Logradouro enderecoEditar, int Id)
        {
            // buscar produto
            var endereco = await _db.Logradouros.FirstOrDefaultAsync(x => x.Id == enderecoEditar.Id);
            if (endereco == null)
                throw new Exception("O endereço informado não foi encontrado.");

            // att
            endereco.Ativo = false;

            // save changes
            _db.Logradouros.Update(endereco);
            await _db.SaveChangesAsync();
        }
    }
}