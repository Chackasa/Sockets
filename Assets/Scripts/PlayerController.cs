using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("right"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 50f * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (Input.GetKey("left"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 50f * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        /*if (Input.GetKey("right"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 50f * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
        }*/
    }
}
