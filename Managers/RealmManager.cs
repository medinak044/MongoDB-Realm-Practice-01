using Realms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmManager : PersistentSingleton<RealmManager>
{
    public Realm realm;
    public MapperProfiles mapper;

    public SeedData seedData => new SeedData(realm, mapper);

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        string dbPath = $"{Application.dataPath}/Resources/Realm/FEO.realm"; // Dev environment vs Production environment
        realm = Realm.GetInstance(dbPath);
        mapper = new MapperProfiles();
    }

    private void OnDisable()
    {
        if (realm != null) realm.Dispose();
    }

    private void Start()
    {
        seedData.InitiateSeedData();
    }
}
