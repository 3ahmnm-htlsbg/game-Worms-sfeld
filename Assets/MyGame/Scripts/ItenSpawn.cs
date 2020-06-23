using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItenSpawn : MonoBehaviour
{

    public Transform[] transforms;
    public GameObject[] itemRef;
    public WormController wormController;

    private  List<Vector3> positions = new List<Vector3>() ;
    // Start is called before the first frame update

    private void Start()
    {
        for (int i = 0; i < transforms.Length ; i++)
        {
            positions.Add(transforms[i].position);
        }
    }

    //INVOKE

    private void Update()
    {
        print(WormController.itemAmmount);

        if(WormController.itemAmmount <= 0)
        {
            StartCoroutine(SpawnItems());
            print("coroutine Started");
        }

        if(WormController.itemAmmount >= 4)
        {
            StopAllCoroutines();
            print("coroutine Stopped");
        }

    }


    IEnumerator SpawnItems()
    {
        yield return new WaitForSeconds(2f);

        int itemIndex = Random.Range(0, itemRef.Length );
        int position = Random.Range(0, positions.Count );

        Instantiate(itemRef[itemIndex], positions[position], Quaternion.identity);
        WormController.itemAmmount++;

        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(SpawnItems());
        
    }
   
}
