using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DumbLaser : Projectiles
{
    [SerializeField] Vector3 _direction;
    [SerializeField] float _lifetime;
    [SerializeField] Stopwatch _stopwatch = new Stopwatch();
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
    private void Awake() {
        _stopwatch.Start();
    }
    public override void SetTargetLocation(Vector3 t)
    {
        _direction = (t - transform.position).normalized;
        base.SetTargetLocation(t);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "player")
        {
            UnityEngine.Debug.Log("playerColliderHit");
            bang();
        }
    }
    private void normalStep()
    {
        transform.position = transform.position + _direction * _Speed * Time.fixedDeltaTime;
        if(_stopwatch.Elapsed.Seconds > _lifetime)
        {
            Destroy(gameObject);
        }
        RaycastHit hit;
        if(Physics.Raycast(transform.position, _direction , out hit, _Speed * Time.fixedDeltaTime))
        {
            UnityEngine.Debug.Log("DumbLaser will hit collider next frame!");
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
}
