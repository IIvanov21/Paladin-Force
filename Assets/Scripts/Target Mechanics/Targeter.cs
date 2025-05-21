using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    private List<Target> targets= new List<Target>();
    public Target CurrentTarget { get; private set; }

    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;
    private Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<Target>(out Target target))
        {
            return;
        }

        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target))
        {
            return;
        }

        RemoveTarget(target);
    }

    public bool SelectTarget()
    {
        if(targets.Count == 0) return false;

        //Variables to track the closest target and its distance from the center of the viewport
        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        //Iterate through all the targets
        foreach(Target target in targets)
        {
            //Get the target's position in the camera's viewport coordinates
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            //Check the target is withing our view
            if(viewPos.x<0.0f||viewPos.x>1.0f || viewPos.y<0.0f||viewPos.y>1.0f) continue;

            //Calculate the squared distance from the centre of the viewport
            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);

            //If this target is closer to the center than the previous closest target, update the closest target
            if(toCenter.sqrMagnitude< closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }

        }

        if(closestTarget == null) return false;

        //Set the closest target as our Current Target
        CurrentTarget = closestTarget;

        //Add the current target to Cinemachine Target group with specific weight and radius
        cinemachineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

        return true;
    }

    public void Cancel()
    {
        //If there is no target selected, exit early.
        if(CurrentTarget == null) return;

        //If we have a target remove it from the cinemachine target group
        cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);

        CurrentTarget = null;
    }

    //Tidy up method, when done using a target.
    public void RemoveTarget(Target target)
    {
        if(CurrentTarget==target)
        {
            cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;   
        }

        //Using the target passed in tidy up list and event references.
        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
  
}
