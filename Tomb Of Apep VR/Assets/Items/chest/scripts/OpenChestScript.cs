using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChestScript : MonoBehaviour
{
    public bool locked = true;
    public GameObject explosiveChest;
    public GameObject openableChest;
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
        GameObject explObj = Instantiate(explosiveChest) as GameObject;
        explObj.GetComponent<ExplodeScript>().Explode();
        Transform tr = explObj.GetComponent<Transform>();
        tr.position = copy.position;
        tr.rotation = copy.rotation;
        selected = 1;
    }
    private void SpawnOpenableObject()
    {
        selected = 2;
        GameObject openableObj = Instantiate(openableChest) as GameObject;
        Transform tr = openableObj.GetComponent<Transform>();
        tr.position = copy.position;
        tr.localRotation = copy.localRotation;
        tr.GetChild(0).position = copy.GetChild(0).position;
        tr.GetChild(1).position = copy.GetChild(1).position;
        HingeJoint hj = tr.GetChild(0).GetComponent<HingeJoint>();
        hj.anchor = new Vector3(1, 1, 1);
        hj.anchor = new Vector3(0, 0, 0);
    }
}
