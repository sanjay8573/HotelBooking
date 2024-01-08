using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    
    public class TeamController : ApiController
    {
        private readonly Iteam _team;
        public TeamController(Iteam tm)
        {
            _team = tm;
        }

        public TeamController()
        {
            _team = new TeamRepository();
        }

        [HttpGet]
        [Route("api/Team/GetTeams/{branchid}")]
        public IEnumerable<Team> GetTeams(int branchid)
        {
            return _team.getTeamByBranchId(branchid);
        }
    }
}
