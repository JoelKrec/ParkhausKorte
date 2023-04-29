namespace ParkhausKorte.Data;

public class ParkingGarage
{
    public readonly int maxParker;
    public readonly int reservedSeasonParkSpaces;
    public readonly int parkSpaceBuffer;
    public readonly int parkSpaces;
    public int currentParkers;
    public int currentNormalParkers;
    public int currentSeasonParkers;

    public ParkingGarage(int parkSpaces = 180, int parkSpaceBuffer = 4, int reservedSeasonParkSpaces = 40)
    {
        this.parkSpaces = parkSpaces;
        this.parkSpaceBuffer = parkSpaceBuffer;
        this.maxParker = parkSpaces - parkSpaceBuffer;
        this.reservedSeasonParkSpaces = reservedSeasonParkSpaces;
    }

    public int GetParkersOfType(ParkerType parkerType)
    {
        int parkersToReturn = 0;

        switch (parkerType)
        {
            case ParkerType.normal:
                parkersToReturn = currentNormalParkers;
                break;
            case ParkerType.season:
                parkersToReturn = currentSeasonParkers;
                break;
            case ParkerType.all:
                parkersToReturn = currentParkers;
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

        switch (parkerType)
        {
            case ParkerType.normal:
                freeSpaces = this.GetFreeSpaces();

                if(freeSpaces > 0)
                {
                    this.currentNormalParkers++;
                    addingSuccessful = true;
                }
                break;
            case ParkerType.season:
                freeSpaces = this.maxParker - this.currentParkers;
                if(freeSpaces > 0)
                {
                    this.currentSeasonParkers++;
                    addingSuccessful = true;
                }
                break;
            case ParkerType.all:
            default:
                break;
        }

        this.currentParkers = this.currentNormalParkers + this.currentSeasonParkers;

        return addingSuccessful;
    }

    public void RemoveParkerOfType(ParkerType parkerType)
    {
        switch(parkerType)
        {
            case ParkerType.normal:
                this.currentNormalParkers--;
                break;
            case ParkerType.season:
                this.currentSeasonParkers--;
                break;
            case ParkerType.all:
            default:
                break;
        }

        this.currentParkers = this.currentNormalParkers + this.currentSeasonParkers;
    }

    public int GetFreeSpaces()
    {
        int freeSpaces = 0;
        if(this.currentSeasonParkers > this.reservedSeasonParkSpaces)
        {
            freeSpaces = this.maxParker - currentParkers;
        }
        else
        {
            freeSpaces = this.maxParker - 40 - this.currentNormalParkers;
        }
        return freeSpaces;
    }
}

public enum ParkerType {
    normal,
    season,
    all,
}
