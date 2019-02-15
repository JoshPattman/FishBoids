using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flocking : MonoBehaviour
{
    public float speed;
    public float range;
    public float avoidRange;
    public float posRotBalance;

    public float turnSpeed = 1;

    bool isTurning = false;

    private GameObject[] getInRange()
    {
        GameObject[] all = GameObject.FindGameObjectsWithTag("Fish");
        GameObject[] tooMany = new GameObject[all.Length];
        int j = 0;
        for (int i = 0; i < all.Length; i++)
        {
            if ((all[i].transform.position - transform.position).magnitude <= range && all[i] != this.gameObject)
            {
                tooMany[j] = all[i];
                j++;
            }
        }
        GameObject[] fishes = new GameObject[j];
        for (int i = 0; i < j; i++)
        {
            fishes[i] = tooMany[i];
        }
        return fishes;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1f, 4f);
    }

    float clamp(float a)
    {
        if (a < 0)
        {
            return 0;
        }
        if (a > 1)
        {
            return 1;
        }
        return a;
    }
    // Update is called once per frame
    void Update()
    {
        avoidRange = GameObject.Find("Avoid").GetComponent<Slider>().value;
        range = GameObject.Find("Range").GetComponent<Slider>().value;
        posRotBalance = GameObject.Find("PosRot").GetComponent<Slider>().value;
        turnSpeed = GameObject.Find("Turn").GetComponent<Slider>().value;

        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);


        GameObject[] friends = getInRange();
        if (friends.Length != 0)
        {
            Quaternion posLook;
            Quaternion rotLook;
            Quaternion avoidLook;
            Vector3 avPos = new Vector3(0, 0, 0);
            foreach (GameObject item in friends)
            {
                avPos += item.transform.position;
            }
            avPos = avPos / friends.Length;
            posLook = Quaternion.LookRotation(avPos - transform.position);


            Quaternion avRot = transform.rotation;
            foreach (GameObject item in friends)
            {
                avRot = Quaternion.Lerp(item.transform.rotation, avRot, 1f / friends.Length);
            }
            rotLook = avRot;

            avoidLook = Quaternion.Euler(posLook.eulerAngles + new Vector3(0, 160, 0));

            Quaternion suggestRot;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, avoidRange))
            {
                //suggestRot = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 160, 0));
                suggestRot = Quaternion.LookRotation(hit.normal - hit.point);
            }
            else
            {
                suggestRot = Quaternion.Lerp(posLook, rotLook, posRotBalance);
                suggestRot = Quaternion.Lerp(avoidLook, suggestRot, clamp(((transform.position - avPos).magnitude / (avoidRange))));
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, suggestRot, 0.04f * turnSpeed);
        }

        else
        {
            //No Mates
            Quaternion suggestRot = transform.rotation;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, avoidRange))
            {
                //suggestRot = Quaternion.Euler(suggestRot.eulerAngles + new Vector3(0, 160, 0));
                Vector3 incomingVec = hit.point - transform.position;
                Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);

                

                //suggestRot = Quaternion.LookRotation(reflectVec);
                suggestRot = Quaternion.LookRotation(hit.normal - hit.point);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, suggestRot, 0.04f * turnSpeed);
        }
    }
    /*public void OnCollisionEnter(Collision collision)
    {
        isTurning = true;
        Debug.Log("Col");
    }
    public void OnCollisionExit(Collision collision)
    {
        isTurning = false;
    }*/
}
