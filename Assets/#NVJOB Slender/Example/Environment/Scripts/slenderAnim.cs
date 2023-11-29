// This script controls the animation of a personalized character's head turning towards a target.

using UnityEngine;

public class PersonalizedCharacterHeadAnimation : MonoBehaviour
{
    // References to the character's body, target to look at, and head transform
    public SkinnedMeshRenderer characterBody;
    public Transform LookAt;
    public Transform characterHead;

    // Parameters to control the look at behavior
    public float blendTime = 0.4f;
    public float rotationSpeed = 5.0f;
    public float weightMultiplier = 1;
    public float clampWeight = 0.5f;
    public Vector3 lookAtWeights = new Vector3(0.4f, 0.8f, 0.9f);
    public bool synchronizeYWithTarget;

    // Internal variables
    Transform characterTransform;
    Animator characterAnimator;
    Vector3 lookAtTargetPosition, currentLookAtPosition;
    float lookAtWeight;

    void Start()
    {
        // Initialize references and default positions
        characterTransform = transform;
        characterAnimator = GetComponent<Animator>();
        lookAtTargetPosition = LookAt.position + characterTransform.forward;
        currentLookAtPosition = characterHead.position + characterTransform.forward;
    }

    void Update()
    {
        // Update the target position
        lookAtTargetPosition = LookAt.position + characterTransform.forward;
    }

    void OnAnimatorIK()
    {
        // Synchronize the y-coordinate of the target position with the head if required
        if (!synchronizeYWithTarget)
            lookAtTargetPosition.y = characterHead.position.y;

        // Calculate the new look at position
        Vector3 directionToTarget = currentLookAtPosition - characterHead.position;
        directionToTarget = Vector3.RotateTowards(directionToTarget, lookAtTargetPosition - characterHead.position, rotationSpeed * Time.deltaTime, float.PositiveInfinity);
        currentLookAtPosition = characterHead.position + directionToTarget;

        // Smoothly transition the look at behavior
        lookAtWeight = Mathf.MoveTowards(lookAtWeight, 1, Time.deltaTime / blendTime);
        characterAnimator.SetLookAtWeight(lookAtWeight * weightMultiplier, lookAtWeights.x, lookAtWeights.y, lookAtWeights.z, clampWeight);
        characterAnimator.SetLookAtPosition(currentLookAtPosition);
    }
}
