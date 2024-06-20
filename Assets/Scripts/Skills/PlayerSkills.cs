using UnityEngine;

public class Skill
{
    public static int outcome = 0;
    public static int rebirth = 0;
    public static bool towerDefense = false;
    public static int SunFireRounds = 0;
    public static int theWorld = 0;


    public bool startSkill = false;
    public TarotType name;

    public int tempPower;
    public int Maxpower = 100;
    public int phyAtt;
    public int magAtt;
    public int RealAtt;
    public int Recover;
    public int Shield;
    public int special;
    public string description;
    public Skill(TarotType name, int maxPower, int phyAtt,
        int magAtt, int realAtt, int recover, int shield)
    {
        tempPower = 0;
        Maxpower = maxPower;
        this.name = name;
        this.phyAtt = phyAtt;
        this.magAtt = magAtt;
        RealAtt = realAtt;
        Recover = recover;
        Shield = shield;
        special = 0;
    }
    public Skill(TarotType name, int maxPower, int phyAtt,
        int magAtt, int realAtt, int recover, int shield, int special)
    {
        tempPower = 0;
        Maxpower = maxPower;
        this.name = name;
        this.phyAtt = phyAtt;
        this.magAtt = magAtt;
        RealAtt = realAtt;
        Recover = recover;
        Shield = shield;
        this.special = special;
    }
    public void raisePower(int power)
    {
        tempPower += power;
        if(tempPower > Maxpower)
        {
            tempPower = Maxpower;
        }
    }
    public void NormalReact(Character player, Character enemy, Skill[] AllSkills)
    {
        tempPower += 5;
        if (tempPower > Maxpower)
            tempPower = Maxpower;
        if (special == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                if (AllSkills[i].name != name)
                {
                    AllSkills[i].raisePower(player.getPowerSpeed);
                }
            }
        }
        if (special != 1)
        {
            player.PhyAttOther(enemy, phyAtt);
            player.MagAttOther(enemy, magAtt);
            player.RealAttOther(enemy, RealAtt);
            player.recover(Recover);
            player.getShield(Shield);
        }
        else
        {
            int random = Random.Range(0, 5);
            switch (random)
            {
                case 0: player.PhyAttOther(enemy, phyAtt); break;
                case 1: player.MagAttOther(enemy, magAtt); break;
                case 2: player.RealAttOther(enemy, RealAtt); break;
                case 3: player.recover(Recover); break;
                case 4: player.getShield(Shield); break;
            }
        }
    }

    public void start(Character player, Character enemy, Skill[] AllSkills)
    {
        if (startSkill)
        {
            startSkill = false;
            tempPower = 0;
            switch (name)
            {
                case TarotType.Fool: skillFool(player, enemy, AllSkills); break;
                case TarotType.Magician: skillMagician(player, enemy, AllSkills); break;
                case TarotType.HighPriestess: skillHighPriestess(player, enemy, AllSkills); break;
                case TarotType.Empress: skillEmpress(player, enemy, AllSkills); break;
                case TarotType.Emperor: skillEmperor(player, enemy, AllSkills); break;
                case TarotType.Hierophant: skillHierophant(player, enemy, AllSkills); break;
                case TarotType.Lovers: skillLovers(player, enemy, AllSkills); break;
                case TarotType.Chariot: skillChariot(player, enemy, AllSkills); break;
                case TarotType.Strength: skillStrength(player, enemy, AllSkills); break;
                case TarotType.Hermit: skillHermit(player, enemy, AllSkills); break;
                case TarotType.WheelOfFortune: skillWheelOfFortune(player, enemy, AllSkills); break;
                case TarotType.Justice: skillJustice(player, enemy, AllSkills); break;
                case TarotType.HangedMan: skillHangedMan(player, enemy, AllSkills); break;
                case TarotType.Death: skillDeath(player, enemy, AllSkills); break;
                case TarotType.Temperance: skillTemperance(player, enemy, AllSkills); break;
                case TarotType.Devil: skillDevil(player, enemy, AllSkills); break;
                case TarotType.Tower: skillTower(player, enemy, AllSkills); break;
                case TarotType.Star: skillStar(player, enemy, AllSkills); break;
                case TarotType.Moon: skillMoon(player, enemy, AllSkills); break;
                case TarotType.Sun: skillSun(player, enemy, AllSkills); break;
                case TarotType.Judgement: skillJudgement(player, enemy, AllSkills); break;
                case TarotType.World: skillWorld(player, enemy, AllSkills); break;
            }
        }

    }

    #region 我方各技能效果

    public void skillFool(Character player, Character enemy, Skill[] AllSkills)
    {
        int random = Random.Range(0, 7);
        switch (random)
        {
            case 0:
                for (int i = 0; i < 6; i++)
                {
                    if (AllSkills[i].name != TarotType.Fool)
                    {
                        AllSkills[i].startSkill = true;
                    }
                }
                break;
            case 1: enemy.recover(10); break;
            case 2:
                enemy.PhyAttOther(player, 1);
                enemy.MagAttOther(player, 1);
                enemy.RealAttOther(player, 1);
                break;
            case 3:
                int random2 = Random.Range(-36, 37);
                int random3 = Random.Range(-36, 37);
                int random4 = Random.Range(-36, 37);
                player.raisePhyAtt(random2);
                player.raiseMagAtt(random3);
                player.raiseRealAtt(random4);
                break;
            case 4:
                int random5 = Random.Range(-36, 37);
                int random6 = Random.Range(-36, 37);
                int random7 = Random.Range(-36, 37);
                enemy.raisePhyAtt(random5);
                enemy.raiseMagAtt(random6);
                enemy.raiseRealAtt(random7); break;
            case 5:                         //重新排列
                break;
            case 6:
                outcome = 2;//直接输掉
                break;
        }
    }
    public void skillMagician(Character player, Character enemy, Skill[] AllSkills)
    {
        player.MagAttOther(enemy, magAtt * 10);
    }
    public void skillHighPriestess(Character player, Character enemy, Skill[] AllSkills)
    {
        player.recover(Recover * 5);
        player.raisePhyAtt(10);
        player.raiseMagAtt(10);

        for (int i = 0; i < 6; i++)
        {
            if (AllSkills[i].name != name)
            {
                AllSkills[i].raisePower(4*player.getPowerSpeed);
            }
        }
    }
    public void skillEmpress(Character player, Character enemy, Skill[] AllSkills)
    {
        player.raiseMagDefense(10);
        player.raiseMagAtt(10);
    }
    public void skillEmperor(Character player, Character enemy, Skill[] AllSkills)
    {
        player.raisePhyDefense(10);
        player.raisePhyAtt(10);
    }
    public void skillHierophant(Character player, Character enemy, Skill[] AllSkills)
    {
        player.recover(20);
    }
    public void skillLovers(Character player, Character enemy, Skill[] AllSkills)
    {

    }
    public void skillChariot(Character player, Character enemy, Skill[] AllSkills)
    {
        player.PhyAttOther(enemy, phyAtt * 10);
    }
    public void skillStrength(Character player, Character enemy, Skill[] AllSkills)
    {
        player.raisePhyAtt(10);
        player.raiseMagAtt(10);
        player.raiseRealAtt(10);
    }
    public void skillHermit(Character player, Character enemy, Skill[] AllSkills)
    {
        player.getShield(30);
    }
    public void skillWheelOfFortune(Character player, Character enemy, Skill[] AllSkills)
    {
        int random = Random.Range(0, 9);
        switch (random)
        {
            case 0: player.PhyAttOther(enemy, 50); break;
            case 1: player.MagAttOther(enemy, 50); break;
            case 2: player.RealAttOther(enemy, 50); break;
            case 3: player.recover(50); break;
            case 4: player.getShield(50); break;
            case 5: player.raisePhyAtt(10); break;
            case 6: player.raiseMagAtt(10); break;
            case 7: player.raisePhyDefense(10); break;
            case 8: player.raiseMagDefense(10); break;
        }
    }
    public void skillJustice(Character player, Character enemy, Skill[] AllSkills)
    {
        player.RealAttOther(enemy, 30);
    }
    public void skillHangedMan(Character player, Character enemy, Skill[] AllSkills)
    {
        if (rebirth == 0)
            rebirth++;
    }
    public void skillDeath(Character player, Character enemy, Skill[] AllSkills)
    {
        outcome = 1;
    }
    public void skillTemperance(Character player, Character enemy, Skill[] AllSkills)
    {
        enemy.raisePhyAtt(-10);
        enemy.raiseMagAtt(-10);
    }
    public void skillDevil(Character player, Character enemy, Skill[] AllSkills)
    {
        if (player.HP > player.MaxHP / 4)
        {
            player.raisePhyAtt(player.HP - player.MaxHP / 4);
            player.raiseMagAtt(player.HP - player.MaxHP / 4);
            player.raiseRealAtt(player.HP - player.MaxHP / 4);
            player.HP = player.MaxHP / 4;
        }
        else player.HP -= 999;
    }
    public void skillTower(Character player, Character enemy, Skill[] AllSkills)
    {
        Maxpower = 300;
        towerDefense = true;
    }
    public void skillStar(Character player, Character enemy, Skill[] AllSkills)
    {
        for (int i = 0; i < 6; i++)
        {
            if (AllSkills[i].name != name)
            {
                AllSkills[i].raisePower(10*player.getPowerSpeed);
            }
        }
    }
    public void skillMoon(Character player, Character enemy, Skill[] AllSkills)
    {

    }
    public void skillSun(Character player, Character enemy, Skill[] AllSkills)
    {
        SunFireRounds = 5;
    }
    public void skillJudgement(Character player, Character enemy, Skill[] AllSkills)
    {
        if(FightProgress.currentWilling==EnemyWilling.Skill)
        {
            player.RealAttOther(enemy, 5);
            player.getShield(20);
        }
    }
    public void skillWorld(Character player, Character enemy, Skill[] AllSkills)
    {
        theWorld = 5;
    }
    #endregion

}
