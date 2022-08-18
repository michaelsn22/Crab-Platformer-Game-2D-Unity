using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    private int MAX_HEALTH = 100;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            Damage(3);
        }
        if(Input.GetKeyDown(KeyCode.K)){
            Damage(3);
        }
    }

    public void Damage(int amount)
    {
        if(amount < 0){
            //throw new System.ArguementOutOfRangeException("Cannot have negative Damage.");
            Debug.Log("Error: Cannot have negative Damage.");
        }
        this.health -= amount;

        if(health <= 0){
            Die();
        }
    }

    public void Heal(int amount)
    {
        if(amount < 0){
            //throw new System.ArguementOutOfRangeException("Cannot have negative Healing.");
            Debug.Log("Error: Cannot have negative Heal.");
        }
        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;

        if(wouldBeOverMaxHealth){
            this.health = MAX_HEALTH;
        }
        else {
            this.health += amount;
        }
        
    }

    private void Die() {
        Debug.Log("I am dead");
        Destroy(gameObject);
        //PlayerDied();
    }

    private void PlayerDied() {
        //levelManager.instance.GameOver();
        //gameObject.SetActive(false);
    }
}
