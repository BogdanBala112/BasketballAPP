package Entities;

public class Match {
    private int matchId;
    private String matchName;
    private int nr_seats;
    private double ticketPrice;

    public Match(int matchId, String matchName, int nr_seats, double ticketPrice) {
        this.matchId = matchId;
        this.matchName = matchName;
        this.nr_seats = nr_seats;
        this.ticketPrice = ticketPrice;
    }

    public void setMatchId(int matchId) {
        this.matchId = matchId;
    }

    public int getMatchId() {
        return matchId;
    }

    public void setmatchName(String matchName){
        this.matchName = matchName;
    }

    public String getMatchName(){
        return matchName;
    }

    public void setNr_seats(int nr_seats){
        this.nr_seats = nr_seats;
    }

    public int getNr_seats(){
        return nr_seats;
    }

    public void setticketPrice(double ticketPrice){
        this.ticketPrice = ticketPrice;
    }

    public double getticketPrice(){
        return ticketPrice;
    }

    public String toString(){
        if (this.nr_seats > 0 )
            return "Match ID: " + this.matchId + " | Match: " + this.matchName + " | Number of Seats: " + this.nr_seats + " | Ticket Price: " + this.ticketPrice;
        else
            return "Match ID: " + this.matchId + " | Match: " + this.matchName + " !!! SOLD OUT !!! ";
    }
}
