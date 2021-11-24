using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Services.Interfaces;

namespace TeamF_Api.Services.Implementations
{
    public class CaffService : ICaffService
    {
        private readonly UserManager<User> _userManager;
        private readonly CAFFShopDbContext _context;

        public CaffService(CAFFShopDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<CaffEntity> AddCaffAsync(CaffEntity caffEntity)
        {
            caffEntity.Id = 0;
            var result = _context.CaffEntity.Add(caffEntity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteCaffAsync(int id)
        {
            var caffentitiy = await _context.CaffEntity
                .Include(e=>e.Images)
                .ThenInclude(ei=>ei.Tags)
               .FirstOrDefaultAsync(e=>e.Id == id);

            foreach (var item in caffentitiy.Images)
            {
                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), item.Address));
            }            
            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), caffentitiy.Address));


            if (caffentitiy != null )
            {
                _context.Remove(caffentitiy);
                _context.SaveChanges();
            }
        }

        public async Task<CaffEntity> GetCaff(int id)
        {
            return await _context.CaffEntity
                .Include(e => e.Images)
                .ThenInclude(ei => ei.Tags)
                .FirstOrDefaultAsync(c=>c.Id == id);
        }

        public async Task<List<CaffEntity>> GetCaffs()
        {
            return  await _context.CaffEntity
                .Include(e => e.Images)
                .ThenInclude(ei => ei.Tags)
                .ToListAsync();
        }

        public async Task<Img> GetImg(int id)
        {
            return await _context.Img
                .Include(ei => ei.Tags)
                .FirstOrDefaultAsync(e=>e.Id == id);
        }
    }
}
