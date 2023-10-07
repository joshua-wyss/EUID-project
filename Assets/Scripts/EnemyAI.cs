using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] float _fireRange = 100;
    [SerializeField] float _fireAngle = 10;
    [SerializeField] float _Speed = 8;
    [SerializeField] List<Vector3> _MovePoints = new List<Vector3>();

    private void Awake() {
        SelectNewManuver();
    }
    private void FixedUpdate() {
        MoveStep(_Speed);
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
        if(Vector3.Distance(_player.position, transform.position) < _fireRange && Vector3.Dot((_player.position - this.transform.position), transform.forward) < _fireAngle)
        {
            Fire();
        }
    }
    private void Fire()
    {
        Debug.Log("Enemy firing at player");
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
        SetMovePoints(Test());
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
        return CurveTowards2(_player.position, 8);
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
            Vector3 v3 = startPoint + x + R * y * (r.magnitude * 7 /2);
            result.Add(v3);
        }
        return result;
    }
    private List<Vector3> CurveTowards2(Vector3 endPoint, int segments)
    {
        List<Vector3> result = new List<Vector3>();

        Vector3 startP = transform.position;
        Vector3 d = endPoint - startP;
        
        Vector3 R = (d).normalized -  transform.forward;
        Debug.DrawRay(startP, d, Color.yellow, 1);
        Debug.DrawRay(startP + d/2, R, Color.red, 1);

        for(int i = 1; i <= segments; i++)
        {
            float y = Mathf.Sin(i/segments * 180 * (Mathf.PI/180));
            result.Add(startP + (d*i/segments) + (R * y * d.magnitude/2));
        }
        
        return result;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        foreach (Vector3 item in _MovePoints)
        {
            Gizmos.DrawSphere(item, .2f);
        }
    }
}
