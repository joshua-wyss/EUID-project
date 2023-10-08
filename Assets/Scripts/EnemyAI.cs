using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageAble
{
    [Header("Offensive: ")]
    [SerializeField] Transform _player;
    [SerializeField] WeaponScript _wScript;
    [SerializeField] float _fireRange = 100;
    [SerializeField] float _fireAngle = 10;
    [Header("Movement: ")]
    [SerializeField] float _maxSpeed = 50;
    [SerializeField] float _minSpeed = 4;
    [SerializeField] float _accleration = 1;

    [Header("Debuging")]
    [SerializeField] float _actualSpeed = 4;
    [SerializeField] List<Vector3> _MovePoints = new List<Vector3>();
    [Header("HP")]
    [SerializeField] int _Health = 8;

    private void Awake() {
        if(_player == null)
        {
            _player = R_Singleton.Instance.GetPlayerGO().transform;
        }
        SelectNewManuver();

    }
    private void FixedUpdate() {
        CheckFire();
        CheckAcceleration();
        MoveStep(_actualSpeed);
    }

    private void MoveStep(float v)
    {
        Vector3 stepGoal = _MovePoints[0];
        transform.LookAt(stepGoal);
        float distance = Vector3.Distance(transform.position, stepGoal);
        if(distance > (v * Time.deltaTime))
        {
            transform.position = Vector3.MoveTowards(transform.position, stepGoal, v * Time.deltaTime);
        }
        else
        {
            transform.position = stepGoal;
            popMovePoint();
            MoveStep(v-distance);
        }
    }
    private void CheckFire()
    {
        if(Vector3.Distance(_player.position, transform.position) < _fireRange && Vector3.Angle((_player.position - this.transform.position), transform.forward) < _fireAngle)
        {
            Fire();
        }
    }
    private void Fire()
    {
        Debug.Log("Enemy firing at player");
        _wScript.FireCommand(new targetLoc(transform.forward), _actualSpeed);
    }
    private void popMovePoint()
    {
        _MovePoints.RemoveAt(0);
        if(_MovePoints.Count < 1)
        {
            SelectNewManuver();
        }
    }
    public void SelectNewManuver()
    {
        //TODO
        if(Vector3.Distance(transform.position, _player.transform.position) < 5 || Vector3.Angle(transform.forward, _player.transform.position  - transform.position) > 90f)
        {
            Debug.Log("Uturn");
            SetMovePoints(UTurn());
        }
        else
        {
            SetMovePoints(Test());
        }
        for(int i = 0; i < _MovePoints.Count -1; i++)
        {
            Debug.DrawRay(_MovePoints[i], _MovePoints[i] - _MovePoints[i+1], Color.gray, 1f);
        }
    }
    public void SetMovePoints(List<Vector3> movePoints)
    {
        _MovePoints = movePoints;
    }
    private List<Vector3> Test(){
        return CurveTowards2(_player.position, 20);
    }
    private List<Vector3> CurveTowards(Vector3 endPoint)
    {
        Vector3 startPoint = transform.position;
        //Vector3 midPoint = (startPoint + endpoint)/2;
        Vector3 r = (endPoint - transform.position) / 7;
        //Debug.DrawRay(startPoint, r, Color.green, 1f);
        Vector3 R = Vector3.ProjectOnPlane(Random.insideUnitSphere, r).normalized;
        //Debug.DrawRay(midPoint, R, Color.blue, 1f);

        List<Vector3> result = new List<Vector3>();

        for (int i = 1; i <= 7; i++)
        {
            Vector3 x = i * r;
            float y = Mathf.Sin(i/7f * 180 * (Mathf.PI/180));
            Debug.Log("x: " + x + " y: " + y + " y * R: " + y * R * (r.magnitude * 7 /2));
            Vector3 v3 = startPoint + x + R * y * (r.magnitude * 7/2);
            result.Add(v3);
        }
        return result;
    }
    private List<Vector3> CurveTowards2(Vector3 endPoint, int segments)
    {
        List<Vector3> result = new List<Vector3>();

        Vector3 startP = transform.position;
        Vector3 d = endPoint - startP;
        
        //Vector3 R = (d).normalized -  transform.forward;
        Vector3 R = transform.forward * Mathf.Sin(Vector3.Angle(transform.forward, d) * Mathf.PI/180);
        //Debug.Log(R + " ANGLE " + Vector3.Angle(R, d));
        Debug.DrawRay(startP, d, Color.yellow, 1);
        Debug.DrawRay(startP + d/2, R, Color.red, 1);

        for(int i = 1; i <= segments; i++)
        {
            float y = Mathf.Sin((float)i/segments * 180 * (Mathf.PI/180));
            //Debug.Log(y);
            result.Add(startP + (d*i/segments) + (R * y * d.magnitude/2));
        }
        
        return result;
    }
    private List<Vector3> UTurn()
    {
        float d = (_actualSpeed + _minSpeed)/2;
        return CurveTowards2(transform.position + (transform.right * d), 10);
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        foreach (Vector3 item in _MovePoints)
        {
            Gizmos.DrawSphere(item, .2f);
        }
    }
    private void CheckAcceleration()
    {
        if(_MovePoints.Count >= 2)
        {
            if(Vector3.Angle(transform.forward, _MovePoints[^1]) < 30)
            {
                _actualSpeed += _accleration * Time.deltaTime;
                //Debug.Log("accelerating");
                if(_actualSpeed > _maxSpeed)
                    _actualSpeed = _maxSpeed;
            }
            else if(Vector3.Angle(transform.forward, _MovePoints[^1]) > 60)
            {
                _actualSpeed -= _accleration * Time.deltaTime;
                //Debug.Log("deccelerate");
                if(_actualSpeed < _minSpeed)
                    _actualSpeed = _minSpeed;
            }
        }
    }

    public void TakeDamage(int i)
    {
        _Health -= i;
        if(_Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
