public class MapperProfiles
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

    public Unit_Battle Map(Unit input, Unit_Battle output)
    {
        output = new Unit_Battle()
        {
            Id = input.Id,
            Name = input.Name,
            HitPoints = input.HitPoints,
            Attack = input.Attack,
            Defense = input.Defense,
            Speed = input.Speed,
        };

        return output;
    }

    public Unit Map(Unit_Battle input, Unit output = null)
    {
        output = new Unit()
        {
            Id = input.Id,
            Name = input.Name,
            HitPoints = input.HitPoints,
            Attack = input.Attack,
            Defense = input.Defense,
            Speed = input.Speed,
        };

        return output;
    }
}

//public interface IMapperProfiles
//{
//    void CreateMap<TSource, TDestination>();
//    TDestination Map<TSource, TDestination>();
//}
