using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SCG.Core.Database;
using SCG.Core.Database.Entities;
using SCG.Core.Interfaces;
using SCG.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.Core.Services
{
    public class UploadService: IServiceMethod<UploadModel>
    {
        private readonly SCGDb _db;
        private readonly IMapper _mapper;

        public UploadService(SCGDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UploadModel> Add(UploadModel model)
        {
            var exists = await Requery(x => x.UserId == model.UserId);
            // Save file and get source path
            model.FilePath = await SaveFile(model, exists?.FilePath);

            if (exists != null)
            {
                model.Id = exists.Id;
                return await Update(model);
            }

            var entity = _mapper.Map<UploadModel, UploadEntity>(model);

            await _db.Uploads.AddAsync(entity);
            await _db.SaveChangesAsync();

            return await Requery(x => x.Id == entity.Id);
        }

        public async Task<UploadModel> Delete(UploadModel model)
        {
            var item = await _db.Uploads.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (item == null)
                throw new Exception("Registro no existente");

            _db.Uploads.Remove(item);
            await _db.SaveChangesAsync();

            return model;
        }

        public async Task<IList<UploadModel>> Select()
        {
            var query = _db.Uploads.ProjectTo<UploadModel>(_mapper.ConfigurationProvider);
            return await query.ToListAsync();
        }

        public async Task<UploadModel> Requery(Func<UploadModel, bool> predicate)
        {
            return (await Select()).Where(predicate).FirstOrDefault();
        }

        public async Task<UploadModel> Update(UploadModel model)
        {
            var item = await _db.Uploads.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (item == null)
                throw new Exception("Registro no encontrado");

            _mapper.Map(model, item);

            await _db.SaveChangesAsync();

            return await Requery(m => m.Id == item.Id);
        }

        private async Task<string> SaveFile(UploadModel model, string existingPath)
        {
            
            if (!string.IsNullOrEmpty(existingPath)) {
                File.Delete(existingPath);
            }

            // Save file with UserId as file's name
            // Frontend only accepts .jpg
            string date = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff");
            string dbPath = Path.Combine("Resources", "Images", $"{model.UserId}-{date}.jpg");
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), dbPath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            return dbPath;
        }

    }
}
