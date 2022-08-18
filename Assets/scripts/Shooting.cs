using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private float shootTimer;

    private float coolDown = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer > coolDown)
        {
            shootTimer = 0f;

            Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
        }
    }
}
