using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    //The input reader provides the player input data.
    [field:SerializeField]
    public InputReader InputReader {  get; private set; }

    [field:SerializeField]
    public CharacterController characterController { get; private set; }
    
    public Vector3 MovementVector;

    [field: SerializeField]
    public float PlayerSpeed { get; private set; } = 5f;

    //Camera movement variables
    [field: SerializeField] public float RotationDamping {  get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    public Transform MainCameraTransform { get; private set; }

    //Animation Variables
    [field:SerializeField] public Animator Animator { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
        MainCameraTransform=Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }

    
}
