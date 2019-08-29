﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.DTO;

namespace TestingService.BLL.Interfaces
{
    public interface IGroupService : IService<GroupDTO>
    {
        IEnumerable<GroupDTO> GetGroupsByQuestId(int questId);
    }
}
