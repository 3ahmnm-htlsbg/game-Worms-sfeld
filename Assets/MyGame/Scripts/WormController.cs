using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WormController : MonoBehaviour
{
    public float strength;
    public float bulletForce;
    public float speed = 5f;
    public int WormHealth = 5;

    public GameObject worm;
    public GameObject ball;
    public Text healthText;

    //Input Keys
    public KeyCode rightKey;
    public KeyCode leftKey;
    public KeyCode jumpKey;
    public KeyCode triggerKey;
    public KeyCode gunUp;
    public KeyCode gunDown;
    

    private void Update()
    {
        Rigidbody WormRigidbody = worm.gameObject.GetComponent<Rigidbody>();

        //moves player a set amount by vector mutiplicatin, as long as the move key is held down
        worm.transform.position += MovePlayer();

        //jumping by adding upwards force if a key is pressed
        Jump(WormRigidbody);

        //shoot bullet if gun is fired
        if (Input.GetKeyDown(triggerKey))
        {
            ShootBall();
        }
       
        //rotate gun up or down
        Quaternion rotation = Quaternion.Euler(0, 0, rotateGun());
        worm.transform.GetChild(0).gameObject.transform.rotation *= rotation;

        DisplayHealth();

        //old method of moving player by adding a force everytime a move key is pressed
        //moveWithForce(WormRigidbody);
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DamageSource")
        {
            Debug.Log("you got shot");
            WormHealth -= 1;
        }
        else if (collision.gameObject.tag == "RespawnZone")
        {
            worm.transform.position = new Vector3(0, 0, 0);
            Debug.Log("you have fallen");
        }
        else if  (collision.gameObject.tag == "HelfItem") {

            Debug.Log("You just got healed");
            WormHealth += 5;
            Destroy(collision.gameObject);
            
        }
        else
        {
            Debug.Log("something else");
        }

    }

    private void ShootBall()
    {   
        //get the required gameobjects
        GameObject Gun = worm.transform.GetChild(0).GetChild(1).gameObject;
        GameObject gunPivot = worm.transform.GetChild(0).gameObject;

        //Insatntiate a new bullet and get its rigidbody
        GameObject newball = Instantiate(ball, Gun.transform.position, Quaternion.identity);
        Rigidbody ballRigid = newball.gameObject.GetComponent<Rigidbody>();

        //calculate the bullet force acording to the current gun roation
        float bally = ((worm.transform.rotation.eulerAngles.y / 90) - 1) * gunPivot.transform.up.x;
        float ballx = ((worm.transform.rotation.eulerAngles.y / 90) - 1) * gunPivot.transform.up.y;

        Vector3 bulletDirection = new Vector3(ballx, -bally , 0);
        Debug.Log("bullet direction: " + bulletDirection + "current Rotation: " + gunPivot.transform.up);

        ballRigid.AddForce(bulletDirection * bulletForce);
    }

    void Jump(Rigidbody rigid)
    {
        if (Input.GetKeyDown(jumpKey))
        {
            Debug.Log("Move Up");

            rigid.AddForce(transform.up * strength, ForceMode.Acceleration);
        }
    }

    void moveWithForce(Rigidbody rigid)
    {
        // This looked kinda strange so it's not actually used
        // but no worrys, its here
        if (Input.GetKey(rightKey))
        {
            Debug.Log("Move Right");

            rigid.AddForce(transform.right * strength, ForceMode.Acceleration);
        }

        if (Input.GetKey(leftKey))
        {
            Debug.Log("Move Left");

            rigid.AddForce(-transform.right * strength, ForceMode.Acceleration);
        }
    }

    Vector3 MovePlayer()
    {
        if (Input.GetKey(rightKey))
        {
            Vector3 input = new Vector3(1, 0, 0);
            Vector3 direction = input.normalized;
            Vector3 velocity = direction * speed;
            Vector3 moveAmount = velocity * Time.deltaTime;
            Debug.Log("Move Right by " + moveAmount);

            //Set roation so the player looks in the direction they're moving in
            worm.transform.rotation = Quaternion.Euler(0, 180, 0);

            return moveAmount;
        }

        else if (Input.GetKey(leftKey))
        {         
            Vector3 input = new Vector3(-1, 0, 0);
            Vector3 direction = input.normalized;
            Vector3 velocity = direction * speed;
            Vector3 moveAmount = velocity * Time.deltaTime;
            Debug.Log("Move Left by " + moveAmount);

            //same deal as before
            worm.transform.rotation = Quaternion.Euler(0, 0, 0);

            return moveAmount;   
        }

        else
        {       
            return new Vector3(0, 0, 0);
        }
    }

    float rotateGun()
    {
        GameObject Gun = worm.transform.GetChild(0).gameObject;
        // works like the MovePlayer function, just with roation instread
        if (Input.GetKey(gunUp)) {

            float rotationSpeed = 180f;
            float roationAmount = rotationSpeed * Time.deltaTime;

            return roationAmount;
        }
        if (Input.GetKey(gunDown))
        {

            float rotationSpeed = -180f;
            float roationAmount = rotationSpeed * Time.deltaTime;

            return roationAmount;
        }
        else
        {
            return 0f;
        }

    }

    void DisplayHealth()
    {
        healthText.text = "Health: " + WormHealth.ToString();

    }
}
