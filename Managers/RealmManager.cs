using Realms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmManager : PersistentSingleton<RealmManager>
{
    Realm realm;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        string dbPath = $"{Application.dataPath}/Resources/Realm/FEO.realm"; // Dev environment vs Production environment
        realm = Realm.GetInstance(dbPath);
    }

    private void OnDisable()
    {
        if (realm != null) realm.Dispose();
    }
}
