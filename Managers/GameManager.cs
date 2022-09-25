using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using TMPro;
using System;
using System.IO;
using UnityEditor;

public class GameManager : Singleton<GameManager>
{
    Realm realm;

    [SerializeField] Unit_Battle playerUnit;
    [SerializeField] Unit_Battle enemyUnit;
    [SerializeField] List<Unit_Battle> unitList = new List<Unit_Battle>();

    [SerializeField] TextMeshProUGUI playerUnitStatsText;
    [SerializeField] TextMeshProUGUI enemyUnitStatsText;

    enum Turn
    {
        player,
        enemy,
    }
    [SerializeField] Turn currentTurn;

    [SerializeField] List<Turn> turnList = new List<Turn>();

    private void OnEnable()
    {
        string dbPath = $"{Application.dataPath}/Resources/Realm/FEO.realm"; // Dev environment vs Production environment
        realm = Realm.GetInstance(dbPath);

        // Find unit from db or create new one
        playerUnit = MapUnitRMToUnit(realm.Find<Unit>("123"));
        if (playerUnit == null)
        {
            realm.Write(() =>
            {
                playerUnit = MapUnitRMToUnit(realm.Add(new Unit()
                {
                    Id = "123",
                    Name = "Leif",
                    HitPoints = 10,
                    Attack = 1,
                }));
            });
        }
        unitList.Add(playerUnit);

        enemyUnit =  MapUnitRMToUnit(realm.Find<Unit>("567"));
        if (enemyUnit == null)
        {
            realm.Write(() =>
            {
                playerUnit = MapUnitRMToUnit(realm.Add(new Unit()
                {
                    Id = "567",
                    Name = "Lilina",
                    HitPoints = 10,
                    Attack = 1,
                }));
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
        turnList.Add(Turn.player);
        turnList.Add(Turn.enemy);

        UpdateStatsUI(playerUnitStatsText, playerUnit);
        UpdateStatsUI(enemyUnitStatsText, enemyUnit);

        currentTurn = Turn.player; // Turn 1
    }

    void Update()
    {
        
    }

    void UpdateStatsUI(TextMeshProUGUI ui, Unit_Battle unit)
    {
        ui.text = 
            $"Name: {unit.Name}\n" +
            $"HP: {unit.HitPoints}\n" +
            $"Atk: {unit.Attack}\n" +
            $"Def: {unit.Defense}\n" +
            $"Spd: {unit.Speed}"; 
    }

    public void PlayerAttacks()
    {
        Attack(enemyUnit, playerUnit.Attack);
    }

    public void EnemyAttacks()
    {
        Attack(playerUnit, enemyUnit.Attack);
    }

    public void Attack(Unit_Battle targetedUnit, int atk)
    {
        targetedUnit.HitPoints += -(atk - targetedUnit.Defense);
        if (currentTurn == Turn.player)
        {
            UpdateStatsUI(enemyUnitStatsText, targetedUnit);
        } 
        else if (currentTurn == Turn.enemy)
        {
            UpdateStatsUI(playerUnitStatsText, targetedUnit);
        }

        EndTurn();
    }

    void EnemyPhase()
    {
        EnemyAttacks(); // Attacks player unit
    }

    void EndTurn()
    {
        // (Delay?)
        ChangeTurn();
    }


    void ChangeTurn()
    {
        int index = turnList.FindIndex(t => t == currentTurn); // Find which index in the List the Turn is located
        if (index + 1 < turnList.Count)
        {
            currentTurn = turnList[index + 1];
        }
        else
        {
            currentTurn = turnList[0]; // Cycle back to beginning of List
        }

        // Enemy AI
        if (currentTurn == Turn.enemy)
        {
            EnemyPhase();
        }
    }


    Unit_Battle MapUnitRMToUnit(Unit rm)
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
}
