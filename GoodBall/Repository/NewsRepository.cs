using DataCollection.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class NewsRepository : BaseRepository<News, NewsRepository>
    {
        private NewsRepository() { }
    }
}
