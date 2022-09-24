using Realms;

public class Unit : RealmObject
{
    [PrimaryKey]
    public string Id { get; set; }
    public string name { get; set; }
    public int hitPoints { get; set; }
    public int attack { get; set; }
    public int defense { get; set; }
    public int speed { get; set; }

    public Unit() { }

    public Unit(
        string Id,
        string name,
        int hitPoints,
        int attack,
        int defense,
        int speed
        )
    {
        this.Id = Id;
        this.name = name;
        this.hitPoints = hitPoints;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
    }
}
