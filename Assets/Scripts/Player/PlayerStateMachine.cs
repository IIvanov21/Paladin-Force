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

    //Targeting References
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField]public float TargetingMovementSpeed { get; private set; }

    //Atacking References
    [field: SerializeField]public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField]public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField]public Health Health { get; private set; }
    [field: SerializeField]public float DodgeDuration {  get; private set; }
    [field: SerializeField]public float PreviousDodgeTime {  get; private set; }
    [field: SerializeField]public float DodgeLength {  get; private set; }
    [field: SerializeField]public float DodgeCoolDown {  get; private set; }
    [field: SerializeField]public float JumpForce {  get; private set; }
    [field: SerializeField]public LedgeDetector LedgeDetector { get; private set; }
    [field: SerializeField]public Attack[] Attacks { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
        MainCameraTransform=Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }

    public void SetDodgeTime(float time)
    {
        PreviousDodgeTime = time;
    }
    
}
