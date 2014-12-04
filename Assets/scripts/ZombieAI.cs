using UnityEngine;
using System.Collections;
using Pathfinding;
using Utilities;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Animation))]
public class ZombieAI : MonoBehaviour
{
     public enum AIState
     {
          IDLE,
          CHASE,
          ATTACK,
          WALK_AROUND,
          DEAD
     }

     [System.NonSerialized]
     public AIState state = AIState.WALK_AROUND;

     [System.NonSerialized]
     public Seeker seeker;

     private Transform player;
     private PlayerBehavior playerScript;

     private AIPath ai;

     int health = 100;

     // Use this for initialization
     void Start()
     {
          ai = GetComponent<AIPath>();
          seeker = GetComponent<Seeker>();
          player = GameObject.FindGameObjectWithTag("Player").transform;
          playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();

          ai.target = player;
     }

     bool dead = false;

     // Update is called once per frame
     void Update()
     {
          AIState state = getState();

          switch (state)
          {
               case AIState.ATTACK:
                    if (!animation.IsPlaying("attack02")) animation.Play("attack02");

                    RaycastHit ray = RayCast.Raycast(transform, player, 3);

                    if (ray.transform.gameObject.tag == "Player")
                    {
                         playerScript.TakeDamage(50);
                    }
                    ai.canMove = false;
                    break;
               case AIState.CHASE:
                    ai.canMove = true;
                    ai.canSearch = true;
                    if (!animation.IsPlaying("run")) animation.Play("run");
                    break;
               case AIState.WALK_AROUND:
                    // to do
                    animation.PlayQueued("walk02");
                    ai.canSearch = false;
                    break;
               case AIState.DEAD:
                    if (!dead) { animation.Play("death"); dead = true; }
                    ai.canSearch = false;
                    break;
               case AIState.IDLE:
                    if (!animation.IsPlaying("idle")) animation.Play("idle");
                    ai.canSearch = false;
                    break;
          }
     }

     public AIState getState()
     {
          float dist = Vector3.Distance(this.transform.position, player.position);

          //if (dist > 10 && health > 0) return AIState.WALK_AROUND;
          if (dist > 3 && dist < 10 && health > 0) return AIState.CHASE;
          if (dist < 3 && health > 0) return AIState.ATTACK;
          if (health <= 0) return AIState.DEAD;

          return AIState.IDLE;
     }

     public void TakeDamage(int damage)
     {
          animation.Play("damage02");
          ai.canMove = false;

          health -= damage;
     }

     public bool isDead() { return dead; }
}