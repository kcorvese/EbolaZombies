using UnityEngine;
using System.Collections;
using Utilities;

public class Spawner : MonoBehaviour 
{
    public ZombieAI spawn;

    private Timer timer;

    public int amount = 0;
    public float timeBetweenSpawns = 0;
    public bool randamizeSpawns = true;
    public float spawnRadius = 0;

    private int spawns = 0;


	// Use this for initialization
	void Start () 
    {
        timer = new Timer(timeBetweenSpawns);
        timer.start();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (amount == spawns) return;

        if(timer.getRemaining() <= 0)
        {
            Vector3 pos = transform.position;

            if(randamizeSpawns)
            {
                pos = new Vector3(transform.position.x + Random.Range(-(spawnRadius / 2), (spawnRadius / 2)), 
                    transform.position.y,
                    transform.position.z + Random.Range(-(spawnRadius / 2), (spawnRadius / 2)));
            }

            Instantiate(spawn, pos, transform.rotation);
            timer.reset();
            spawns++;
        }
	}

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.white;

        Vector3 pos = transform.position;
        pos.y = pos.y + 0.2f;

        Gizmos.DrawWireCube(pos, new Vector3(spawnRadius, 0, spawnRadius));


   
    }
}