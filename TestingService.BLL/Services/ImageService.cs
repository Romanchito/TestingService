using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.DTO;
using TestingService.BLL.Interfaces;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.BLL.Services
{
    public class ImageService : IImageSrvice, IDisposable
    {
        private IUnitOfWork Database { get; set; }

        public ImageService(IUnitOfWork database)
        {
            Database = database;
        }
        public void Create(ImageDTO item)
        {
            Database.Images.Create(Mapper.Map<ImageDTO, Image>(item));
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Images.Delete(id);
            Database.Save();
        }

        public IEnumerable<ImageDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Image, ImageDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Image>, List<ImageDTO>>(Database.Images.GetAll());
        }

        public ImageDTO GetById(int? id)
        {
            if (id == null) return null;
            var img = Database.Images.GetById(id);
            if (img == null) throw new Exception("Задание не найдено");
            return new ImageDTO { Id = img.Id, Data = img.Data, Name = img.Name };
        }

        public void Update(ImageDTO item)
        {
            Image img = new Image { Id = item.Id, Name = item.Name, Data = item.Data };
            Database.Images.Update(img);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
