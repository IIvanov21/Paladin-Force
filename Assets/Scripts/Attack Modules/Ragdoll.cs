using Unity.VisualScripting;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;

    private Collider[] colliders;
    private Rigidbody[] allRigidBodies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colliders = GetComponentsInChildren<Collider>(true);
        allRigidBodies = GetComponentsInChildren<Rigidbody>(true);

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;
            }
        }

        foreach(Rigidbody rb in allRigidBodies)
        {
            if(rb.CompareTag("Ragdoll"))
            {
                rb.isKinematic = !isRagdoll;
                rb.useGravity = isRagdoll;
            }
        }

        characterController.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
    }
}
