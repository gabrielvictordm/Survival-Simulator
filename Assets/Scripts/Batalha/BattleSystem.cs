using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;


public class BattleSystem : MonoBehaviour
{
    public static BattleSystem BS { get; private set;}
    private void Awake()
    {   if (BS != null && BS!= this)
        {
            Destroy(gameObject);
        }
        else
        {
            BS = this;
        }
        
      
    }
    
    public enum BattleState
    {
        START,
        PLAYERTURN,
        ENEMYTURN,
        SELECTATTACK,
        WON,
        LOST
    }

    public int aliveEnemies =2;
    public int target=0;
    


    public Animator playerAnimAttack;
    public Animator enemyAnimAttack1;
    public Animator enemyAnimAttack2;

    public GameObject playerPrefab;
    public GameObject[] enemyPrefabs;

    public Transform playerBattleStation;
    public Transform EnemyBattleStation1;
    public Transform EnemyBattleStation2;

    

    private PlayerUnit playerUnit;
    private Unit enemyUnit1;
    private Unit enemyUnit2;

    public Text startText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD1;
    public BattleHUD enemyHUD2;
    

    public BattleState state;

    public GameObject singleBT;
    public GameObject multipleBT;
    public GameObject powerBT;
    public GameObject shieldBT;
    public GameObject btTarget1;
    public GameObject btTarget2;
    public GameObject btTargetPower1;
    public GameObject btTargetPower2;
    public GameObject returnBT;
    public Slider furySlider;
    public Button singleColor;
    public Button multipleColor;
    public Button powerColor;

