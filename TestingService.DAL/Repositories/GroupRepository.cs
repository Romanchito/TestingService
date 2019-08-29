using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestingService.DAL.EFContext;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.DAL.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private Context db;

        public GroupRepository(Context db)
        {
            this.db = db;
        }

        public void Create(Group item)
        {
            db.Groups.Add(item);
        }

        public void Delete(int id)
        {
            List<Quest> quests = db.Quests.Where(x => x.GroupId == id).ToList();
            List<User> users = db.Users.Where(x => x.GroupId == id).ToList();
            foreach (var item in quests)
            {
                item.GroupId = null;
            }

            foreach (var item in users)
            {
                item.GroupId = null;
            }

            Group group = db.Groups.Find(id);
            if (group != null)
            {
                db.Groups.Remove(group);
            }
        }

        public IEnumerable<Group> GetAll()
        {
            IEnumerable<Group> groups = db.Groups;
            foreach (var item in groups)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return groups;
        }

        public Group GetById(int? id)
        {
            Group group = db.Groups.Find(id);
            db.Entry(group).State = EntityState.Detached;
            return group;
        }

        public IEnumerable<Group> GetGroupsByQuestId(int questId)
        {
            List<Group> groups = new List<Group>();

            foreach (var group in db.Groups)
            {
                foreach (var quest in group.Quests)
                {
                    if (quest.Id.Equals(questId))
                    {
                        groups.Add(group);
                    }
                }
            }
            return groups;
        }

        public void Update(Group item)
        {
            Group group = db.Groups.Find(item.Id);
            group.Name = item.Name;
        }
    }
}