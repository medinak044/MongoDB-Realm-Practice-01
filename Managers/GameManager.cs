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
    [SerializeField] List<Unit> unitList = new List<Unit>();

    [SerializeField] TextMeshProUGUI playerUnitStatsText;
    [SerializeField] TextMeshProUGUI enemyUnitStatsText;

    enum Turn
    {
        player,
        enemy,
    }
    Turn turn;

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
        unitList.Add(playerUnit);

        enemyUnit = realm.Find<Unit>("567");
        if (enemyUnit == null)
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
        unitList.Add(enemyUnit);

        var all = new List<Unit>();
        all.AddRange(realm.All<Unit>()); // Find all units
        Debug.Log(all.Count);
    }

    private void OnDisable()
    {
        realm.Dispose();
    }

    void Start()
    {
        UpdateStatsUI(playerUnitStatsText, playerUnit);
        UpdateStatsUI(enemyUnitStatsText, enemyUnit);

        turn = Turn.player; // Turn 1
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
        ChangeTurn();
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

    void EnemyPhase()
    {
        // Attacks player unit
        EnemyAttacks();
    }

    void ChangeTurn()
    {
        if (turn == Turn.player)
        {
            turn = Turn.enemy;
            EnemyPhase();
            turn = Turn.player;
        }
    }
}
