using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFish : MonoBehaviour
{
    public GameObject fish;
    public int numFish;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numFish; i++)
        {
            Instantiate(fish, transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)), transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
