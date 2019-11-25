using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviourScript : MonoBehaviour
{

    CharacterController characterController;

    public float speed = 0.5f;
    public float gravity = 20.0f;
    public bool isAggressive;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 heading = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Vector3 forward = Vector3.zero;
    private float distance;
    private GameObject player;
    private float timePaused;
    private float calculateDirection;
    private float angle;
    private bool isSeen;
    private RaycastHit hit;
    private float distanceMin;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        player = GameObject.Find("Player");
        isSeen = false;
        calculateDirection = 0.0f;
        if (!isAggressive)
        {
            distanceMin = 1.5f;
        }
        else
        {
            distanceMin = 1.0f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        timePaused -= Time.deltaTime;
        calculateDirection -= Time.deltaTime;
        PlayerInteractionsScript playerScript = player.GetComponent<PlayerInteractionsScript>();

        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // The robot moves toward the player

            heading = player.transform.position - transform.position;
            heading.y = 0;

            //Magnitude is heavy so we don't calculate it each frame
            if (calculateDirection <= 0.0f)
            {
                
                distance = heading.magnitude;
                direction = heading / distance; // This is now the normalized direction.
                calculateDirection = 0.2f;
            }
            

            moveDirection = direction * speed;

            forward = transform.forward;
            angle = Vector3.Angle(forward, heading);

            //If the player is in the vision cone and there is no obstacle between them, the player is seen
            //if (angle < 20.0f)
            //if (angle < 20.0f && !Physics.Linecast(transform.position, player.transform.position))
            Physics.Linecast(transform.position, player.transform.position, out hit);

            try
            {
                GameObject findPlayer = GameObject.Find("Player");
                if (findPlayer && hit.transform.gameObject != null)
                {
                    if (angle < 20.0f && hit.transform.gameObject.tag == "Player" && !isSeen)
                    {
                        isSeen = true;
                    }
                }
                
                
            }
            catch (NullReferenceException ex)
            {
                Debug.Log("Error");
            }



        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        //If the robot has been close enough of the player it will wait until coming again
        if (distance < distanceMin)
        {
            timePaused = 1.5f;

            //If the robot is aggressive and the player isn't invincible, the player lose a life
            if (isAggressive && playerScript.invincible <= 0.0f)
            {
                playerScript.life--;
                playerScript.invincible = 5.0f;
            }
            
        }

        //If the robot is or just was too close or if the robot never saw the player, it doesn't move
        if (timePaused > 0.0f || !isSeen)
        {
            moveDirection.x = 0;
            moveDirection.z = 0;
        }
        characterController.Move(moveDirection * Time.deltaTime);

    }

}

