using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChestScript : MonoBehaviour
{
    public bool locked = true;
    public GameObject explosiveChest = null;
    public GameObject openableChest = null;
    public GameObject itemToSpawnPrefab = null;
    public int selected = -1;
    private Transform copy;

    public void DestroyObj()
    {
        if (locked == false)
        {
            copy = transform;
            foreach (Transform t in transform)
            {
                if(t.gameObject.name.Equals("sound"))
                    Destroy(t.gameObject, 5);
                else
                    Destroy(t.gameObject, 0);
            }

            int chance = Random.Range(1, 100);
            if (chance < 30)
            {
                // explode chest
                SpawnExplosiveObject();
            }
            else
            {
                // open chest
                SpawnOpenableObject();
                
            }
        }
    }

    private void SpawnExplosiveObject()
    {
        GameObject explObj = Instantiate(explosiveChest, copy.position, copy.rotation) as GameObject;
        explObj.GetComponent<ExplodeScript>().Explode();
        selected = 1;
    }
    private void SpawnOpenableObject()
    {
        selected = 2;
        GameObject openableObj = Instantiate(openableChest, copy.position, copy.rotation) as GameObject;
        Transform spawnAnchor = openableObj.GetComponentInChildren<ItemSpawnAnchor>().transform;
        if (itemToSpawnPrefab != null)
            Instantiate(itemToSpawnPrefab, spawnAnchor.position, spawnAnchor.rotation);
    }
}
