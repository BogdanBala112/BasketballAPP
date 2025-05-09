package service;

import Entities.Ticket;
import Repository.TicketRepository;

import java.util.List;

public class TicketService {
    public TicketRepository ticketRepository;

    public TicketService(TicketRepository ticketRepository) {
        this.ticketRepository = ticketRepository;
    }

    public List<Ticket> getTickets(){
        return ticketRepository.getAll();
    }

    public void addTicket(Ticket ticket){
        ticketRepository.add(ticket);
    }

    public void deleteTicket(Integer id){
        ticketRepository.remove(id);
    }

    public void updateTicket(int id, Ticket ticket){
        ticketRepository.update(id, ticket);
    }
}
