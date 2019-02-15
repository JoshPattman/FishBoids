using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class FlockRule : MonoBehaviour
{
    public Quaternion direction;
    public float speed;
    public bool isApplicable;
    public int priority;
    abstract public void calculate(GameObject[] mates, float avoidRange, int posRot);
}
