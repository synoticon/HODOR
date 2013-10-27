using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HODOR.src.Globals;

namespace HODOR.src.DAO
{
    public class SupportTicketDAO
    {
        public static SupportTicket createAndGetSupportTicket(Benutzer submitter, Programm regardingProgram, Int32 releaseNummer, Int32 subreleaseNummer, Int32 buildNummer)
        {
            SupportTicket ticket = new SupportTicket();
            ticket.Benutzer = submitter;
            ticket.Programm = regardingProgram;
            ticket.EinreichungsDatum = DateTime.Now;
            ticket.ReleaseNummer = releaseNummer;
            ticket.SubreleaseNummer = subreleaseNummer;
            ticket.BuildNummer = buildNummer;
            ticket.IsOpen = true;

            HodorGlobals.getHodorContext().SupportTickets.AddObject(ticket);
            HodorGlobals.save();

            return ticket;
        }

        public static SupportTicket createAndGetSupportTicket(Benutzer submitter, String caseDescription, Programm regardingProgram, Int32 releaseNummer, Int32 subreleaseNummer, Int32 buildNummer)
        {
            SupportTicket ticket = createAndGetSupportTicket(submitter, regardingProgram, releaseNummer, subreleaseNummer, buildNummer);
            ticket.Fallbeschreibung = caseDescription;
            HodorGlobals.save();

            return ticket;
        }

        public static void deleteSupportTicket(SupportTicket ticket)
        {
            HodorGlobals.getHodorContext().SupportTickets.DeleteObject(ticket);
            HodorGlobals.save();
        }

        public static SupportTicket getSupportTicketByIdOrNull(Int32 ticketId)
        {
            List<SupportTicket> ticketList = HodorGlobals.getHodorContext().SupportTickets.Where(t => t.TicketID == ticketId).ToList<SupportTicket>();

            if (ticketList.Count == 0)
            {
                //no ticket with that ID
                return null;
            }
            if (ticketList.Count >= 2)
            {
                throw new Exception("Data inconsistency detected! More than one SupportTicket with TicketID: " + ticketId);
            }

            //safe to assume it's only one ticket as expected
            return ticketList[0];
        }

        public static List<SupportTicket> getAllSupportTickets()
        {
            return HodorGlobals.getHodorContext().SupportTickets.OrderByDescending(t => t.EinreichungsDatum).ToList<SupportTicket>();
        }

        public static List<SupportTicket> getAllOpenSupportTickets()
        {
            List<SupportTicket> resultList = HodorGlobals.getHodorContext().SupportTickets.Where(t => t.IsOpen == true).OrderByDescending(t => t.EinreichungsDatum).ToList<SupportTicket>();

            return resultList;
        }

        public static List<SupportTicket> getAllOpenSupportTicketsForUser(Benutzer user)
        {
            return HodorGlobals.getHodorContext().SupportTickets.Where(t => t.IsOpen == true && t.ErstellerID == user.BenutzerID).OrderByDescending(t => t.EinreichungsDatum).ToList<SupportTicket>();
        }

        public static List<SupportTicket> getAllClosedSupportTickets()
        {
            List<SupportTicket> resultList = HodorGlobals.getHodorContext().SupportTickets.Where(t => t.IsOpen == false).OrderByDescending(t => t.EinreichungsDatum).ToList<SupportTicket>();

            return resultList;
        }

        public static List<SupportTicket> getAllClosedSupportTicketsForUser(Benutzer user)
        {
            return HodorGlobals.getHodorContext().SupportTickets.Where(t => t.IsOpen == false && t.ErstellerID == user.BenutzerID).OrderByDescending(t => t.EinreichungsDatum).ToList<SupportTicket>();
        }
    }
}