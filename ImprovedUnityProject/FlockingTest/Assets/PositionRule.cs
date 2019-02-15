using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRule : FlockRule
{
    public override void calculate(GameObject[] mates, float avoidRange, int posRot)
    {
        if (mates.Length != 0)
        {
            priority = 10 - posRot;
            speed = 2;
            Vector3 avPos = new Vector3(0, 0, 0);
            foreach (GameObject item in mates)
            {
                avPos += item.transform.position;
            }
            avPos = avPos / mates.Length;
            direction = Quaternion.LookRotation(avPos - transform.position);
            speed = ((avPos - transform.position).magnitude / avoidRange) * 5;
            isApplicable = true;
        }
        else
        {
            priority = 0;
            isApplicable = false;
            direction = transform.rotation;
            speed = 1;
        }
    }
}
