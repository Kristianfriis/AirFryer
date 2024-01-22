using AirFryer.Client.Models;

namespace AirFryer.Client.Services;

public class MinuteCalculator
{
    public MinuteCalculation Calculate(int? regular, int? airFryer)
    {
        var result = new MinuteCalculation();

        if(regular != null)
        {
            result.RegularOven = regular.Value;
            result.AirFryer = Convert.ToInt16(Math.Round(regular.Value * 0.8));
        }
        if(airFryer != null)
        {
            result.RegularOven = Convert.ToInt16(Math.Round(airFryer.Value / 0.8));
            result.AirFryer = airFryer.Value;
        }

        return result;
    }
}
