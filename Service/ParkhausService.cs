namespace ParkhausKorte.Service;

public class ParkhausService
{
    const int maxPlaetze = 176;
    int dauerParker = 45;
    int kurzParker = 15;

    public int getFreieDauerPlaetze()
    {
        return maxPlaetze - (this.dauerParker + this.kurzParker);
    }

    public int getFreieKurzPlaetze()
    {
        if (this.dauerParker < 40) {
            return maxPlaetze - 40 - this.kurzParker;
        } else {
            return maxPlaetze - (this.dauerParker + this.kurzParker);
        }
    }

    public Task<Parkhaus[]> GetForecastAsync()
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray());
    }
}