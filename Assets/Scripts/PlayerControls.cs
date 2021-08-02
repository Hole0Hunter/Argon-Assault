using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("")] float xThrow;
    [Tooltip("")] float yThrow;

    [Tooltip("How fast ship moves in the respective direction")]
    [SerializeField] float sensitivity = 30f;

    [Tooltip("How far the player can move horizontally")]
    [SerializeField] float xRange = 7f;
    [Tooltip("How far the player can move vertically")]
    [SerializeField] float yRange = 7f;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -50f;

    [Header("Lazer guns array")]
    [Tooltip("Add all player lazers here")]
    [SerializeField] GameObject[] lazers;
    bool lazersActive;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
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

    void ProcessFiring()
    {
        bool buttonPressed = Input.GetKey(KeyCode.Space);
        if (buttonPressed)
        {
            SetLazersActive(buttonPressed);
        }
        else
        {
            SetLazersActive(buttonPressed);
        }
    }

    void SetLazersActive(bool buttonPressed)
    {
        foreach (GameObject lazer in lazers)
        {
            // lazer.GetComponent<ParticleSystem>().enableEmission = buttonPressed; 
            // this is an old method, deprecated
            // lazer.GetComponent<ParticleSystem>().emission.enabled = buttonPressed;
            // no idea why this is wrong

            var emissionModule = lazer.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = buttonPressed;
        }
    }
}
