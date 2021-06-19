using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspMvcNHibernate.Models;
using NHibernate;


namespace aspMvcNHibernate.Repositories
{
    public class FuncionarioRepository : IRepository<Funcionarios>
    {
        private ISession _session;
        public FuncionarioRepository(ISession session) => _session = session;

        public async Task Add(Funcionarios item)
        {
            ITransaction transaction = null;

            try
            {
                transaction = _session.BeginTransaction();
                await _session.SaveAsync(item);
                await transaction.CommitAsync();
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public IEnumerable<Funcionarios> FindAll() =>
            _session.Query<Funcionarios>().ToList();


        public async Task Remove(long id)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                var item = await _session.GetAsync<Funcionarios>(id);
                await _session.DeleteAsync(item);
                await transaction.CommitAsync();
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public async Task Update(Funcionarios item) {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                await _session.UpdateAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }
    }
}
