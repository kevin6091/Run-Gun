using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.Searcher.SearcherWindow;
using UnityEngine.UIElements;
using System.Security.Cryptography;
using System.Timers;
using System;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Splines.Interpolators;

public class DroneController : MonoBehaviour
{
    private float _separationRadius = 2.5f;
    private float _neighborRadius = 5.0f;
    private float _separationWeight = 1.0f;
    private float _alignmentWeight = 1.0f;
    private float _cohesionWeight = 1.2f;
    private float _leaderWeight = 1.8f;
    private float _toTargetForLeaderWeight = 1.5f;
    private float _accTime = 0.0f;
    private bool _boolean = false;

    public float DistanceToTarget { get; set; }
    public Vector3 Velocity { get; set; }
    Player Player { get; set; }

    static DroneController _leader = null;
    static public Vector3 DroneTarget { get; set; }

    void Start()
    {
        Player = (Managers.Scene.CurrentScene as GameScene).GamePlayer;
        transform.position = Player.transform.position + new Vector3(0.0f, 8.0f, -8.0f);

        if (_leader == null) //한번밖에 실행안되는 곳 / Static변수 다루기
        {
            _leader = this;
            StartCoroutine(Co_SortLeader());
            StartCoroutine(Co_MoveTarget());
        }
    }

    void Update()
    {
        DroneTarget = Player.transform.position + new Vector3(0.0f, 5.0f, -2.0f);
        DistanceToTarget = (DroneTarget - transform.position).magnitude;
        Velocity = (DroneTarget - transform.position).normalized;

        Vector3 movement = CalculateFlocking();

        transform.position += movement * Time.deltaTime;

        // movement.x -> z축 회전
        // movement.z -> x축 회전
        float zAngle = -movement.x * 3.0f;
        float xAngle = movement.z * 3.0f;
        
        Vector3 currentEuler = transform.localEulerAngles;
        currentEuler = NormalizeAngles(currentEuler);

        if(currentEuler.z + zAngle >= 30.0f || currentEuler.z + zAngle <= -30.0f)
        {
            zAngle = 0.0f;
        }
        if(currentEuler.x + xAngle >= 30.0f || currentEuler.x + xAngle <= -30.0f)
        {
            xAngle = 0.0f;
        }

        Quaternion baseRot = Quaternion.identity;
        Quaternion addRot = Quaternion.Euler(xAngle, 0.0f, zAngle);
        Quaternion finalRot = transform.localRotation * addRot;

        finalRot = Quaternion.Lerp(finalRot, baseRot, 0.1f);

        transform.localRotation = finalRot;
    }

    Vector3 NormalizeAngles(Vector3 angles)
    {
        angles.x = NormalizeAngle(angles.x);
        angles.y = NormalizeAngle(angles.y);
        angles.z = NormalizeAngle(angles.z);
        return angles;
    }

    float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }

    public Vector3 CalculateFlocking()
    {
        Vector3 separation = Vector3.zero;
        Vector3 alignment = Vector3.zero;
        Vector3 cohesion = Vector3.zero;
        Vector3 toLeader = Vector3.zero;
        Vector3 toTargetForLeader = Vector3.zero;

        int neighborCount = 0;

        foreach (Drone ele in Drone.Drones)
        {
            DroneController eleController = ele.DroneController;

            if (eleController == _leader && eleController == this)
            {
                toTargetForLeader = (DroneTarget - transform.position);
            }

            if (eleController == this)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, eleController.transform.position);
            if (distance == 0.0f)
            {
                distance = 0.1f;
            }

            // Separation: 가까우면 멀어지기
            if (distance < _separationRadius)
                separation -= (eleController.transform.position - transform.position).normalized / distance; // 더 가까울수록 더 강하게

            // Alignment & Cohesion: neighborRadius 내의 모든 드론
            if (distance < _neighborRadius)
            {
                alignment += eleController.Velocity;
                cohesion += eleController.transform.position;
                neighborCount++;
            }
        }

        if (neighborCount > 0)
        {
            alignment /= neighborCount;
            alignment -= Velocity; // 내 속도와 차이만큼 맞추기

            cohesion /= neighborCount;
            cohesion = (cohesion - transform.position).normalized; // 내 위치에서 이웃의 무게중심으로 향함
        }

        toLeader = (_leader.transform.position - transform.position).normalized;

        // 가중치 조합
        Vector3 move =
            separation * _separationWeight +
            alignment * _alignmentWeight +
            cohesion * _cohesionWeight + 
            toLeader * _leaderWeight +
            toTargetForLeader * _toTargetForLeaderWeight;

        return move;
    }

    IEnumerator Co_SortLeader()
    {
        yield return null;

        while (true)
        {
            Drone.Drones.Sort();
            _leader = Drone.Drones[0].DroneController;

            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator Co_MoveTarget()
    {
        while (true)
        {
            if (_accTime <= -1.0f && _boolean == false)
            {
                _boolean = true;
                yield return new WaitForSeconds(3.0f);
            }
            else if (_accTime >= 1.0f && _boolean == true)
            {
                _boolean = false;
                yield return new WaitForSeconds(3.0f);
            }

            if (_boolean == true)
            {
                _accTime += Time.deltaTime * Time.deltaTime * 15.0f;
            }
            else
            {
                _accTime -= Time.deltaTime * Time.deltaTime * 15.0f;
            }

            DroneTarget = new Vector3(DroneTarget.x, DroneTarget.y, DroneTarget.z + _accTime * 5.0f);

            yield return null;
        }
    }
}
