using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int WormHealth = 5;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("it be collidin");
    }
}
