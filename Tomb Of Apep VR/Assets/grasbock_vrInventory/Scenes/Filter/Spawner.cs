using GVRI.core;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public ItemInfo[] items;
    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime += Random.Range(1.5f, 3.0f);
    }

    float nextSpawnTime = 1.5f;
    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + Random.Range(1.5f, 3.0f);

            Instantiate(items[Random.Range(0, items.Length)].prefab, transform.position, transform.rotation);
        }
    }
}
