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
    MapperProfiles mapper;

    private void OnEnable()
    {
        realm = RealmManager.Instance.realm;
        mapper = RealmManager.Instance.mapper;

        // Find unit from db or create new one
        playerUnit = mapper.Map(realm.Find<Unit>("123"), new Unit_Battle());
        unitList.Add(playerUnit);

        enemyUnit = mapper.Map(realm.Find<Unit>("567"), new Unit_Battle());
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

}
