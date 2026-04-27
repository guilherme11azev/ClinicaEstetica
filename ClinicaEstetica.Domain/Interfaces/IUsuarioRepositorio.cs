using ClinicaEstetica.Domain.Entities;

namespace ClinicaEstetica.Domain.Interfaces;

public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
{
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task<bool> EmailJaCadastradoAsync(string email);
}