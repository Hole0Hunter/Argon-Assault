using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyExplosionVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 10;
    [SerializeField] int hitPoints = 2;

    GameObject parentGameObject;
    ScoreBoard scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        KillEnemy();
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        hitPoints--;
    }

    void KillEnemy()
    {
        if(hitPoints == 0)
        {
            scoreBoard.IncreaseScore(scorePerHit);
            GameObject vfx = Instantiate(enemyExplosionVFX, transform.position, Quaternion.identity);
            vfx.transform.parent = parentGameObject.transform;
            Destroy(gameObject);
        }
    }

}
