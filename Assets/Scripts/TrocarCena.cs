using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCena : MonoBehaviour
{   
    public int enemyNumber1;
    public int enemyNumber2;
    public int currentEnemy;

    public Transform thisPosition;

    public string nomeDaCena;
    
    
    

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("colidiu");
            TipoInimigo.TI.tipoInimigo1 = enemyNumber1;
            TipoInimigo.TI.tipoInimigo2 = enemyNumber2;
            CombatTransition.instance.currentenemy = currentEnemy;
            CombatTransition.instance.lastPosition = thisPosition;
            SceneManager.LoadScene(nomeDaCena);
            Destroy(this.gameObject);
            

        }
    }
}