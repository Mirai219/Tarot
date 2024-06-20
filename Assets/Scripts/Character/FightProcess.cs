using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum EnemyWilling
{
    none = -1,
    PhyAttack,
    SuperPhyAttack,
    MagAttack,
    SuperMagAttack,
    RealAttack,
    SuperRealAttack,
    GetShield,
    Recover,
    Skill,
    AccumulatePower,
}

public class FightProgress : MonoBehaviour
{
    public TMP_Text playertext;
    public TMP_Text enemytext;
    public bool start=false;

    public void startnow()
    {
        start = true;
    }


    public static int x= 10;

    public static Character enemy;
    public static Character player;

    public static EnemyWilling[] WillingList;
    public static EnemyWilling currentWilling;


    public delegate void enemyChangeWilling();
    public static enemyChangeWilling currentEnemyWillingchange;

    public delegate void enemySkill(Character enemy,Character player);
    public static enemySkill enemyskill;

    public delegate bool ifWin(Character enemy, Character player);
    public static ifWin ifwin;

    public delegate void Area(Character enemy, Character player);
    public static Area areaEffect;

    public delegate void performance();
    public static performance enemyPerformance;

    public static int stage = 0;            
    public int[] chosenTarotNumber;         //塔罗牌序号
    public TarotType[] chosenTarot;         //塔罗牌名字
    Skill[] skills;                         //塔罗牌库

    public static Skill[] skillsChosenByPlayer=new Skill[6];

    public static Dictionary<TarotType, Skill> SKILLS = new Dictionary<TarotType, Skill>();


    public static bool move = false;
    public static Dictionary<TarotType, int> oneroundNumber = new Dictionary<TarotType, int>();


    void Start()
    {
        player = new Character("Elysia", 150, 1, 1, 1, 1, 1);
        #region awakeTarot
        chosenTarotNumber = new int[6];
        
        chosenTarotNumber[0] = 0;
        chosenTarotNumber[1] = 1;
        chosenTarotNumber[2] = 2;
        chosenTarotNumber[3] = 3;
        chosenTarotNumber[4] = 4;
        chosenTarotNumber[5] = 5;

        chosenTarot = new TarotType[6];
        for (int i = 0; i < 6; i++)
        {
            chosenTarot[i] = (TarotType)chosenTarotNumber[i];
        }
        #endregion
        #region skills
        skills = new Skill[22];
        skills[0] = new Skill(TarotType.Fool, 100, 1, 1, 1, 1, 1, 1);
        skills[0].description = "You may have good luck, but it can also backfire. Randomly create various effects.";
        skills[1] = new Skill(TarotType.Magician, 100, 0, 1, 1, 0, 0);
        skills[1].description = "I am the emperor of magic! Causing massive magical damage.";
        skills[2] = new Skill(TarotType.HighPriestess, 100, 0, 0, 1, 1, 0);
        skills[2].description = "Only do your job well. Restore stamina, increase damage multiplier, and recharge other skills.";
        skills[3] = new Skill(TarotType.Empress, 100, 0, 1, 1, 0, 0);
        skills[3].description = "For Her Majesty the Empress! Improve magic defense and magic damage.";
        skills[4] = new Skill(TarotType.Emperor, 100, 1, 0, 1, 0, 0);
        skills[4].description = "I am an eternal emperor. Improve physical defense and physical damage multiplier.";
        skills[5] = new Skill(TarotType.Hierophant, 100, 0, 0, 1, 1, 0);
        skills[5].description = "Oh, my God. Restore physical strength.";
        skills[6] = new Skill(TarotType.Lovers, 100, 0, 0, 1, 1, 0);
        skills[7] = new Skill(TarotType.Chariot, 100, 1, 0, 1, 0, 0);
        skills[8] = new Skill(TarotType.Strength, 100, 1, 1, 1, 0, 0);
        skills[9] = new Skill(TarotType.Hermit, 100, 0, 0, 1, 0, 1);
        skills[10] = new Skill(TarotType.WheelOfFortune, 100, 1, 1, 1, 1, 1, 1);
        skills[11] = new Skill(TarotType.Justice, 100, 0, 0, 1, 0, 0);
        skills[12] = new Skill(TarotType.HangedMan, 100, 0, 0, 0, 0, 0);
        skills[13] = new Skill(TarotType.Death, 100, 0, 0, 0, 0, 0);
        skills[14] = new Skill(TarotType.Temperance, 100, 0, 0, 1, 0, 1);
        skills[15] = new Skill(TarotType.Devil, 100, 1, 1, 1, 0, 0);
        skills[16] = new Skill(TarotType.Tower, 100, 0, 0, 1, 0, 1);
        skills[17] = new Skill(TarotType.Star, 100, 0, 0, 1, 0, 0, 2);
        skills[18] = new Skill(TarotType.Moon, 100, 0, 0, 1, 1, 1);
        skills[19] = new Skill(TarotType.Sun, 100, 1, 1, 1, 0, 0);
        skills[20] = new Skill(TarotType.Judgement, 100, 1, 1, 1, 0, 0);
        skills[21] = new Skill(TarotType.World, 100, 0, 0, 1, 0, 0, 2);
        for(int i=0; i<22;i++)
        {
            SKILLS.Add((TarotType)i, skills[i]);
        }
        #endregion
        for (int i = 0; i < 6; i++)
            skillsChosenByPlayer[i] = skills[chosenTarotNumber[i]];
    }


