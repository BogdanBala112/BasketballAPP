using System.Collections.Generic;
using Domain;
using projectC.Repository;

namespace projectC.Service
{
    public class MatchService
    {
        private readonly MatchRepository matchRepository;

        public MatchService(MatchRepository repository)
        {
            matchRepository = repository;
        }

        public List<Match> GetMatches() => matchRepository.GetAll();

        public void AddMatch(Match match) => matchRepository.Add(match);

        public void DeleteMatch(int id) => matchRepository.Remove(id);

        public void UpdateMatch(int id, Match match) => matchRepository.Update(id, match);

        public bool SellTickets(int matchId, int count) => matchRepository.SellTicketBySeats(matchId, count);
    }
}