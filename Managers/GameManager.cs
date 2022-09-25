using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    Realm realm;
    [SerializeField] Unit playerUnit;
    [SerializeField] Unit enemyUnit;

    [SerializeField] TextMeshProUGUI playerUnitStatsText;
    //[SerializeField] TextMeshProUGUI enemyUnitStatsText;

    private void OnEnable()
    {
        realm = Realm.GetInstance();
        // Find unit from db or create new one
        playerUnit = realm.Find<Unit>("123");
        if (playerUnit == null)
        {
            realm.Write(() =>
            {
                playerUnit = realm.Add(new Unit()
                {
                    Id = "123",
                    Name = "Leif",
                    HitPoints = 10
                });
            });
        }
    }

    private void OnDisable()
    {
        realm.Dispose();
    }

    void Start()
    {
        UpdateStatsUI(playerUnit);

        Debug.Log(playerUnit.Name);
    }

    void Update()
    {
        
    }

    void UpdateStatsUI(Unit unit)
    {
        playerUnitStatsText.text = 
            $"Name: {unit.Name}\n" +
            $"HP: {unit.HitPoints}"; 
    }

    public void SetHP(int num)
    {
        playerUnit.HitPoints += num;
    }
}