    private Color bluegem = new Color(9f / 255f, 132f / 255f, 227f / 255f, 1f);
    private Color greengem = new Color(0f,184f/255f,94f/255f,1f);
    private Color redgem = new Color(214f/255,48f/255f,49f/255f,1f);
    public void SetFury()
    {
        furySlider.value = playerUnit.fury;
    }

   
    

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    void setButtonColors()
    {
        switch (playerUnit.equipedGemSingle)
        {
           case "Blue": singleBT.GetComponent<Image>().color = bluegem;
               break;
           case "Green": singleBT.GetComponent<Image>().color = greengem;
               Debug.Log(greengem);
               break;
           case "Red": singleBT.GetComponent<Image>().color = redgem;
               Debug.Log(redgem);
               break;
           default: singleBT.GetComponent<Image>().color = Color.white;
               break;
        }
        switch (playerUnit.equipedGemMultiple)
        {
            case "Blue": multipleBT.GetComponent<Image>().color = bluegem;
                Debug.Log(bluegem);
                break;
            case "Green": multipleBT.GetComponent<Image>().color = greengem;
                Debug.Log(greengem);
                break;
            case "Red": multipleBT.GetComponent<Image>().color = redgem;
                Debug.Log(redgem);
                break;
            default: multipleBT.GetComponent<Image>().color = Color.white;
               
                break;
        }
        switch (playerUnit.equipedGemPower)
        {
            case "Blue": powerBT.GetComponent<Image>().color = bluegem;
                Debug.Log(bluegem);
                break;
            case "Green": powerBT.GetComponent<Image>().color = greengem;
                Debug.Log(greengem);
                break;
            case "Red": powerBT.GetComponent<Image>().color = redgem;
                Debug.Log(redgem);
                break;
            default: powerBT.GetComponent<Image>().color = Color.white;
                
                break;
        }
        
    }
    
    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation.transform);
        playerUnit = playerGO.GetComponent<PlayerUnit>();
        playerAnimAttack = playerGO.GetComponent<Animator>();
        GameObject enemyGo1 = Instantiate(enemyPrefabs[TipoInimigo.TI.tipoInimigo1], EnemyBattleStation1.transform);
        enemyUnit1 = enemyGo1.GetComponent<Unit>();
        enemyAnimAttack1 = enemyGo1.GetComponent<Animator>();
        GameObject enemyGo2 = Instantiate(enemyPrefabs[TipoInimigo.TI.tipoInimigo2], EnemyBattleStation2.transform);
        enemyUnit2 = enemyGo2.GetComponent<Unit>();
        enemyAnimAttack2 = enemyGo2.GetComponent<Animator>();
        playerUnit.LoadStats();
       
        
        startText.text = "A Wild " + enemyUnit1.unitName +" and "+ enemyUnit2.unitName +" appeared";

        playerHUD.SetPlayerHUD(playerUnit);
        enemyHUD1.SetEnemyHUD(enemyUnit1);
        enemyHUD2.SetEnemyHUD(enemyUnit2);
        SetFury();
        
        
        yield return new WaitForSeconds(2);
        setButtonColors();
        

        state = BattleState.PLAYERTURN;
        
        PlayerTurn();
    }
 void PlayerTurn()
 {
     playerUnit.fury += 10;
     SetFury();
        playerUnit.shield = 0;
        startText.text = "Choose an action: ";
        if (playerUnit.fury >= playerUnit.singleCost)
        {
            singleBT.SetActive(true);
        }

        if (playerUnit.fury >= playerUnit.multipleCost)
        {
            multipleBT.SetActive(true);
        }

        if (playerUnit.fury >= playerUnit.powerSingleCost)
        {
            powerBT.SetActive(true);
        }
       
        shieldBT.SetActive(true);
        
    }

 
 
    IEnumerator SingleAttack()
    {   state = BattleState.PLAYERTURN;
        returnBT.SetActive(false);
        
        playerUnit.fury -= playerUnit.singleCost;
        SetFury();
        if (target == 1)
        {
                    enemyUnit1.TakeDamage(playerUnit.damageSingle,playerUnit.equipedGemSingle,playerUnit);
                    startText.text = playerUnit.unitName + " is Attacking";
                    playerAnimAttack.SetBool("Attacking", true);
                    DeActivateBt();
                    yield return new WaitForSeconds(1f);

                    startText.text = "The attack is Successful";
                    

                    yield return new WaitForSeconds(1);
                    playerAnimAttack.SetBool("Attacking", false);
                    enemyHUD1.SetHP(enemyUnit1.currentHP);
                    enemyHUD1.SetDebuffs(enemyUnit1);
                    
                    yield return new WaitForSeconds(.5f);
                    playerHUD.SetHP(playerUnit.currentHP);
                    
                    

                    if (aliveEnemies<=0)
                    {
                        state = BattleState.WON;
                        EndBattle();
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
        }
        else if (target == 2)
        {
            enemyUnit2.TakeDamage(playerUnit.damageSingle,playerUnit.equipedGemSingle,playerUnit);
            startText.text = playerUnit.unitName + " is Attacking";
            playerAnimAttack.SetBool("Attacking", true);
            DeActivateBt();
            yield return new WaitForSeconds(1f);
            startText.text = "The attack is Successful";
            yield return new WaitForSeconds(1);
            playerAnimAttack.SetBool("Attacking", false);
            enemyHUD2.SetHP(enemyUnit2.currentHP);
            enemyHUD2.SetDebuffs(enemyUnit2);
            yield return new WaitForSeconds(.5f);
            playerHUD.SetHP(playerUnit.currentHP);

            if (aliveEnemies<=0)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }

        target = 0;


    }
    IEnumerator PowerAttack()
    {   state = BattleState.PLAYERTURN;
        returnBT.SetActive(false);
        playerUnit.fury -= playerUnit.powerSingleCost;
        SetFury();
        if (target == 1)
        {  DeActivateBt();
            //INSTANCE 1
            enemyUnit1.TakeDamage(playerUnit.damagePower,playerUnit.equipedGemPower,playerUnit);
            startText.text = playerUnit.unitName + " first strike";
            playerAnimAttack.SetBool("Attacking", true);
            
            yield return new WaitForSeconds(1f);
            enemyHUD1.SetHP(enemyUnit1.currentHP);
            enemyHUD1.SetDebuffs(enemyUnit1);
            yield return new WaitForSeconds(.5f);
            playerHUD.SetHP(playerUnit.currentHP);
            playerAnimAttack.SetBool("Attacking", false);
            yield return new WaitForSeconds(1f);
            //INSTANCE 2
            enemyUnit1.TakeDamage(playerUnit.damagePower,playerUnit.equipedGemPower,playerUnit);
            startText.text = playerUnit.unitName + " second strike";
            playerAnimAttack.SetBool("Attacking", true);
            
          
            
            yield return new WaitForSeconds(1f);
            enemyHUD1.SetHP(enemyUnit1.currentHP);
            enemyHUD1.SetDebuffs(enemyUnit1);
            yield return new WaitForSeconds(.5f);
            playerHUD.SetHP(playerUnit.currentHP);
            playerAnimAttack.SetBool("Attacking", false);
            yield return new WaitForSeconds(1f);
            //INSTANCE 3
            enemyUnit1.TakeDamage(playerUnit.damagePower,playerUnit.equipedGemPower,playerUnit);
            startText.text = playerUnit.unitName + " third strike";
            playerAnimAttack.SetBool("Attacking", true);
            
            
            
            yield return new WaitForSeconds(2f);
            playerAnimAttack.SetBool("Attacking", false);
            enemyHUD1.SetHP(enemyUnit1.currentHP);
            enemyHUD1.SetDebuffs(enemyUnit1);
            yield return new WaitForSeconds(.5f);
            playerHUD.SetHP(playerUnit.currentHP);
            

            yield return new WaitForSeconds(1);
            

            if (aliveEnemies<=0)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else if (target == 2)
        {  DeActivateBt();
            //INSTANCE 1
            enemyUnit2.TakeDamage(playerUnit.damagePower,playerUnit.equipedGemPower,playerUnit);
            startText.text = playerUnit.unitName + " first strike";
            playerAnimAttack.SetBool("Attacking", true);
           
            
            
            yield return new WaitForSeconds(1f);
            enemyHUD2.SetHP(enemyUnit2.currentHP);
            enemyHUD2.SetDebuffs(enemyUnit2);
            yield return new WaitForSeconds(.5f);
            playerHUD.SetHP(playerUnit.currentHP);
            playerAnimAttack.SetBool("Attacking", false);
            yield return new WaitForSeconds(1f);
            //INSTANCE 2
            enemyUnit2.TakeDamage(playerUnit.damagePower,playerUnit.equipedGemPower,playerUnit);
            startText.text = playerUnit.unitName + " second strike";
            playerAnimAttack.SetBool("Attacking", true);
           
            
            
            yield return new WaitForSeconds(1f);
            enemyHUD2.SetHP(enemyUnit2.currentHP);
            enemyHUD2.SetDebuffs(enemyUnit2);
            yield return new WaitForSeconds(.5f);
            playerHUD.SetHP(playerUnit.currentHP);
            playerAnimAttack.SetBool("Attacking", false);
            yield return new WaitForSeconds(1f);
            //INSTANCE 3
            enemyUnit2.TakeDamage(playerUnit.damagePower,playerUnit.equipedGemPower,playerUnit);
            startText.text = playerUnit.unitName + " third strike";
            playerAnimAttack.SetBool("Attacking", true);
            
            
           
            yield return new WaitForSeconds(1f);
            enemyHUD2.SetHP(enemyUnit2.currentHP);
            enemyHUD2.SetDebuffs(enemyUnit2);
            yield return new WaitForSeconds(.5f);
            playerHUD.SetHP(playerUnit.currentHP);

            playerAnimAttack.SetBool("Attacking", false);

            yield return new WaitForSeconds(1f);
            

            if (aliveEnemies<=0)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }

        target = 0;


    }

    void DeActivateBt()
    {
        singleBT.SetActive(false);
        multipleBT.SetActive(false);
        shieldBT.SetActive(false);
        btTarget1.SetActive(false);
        btTarget2.SetActive(false);
        btTargetPower1.SetActive(false);
        btTargetPower2.SetActive(false);
        returnBT.SetActive(false);
    }

    IEnumerator MultipleAttack()
    {
        state = BattleState.PLAYERTURN;
        playerUnit.fury -= playerUnit.multipleCost;
        SetFury();
        enemyUnit1.TakeDamage(playerUnit.damageMultiple,playerUnit.equipedGemMultiple,playerUnit);
        enemyUnit2 .TakeDamage(playerUnit.damageMultiple,playerUnit.equipedGemMultiple,playerUnit);
        startText.text = playerUnit.unitName + " is Attacking " + enemyUnit1.unitName + " and " + enemyUnit2.unitName;
        playerAnimAttack.SetBool("Attacking", true);
        singleBT.SetActive(false);
        multipleBT.SetActive(false);
        shieldBT.SetActive(false);
        powerBT.SetActive(false);
        yield return new WaitForSeconds(1f);

        startText.text = "The attack is Successful";

        yield return new WaitForSeconds(1);
        playerAnimAttack.SetBool("Attacking", false);
        enemyHUD1.SetHP(enemyUnit1.currentHP);
        enemyHUD1.SetDebuffs(enemyUnit1);
        enemyHUD2.SetHP(enemyUnit2.currentHP);
        enemyHUD2.SetDebuffs(enemyUnit2);
        yield return new WaitForSeconds(.5f);
        playerHUD.SetHP(playerUnit.currentHP);

        if (aliveEnemies<=0)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void  SelectTarget(int n) //Chamar no BT attack1
    {
        singleBT.SetActive(false);
        multipleBT.SetActive(false);
        shieldBT.SetActive(false);
        powerBT.SetActive(false);
        returnBT.SetActive(true);
       
        
        
        
        startText.text = "Choose a target";
        if (enemyUnit1.isAlive == true)
        {   switch(n)

            {
                case 1: 
                    btTarget1.SetActive(true); 
                    break;
                case 2: 
                    btTargetPower1.SetActive(true); 
                    break;
            }
           
        }
        if (enemyUnit2.isAlive == true)
        {    switch(n)

            {
                case 1: 
                    btTarget2.SetActive(true); 
                    break;
                case 2: 
                    btTargetPower2.SetActive(true); 
                    break;
            }
            
        }
        
        

    }
    public void ReturnBT()
    {
        state = BattleState.PLAYERTURN;
        btTarget1.SetActive(false);
        btTarget2.SetActive(false);
        btTargetPower1.SetActive(false);
        btTargetPower2.SetActive(false);
        returnBT.SetActive(false);
        PlayerTurn();
        
    }

    public void AttackEnemy1()
    {
        target = 1;
        StartCoroutine(SingleAttack());
    }
    public void AttackEnemy2()
    {
        target = 2;
        StartCoroutine(SingleAttack());
    }
    public void AttackEnemy1Power()
    {
        target = 1;
        StartCoroutine(PowerAttack());
    }
    public void AttackEnemy2Power()
    {
        target = 2;
        StartCoroutine(PowerAttack());
    }

    IEnumerator EnemyTurn()
    { //turno inimigo 1
        if (enemyUnit1.isAlive == true)
        { 
            if (enemyUnit1.poisonRounds > 0)
            {   enemyHUD1.SetDebuffs(enemyUnit1);
                enemyUnit1.Poison();
                startText.text = enemyUnit1.unitName + " is poisoned";
                yield return new WaitForSeconds(1f);
                enemyHUD1.SetHP(enemyUnit1.currentHP);
                enemyHUD1.SetDebuffs(enemyUnit1);

                yield return new WaitForSeconds(1f);

            }
            startText.text = enemyUnit1.unitName + " attacks";
                    enemyAnimAttack1.SetBool("Attacking", true);
                    yield return new WaitForSeconds(1f);
                     playerUnit.TakeDamage(enemyUnit1.damage,enemyUnit1);
                     
            
            
            
                    yield return new WaitForSeconds(1f);
                    playerHUD.SetHP(playerUnit.currentHP);
                    enemyHUD1.SetDebuffs(enemyUnit1);
                    enemyAnimAttack1.SetBool("Attacking", false);
                    if (playerUnit.currentHP<=0)
                    {
                        state = BattleState.LOST;
                        EndBattle();
                    }
                   
        }

        yield return new WaitForSeconds(.5f);
        
        // turno inimigo 2
        if (enemyUnit2.isAlive == true)
        {    if (enemyUnit2.poisonRounds > 0)
            {   enemyHUD2.SetDebuffs(enemyUnit2);
                enemyUnit2.Poison();
                startText.text = enemyUnit2.unitName + " is poisoned";
                yield return new WaitForSeconds(1f);
                enemyHUD2.SetHP(enemyUnit2.currentHP);
                enemyHUD2.SetDebuffs(enemyUnit2);
                yield return new WaitForSeconds(1f);
                if (enemyUnit2.isAlive == false)
                {
                    
                }

            }
            startText.text = enemyUnit2.unitName + " attacks";
                    enemyAnimAttack2.SetBool("Attacking", true);
                    yield return new WaitForSeconds(1f);
                     playerUnit.TakeDamage(enemyUnit2.damage,enemyUnit2);
            
            
            
                    yield return new WaitForSeconds(1f);
                    playerHUD.SetHP(playerUnit.currentHP);
                    enemyHUD2.SetDebuffs(enemyUnit2);
                    enemyAnimAttack2.SetBool("Attacking", false);
                    if (playerUnit.currentHP<=0)
                    {
                        state = BattleState.LOST;
                        EndBattle();
                    }
                   
        }
        state = BattleState.PLAYERTURN;
        PlayerTurn();
        
    }

    void EndBattle()
    {
        singleBT.SetActive(false);
        multipleBT.SetActive(false);
        shieldBT.SetActive(false);
        powerBT.SetActive(false);
        if (state == BattleState.WON)
        {
            startText.text = "You won the Battle";
            playerAnimAttack.SetTrigger("BackFlip");
            DeActivateBt();
            if (CombatTransition.instance.currentenemy ==0)
            {
                CombatTransition.instance.azuldead = true;
            }
            if (CombatTransition.instance.currentenemy ==1)
            {
                CombatTransition.instance.vermeldead = true;
            }
            if (CombatTransition.instance.currentenemy ==2)
            {
                CombatTransition.instance.verdedead = true;
            }

            if (CombatTransition.instance.currentenemy == 3)
            {
                CombatTransition.instance.golemDeadd = true;
            }

            StartCoroutine(ChangeScene());
        }
        else if (state == BattleState.LOST)
        {
            startText.text = "You lost the Battle";
            enemyAnimAttack1.SetTrigger("EBackFlip");
            enemyAnimAttack2.SetTrigger("EBackFlip");
            DeActivateBt();
            StartCoroutine(ChangeScene());
        }
    }
    
    public void Attack1Button()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;

        }

        state = BattleState.SELECTATTACK;
        
        SelectTarget(1);

    }
    public void PowerAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;

        }

        state = BattleState.SELECTATTACK;
        
        SelectTarget(2);

    }
    

    public void MultipleAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;

        }
        
        StartCoroutine(MultipleAttack());
    }
    

    public void Shield()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;

            
        }
        singleBT.SetActive(false);
        multipleBT.SetActive(false);
        shieldBT.SetActive(false);
        powerBT.SetActive(false);
        playerUnit.shield = 0.25f;
        StartCoroutine(EnemyTurn());

    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(5);
        CombatTransition.instance.ChangeScene();
    }
}
