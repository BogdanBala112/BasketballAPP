package ControllerEntities;

import Entities.Ticket;
import Repository.TicketRepository;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import service.TicketService;

import java.util.List;

public class TicketController {
    private final TicketService ticketService;
    private final ObservableList<Ticket> ticketList;

    public TicketController() {
        TicketRepository repository = new TicketRepository();
        this.ticketService = new TicketService(repository);
        this.ticketList = FXCollections.observableArrayList(ticketService.getTickets());
    }

    public ObservableList<Ticket> getTicketList() {
        return ticketList;
    }

    public void addTicket(Ticket ticket) {
        int size = ticketService.getTickets().size();
        ticketService.addTicket(ticket);
        if (size + 1 == ticketService.getTickets().size())
            ticketList.add(ticket);
    }

    public void deleteTicket(int id) {
        ticketService.deleteTicket(id);
        ticketList.removeIf(m -> m.getticketId() == id);
    }

    public void updateTicket(int id, Ticket ticket) {
        ticketService.updateTicket(id, ticket);
        ticketList.clear();
        ticketList.addAll(ticketService.getTickets());
    }

    public void loadTicketsFromDatabase() {
        List<Ticket> freshTickets = ticketService.getTickets();
        ticketList.setAll(freshTickets);
    }
}
