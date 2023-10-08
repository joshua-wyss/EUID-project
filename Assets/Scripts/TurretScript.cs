using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] GameObject _BarrelGO;
    [SerializeField] WeaponScript _weaponScript;
    [SerializeField] float _turnSpeed;
    [SerializeField] bool _balistics;
    [SerializeField] float leadS; //in frames
    [SerializeField] float _aimMinAngle = 10;
    [SerializeField] float range = 100;
    private void FixedUpdate() {
        //_BarrelGO.transform.LookAt(targetPos);
        if(Vector3.Distance(transform.position, _target.position) <= range)
        {
            TrackTarget();
            CheckIfPointingAtTarget();
        }
    }
    private void CheckIfPointingAtTarget()
    {
        Vector3 tPos = targetPos;
        Vector3 v3ToTarget = tPos - _BarrelGO.transform.position;
        Vector3 v3actual = _BarrelGO.transform.forward;
        if(Vector3.Angle(v3ToTarget, v3actual) < _aimMinAngle)
        {
            _weaponScript.FireCommand(new targetLoc(tPos), 0f);
        }
    }
    private void TrackTarget()
    {
        Vector3 lookDirection = (targetPos - _BarrelGO.transform.position).normalized;

        _BarrelGO.transform.rotation = Quaternion.RotateTowards(_BarrelGO.transform.rotation, Quaternion.LookRotation(lookDirection), _turnSpeed * Time.deltaTime);
    }
    private Vector3 CalculateTargetTrajectory()
    {
        Rigidbody targetRigi = _target.GetComponent<Rigidbody>();
        if(targetRigi != null)
        {
            //Debug.Log("rigibody found");
            //var t = distanceToTarget / _weaponScript.getProjectileSpeed;
            return targetRigi.velocity * (distanceToTarget / _weaponScript.getProjectileSpeed);
        }
        return Vector3.zero;
    }
    private Vector3 targetPos => _balistics ? (CalculateTargetTrajectory() + _target.transform.position)
     : _target.transform.position;
    private float distanceToTarget => (_target.transform.position - _BarrelGO.transform.position).magnitude;
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_BarrelGO.transform.position, _BarrelGO.transform.forward * 300);
    }
}
