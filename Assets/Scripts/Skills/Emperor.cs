using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Emperor : MonoBehaviour
{
    int x = 3;
    int currentWillingNumber = -1;
    void Start()
    {
        FightProgress.WillingList = new EnemyWilling[5];
        FightProgress.x = x;
        FightProgress.enemyskill = enemySkill;
        FightProgress.currentEnemyWillingchange = WillingChange;
        FightProgress.ifwin = Win;
        FightProgress.areaEffect = Area;
        FightProgress.enemy = new Character("Emperor", 200, 1, 1, 1, 1, 1);//血量 物理攻击 物理防御 魔法攻击 魔法防御 真实伤害
        FightProgress.WillingList[0] = EnemyWilling.PhyAttack;
        FightProgress.WillingList[1] = EnemyWilling.RealAttack;
        FightProgress.WillingList[2] = EnemyWilling.Skill;
        FightProgress.WillingList[3] = EnemyWilling.AccumulatePower;
        FightProgress.WillingList[4] = EnemyWilling.SuperPhyAttack;
    }
    void enemySkill(Character enemy, Character player)
    {
        player.raisePhyDefense(-1);
    }
    void WillingChange()
    {
        currentWillingNumber = (currentWillingNumber + 1) % 5;
        FightProgress.currentWilling = FightProgress.WillingList[currentWillingNumber];
    }
    void Area(Character enemy, Character player)
    {
       

    }
    bool Win(Character enemy, Character player)
    {
        if (enemy.HP <= 1)
            return true;
        else return false;
    }
}
