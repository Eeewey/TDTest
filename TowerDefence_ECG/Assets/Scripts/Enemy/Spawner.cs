﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public float m_interval = 3;
    public GameObject m_moveTarget;
    public Monster monsterPrefab;

    private float m_lastSpawn = -1;

    void Update()
    {
        if (Time.time > m_lastSpawn + m_interval)
        {
            // var newMonster = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            // var r = newMonster.AddComponent<Rigidbody>();
            // newMonster.AddComponent<CapsuleCollider>();
            // r.useGravity = false;
            // newMonster.transform.position = transform.position;
            // var monsterBeh = newMonster.AddComponent<Monster>();
            // monsterBeh.m_moveTarget = m_moveTarget;

            var newMonster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);

            newMonster.m_moveTarget = m_moveTarget;

            m_lastSpawn = Time.time;
        }
    }
}