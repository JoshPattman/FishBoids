using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playVid : MonoBehaviour
{
    public MovieTexture mt;
    // Start is called before the first frame update
    void Start()
    {
        Projector proj = GetComponent<Projector>();
        proj.material.SetTexture("_ShadowTex", mt);
        mt.loop = true;
        mt.Play();
    }

}
