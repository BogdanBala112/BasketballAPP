namespace Domain{
    public class Match{
        public int matchId{get;set;}
        public string matchName{get;set;}
        public int nr_seats{get;set;}
        public double ticketPrice{get;set;}

        public Match(int matchId, string matchName, int nr_seats, double ticketPrice){
            this.matchId = matchId;
            this.matchName = matchName;
            this.nr_seats = nr_seats;
            this.ticketPrice = ticketPrice;
        }
        
        public override string ToString(){
            return "MatchId: " + matchId + " MatchName: " + matchName + " Nr_seats: " + nr_seats + " TicketPrice: " + ticketPrice;
        }

    }
}