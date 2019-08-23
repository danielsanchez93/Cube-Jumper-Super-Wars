using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class EnemyMovement :  PoolObject
{
    public float speed;
    public bool isHarmful;
    Rigidbody2D rb;
    [SerializeField]
    PoolManager poolManager;

    private void Start()
    {
        Invoke("BackToPool",5);
        rb = GetComponent<Rigidbody2D>();
        poolManager = FindObjectOfType<PoolManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }


    void BackToPool()
    {
        //onPoolObjectFinished += ParticleCall;
        Destroy(gameObject,5);
    }

    void ParticleCall()
    {
        ParticleSystem particleSystem = FindObjectOfType<ParticleSystem>();
        particleSystem.transform.position = transform.position;
        particleSystem.Play();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            if(isHarmful)
            {
                FindObjectOfType<CharacterUIManager>().DecreaseLives();
            }
            else
            {
                FindObjectOfType<CharacterUIManager>().IncreaseScore();
            }
            ParticleCall();
            poolManager.StoreObject(gameObject);
        }
    }

    public override void InitializePoolObject(PoolObjectParams parameters)
    {
        transform.position = parameters.startingPosition;
    }
}
