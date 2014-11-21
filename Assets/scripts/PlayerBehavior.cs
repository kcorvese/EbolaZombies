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

    private CharacterMotor chMotor;
    //private Transform tr;
    private float dist; // distance to ground

    private bool zoomed = false;
    private bool reloading = false;
    private bool crouching = false;

    // Use this for initialization
    void Start()
    {
        Screen.lockCursor = true;

        chMotor = GetComponent<CharacterMotor>();
        //tr = transform;
        //CharacterController ch = GetComponent<CharacterController>();
        //dist = ch.height / 2; // calculate distance to ground
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float vScale = 1.0f;
        float speed = walkSpeed;

        if (Input.GetButton("Fire1"))
        {
            string anim = (zoomed) ? "shoot_zoom" : "shoot";

            if (!anims.IsPlaying(anim) && anims.Play(anim))
            {
                RaycastHit hit = RayCast.Raycast();

                GameObject objHit = hit.transform.gameObject;

                if(objHit.tag == "Zombie")
                {
                    ZombieAI zombie = objHit.GetComponent<ZombieAI>();
                    if (!zombie.isDead()) zombie.TakeDamage();
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
        /*vScale = (crouching) ? 0.5f : 1.0f;
        speed = (crouching) ? crchSpeed : walkSpeed; // slow down when crouching

        chMotor.movement.maxForwardSpeed = speed; // set max speed
        float ultScale = tr.localScale.y; // crouch/stand up smoothly 

        Vector3 tmpScale = tr.localScale;
        Vector3 tmpPosition = tr.position;

        tmpScale.y = Mathf.Lerp(tr.localScale.y, vScale, 5 * Time.deltaTime);
        tr.localScale = tmpScale;

        tmpPosition.y += dist * (tr.localScale.y - ultScale); // fix vertical position        
        tr.position = tmpPosition;*/
        if (!anims.IsPlaying("shoot") && !anims.IsPlaying("shoot_zoom") && !anims.IsPlaying("reload") && !anims.IsPlaying("unzoom") && !anims.IsPlaying("zoom")) anims.Play("idle");
    }

    void OnCollisionEnter(Collision col)
    {

    }
}