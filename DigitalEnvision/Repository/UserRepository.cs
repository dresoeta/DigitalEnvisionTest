using System.Threading.Tasks;
using DigitalEnvision.Interface;
using Microsoft.EntityFrameworkCore;
using DigitalEnvision.Data;
using DigitalEnvision.Models;

namespace DigitalEnvision.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BirthdayContext _context;
        public UserRepository(BirthdayContext context)
        {
            this._context = context;
        }

        public void Insert<T>(T entity)
        {
            _context.AddAsync(entity);
        }

        public void Update<T>(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
