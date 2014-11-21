using UnityEngine;
using System.Collections;
using Pathfinding;

public class ZombieAI : MonoBehaviour
{
    public Seeker seeker;
    public Animation anims;
    public Transform player;

    private AIPath ai;

    int health = 100;

    // Use this for initialization
    void Start()
    {
        ai = GameObject.Find("tyrant _zombie").GetComponent<AIPath>();
    }

    bool dead = false;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (!dead) { anims.Play("death"); dead = true; }

            //if(!anims.IsPlaying("death")) Destroy(this.gameObject);
            ai.canSearch = false;
            return;
        }

        if (anims.IsPlaying("damage02") || anims.IsPlaying("attack02")) return;

        float dist = Vector3.Distance(this.transform.position, player.position);

        //Debug.Log(dist);

        if (dist > 3) anims.Play("walk02");
        else
        {
            anims.Play("attack02");
        }
        ai.canMove = (dist > 3);
    }

    public void TakeDamage()
    {
        anims.Play("damage02");
        ai.canMove = false;

        health -= Random.Range(30, 50);
    }

    public bool isDead() { return dead; }
}