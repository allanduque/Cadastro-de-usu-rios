namespace Cadastro_Usuarios_Domain.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
