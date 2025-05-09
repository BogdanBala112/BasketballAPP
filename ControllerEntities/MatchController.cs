using System;
using System.Collections.Generic;
using projectC.Repository;
using Domain;
using System.Collections.ObjectModel;
using System.Linq;
using projectC.Service;

namespace projectC.ControllerEntities
{
    public class MatchController
    {
        private readonly MatchService matchService;
        private static ObservableCollection<Match> matchList;

        static MatchController()
        {
            matchList = new ObservableCollection<Match>(new MatchService(new MatchRepository()).GetMatches());
        }
        
        public MatchController()
        {
            var repository = new MatchRepository();
            this.matchService = new MatchService(repository);
        }
        

        public ObservableCollection<Match> GetMatchList()
        {
            return matchList;
        }

        public void AddMatch(Match match)
        {
            matchService.AddMatch(match);
            matchList.Add(match);
        }

        public void DeleteMatch(int id)
        {
            matchService.DeleteMatch(id);
            var matchToRemove = matchList.FirstOrDefault(m => m.matchId == id);
            if (matchToRemove != null)
            {
                matchList.Remove(matchToRemove);
            }
        }

        public void UpdateMatch(int id, Match match)
        {
            matchService.UpdateMatch(id, match);
  
            matchList.Clear();
            foreach (var updatedMatch in matchService.GetMatches())
            {
                matchList.Add(updatedMatch);
            }
        }
        public bool BuyTickets(int matchId, int number)
        {
            return matchService.SellTickets(matchId, number);
        }
        
        public void RefreshMatches()
        {
            matchList.Clear();
            foreach (var match in matchService.GetMatches())
            {
                matchList.Add(match);
            }
        }
    }
}
