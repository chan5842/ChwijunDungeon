using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject Effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left * 70 * Time.deltaTime);
    }

    private void OnDisable()
    {
        //Instantiate(Effect, transform.position + new Vector3(1, 0, 0), transform.rotation);
    }
}
