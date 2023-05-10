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
        return this.parkingGarageContext.parkers.Count(w => w.ticket == ParkerEntity.TicketType.short_term);
    }

    /*
    * Funktion zum zählen der Menge aller sich aktuell im Parkhaus befindlichen Dauerparker
    */
    public int getCurrentSeasonParkers()
    {
        return this.parkingGarageContext.parkers.Count(w => w.ticket == ParkerEntity.TicketType.season);
    }

    /*
    * Funktion zum zählen der Menge aller sich aktuell im Parkhaus befindlichen Parker
    */
    public int getCurrentParkers()
    {
        return this.parkingGarageContext.parkers.Count();
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
    public void removeNormalParker(string _numberPlate = "")
    {
        this.removeParker(ParkerEntity.TicketType.short_term, _numberPlate);
    }

/*
    * Funktion zum entfernen eines Dauerparkers
    *
    * Nimmt ein Kennzeichen vom Typ String als Parameter, welcher Parker entfernt werden soll
    */
    public void removeSeasonParker(string _numberPlate = "")
    {
        this.removeParker(ParkerEntity.TicketType.season, _numberPlate);
    }

    private void removeParker(ParkerEntity.TicketType ticketType, string _numberPlate = "")
    {
        if (_numberPlate == "") {                
            int longestNormalParkerId = this.parkingGarageContext.parkers
                .Where(parkers => parkers.ticket == ticketType)
                .OrderBy(parkers => parkers.entryTime)
                .Select(parkers => parkers.Id)
                .FirstOrDefault();

            this.parkingGarageContext.Remove(
                this.parkingGarageContext.parkers.Single(parkers => parkers.Id == longestNormalParkerId)
            );
        } else {
            this.parkingGarageContext.Remove(
                this.parkingGarageContext.parkers.Single(parkers => parkers.numberPlate == _numberPlate)
            );
        }

        this.parkingGarageContext.SaveChanges();
    }
}