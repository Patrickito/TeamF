using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL.Entity;

namespace TeamF_Api.Services.Interfaces
{
    public interface ICaffService
    {
        Task<CaffEntity> AddCaffAsync(CaffEntity caffEntity);
        Task DeleteCaffAsync(int id);
        Task<List<CaffEntity>> GetCaffs();
        Task<CaffEntity> GetCaff(int id);
        Task<Img> GetImg(int id);
    }
}
