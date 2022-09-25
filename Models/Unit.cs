using Realms;

public class Unit : RealmObject
{
    [PrimaryKey]
    public string Id { get; set; }
    public string Name { get; set; }
    public int HitPoints { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }

    //public Unit() { }

    //public Unit(
    //    string Id,
    //    string name,
    //    int hitPoints,
    //    int attack,
    //    int defense,
    //    int speed
    //    )
    //{
    //    this.Id = Id;
    //    this.name = name;
    //    this.hitPoints = hitPoints;
    //    this.attack = attack;
    //    this.defense = defense;
    //    this.speed = speed;
    //}
}
