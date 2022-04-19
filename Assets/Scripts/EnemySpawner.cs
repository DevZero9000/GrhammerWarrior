using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public int enemyCount;
    float randX;
    Vector2 whereToSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Update()
    {
        if(enemyCount <= 6)
        {
            randX = Random.Range(23f, 96f);
            whereToSpawn = new Vector2(randX, transform.position.y);
            Instantiate(Enemy, whereToSpawn, Quaternion.identity);
            enemyCount+=1;

        }
        //else if ( enemyCount >= 6)
        
        //    DestroyObject(Enemy);
        
    }
}
