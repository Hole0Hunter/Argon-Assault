using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    float xThrow;
    float yThrow;

    [SerializeField] float sensitivity = 30f;

    [SerializeField] float xRange = 7f;
    [SerializeField] float yRange = 7f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();

    }

    void ProcessRotation()
    {
        /*                  Position on screen      Control throw
         * Pitch            Coupled                 Coupled
         * Yaw              Coupled                 -------
         * Roll             -------                 Coupled
        */

        float currXPos = transform.localPosition.x;
        float currYPos = transform.localPosition.y;
        float currZPos = transform.localPosition.z;

        // Dealing with Pitch
        float pitchDueToPosition = currYPos * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor * Time.deltaTime * sensitivity;
        float pitch = pitchDueToPosition + pitchDueToControl;

        // Dealing with Yaw
        float yawDueToPosition = currXPos * positionYawFactor;
        float yaw = yawDueToPosition;

        // Dealing with Roll
        float rollDueToControl = xThrow * controlRollFactor * Time.deltaTime * sensitivity;
        float roll = rollDueToControl;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float currXPos = transform.localPosition.x;
        float currYPos = transform.localPosition.y;
        float currZPos = transform.localPosition.z;

        float xOffset = xThrow * Time.deltaTime * sensitivity;
        float yOffset = yThrow * Time.deltaTime * sensitivity;
        // we don't need a Z Offset 

        float rawXPos = currXPos + xOffset;
        float rawYPos = currYPos + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, currZPos);
    }
}
