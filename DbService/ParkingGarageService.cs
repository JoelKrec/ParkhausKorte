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
    * Funktion zum hinzufügen eines Kurzparkers
    *
    * Gibt einen String mit dem Kennzeichen des Parkers zurück
    */
    public string addNormalParker(string _numberPlate = "")
    {
        return this.addParker(ParkerEntity.TicketType.short_term, _numberPlate);
    }

    /*
    * Funktion zum hinzufügen eines Dauerparkers
    *
    * Gibt einen String mit dem Kennzeichen des Parkers zurück
    */
    public string addSeasonParker(string _numberPlate = "")
    {
        return this.addParker(ParkerEntity.TicketType.season, _numberPlate);
    }

    private string addParker(ParkerEntity.TicketType ticketType, string _numberPlate = "")
    {
        ParkerEntity parker = new ParkerEntity()
        {
            numberPlate = _numberPlate == "" ? "te-st" + this.rnd.Next(10000) : _numberPlate,
            entryTime = DateTime.Now,
            ticket = ticketType
        };
        this.parkingGarageContext.Add(parker);
        this.parkingGarageContext.SaveChanges();

        return this.parkingGarageContext.parkers
            .Where(parkers => parkers.Id == parker.Id)
            .Select(parkers => parkers.numberPlate)
            .SingleOrDefault() ?? "";
    }

    /*
    * Funktion zum entfernen eines Kurzparkers
    *
    * Nimmt ein Kennzeichen vom Typ String als Parameter, welcher Parker entfernt werden soll
    */
    public int removeNormalParker(string _numberPlate = "")
    {
        return this.removeParker(ParkerEntity.TicketType.short_term, _numberPlate);
    }

    /*
    * Funktion zum entfernen eines Dauerparkers
    *
    * Nimmt ein Kennzeichen vom Typ String als Parameter, welcher Parker entfernt werden soll
    */
    public int removeSeasonParker(string _numberPlate = "")
    {
        return this.removeParker(ParkerEntity.TicketType.season, _numberPlate);
    }

    private int removeParker(ParkerEntity.TicketType ticketType, string _numberPlate = "")
    {
        int longestNormalParkerId = this.parkingGarageContext.Parkers
            .Where(parkers => parkers.ticket == ticketType)
            .OrderBy(parkers => parkers.entryTime)
            .Select(parkers => parkers.Id)
            .FirstOrDefault();

        ParkerEntity longestNormalParker = new ParkerEntity();
        if (_numberPlate == "") {    
            longestNormalParker = this.parkingGarageContext.Parkers.Single(parkers => parkers.Id == longestNormalParkerId);
        } else {
            longestNormalParker = this.parkingGarageContext.Parkers.Single(parkers => parkers.numberPlate == _numberPlate);
        }

        if (longestNormalParker != null) {
            longestNormalParker.exitTime = DateTime.Now;
            this.parkingGarageContext.SaveChanges();
        }

        return longestNormalParkerId;
    }

    /*
    * Funktion zum zuweisen eines Parkplatzes an einen Parker
    *
    * Nimmt eine Parker-Id vom Typ int als Parameter
    */
    public void assignParkingplace(int parkerId)
    {
        ParkingSpotEntity parkingspot = new ParkingSpotEntity()
        {
            parkerId = parkerId
        };
        this.parkingGarageContext.Add(parkingspot);
        this.parkingGarageContext.SaveChanges();
    }

    /*
    * Funktion zum entfernen einer Parkplatzzuweisung eines Parkers
    *
    * Nimmt eine Parker-Id vom Typ int als Parameter
    */
    public void unassignParkingplace(int parkerId)
    {
        this.parkingGarageContext.Remove(
            this.parkingGarageContext.ParkingSpots.Single(parkers => parkers.Id == parkerId)
        );
    }

    /*
    * Funktion zum ermitteln der Parkdauer eines Parkers
    *
    * Nimmt eine Parker-Id vom Typ int als Parameter
    */
    public int getParkingDurationMinutes(int parkerId)
    {
        TimeSpan? timeParked = this.parkingGarageContext.Parkers
            .Where(parkers => parkers.Id == parkerId)
            .Select(parkers => parkers.exitTime - parkers.entryTime)
            .Single();

        if (timeParked == null) {
            return 0;
        }

        return ((TimeSpan)timeParked).Minutes / 60;
    }
}