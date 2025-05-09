using System.Collections.Generic;
using Domain;
using projectC.Repository;

namespace projectC.Service
{
    public class TicketService
    {
        private readonly TicketRepository ticketRepository;

        public TicketService(TicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public List<Ticket> GetTickets()
        {
            return ticketRepository.GetAll();
        }

        public void AddTicket(Ticket ticket)
        {
            ticketRepository.Add(ticket);
        }

        public void DeleteTicket(int ticketId)
        {
            ticketRepository.Remove(ticketId);
        }

        public void UpdateTicket(int ticketId, Ticket ticket)
        {
            ticketRepository.Update(ticketId, ticket);
        }
    }
}