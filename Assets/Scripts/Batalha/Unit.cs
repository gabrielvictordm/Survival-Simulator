using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int level;
    public int damage;
    public float maxHP;
    public float currentHP;
    public bool isAlive = true;
    public string element;
    public string weakGem;
    public string strongGem;
    public int poisonDmg;
    public int poisonRounds;
    public int debuffRounds;



    public void TakeDamage(float dmg, string gem, PlayerUnit pUnit)
    {
        
        
            if(weakGem == gem)
            {
                currentHP -= dmg*2;
               
            }
            else if (strongGem == gem)
            {
                             currentHP -= dmg/2;
                             
            }
            else
            {
                  currentHP -= dmg;
                       
            }

            if (gem == "Red")
            {
                pUnit.currentHP += (dmg / 2);
                if (currentHP >= maxHP)
                {
                    currentHP = maxHP;
                }
            }
            else if (gem == "Green")
            {
                poisonRounds += 2;
            }
            else if (gem == "Blue")
            {
                debuffRounds++;
            }
            
            
            
       

      
       
        
        
       
        if (currentHP <= 0&& isAlive ==true)
        {
            isAlive = false;
           
            BattleSystem.BS.aliveEnemies--;
            this.gameObject.SetActive(false);
            
        }
      

        


    }

    public void Poison()
    {   
        if (poisonRounds>0)
        {
            currentHP -= poisonDmg;
            poisonRounds--;
            if (currentHP <= 0&& isAlive ==true)
            {
                isAlive = false;
           
                BattleSystem.BS.aliveEnemies--;
                this.gameObject.SetActive(false);
            
            }

        }
    }

}
