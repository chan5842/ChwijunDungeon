using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EffectEnd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EffectEnd()
    {
        Destroy(gameObject, 1f);
        //gameObject.SetActive(false);
    }
}
