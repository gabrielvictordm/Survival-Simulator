using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoGema : MonoBehaviour
{
    public int tipogema;
    public int array;

    public GameObject[] gemaUI;

    public void SetGem()
    {   
        PlayerStats.instance.pickaxeGems[array] = tipogema;
        for (int i = 0; i < gemaUI.Length; i++)
        {
            gemaUI[i].SetActive(false);
        }

        gemaUI[tipogema].SetActive(true);


    }

    public void ClearGem()
    {
        PlayerStats.instance.pickaxeGems[array] = 0;
        for (int i = 0; i < 3; i++)
        {   
            gemaUI[i].SetActive(false);
        }
    }
}
