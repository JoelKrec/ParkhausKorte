namespace ParkhausKorte.DbService;

using ParkhausKorte.DbService;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class ParkingGarageService
{
    private ParkingGarageContext parkingGarageContext;
    private Random rnd = new Random();
    public ParkingGarageService(ParkingGarageContext _parkingGarageContext)
    {
        this.parkingGarageContext = _parkingGarageContext;
    }

    /*
    * Funktion zum auslesen der Menge insgesamt vorhandener Parkplätze (ohne Inbetrachnahme des Puffers)
    */
    public int getMaxParkingSpaces()
    {
        return this.parkingGarageContext.parkinggarage.Select(u => u.maxParkingSpaces).SingleOrDefault();
    }

    /*
    * Funktion zum auslesen der festgelegten Menge von als Dauerparkplatz reservierten Parkplätzen
    */
    public int getReservedSeasonParkingSpaces()
    {
        return this.parkingGarageContext.parkinggarage.Select(u => u.reservedSeasonParkingSpaces).SingleOrDefault();
    }

    /*
    * Funktion zum auslesen der festgelegten Menge von als Puffer reservierten Parkplätzen
    */
    public int getParkingSpaceBuffer()
    {
        return this.parkingGarageContext.parkinggarage.Select(u => u.parkingSpaceBuffer).SingleOrDefault();
    }

    /*
    * Funktion zum zählen der Menge aller sich aktuell im Parkhaus befindlichen Kurzparker
    */
    public int getCurrentNormalParkers()
    {
        return this.parkingGarageContext.Parkers
            .Where(parkers => parkers.exitTime == null)
            .Count(w => w.ticket == ParkerEntity.TicketType.short_term);
    }

    /*
    * Funktion zum zählen der Menge aller sich aktuell im Parkhaus befindlichen Dauerparker
    */
    public int getCurrentSeasonParkers()
    {
        return this.parkingGarageContext.Parkers
            .Where(parkers => parkers.exitTime == null)
            .Count(parkers => parkers.ticket == ParkerEntity.TicketType.season);
    }

    /*
    * Funktion zum zählen der Menge aller sich aktuell im Parkhaus befindlichen Parker
    */
    public int getCurrentParkers()
    {
        return this.parkingGarageContext.Parkers
            .Where(parkers => parkers.exitTime == null)
            .Count();
    }

    /*
    * Funktion zum holen eines zufälligen Parkers
    */
    public int getRandomParker()
    {
        return this.parkingGarageContext.Parkers
            .Skip(this.rnd.Next(this.getCurrentParkers()))
            .Select(parkers => parkers.Id)
            .FirstOrDefault();
    }

    /*
    * Funktion zum holen eines zufälligen Parkers des angegebenen Typs
    */
    public int getRandomParkerOfType(ParkerEntity.TicketType ticketType)
    {
        return this.parkingGarageContext.Parkers
            .Where(parkers => parkers.ticket == ticketType)
            .Skip(this.rnd.Next(this.getCurrentParkers()))
            .Select(parkers => parkers.Id)
            .FirstOrDefault();
    }

    /*
    * Funktion zum hinzufügen eines Kurzparkers, ggf. mit angabe des Nummernschildes
    *
    * Gibt einen String mit dem Kennzeichen des Parkers zurück
    */
    public int addNormalParker(string _numberPlate = "")
    {
        return this.addParker(ParkerEntity.TicketType.short_term, _numberPlate);
    }

    /*
    * Funktion zum hinzufügen eines Dauerparkers, ggf. mit angabe des Nummernschildes
    *
    * Gibt die Datensatz-Id des Parkers zurück
    */
    public int addSeasonParker(string _numberPlate = "")
    {
        return this.addParker(ParkerEntity.TicketType.season, _numberPlate);
    }

    /*
    * Funktion zum hinzufügen eines Parkers des angegebenen Typs, ggf. mit angabe des Nummernschildes
    */
    private int addParker(ParkerEntity.TicketType ticketType, string _numberPlate = "")
    {
        ParkerEntity parker = new ParkerEntity()
        {
            numberPlate = _numberPlate == "" ? "te-st" + this.rnd.Next(10000) : _numberPlate,
            entryTime = DateTime.Now,
            ticket = ticketType
        };
        this.parkingGarageContext.Add(parker);
        this.parkingGarageContext.SaveChanges();

        this.assignParkingSpot(parker.Id);

        return parker.Id;
    }

    /*
    * Funktion zum entfernen eines Kurzparkers
    */
    public int removeNormalParker()
    {
        return this.removeParkerOfType(ParkerEntity.TicketType.short_term);
    }

    /*
    * Funktion zum entfernen eines Dauerparkers
    */
    public int removeSeasonParker()
    {
        return this.removeParkerOfType(ParkerEntity.TicketType.season);
    }

    /*
    * Funktion zum entfernen eines Parkers des angegebenen Typs
    */
    private int removeParkerOfType(ParkerEntity.TicketType ticketType)
    {
        int longestNormalParkerId = this.parkingGarageContext.Parkers
            .Where(parkers => parkers.ticket == ticketType)
            .Where(parkers => parkers.exitTime == null)
            .OrderBy(parkers => parkers.entryTime)
            .Select(parkers => parkers.Id)
            .FirstOrDefault();

        ParkerEntity longestNormalParker = new ParkerEntity();
        longestNormalParker = this.parkingGarageContext.Parkers.Single(parkers => parkers.Id == longestNormalParkerId);

        if (longestNormalParker != null) {
            // Kurzparker werden gelöscht, Dauerparker nicht
            if (ticketType == ParkerEntity.TicketType.short_term) {
                this.removeParker(longestNormalParkerId);
            } else if (ticketType == ParkerEntity.TicketType.season) {
                longestNormalParker.exitTime = DateTime.Now;
                this.parkingGarageContext.SaveChanges();
            }
        }

        this.unassignParkingSpot(longestNormalParkerId);

        return longestNormalParkerId;
    }

    public void removeParker(int parkerId)
    {
        this.parkingGarageContext.Remove(
            this.parkingGarageContext.Parkers.Single(parkers => parkers.Id == parkerId)
        );
        this.parkingGarageContext.SaveChanges();
    }

    /*
    * Funktion zum zuweisen eines Parkplatzes an einen Parker
    *
    * Nimmt eine Parker-Id vom Typ int als Parameter
    */
    public void assignParkingSpot(int parkerId)
    {
        ParkingSpotEntity parkingspot = this.parkingGarageContext.ParkingSpots
            .Where(parkingspots => parkingspots.parkerId == null)
            .First();

        parkingspot.parkerId = parkerId;
        this.parkingGarageContext.SaveChanges();
    }

    /*
    * Funktion zum entfernen einer Parkplatzzuweisung eines Parkers
    *
    * Nimmt eine Parker-Id vom Typ int als Parameter
    */
    public void unassignParkingSpot(int parkerId)
    {
        ParkingSpotEntity parkingspot = this.parkingGarageContext.ParkingSpots
            .Where(parkingspots => parkingspots.parkerId == parkerId)
            .Single();

        parkingspot.parkerId = null;
        this.parkingGarageContext.SaveChanges();
    }

    /*
    * Funktion zum auslesen der Parkplatz-Id eines Parkers
    *
    * Nimmt eine Parker-Id vom Typ int als Parameter
    */
    public int getParkingSpot(int parkerId)
    {
        return this.parkingGarageContext.ParkingSpots
            .Where(parkingspots => parkingspots.parkerId == parkerId)
            .Select(parkingspots => parkingspots.Id)
            .Single();
    }

    /*
    * Funktion zum auslesen aller Parkplätze
    *
    */
    public List<JoinedResultset> getParkingSpots()
    {
        return this.parkingGarageContext.ParkingSpots
            .GroupJoin(
                this.parkingGarageContext.Parkers,
                parkingSpots => parkingSpots.parkerId,
                parkers => parkers.Id,
                (parkingSpots, parkers) => new {
                    parkingSpotId = parkingSpots.Id,
                    Parkers = parkers
                })
            .SelectMany(
                parkers => parkers.Parkers.DefaultIfEmpty(),
                (parkingSpots, parkers) => new JoinedResultset {
                    parkingSpotId = parkingSpots.parkingSpotId,
                    numberPlate = parkers.numberPlate
                })
            .ToList();
    }

    /*
    * Hilfsfunktion der Simulation zum generieren von Parkplätzen basierend auf der festgelegten Anzahl
    *
    * Nimmt eine Parker-Id vom Typ int als Parameter
    */
    public void generateParkingSpots()
    {
        this.parkingGarageContext.ParkingSpots.RemoveRange(this.parkingGarageContext.ParkingSpots);

        for (int i = this.getMaxParkingSpaces(); i > 0; i--) {
            ParkingSpotEntity parkingspot = new ParkingSpotEntity()
            {
                Id = i,
                parkerId = null
            };
            this.parkingGarageContext.Add(parkingspot);
        }

        this.parkingGarageContext.SaveChanges();
    }

    /*
    * Funktion zum ermitteln der Parkdauer eines Parkers
    *
    * Nimmt eine Parker-Id vom Typ int als Parameter
    */
    public int getParkingDurationMinutes(int parkerId)
    {
        DateTime? entryTime = this.parkingGarageContext.Parkers
            .Where(parkers => parkers.Id == parkerId)
            .Select(parkers => parkers.entryTime)
            .SingleOrDefault();
            
        DateTime? exitTime = this.parkingGarageContext.Parkers
            .Where(parkers => parkers.Id == parkerId)
            .Select(parkers => parkers.exitTime)
            .SingleOrDefault();

        if (exitTime == null) {
            exitTime = DateTime.Now;
        }

        return ((exitTime ?? DateTime.Now) - (entryTime ?? DateTime.Now)).Minutes;
    }
}