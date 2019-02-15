using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashingRule : FlockRule
{
    public override void calculate(GameObject[] mates, float avoidRange, int posRot)
    {
        if (mates.Length != 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, avoidRange))
            {
                //Vector3 incomingVec = hit.point - transform.position;
                //Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
                direction = Quaternion.LookRotation(hit.normal - hit.point);
                priority = 10;
                isApplicable = true;
                speed = 3;
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
