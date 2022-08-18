using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 200f;

    [SerializeField]
    private Vector2 direction;

    [SerializeField]
    private float lifeTime = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }

    internal void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void Awake()
    {
        Invoke("DestroySelf", lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DestroySelf();
    }
}
