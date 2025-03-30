using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Interfaces.Repositories;
using Warranty.Core.Models;

namespace Warranty.Data.Repositories
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext) { }
        public async Task<List<UserModel>> GetFull()
        {
            return await _dbSet.Include(u=>u.Role).ToListAsync();
        }
        public async Task<UserModel> GetUserByEmail(string email)
        {


            return await _dbSet.Include(u => u.Role)  // כולל את ה-Role של המשתמש
                         .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<UserModel?> UpdateUserNameAsync(int id, UserModel user)
        {
            var u = await _dbSet.FindAsync(id);
            if (user == null)
            {
                return null; // המשתמש לא נמצא
            }

            u.NameUser = user.NameUser; // עדכון השם בלבד
            u.IsAccessEmails = user.IsAccessEmails;
            return u;
        }

    }
}
