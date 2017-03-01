using DataCollection.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MatchRepository : BaseRepository<Match, MatchRepository>
    {
        private MatchRepository()
        {
        }
    }
}
