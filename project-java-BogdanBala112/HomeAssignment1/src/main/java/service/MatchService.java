package service;

import Entities.Match;
import Repository.MatchRepository;

import java.util.List;

public class MatchService {
    public MatchRepository matchRepository;

    public MatchService(MatchRepository matchRepository){
        this.matchRepository = matchRepository;
    }

    public List<Match> getMatches(){
        return matchRepository.getAll();
    }

    public void addMatch(Match match){
        matchRepository.add(match);
    }

    public void deleteMatch(int id){
        matchRepository.remove(id);
    }

    public void updateMatch(int id, Match match){
        matchRepository.update(id, match);
    }

    public boolean sellTickets(int matchId, int number){
        return matchRepository.sellTicketBySeats(matchId, number);
    }

}
