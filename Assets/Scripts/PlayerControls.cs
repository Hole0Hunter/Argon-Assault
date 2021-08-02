using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xThrow =  Input.GetAxis("Horizontal");
        float yThrow = Input.GetAxis("Vertical");

        float currXPos = transform.localPosition.x;
        float currYPos = transform.localPosition.y;
        float currZPos = transform.localPosition.z;

        transform.localPosition = new Vector3(currXPos, currYPos, currZPos);
        
    }
}
