using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour
{
    public float strength;
    public float bulletForce;
    public GameObject worm;
    public GameObject ball;
    public KeyCode rightKey;
    public KeyCode leftKey;
    public KeyCode jumpKey;
    public KeyCode triggerKey;
    public KeyCode gunUp;
    public KeyCode gunDown;
    float speed = 5f;

    private void Update()
    {
        Rigidbody WormRigidbody = worm.gameObject.GetComponent<Rigidbody>();

        Jump(WormRigidbody);

        if (Input.GetKeyDown(triggerKey))
        {
            ShootBall();
        }

        worm.transform.position += MovePlayer();

        Quaternion rotation = Quaternion.Euler(0, 0, rotateGun());
        worm.transform.GetChild(0).gameObject.transform.rotation *= rotation;
        
        //moveWithForce(WormRigidbody);
    }

    private void ShootBall()
    {
        GameObject Gun = worm.transform.GetChild(0).GetChild(1).gameObject;
        GameObject gunPivot = worm.transform.GetChild(0).gameObject;

        GameObject newball = Instantiate(ball, Gun.transform.position, Quaternion.identity);
        Rigidbody ballRigid = newball.gameObject.GetComponent<Rigidbody>();

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
}
