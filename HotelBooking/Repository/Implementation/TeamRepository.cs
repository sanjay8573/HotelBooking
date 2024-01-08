using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class TeamRepository : Iteam
    {
        private readonly DeptContext _context;
        public TeamRepository(DeptContext context)
        {
            _context = context;
        }
        public TeamRepository()
        {
            _context = new DeptContext();

        }
        public int AddTeam(Team TeamEntity)
        {
            int result = -1;

            if (TeamEntity != null)
            {
                _context.Team.Add(TeamEntity);
                _context.SaveChanges();
                result = TeamEntity.Id;
            }
            return result;
        }

        public void DeleteTeam(int TeamId)
        {
            var TeamEntity = _context.Team.Find(TeamId);
            if (TeamEntity != null)
            {
                _context.Team.Remove(TeamEntity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Team> getAllTeams()
        {
            return _context.Team.ToArray();
        }

        public IEnumerable<Team> getTeamByBranchId(int BranchId)
        {
            return _context.Team.Where(b => b.BranchId == BranchId).ToArray();
        }

        public Team getTeamById(int TeamId)
        {
            return _context.Team.Where(b => b.Id == TeamId).SingleOrDefault();
        }

        public int UpdateTeam(Team TeamEntity)
        {
            int result = -1;

            if (TeamEntity != null)
            {
                //_context.Team.Update(TeamEntity);
                _context.SaveChanges();
                result = TeamEntity.Id;
            }
            return result;
        }
    }
}
