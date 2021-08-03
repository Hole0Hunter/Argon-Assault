using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyExplosionVFX;
    [SerializeField] Transform parent;

    ScoreBoard scoreBoard;
    [SerializeField]int scorePerHit = 10;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
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

    void KillEnemy()
    {
        GameObject vfx = Instantiate(enemyExplosionVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

    void ProcessHit()
    {
        scoreBoard.IncreaseScore(scorePerHit);
    }
}
