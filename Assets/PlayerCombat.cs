using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	public Animator animator;
	
	public Transform attackPointR;
	public Transform attackPointL;
	public float attackRange = 0.5f;
	
	public LayerMask enemyLayers;

	public int attackDamage = 40;

    // Update is called once per frame
    void Update()
    {
	if (Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.J)){ 
		AttackBoth();
}
        else if (Input.GetKeyDown(KeyCode.K)){ 
		Attack();
}
else if (Input.GetKeyDown(KeyCode.J)){ 
		AttackLeft();
}
	
    } //end of update
	void Attack(){
	//play an attack animation
	animator.SetTrigger("Attack");
	//detect all enemies in range and apply damage
	Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointR.position, attackRange, enemyLayers);
	//damage them
	foreach(Collider2D enemy in hitEnemies){
	Debug.Log("We hit " + enemy.name);
	enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
}
}

void AttackLeft(){
	//play an attack animation on left side of crab
	animator.SetTrigger("AttackLeft");
	//detect all enemies in range and apply damage
	Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointL.position, attackRange, enemyLayers);
	//damage them
foreach(Collider2D enemy in hitEnemies){
	Debug.Log("We hit " + enemy.name);
	enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
}
}
void AttackBoth(){
	//play an attack animation on both sides of crab
	animator.SetTrigger("AttackBoth");
	//detect all enemies in range and apply damage
Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointR.position, attackRange, enemyLayers);
	//damage them
foreach(Collider2D enemy in hitEnemies){
	Debug.Log("We hit " + enemy.name);
	enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
}
}

void OnDrawGizmosSelected()
{
	if (attackPointR == null){
	return;
}
if (attackPointL == null){
	return;
}
	Gizmos.DrawWireSphere(attackPointR.position, attackRange);
	Gizmos.DrawWireSphere(attackPointL.position, attackRange);
}
}
