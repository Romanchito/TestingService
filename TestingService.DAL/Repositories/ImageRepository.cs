using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.EFContext;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.DAL.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private Context db;
        public ImageRepository(Context db)
        {
            this.db = db;
        }
        public void Create(Image item)
        {
            db.Images.Add(item);
        }

        public void Delete(int id)
        {
            foreach (var item in db.Questions)
            {
                if (item.ImageId.Equals(id))
                {
                    item.ImageId = null;
                }
            }
            Image image = db.Images.Find(id);
            if (image != null)
            {
                db.Images.Remove(image);
            }            
        }

        public IEnumerable<Image> GetAll()
        {
            IEnumerable<Image> images = db.Images;
            foreach (var item in images)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return images;
        }

        public Image GetById(int? id)
        {
            Image image = db.Images.Find(id);            
            return image;
        }

        public void Update(Image item)
        {
            Image img = db.Images.Find(item.Id);
            img.Name = item.Name;
            img.Data = item.Data;
        }
    }
}
