using UnityEngine;
using System.Collections;
using Utilities;

public class PlayerBehavior : MonoBehaviour
{
     public float walkSpeed = 7; // regular speed
     public float crchSpeed = 3; // crouching speed
     public float runSpeed = 20; // run speed

     public Animation anims;
     public ParticleSystem particle;

     public Camera mainCamera;
     public CharacterController cController;

     //private MouseLook mouse;
     private CharacterMotor chMotor;

     private bool zoomed = false;
     private bool reloading = false;
     private bool crouching = false;

     private int health = 100;

     // Use this for initialization
     void Start()
     {
          Screen.lockCursor = true;

          //mouse = Camera.main.GetComponent<MouseLook>();
          chMotor = GetComponent<CharacterMotor>();
     }

     // Update is called once per frame
     void Update()
     {
          if (health <= 0)
          {
               Time.timeScale = 0;
               chMotor.canControl = false;
               return;
          }

          //float vScale = 1.0f;
          float speed = walkSpeed;

          if (Input.GetButton("Fire1"))
          {
               string anim = (zoomed) ? "shoot_zoom" : "shoot";

               if (!anims.IsPlaying(anim) && anims.Play(anim))
               {
                    RaycastHit hit = RayCast.Raycast(GameObject.Find("flashlight").transform, 100);

                    GameObject objHit = hit.transform.gameObject;

                    ZombieAI zombie = objHit.GetComponentInParent<ZombieAI>();

                    if (!zombie.isDead())
                    {
                         if (objHit.tag == "ZombieHead") zombie.TakeDamage(60);
                         if (objHit.tag == "ZombieCollider") zombie.TakeDamage(40);
                    }
               }
          }

          if (Input.GetButton("Sprint") && !crouching) speed = runSpeed;

          if (Input.GetButtonDown("Crouch")) crouching = !crouching;

          if (Input.GetButton("Reload") || (Input.GetButton("Crouch") && Input.GetButton("Reload")))
          {
               if (zoomed)
               {
                    anims.Play("unzoom");
                    anims.PlayQueued("reload");
                    zoomed = false;
               }
               anims.Play("reload");
          }

          if (anims.IsPlaying("reload")) reloading = true;
          else reloading = false;

          if (Input.GetMouseButton(1))
          {
               if (zoomed || reloading) return;
               anims.Play("zoom");
               zoomed = true;
          }
          else
          {
               if (!zoomed) return;
               anims.Play("unzoom");
               zoomed = false;
          }

          chMotor.movement.maxForwardSpeed = speed;
          if (!anims.IsPlaying("shoot") && !anims.IsPlaying("shoot_zoom") && !anims.IsPlaying("reload") && !anims.IsPlaying("unzoom") && !anims.IsPlaying("zoom")) anims.Play("idle");
     }

     void OnGUI()
     {
          if (health > 0) return;

          string text = "You died!";
          Vector2 textDims = GUI.skin.label.CalcSize(new GUIContent(text));
          float x = (Screen.width / 2) - (textDims.x / 2);
          float y = (Screen.height / 2) - (textDims.y / 2);

          GUI.Label(new Rect(x, y, Screen.width, 100), text);
     }

     public void TakeDamage(int damage)
     {
          health -= damage;
     }
}