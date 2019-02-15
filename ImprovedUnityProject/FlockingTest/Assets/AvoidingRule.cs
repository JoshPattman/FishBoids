using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidingRule : FlockRule
{
    public override void calculate(GameObject[] mates, float avoidRange, int posRot)
    {
        if (mates.Length != 0)
        {
            priority = 5;
            speed = 1;
            Vector3 avPos = new Vector3(0, 0, 0);
            foreach (GameObject item in mates)
            {
                avPos += item.transform.position;
            }
            avPos = avPos / mates.Length;
            if ((transform.position - avPos).magnitude < avoidRange)
            {
                direction = Quaternion.LookRotation(transform.position - avPos);
                priority = (int)(((transform.position - avPos).magnitude / avoidRange) * 5);
                isApplicable = true;
            }
            else {
                priority = 0;
                isApplicable = false;
                direction = transform.rotation;
                speed = 1;
            }
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
