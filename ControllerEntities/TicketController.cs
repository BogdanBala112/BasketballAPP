using System.Collections.ObjectModel;
using System.Linq;
using Domain;
using projectC.Repository;
using projectC.Service;

namespace projectC.ControllerEntities
{
    public class TicketController
    {
        private readonly TicketService ticketService;
        public ObservableCollection<Ticket> TicketList { get; }

        public TicketController()
        {
            var repository = new TicketRepository();
            ticketService = new TicketService(repository);
            TicketList = new ObservableCollection<Ticket>(ticketService.GetTickets());
        }

        public void AddTicket(Ticket ticket)
        {
            ticketService.AddTicket(ticket);
            TicketList.Add(ticket);
        }

        public void DeleteTicket(int id)
        {
            ticketService.DeleteTicket(id);
            var ticketToRemove = TicketList.FirstOrDefault(t => t.ticketId == id);
            if (ticketToRemove != null) TicketList.Remove(ticketToRemove);
        }

        public void UpdateTicket(int id, Ticket ticket)
        {
            ticketService.UpdateTicket(id, ticket);
            RefreshTickets();
        }

        public void RefreshTickets()
        {
            TicketList.Clear();
            foreach (var t in ticketService.GetTickets())
                TicketList.Add(t);
        }
    }
}