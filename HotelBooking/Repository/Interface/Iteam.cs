using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface Iteam
    {
        IEnumerable<Team> getAllTeams();
        Team getTeamById(int TeamId);
        IEnumerable<Team> getTeamByBranchId(int BranchId);
        int AddTeam(Team TeamEntity);
        int UpdateTeam(Team TeamEntity);
        void DeleteTeam(int TeamId);
    }
}
