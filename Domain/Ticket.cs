namespace Domain{
    public class Ticket{
        public int ticketId{get;set;}
        public double ticketPrice{get;set;}
        public int nr_seats{get;set;}
        public int matchId{get;set;}

        public Ticket(int ticketId,double ticketPrice, int nr_seats, int matchId){
            this.ticketId = ticketId;
            this.ticketPrice = ticketPrice;
            this.nr_seats = nr_seats;
            this.matchId = matchId;
        }

        public override string ToString(){
            return "TicketId: " + ticketId + " TicketPrice: " + ticketPrice + " Nr_seats: " + nr_seats + " MatchId: " + matchId;
        }

    }
}