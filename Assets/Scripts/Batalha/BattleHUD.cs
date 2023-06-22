using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
   public Text nameText;
   public Text leveltext;
   public Slider hpSlider;

   public Text enemyTxt;
   public GameObject poison;
   public GameObject debuff;


   public void SetDebuffs(Unit unit)
   {
      if (unit.poisonRounds>0) poison.SetActive(true);
      else poison.SetActive(false);
      if (unit.debuffRounds > 0) debuff.SetActive(true);
      else debuff.SetActive(false);
   }
   public void SetEnemyHUD(Unit unit)
   {
      nameText.text = unit.unitName;
      leveltext.text = "Lvl " + unit.level;
      hpSlider.maxValue = unit.maxHP;
      hpSlider.value = unit.currentHP;
      enemyTxt.text = unit.unitName;
      

   }
   public void SetPlayerHUD(PlayerUnit playerUnit)
   {
      nameText.text = playerUnit.unitName;
      leveltext.text = "Lvl " + playerUnit.level;
      hpSlider.maxValue = playerUnit.maxHP;
      hpSlider.value = playerUnit.currentHP;

   }

   public void SetHP(float hp)
   {
      hpSlider.value = hp;
      
   }
   
   
}
