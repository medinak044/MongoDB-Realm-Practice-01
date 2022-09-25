using Realms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedData : Singleton<SeedData>
{
    MapperProfiles mp;
    Realm realm;

    public SeedData(MapperProfiles mapperProfiles)
    {
        this.mp = mapperProfiles;
    }

    private void Start()
    {
        string dbPath = $"{Application.dataPath}/Resources/Realm/FEO.realm"; // Dev environment vs Production environment
        realm = Realm.GetInstance(dbPath);
    }

    public void InitiateSeedData()
    {
        if (mp.MapUnitRMToUnit(realm.Find<Unit>("123")) == null)
        {
            realm.Write(() =>
            {
                realm.Add(new Unit()
                {
                    Id = "123",
                    Name = "Leif",
                    HitPoints = 10,
                    Attack = 1,
                });
            });
        }

        if (mp.MapUnitRMToUnit(realm.Find<Unit>("567")) == null)
        {
            realm.Write(() =>
            {
                realm.Add(new Unit()
                {
                    Id = "567",
                    Name = "Lilina",
                    HitPoints = 10,
                    Attack = 1,
                });
            });
        }

    }
}
