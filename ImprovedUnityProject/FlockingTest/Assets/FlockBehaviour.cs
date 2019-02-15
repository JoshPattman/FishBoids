using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlockBehaviour : MonoBehaviour
{
    float turnSpeed;
    int posRotBalance;
    float range;
    float avoidRange;
    FlockRule[] rules;
    float averageSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rules = GetComponents<FlockRule>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) < 30)
        {
            avoidRange = GameObject.Find("Avoid").GetComponent<Slider>().value;
            range = GameObject.Find("Range").GetComponent<Slider>().value;
            posRotBalance = (int)GameObject.Find("PosRot").GetComponent<Slider>().value;
            turnSpeed = GameObject.Find("Turn").GetComponent<Slider>().value;
            averageSpeed = 0;
            int num = 0;
            GameObject[] friends = getFriends();
            Quaternion[] allRots = new Quaternion[friends.Length];
            foreach (FlockRule rule in rules)
            {
                rule.calculate(friends, avoidRange, posRotBalance);
                if (rule.isApplicable)
                {
                    for (int x = 0; x < rule.priority; x++)
                    {
                        allRots = append(allRots, rule.direction);
                        averageSpeed = averageSpeed + rule.speed;
                        num++;
                    }
                }
            }
            if (num != 0)
            {
                Quaternion avRot = calcAv(allRots);
                averageSpeed = averageSpeed / num;
                transform.rotation = Quaternion.Lerp(transform.rotation, avRot, 0.04f * turnSpeed);
            }
            else
            {
                averageSpeed = 1;
            }
        }
        transform.Translate(Vector3.forward * averageSpeed * Time.deltaTime);

    }

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
    private GameObject[] getFriends() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        GameObject[] fishes = new GameObject[hitColliders.Length];
        for (int i = 0; i < hitColliders.Length; i++)
        {
            fishes[i] = hitColliders[i].gameObject;
        }
        return fishes;
    }

    private Quaternion calcAv(Quaternion[] rots) {
        float x, y, z, w;
        x = 0;
        y = 0;
        z = 0;
        w = 0;
        foreach (Quaternion q in rots)
        {
            x += q.x; y += q.y; z += q.z; w += q.w;
        }
        float k = 1.0f / Mathf.Sqrt(x * x + y * y + z * z + w * w);
        return new Quaternion(x * k, y * k, z * k, w * k);
    }
    private Quaternion[] append(Quaternion[] quats, Quaternion q) {
        Quaternion[] q2 = new Quaternion[quats.Length + 1];
        for (int i = 0; i < quats.Length; i++)
        {
            q2[i] = quats[i];

        }
        q2[quats.Length] = q;
        return q2;
    }
}
