using ControleEstoque.API.Data;
using ControleEstoque.API.DTOs;
using ControleEstoque.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleEstoque.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<UsuarioDto?> BuscarUsuarioPorEmail(string email)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
                return null;
            return MapearParaDto(usuario);
        }

        public async Task<UsuarioDto?> BuscarUsuarioPorId(int id)
        {
           var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
                return null;
            return  MapearParaDto(usuario);
            
        }

        public async Task<IEnumerable<UsuarioDto>> ListarTodosUsuariosAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios.Select(MapearParaDto);
        }

        public async Task<UsuarioDto> RegistrarCaixa(CriarCaixaDto dto)
        {
            var caixa = new Caixa
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = dto.Senha, // Em produção, aplicar hash na senha
                Perfil = PerfilUsuario.Caixa,
                Turno = dto.Turno
            };
            _context.Caixas.Add(caixa);
            await _context.SaveChangesAsync();
            return MapearParaDto(caixa);
        }

        public async Task<UsuarioDto> RegistrarCliente(CriarClienteDto dto)
        {
            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = dto.Senha, // Em produção, aplicar hash na senha
                Perfil = PerfilUsuario.Cliente,
                CPF = dto.CPF
            };
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return MapearParaDto(cliente);
        }

        public async Task<UsuarioDto> RegistrarGerente(CriarGerenteDto dto)
        {
            var gerente = new Gerente
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = dto.Senha, // Em produção, aplicar hash na senha
                Perfil = PerfilUsuario.Gerente,
                Setor = dto.Setor
            };
            _context.Gerentes.Add(gerente);
           await _context.SaveChangesAsync();
            return MapearParaDto(gerente);
        }

        private UsuarioDto MapearParaDto(Usuario usuario)
        {
            var dto = new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil,
                };
            if (usuario is Cliente cliente)
            {              
                dto.CPF = cliente.CPF;
                }
            if (usuario is Caixa caixa)
            {
                dto.Turno = caixa.Turno;
            }
            if (usuario is Gerente gerente)
            {
                dto.Setor = gerente.Setor;
            }
            return dto;
        }
    }
}
