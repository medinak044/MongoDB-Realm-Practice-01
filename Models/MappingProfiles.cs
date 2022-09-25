public class MapperProfiles: Singleton<MapperProfiles>
{
    public Unit_Battle MapUnitRMToUnit(Unit rm)
    {
        Unit_Battle unit = new Unit_Battle()
        {
            Id = rm.Id,
            Name = rm.Name,
            HitPoints = rm.HitPoints,
            Attack = rm.Attack,
            Defense = rm.Defense,
            Speed = rm.Speed,
        };

        return unit;
    }

}
