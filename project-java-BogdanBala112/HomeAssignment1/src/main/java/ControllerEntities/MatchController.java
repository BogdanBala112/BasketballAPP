package ControllerEntities;

import Entities.Match;
import Repository.MatchRepository;
import Repository.TicketRepository;
import service.MatchService;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import service.TicketService;

public class MatchController {
    private final MatchService matchService;
    private final ObservableList<Match> matchList;

    public MatchController() {
        MatchRepository repository = new MatchRepository();
        this.matchService = new MatchService(repository);
        this.matchList = FXCollections.observableArrayList(matchService.getMatches());
    }

    public ObservableList<Match> getMatchList() {
        return matchList;
    }

    public void addMatch(Match match) {
        matchService.addMatch(match);
        matchList.add(match);
    }

    public void deleteMatch(int id) {
        matchService.deleteMatch(id);
        matchList.removeIf(m -> m.getMatchId() == id);
    }

    public void updateMatch(int id, Match match) {
        matchService.updateMatch(id, match);
        matchList.clear();
        matchList.addAll(matchService.getMatches());
    }

    public boolean buyTickets(int matchId, int number){
        return matchService.sellTickets(matchId, number);
    }

    public void refreshMatches() {
        matchList.setAll(matchService.getMatches());
    }


}
