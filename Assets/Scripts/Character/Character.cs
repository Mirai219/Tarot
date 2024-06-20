public class Character
{
    public string name;
    public int MaxHP;
    public int HP;
    public int shield;
    public int PhyAttack;
    public int PhyDefense;
    public int MagAttack;
    public int MagDefense;
    public int RealAttack;
    public int getPowerSpeed;
    public Character(string newname,int newHP,int newPhyAttack,
        int newPhyDefense,int newMagattack,int newMagDefense,int newRealAttack)
    {
        name = newname;
        MaxHP = newHP;
        HP = newHP;
        shield = 0;
        PhyAttack = newPhyAttack;            
        PhyDefense = newPhyDefense;
        MagAttack=newMagattack;
        MagDefense = newMagDefense;
        RealAttack = newRealAttack;
        getPowerSpeed = 5;
    }

    public void PhyAttOther(Character another,int att)
    {
        another.shield-= (att * PhyAttack - another.PhyDefense)>0? (att * PhyAttack - another.PhyDefense):0;
        if (another.shield < 0)
        {
            another.HP += another.shield;
            another.shield = 0;
        }
    }
    public void MagAttOther(Character another, int att)
    {
        another.shield -= (att * MagAttack - another.MagDefense)>0? (att * MagAttack - another.MagDefense):0;
        if (another.shield < 0)
        {
            another.HP += another.shield;
            another.shield = 0;
        }
    }
    public void RealAttOther(Character another, int att)
    {
        another.shield -= att * RealAttack;
        if (another.shield < 0)
        {
            another.HP += another.shield;
            another.shield = 0;
        }
    }
    #region 数据处理
    public void recover(int nubmer)
    { HP += nubmer;if (HP > MaxHP) HP = MaxHP; }
    public void getShield(int number)
    {   shield += number;}
    public void raisePhyAtt(int nubmer)
    { PhyAttack += nubmer;if (PhyAttack < 0) PhyAttack = 0; }
    public void raiseMagAtt(int nubmer)
    { MagAttack += nubmer;if (MagAttack < 0) MagAttack = 0; }
    public void raiseRealAtt(int nubmer)
    { RealAttack += nubmer;if (RealAttack < 0) RealAttack = 0; }
    public void raisePhyDefense(int nubmer)
    { PhyDefense += nubmer;if (PhyDefense < 0) PhyDefense = 0; }
    public void raiseMagDefense(int nubmer)
    { MagDefense += nubmer;if (MagDefense < 0) MagDefense = 0; }
    public void raisePowerSpeed(int number)
    { getPowerSpeed += number;if (getPowerSpeed < 0) getPowerSpeed = 0;}
    #endregion
}



