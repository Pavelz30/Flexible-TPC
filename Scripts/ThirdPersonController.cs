﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Oh? You're approaching me? Instead of running away, you're coming right to me?
Even Though your first assets imported, Standard Assets, told you the secret of CSharp,
*dead standard asset scripts* like an exam student scrambling to finish the problems
on an exam until the last moments before the chime?
*/
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement settings")]
    public float rotationSpeed = 0.1f;
    [Tooltip("Specify the minimum input magnitude required for the character to start rotating.")]
    public float allowRotation;
    public bool allowSprinting;

    [Header("Animator settings")]
    public string inputMagnitudeParameter = "InputMagnitude";
    public string sprintingBoolParameter = "Sprinting";

    // Receive inputs from a separate script
    [HideInInspector] public Vector3 desiredMovementDirection;
    [HideInInspector] public bool isSprinting;

    private Animator characterAnimator;
    private CharacterController characterController;
    // These are used to calculate movement and rotation
    private float inputMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = this.GetComponent<Animator>();
        characterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        inputMagnitude = desiredMovementDirection.sqrMagnitude;

        characterAnimator.SetFloat(inputMagnitudeParameter, inputMagnitude, 0f, Time.deltaTime);
        if (allowSprinting)
            characterAnimator.SetBool(sprintingBoolParameter, isSprinting);

        if (inputMagnitude > allowRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMovementDirection), rotationSpeed);
        }
    }
}