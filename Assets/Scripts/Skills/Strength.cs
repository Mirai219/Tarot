using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : MonoBehaviour
{
    int x = 10;
    int currentWillingNumber=-1;
    void Start()
    {
        FightProgress.WillingList = new EnemyWilling[3];
        FightProgress.x = x;
        FightProgress.enemyskill = enemyStrengthSkill;
        FightProgress.currentEnemyWillingchange = strengthWillingChange;
        FightProgress.ifwin = strengthWin;
        FightProgress.areaEffect = strengthArea;
        FightProgress.enemy = new Character("Strength", 200, 1, 1, 1, 1, 1);
        FightProgress.WillingList[0] = EnemyWilling.RealAttack;
        FightProgress.WillingList[1] = EnemyWilling.SuperRealAttack;
        FightProgress.WillingList[2] = EnemyWilling.Skill;
    }
    void enemyStrengthSkill(Character enemy,Character player)
    {
        enemy.raisePhyAtt(x);
    }
    void strengthWillingChange()
    {
        currentWillingNumber = (currentWillingNumber+1) % 3;
        FightProgress.currentWilling= FightProgress.WillingList[currentWillingNumber];
    }
    void strengthArea(Character enemy,Character player)
    {
        if (enemy.HP > player.HP)
        {
            player.HP -= 5;
        }
        if(enemy.HP<player.HP)
        {
            enemy.HP -= 5;
        }

    }
    bool strengthWin(Character enemy,Character player)
    {
        if (enemy.HP <= 1)
          return true;
        else return false;
    }
}
