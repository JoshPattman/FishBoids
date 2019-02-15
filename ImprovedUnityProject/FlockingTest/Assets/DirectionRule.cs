using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionRule : FlockRule
{
    public override void calculate(GameObject[] mates, float avoidRange, int posRot) {
        if (mates.Length != 0)
        {
            priority = posRot;
            speed = 3;
            Quaternion avRot = transform.rotation;
            float divider = 1f;
            foreach (GameObject item in mates)
            {
                avRot = Quaternion.Lerp(item.transform.rotation, avRot, divider);
                divider = divider / 2;
            }
            direction = avRot;
            isApplicable = true;
        }
        else {
            priority = 0;
            isApplicable = false;
            direction = transform.rotation;
            speed = 1;
        }
    }
}
