using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public string unitName;
    public int level;
    public float damageSingle;
    public float damageMultiple;
    public float damagePower;

    public float maxHP;
    public float currentHP;
    public float shield =0;//porcentagem
    public float armor;
    public float fury;
    public float singleCost;
    public float multipleCost;
    public float powerSingleCost;
    public float ultimateCost;
    public string equipedGemSingle;
    public string equipedGemMultiple;
    public string equipedGemPower;
    
    
    
   

    
    
        
    

    
    
 public void LoadStats()
    {
        level = PlayerStats.instance.level;
        damageSingle = PlayerStats.instance.damage;
        damageMultiple = damageSingle * 0.75f;
        damagePower = damageSingle * 0.5f;
        maxHP = PlayerStats.instance.maxHp;
        currentHP = maxHP;
        armor = PlayerStats.instance.armor;
        fury = PlayerStats.instance.fury;
        singleCost = PlayerStats.instance.singleCost;
        multipleCost = PlayerStats.instance.multipleCost;
        powerSingleCost = PlayerStats.instance.powerSingleCost;
        ultimateCost = PlayerStats.instance.ultimateCost;
        switch (PlayerStats.instance.pickaxeGems[0])
        {
            case 0:
                equipedGemSingle = "None";
                break;
            case 1:
                equipedGemSingle = "Blue";
                break;
            case 2:
                equipedGemSingle = "Green";
                break;
            case 3:
                equipedGemSingle = "Red";
                break;
        }
        switch (PlayerStats.instance.pickaxeGems[1])
        {
            case 0:
                equipedGemMultiple = "None";
                break;
            case 1:
                equipedGemMultiple = "Blue";
                break;
            case 2:
                equipedGemMultiple = "Green";
                break;
            case 3:
                equipedGemMultiple = "Red";
                break;
        }
        switch (PlayerStats.instance.pickaxeGems[2])
        {
            case 0:
                equipedGemPower = "None";
                break;
            case 1:
                equipedGemPower = "Blue";
                break;
            case 2:
                equipedGemPower = "Green";
                break;
            case 3:
                equipedGemPower = "Red";
                break;
        }


    }
    public bool TakeDamage(float dmg,Unit unit)
    {
        if (unit.debuffRounds > 0)
        {
            dmg /= 2;
            unit.debuffRounds--;
        }
        if (shield > 0)
        {
            dmg -= dmg * shield - armor;
           
            currentHP -= dmg;
        }
        else
        {
            currentHP -= dmg;
        }
        
        
        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        

    }

   
    
}
