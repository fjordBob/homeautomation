namespace Homeautomation.Service.Models;
public class SwitchHistory
{
    public int Id
    {
        get; set;
    }

    public string? DeviceId
    {
        get; set;
    }

    public List<Switch>? Values
    {
        get; set;
    }
}