package Entities;

public class Ticket {
    private int ticketId;
    private int nr_seats;
    private int matchId_fk;

    public Ticket(int ticketId,int nr_seats, int matchId_fk) {
        this.ticketId = ticketId;
        this.nr_seats = nr_seats;
        this.matchId_fk = matchId_fk;
    }

    public void setticketId(int ticketId){
        this.ticketId = ticketId;
    }

    public int getticketId(){
        return this.ticketId;
    }

    public void setnr_seats(int nr_seats){
        this.nr_seats = nr_seats;
    }

    public int getnr_seats(){
        return this.nr_seats;
    }

    public void setmatchId(int matchId_fk){
        this.matchId_fk = matchId_fk;
    }

    public int getmatchId(){
        return this.matchId_fk;
    }

    public String toString(){
        return "Ticket ID: " + this.ticketId + " Seats: " + this.nr_seats + " Match ID: " + this.matchId_fk;
    }

}
