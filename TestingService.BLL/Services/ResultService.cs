using AutoMapper;
using System;
using System.Collections.Generic;
using TestingService.BLL.DTO;
using TestingService.BLL.Interfaces;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.BLL.Services
{
    public class ResultService : IResultService
    {
        private IUnitOfWork Database { get; set; }

        public ResultService(IUnitOfWork database)
        {
            Database = database;
        }

        public void Create(ResultDTO item)
        {
            Database.Results.Create(Mapper.Map<ResultDTO, Result>(item));
            Database.Save();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResultDTO> FindResultsByGroupId(int id)
        {
            return Mapper.Map<IEnumerable<Result>, List<ResultDTO>>(Database.Results.FindResultsByGroupId(id));
        }

        public IEnumerable<ResultDTO> FindResultsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResultDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public ResultDTO GetById(int? id)
        {
            return Mapper.Map<Result, ResultDTO>(Database.Results.GetById(id));
        }

        public void Update(ResultDTO item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResultDTO> FindResultsByQuestId(int id)
        {
            return Mapper.Map<IEnumerable<Result>, List<ResultDTO>>(Database.Results.FindResultsByQuestId(id));
        }

        public IEnumerable<ResultDTO> MultyFindResultsByQuestId(int id, string group, string dateFrist, string dateSecond, string mark)
        {
            return Mapper.Map<IEnumerable<Result>, List<ResultDTO>>(Database.Results.MultyFindResultsByQuestId(id,group,dateFrist,dateSecond,mark));
        }
    }
}