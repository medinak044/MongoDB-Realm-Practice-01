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
    [SerializeField] TextMeshProUGUI enemyUnitStatsText;

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
                    HitPoints = 10,
                    Attack = 1,
                });
            });
        }

        playerUnit = realm.Find<Unit>("567");
        if (playerUnit == null)
        {
            realm.Write(() =>
            {
                playerUnit = realm.Add(new Unit()
                {
                    Id = "567",
                    Name = "Lilina",
                    HitPoints = 10,
                    Attack = 1,
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
        UpdateStatsUI(playerUnitStatsText, playerUnit);
        UpdateStatsUI(enemyUnitStatsText, enemyUnit);

    }

    void Update()
    {
        
    }

    void UpdateStatsUI(TextMeshProUGUI ui, Unit unit)
    {
        ui.text = 
            $"Name: {unit.Name}\n" +
            $"HP: {unit.HitPoints}"; 
    }

    public void PlayerAttacks()
    {
        Attack(true, enemyUnit, playerUnit.Attack);
    }

    public void EnemyAttacks()
    {
        Attack(false, playerUnit, enemyUnit.Attack);
    }

    public void Attack(bool isPlayerUnit, Unit targetedUnit, int damage)
    {
        targetedUnit.HitPoints += -(damage - targetedUnit.Defense);
        if (isPlayerUnit)
        {
            UpdateStatsUI(playerUnitStatsText, targetedUnit);
        } 
        else
        {
            UpdateStatsUI(enemyUnitStatsText, targetedUnit);
        }
    }
}
