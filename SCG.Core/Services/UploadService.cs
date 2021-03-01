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
using WebApi.Utilities.Http;
using WebApi.Utilities.IQueryableExtensions;

namespace SCG.Core.Services
{
    public class UploadService: IServiceMethod<UploadModel>
    {
        private readonly SCGDb _db;

        public UploadService(SCGDb db)
        {
            _db = db;
        }

        public UploadModel Add(UploadModel model)
        {
            var dbPath = SaveFile(model);

            var exists = Requery(x => x.UserId == model.UserId);

            if (exists != null)
            {
                return Update(new UploadModel {
                    Id = exists.Id,
                    FileName = model.FileName,
                    FilePath = dbPath,
                    UserId = model.UserId
                });
            }

            var entity = new UploadEntity
            {
                FileName = model.FileName,
                FilePath = dbPath,
                UserId = model.UserId
            };

            _db.Uploads.Add(entity);
            _db.SaveChanges();

            return Requery(x => x.Id == entity.Id);
        }

        public UploadModel Delete(UploadModel model)
        {
            var item = _db.Uploads.FirstOrDefault(x => x.Id == model.Id);

            if (item == null)
                throw new Exception("Registro no existente");

            _db.Uploads.Remove(item);

            _db.SaveChanges();

            return model;
        }

        public IQueryable<UploadModel> Select()
        {
            var query = from x in _db.Uploads
                        select new UploadModel
                        {
                            Id = x.Id,
                            FileName = x.FileName,
                            FilePath = x.FilePath,
                            UserId = x.UserId
                        };

            return query;
        }

        public List<UploadModel> GetPage(APIRequest request)
        {
            return Select()
                    .AddFilter(request.Filters)
                    .AddSortBy(request.Sorts)
                    .AddPagination(request.Pagination);
        }

        public UploadModel Requery(Func<UploadModel, bool> predicate)
        {
            return Select().Where(predicate).FirstOrDefault();
        }

        public UploadModel Update(UploadModel model)
        {
            var item = _db.Uploads.FirstOrDefault(x => x.Id == model.Id);

            if (item == null)
                throw new Exception("Registro no encontrado");

            item.FileName = model.FileName;
            item.FilePath = model.FilePath;
            item.UserId = model.UserId;

            _db.SaveChanges();

            return Requery(m => m.Id == item.Id);
        }

        private string SaveFile(UploadModel model)
        {
            var ext = model.FileName.Split('.').LastOrDefault();
            var dbPath = Path.Combine("Resources", "Images", $"{model.UserId}.{ext}");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), dbPath);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.File.CopyTo(stream);
            }

            return dbPath;
        }

    }
}
