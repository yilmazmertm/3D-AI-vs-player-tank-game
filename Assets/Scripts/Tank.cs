using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    public Rigidbody rb { get {return GetComponent<Rigidbody>();} }

    public Material mat;

    public float moveSpeed = 10f;
    public float rotSpeed = 240f;
    public Transform other;
    public Transform turret;
    public Rigidbody bombPrefab;
    public Transform bombSpawn;
    [Range(10000f, 30000f)]
    public float bombSpeed = 10000f;
    void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();

    protected abstract IEnumerator LookAt(Transform other);


    protected void Fire()
    {
        var bomb = Instantiate(bombPrefab, bombSpawn.position, Quaternion.identity);

        bomb.AddForce(turret.forward * bombSpeed);
    }

    protected void createMoveEffect(float moveAxis)
    {
        mat.mainTextureOffset += new Vector2(moveAxis, 0);
    }
}
