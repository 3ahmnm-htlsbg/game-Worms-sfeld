using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour
{

    public int x;
    public string text;
    public float number;

    // Start is called before the first frame update
    void Start()
    {
        /* if (x > 0)
         {
             Debug.Log("x ist größer als 0");
         }
         else
         {
             Debug.Log("x ist kleiner als 0");
         }


         if (text == "OwO")
         {
             Debug.Log("OwO what's this? Its Weeb o' clock");
         }
         else
         {
             Debug.Log("This does not have OWO Energy");
         } */

        if (number > 10)
        {
            Debug.Log("Nummer ist größer als 10");
        }
        else
        {
            Debug.Log("Nummer ist kleiner als 10");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