    public static float timer = 0;
    void Update()
    {
        playertext.text = "player: HP:" + player.HP + '\n'+"shield:" + player.shield;
        enemytext.text = enemy.name+": HP:" + enemy.HP+'\n'+"willing:"+currentWilling;

        #region special
        if (player.HP <= 0)
        {
            if (Skill.rebirth == 1)
            {
                Skill.rebirth++;
                player.HP = player.MaxHP;
                player.raisePhyAtt(10);
                player.raiseMagAtt(10);
                player.raisePowerSpeed(3);
            }
            else
            {
                Skill.outcome = 2;
            }
        }
        if(enemy.HP <= 0)
        { Skill.outcome = 1; }
        #endregion

        if(start==false)
        {
            oneroundNumber.Clear();
            move = false;
        }

        if (start == true)
        {
            for(int i=0;i< 6; i++)
            {
                skillsChosenByPlayer[i].start(player, enemy, skillsChosenByPlayer);
            }
            if (ifwin(enemy, player))
            {
                Skill.outcome = 1;
            }
            if (Skill.outcome == 0)
            {
                switch (stage)
                {
                    case 0://我方行动                        
                        if (move)
                        {
                            timer += Time.deltaTime;
                        }
                        if (timer > 1f)
                        {
                            stage = 1;
                            timer = 0;
                        }

                        break;
                    case 1://我方结算
                        foreach (var item in oneroundNumber)
                        {
                            Debug.Log(item);
                            for (int i = item.Value; i > 0; i--)
                            {
                                SKILLS[item.Key].NormalReact(player, enemy, skillsChosenByPlayer);
                            }
                        }
                        oneroundNumber.Clear();
                        move = false;
                        stage = 2;
                        break;
                    case 2://场地效果
                        areaEffect(enemy,player);
                        stage = 3;
                        if(Skill.theWorld>0)
                        {
                            Skill.theWorld--;
                            stage = 0;
                        }
                        break;
                    case 3://boss演出


                        stage = 4;
                        break;
                    case 4://boss结算
                        Debug.Log(enemy.HP);
                        switch (currentWilling)
                        {
                            case EnemyWilling.PhyAttack:
                                enemy.PhyAttOther(player, 5);
                                break;
                            case EnemyWilling.SuperPhyAttack:
                                enemy.PhyAttOther(player, x);
                                break;
                            case EnemyWilling.MagAttack:
                                enemy.MagAttOther(player, 5);
                                break;
                            case EnemyWilling.SuperMagAttack:
                                enemy.MagAttOther(player, x);
                                break;
                            case EnemyWilling.RealAttack:
                                enemy.RealAttOther(player, 5);
                                break;
                            case EnemyWilling.SuperRealAttack:
                                enemy.RealAttOther(player, x);
                                break;
                            case EnemyWilling.Skill:
                                enemyskill(enemy, player);
                                break;
                        }
                        currentEnemyWillingchange();
                        stage = 0;
                        break;
                }
            }
            else
            {
                clearAfterGame();
            }
        }
    }
    public void clearAfterGame()
    {
        for (int i = 0; i < 6; i++)
        {
            skills[chosenTarotNumber[i]].tempPower = 0;
            skills[16].Maxpower = 100;
        }
        Skill.outcome = 0;
    }
}
