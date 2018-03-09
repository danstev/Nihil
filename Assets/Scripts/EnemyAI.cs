﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public Transform target = null;
    private float tempTimer;
    public float speed, reach, timer;
    public int damage;
    public bool dead;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (dead)
            return;

        tempTimer -= Time.deltaTime;

        if (target == null && tempTimer <= 0)
        {
            timer = Random.Range(1f, 5f);
            tempTimer = timer;
            StartCoroutine(RandomMove(timer, transform));
            Debug.Log(gameObject.name + " is randomly moving.");
        }
        else if (target != null && tempTimer <= 0)
        {
            timer = Random.Range(1f, 5f);
            tempTimer = timer;
            StartCoroutine(AttackMove(timer, target));
            Debug.Log(gameObject.name + " is attacking: " + target.name + ".");
        }

    }

    IEnumerator AttackMove(float time, Transform target)
    {
        float start = Time.time;
        //transform.LookAt(target, Vector3.up);

        float attackTimer = 1f;

        while (Time.time <= start + time)
        {

            if (dead)
            {
                yield break;
            }

            if (attackTimer <= 0)
            {
                Attack();
                attackTimer = 1f;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            yield return new WaitForEndOfFrame();
        }
    }

    void Attack()
    {
        if (target == null)
        {

        }
        else if (reach > Vector3.Distance(transform.position, target.transform.position))
        {
            target.transform.SendMessage(("TakeDamage"), damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void SetDead()
    {
        dead = true;
    }

    IEnumerator RandomMove(float time, Transform t)
    {
        float start = Time.time;
        Vector3 euler = t.eulerAngles;
        euler.y = Random.Range(0f, 360f);
        t.eulerAngles = euler;
        while (Time.time <= start + time)
        {

            if (dead)
            {
                yield break;
            }

            t.position = t.position + (t.transform.forward * Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null && other.tag == "Player")
        {
            Debug.Log(other.name + " has entered aggro range.");
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.name + " has left aggro range.");
            target = null;
        }
    }
}
