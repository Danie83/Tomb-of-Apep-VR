using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleRoomba : GVRI.DynamicInventory
{
    public Rigidbody rb;
    public float speed = 1.0f;
    public float rotSpeed = 1.0f;
    public GameObject master = null;
    public float deltaSlots = 0.2f;
    public float heightOffset = 0.2f;

    // Start is called before the first frame update
    protected override void Start()
    {
        if (!rb) rb = gameObject.GetComponent<Rigidbody>();
        if (!rb) rb = gameObject.AddComponent<Rigidbody>();

        base.Start();
        Open();
    }

    // Update is called once per frame
    void Update()
    {
        var items = FindObjectsOfType<GVRI.Item>();
        //find closest item
        GameObject closest = null;
        float smallestSqrDistance = 10000.0f;
        foreach(GVRI.Item item in items)
        {
            float sqrMagnitude = (item.gameObject.transform.position - transform.position).sqrMagnitude;
            if(sqrMagnitude < smallestSqrDistance)
            {
                closest = item.gameObject;
                smallestSqrDistance = sqrMagnitude;
            }
        }
        //track
        if (!closest)
        {
            //found nothing, return to master
            if (!master) return;
            Vector3 delta = master.transform.position - transform.position;
            delta.y = 0.0f;
            transform.forward = Vector3.RotateTowards(transform.forward, delta, rotSpeed*Time.deltaTime, 0);
            if(delta.sqrMagnitude > 1.0f)
                rb.velocity = speed * transform.forward;
        }
        else
        {
            //found an item -> COLLECT!
            Vector3 delta = closest.transform.position - transform.position;
            delta.y = 0.0f;
            transform.forward = Vector3.RotateTowards(transform.forward, delta, rotSpeed * Time.deltaTime, 0);
            rb.velocity = speed * transform.forward;
        }
    }

    public override void UpdateSlots()
    {
        base.UpdateSlots();

        if (!inserter) return;

        var count = slots.Count;
        if (count == 0) return;

        //place slots
        int i = 0;
        foreach (GVRI.Slot bs in slots)
        {
            //place relative to insert coordinates
            bs.gameObject.transform.parent = null;
            bs.gameObject.transform.position = inserter.transform.position + new Vector3(0, deltaSlots * i + heightOffset, 0);
            bs.gameObject.transform.parent = transform; //set parent pack to inventory
            i++;
        }

    }
}
