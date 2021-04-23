using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************************************************
 * References:
 * Movement: https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
 * Swinging: https://pastebin.com/CrBAPDxZ
 * Raycasting from camera: https://docs.unity3d.com/Manual/CameraRays.html
 ************************************************************************************/
public class SpiderMove : MonoBehaviour
{
    CharacterController characterController;
    public bool canMove = true;

    public float speed = 6.0f;
    public float jumpSpeed = 5.0f;
    public float gravity = 9.0f;

    public Camera mainCamera;
    public Transform hookAnchor;
    public float ropeLength = 0f;
    public LineRenderer web;
    //public float maxVelocity = 10f;
    //public float swingForwardSpeed = 10f;
    public float swingStrafeSpeed = 50f;

    private Vector3 hitPoint;
    private Vector3 moveDirection = Vector3.zero;
    private bool canGrapple = false;
    private bool Swinging = false;
    private float dist;
    private Vector3 newVel;
    private Rigidbody rb;
    private float distToGround;
    private bool haveJumped = false;
    public bool safe = false;

    public Animator spiderAnimator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        web.enabled = false;
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    void Update()
    {
        if (canMove)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed, moveDirection.y, 0.0f);

            if (characterController.isGrounded)
            {
                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                    haveJumped = true;
                    spiderAnimator.SetBool("Jumping", true);
                }
            }

            // Apply gravity. 
            if (Swinging == false || haveJumped)
            {
                print("applying gravity");
                moveDirection.y -= gravity * Time.deltaTime;
                haveJumped = false;
            }

            // Move the spider
            characterController.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxis("Horizontal") != 0.0f)
            {
                spiderAnimator.SetBool("Walking", true);
            }

            else
            {
                spiderAnimator.SetBool("Walking", false);
            }

            Swing();

            //update length of web
            web.SetPosition(0, this.transform.position);
            web.SetPosition(1, hookAnchor.transform.position);
        }

        spiderAnimator.SetBool("Jumping", false);
    }

    private void FixedUpdate()
    {
        if (!characterController.isGrounded && Swinging && transform.position.y <= hookAnchor.position.y)
        {
            print("inside fixed up 1st if");
            moveDirection.y = -4.0f;
            Debug.Log(moveDirection.y);
            if (moveDirection.x > 0 /*&& rb.velocity.y < 0f*/)
            {
                rb.AddForce(transform.right /** moveDirection.x*/ * swingStrafeSpeed);
            }
        }
        else if (!characterController.isGrounded)
        {
            print("inside fixed up 2nd if");
            rb.AddForce(transform.right * moveDirection.x * speed / 2);
        }
    }

    void Swing()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider)
                {
                    hitPoint = hit.point;
                    canGrapple = true;
                    hookAnchor.position = hitPoint;
                    dist = Vector3.Distance(transform.position, hookAnchor.position);
                    ropeLength = dist;
                    canGrapple = true;
                    Swinging = true;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (canGrapple)
            {
                web.enabled = true;
                Vector3 v = transform.position - hookAnchor.position;
                float distance = v.magnitude;
                newVel = v;
                Vector3 myUp = (transform.position - hookAnchor.position).normalized;

                if (distance > ropeLength)
                {
                    newVel.Normalize();
                    v = Vector3.ClampMagnitude(v, ropeLength);
                    transform.position = hookAnchor.position + v;
                    float x = Vector3.Dot(newVel, rb.velocity);
                    newVel *= x;
                    rb.velocity -= newVel;
                }

                //Makes the rope longer
                if (Input.GetKey(KeyCode.Q))
                {
                    ropeLength += .005f;
                    Debug.Log("lengthening rope");
                }

                //Makes the rope shorter
                if (Input.GetKey(KeyCode.E))
                {
                    ropeLength -= .005f;
                    Debug.Log("shortening rope");
                }

                //makes the rope shorter if the player is going to hit the ground
                if (Physics.Raycast(transform.position, Vector3.down, distToGround + 1))
                {
                    ropeLength -= .1f;
                }
            }
        }

        //When the mouse is let go or the player hits the gorund, the player stops moving
        if (Input.GetMouseButtonUp(0))
        {
            web.enabled = false;
            canGrapple = false;
            Swinging = false;
        }
    }

    //The isGrounded Check for the player
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -transform.up, distToGround + .05f);
    }
}
