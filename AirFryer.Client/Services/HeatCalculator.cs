using AirFryer.Client.Models;

namespace AirFryer.Client.Services;

public class HeatCalculator
{
    public HeatCalculation Calculate(int? regular, int? hotAir)
    {
        var result = new HeatCalculation();

        if(regular != null)
        {
            result.RegularOven = regular.Value;
            result.HotAirOven = regular.Value - 20;
            result.AirFryer = result.HotAirOven - 20;
        }

        if(hotAir != null)
        {
            result.RegularOven = hotAir.Value + 20;
            result.HotAirOven = hotAir.Value;
            result.AirFryer = result.HotAirOven - 20;
        }

        return result;
    }
}
