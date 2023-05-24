namespace ParkhausKorte.DbService;

using System;

public class ParkingGarageEntity
{
    public int maxParkingSpaces { get; set; }
    public int reservedSeasonParkingSpaces { get; set; }
    public int parkingSpaceBuffer { get; set; }
}

public class ParkerEntity
{
    public enum TicketType
    {
        short_term,
        season
    };

    public int Id { get; set; }
    public string numberPlate { get; set; }
    public DateTime entryTime { get; set; }
    public Nullable<DateTime> exitTime { get; set; }
    public TicketType ticket { get; set; }
}