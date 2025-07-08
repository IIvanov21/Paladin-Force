using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    private float verticalVelocity;
    private Vector3 impact;
    private Vector3 dampingVelocity;
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;
    [SerializeField] private float drag = 0.3f;

 

    // Update is called once per frame
    void Update()
    {
        //Gravity Handler
        if(characterController.isGrounded && verticalVelocity<0.0f)
        {
            verticalVelocity=Physics.gravity.y *Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact=Vector3.SmoothDamp(impact,Vector3.zero, ref dampingVelocity,drag);

        if(impact.sqrMagnitude<0.2f*0.2f)
        {
            impact = Vector3.zero;
        }
    }

    public void AddForce(Vector3 force)
    {
        impact+=force;
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity+=jumpForce;
    }

    internal void Reset()
    {
        impact=Vector3.zero;
        verticalVelocity=0;
    }
}
