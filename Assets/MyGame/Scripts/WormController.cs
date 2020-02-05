using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour
{
    public float strength;
    public float ballStength;
    public GameObject Worm;
    public GameObject Ball;
    public KeyCode RightKey;
    public KeyCode LeftKey;
    public KeyCode JumpKey;
    public KeyCode TriggerKey;

    private void Update()
    {
        Rigidbody WormRigidbody = Worm.gameObject.GetComponent<Rigidbody>();

        if (Input.GetKeyDown(RightKey))
        {
           
            Debug.Log("Move Right");

            WormRigidbody.AddForce(transform.right*strength, ForceMode.Acceleration);
        }
        if (Input.GetKeyDown(LeftKey))
        {
            Debug.Log("Move Left");

            WormRigidbody.AddForce(-transform.right * strength, ForceMode.Acceleration);
        }
        if (Input.GetKeyDown(JumpKey))
        {
            Debug.Log("Move Up");

            WormRigidbody.AddForce(transform.up * strength, ForceMode.Acceleration);
        }

        if (Input.GetKeyDown(TriggerKey))
        {
            ShootBall();
        }

    }

    private void ShootBall()
    {
        
        Vector3 ballPos = Worm.transform.position + new Vector3(1, 0, 0);
        GameObject newball = Instantiate(Ball, ballPos, Quaternion.identity);
        Rigidbody ballRigid = newball.gameObject.GetComponent<Rigidbody>();
        ballRigid.AddForce(transform.right * ballStength);
    }
}
