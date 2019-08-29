using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingService.DAL.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
