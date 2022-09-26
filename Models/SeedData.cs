using Realms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedData
{
    MapperProfiles mapper;
    Realm realm;

    public SeedData(Realm realm, MapperProfiles mapperProfiles)
    {
        this.realm = realm;
        this.mapper = mapperProfiles;
    }

    public void InitiateSeedData()
    {
        if (realm.Find<Unit>("123") == null)
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

        if (realm.Find<Unit>("567") == null)
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

        if (realm.Find<Unit>("24601") == null)
        {
            realm.Write(() =>
            {
                realm.Add(new Unit()
                {
                    Id = "24601",
                    Name = "Galzus",
                    HitPoints = 30,
                    Attack = 20,
                });
            });
        }
    }
}
