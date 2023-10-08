using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammedShell : Projectiles
{
    [SerializeField] Vector3 _direction;
    [SerializeField] ExplosionScript _explosionScript;
    [SerializeField] GameObject _BulletObject;
    private Vector3 _endPoint;
    private void FixedUpdate() {
        if (_endPoint != Vector3.zero)
        {
            bangAtEndPoint();
        }
        else
        {
            normalStep();
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "player")
        {
            //UnityEngine.Debug.Log("playerColliderHit by ProgrammedShell");
            bang();
        }
    }
    private void normalStep()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetLocation.V3, _Speed*Time.deltaTime);
        if(_targetLocation.V3 == transform.position)
        {
            bang();
        }
        RaycastHit hit;
        if(Physics.Raycast(transform.position, _direction , out hit, _Speed * Time.fixedDeltaTime))
        {
            //UnityEngine.Debug.Log("ProgrammedShell will hit collider next frame!");
            _endPoint = hit.point;
        }
    }
    private void bang()
    {
        _BulletObject.SetActive(false);
        _explosionScript.gameObject.SetActive(true);
        this.enabled = false;
    }
    private void bangAtEndPoint()
    {
        transform.position = _endPoint;
        bang();
    }
    public override void SetTargetLocation(targetLoc t)
    {
        _direction = (t.V3 - transform.position).normalized;
        base.SetTargetLocation(t);
    }
    public override bool isSmartAmo => true;
}
