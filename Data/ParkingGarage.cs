namespace ParkhausKorte.Data;

using ParkhausKorte.DbService;

public class ParkingGarage
{
    public ParkingGarageService parkingGarageService;
    public readonly int availableParkingSpaces;
    public readonly int reservedSeasonParkingSpaces;
    public readonly int parkingSpaceBuffer;
    public readonly int totalParkingSpaces;
    public readonly float parkingCostPerMinute = 1; //€

    public ParkingGarage(ParkingGarageContext _parkingGarageContext)
    {
        this.parkingGarageService = new ParkingGarageService(_parkingGarageContext);
        this.totalParkingSpaces = this.parkingGarageService.getMaxParkingSpaces();
        this.parkingSpaceBuffer = this.parkingGarageService.getParkingSpaceBuffer();
        this.availableParkingSpaces = this.totalParkingSpaces - this.parkingSpaceBuffer;
        this.reservedSeasonParkingSpaces = this.parkingGarageService.getReservedSeasonParkingSpaces();
    }

    public int GetParkersOfType(ParkerType parkerType)
    {
        int parkersToReturn = 0;

        switch (parkerType)
        {
            case ParkerType.normal:
                parkersToReturn = this.parkingGarageService.getCurrentNormalParkers();
                break;
            case ParkerType.season:
                parkersToReturn = this.parkingGarageService.getCurrentSeasonParkers();
                break;
            case ParkerType.all:
                parkersToReturn = this.parkingGarageService.getCurrentParkers();
                break;
            default:
                parkersToReturn = -1;
                break;
        }

        return parkersToReturn;
    }

    public bool AddParkerOfType(ParkerType parkerType)
    {
        bool addingSuccessful = false;
        int freeSpaces = 0;
        int currentParkers = this.parkingGarageService.getCurrentParkers();

        switch (parkerType)
        {
            case ParkerType.normal:
                freeSpaces = this.GetFreeSpaces();

                if(freeSpaces > 0)
                {
                    this.parkingGarageService.addNormalParker();
                    addingSuccessful = true;
                }
                break;
            case ParkerType.season:
                freeSpaces = this.availableParkingSpaces - currentParkers;
                if(freeSpaces > 0)
                {
                    this.parkingGarageService.addSeasonParker();
                    addingSuccessful = true;
                }
                break;
            case ParkerType.all:
            default:
                break;
        }

        return addingSuccessful;
    }

    public int RemoveRandomParkerOfType(ParkerType parkerType)
    {
        switch(parkerType)
        {
            case ParkerType.normal:
                return this.parkingGarageService.removeNormalParker();
            case ParkerType.season:
                return this.parkingGarageService.removeSeasonParker();
            case ParkerType.all:
            default:
                return 0;
        }
    }

     public void RemoveParker(int parkerId)
    {
        this.parkingGarageService.removeParker(parkerId);
    }

    public int GetFreeSpaces()
    {
        int freeSpaces = 0;
        int currentSeasonParkers = this.parkingGarageService.getCurrentSeasonParkers();
        int currentNormalParkers = this.parkingGarageService.getCurrentNormalParkers();
        int currentParkers = this.parkingGarageService.getCurrentParkers();

        if(currentSeasonParkers > this.reservedSeasonParkingSpaces)
        {
            freeSpaces = this.availableParkingSpaces - currentParkers;
        }
        else
        {
            freeSpaces = this.availableParkingSpaces - 40 - currentNormalParkers;
        }
        return freeSpaces;
    }

    public int GetCurrentSeasonParkers()
    {
        return this.parkingGarageService.getCurrentSeasonParkers();
    }

    public int GetCurrentNormalParkers()
    {
        return this.parkingGarageService.getCurrentNormalParkers();
    }

    public int GetCurrentParkers()
    {
        return this.parkingGarageService.getCurrentParkers();
    }

    public int GetRandomParkerOfType(ParkerType parkerType)
    {
        switch(parkerType)
        {
            case ParkerType.normal:
                return this.parkingGarageService.getRandomParkerOfType(ParkerEntity.TicketType.short_term);
            case ParkerType.season:
                return this.parkingGarageService.getRandomParkerOfType(ParkerEntity.TicketType.season);
            case ParkerType.all:
            default:
                return this.parkingGarageService.getRandomParker();
        }
    }

    public float GetParkingDuration(int parkerId)
    {
        return this.parkingGarageService.getParkingDurationMinutes(parkerId);
    }

    public float GetParkingCost(int parkerId)
    {
        return this.parkingGarageService.getParkingDurationMinutes(parkerId) * this.parkingCostPerMinute;
    }

    public List<JoinedResultset> GetParkingSpots()
    {
        return this.parkingGarageService.getParkingSpots();
    }
}

public enum ParkerType {
    normal,
    season,
    all,
}
