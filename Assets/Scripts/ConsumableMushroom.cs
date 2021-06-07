using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableMushroom : MonoBehaviour
{
    public BoxCollider2D[] colliders;
    private float velocity = 3.0f;
    private bool goRight; // true: right | false: left
    private Rigidbody2D mushbody;
    private Vector2 direction;

    private void Start()
    {
        mushbody = GetComponent<Rigidbody2D>();
        int rnd = Random.Range(0, 2);
        direction = rnd == 0 ? Vector2.left : Vector2.right;

        mushbody.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
        mushbody.AddForce(direction * velocity, ForceMode2D.Impulse);
        goRight = true;
        InvokeRepeating("StartSwitching", 0, 5);
    }
    // Update is called once per frame
    void Update()
    {

        if (!goRight)
        {
            // move left
            transform.Translate(Vector2.left * Time.deltaTime * velocity);
        }
        else
        {
            // move right
            transform.Translate(Vector2.right * Time.deltaTime * velocity);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SwitchDirection();
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("Obstacles"))
        {
            SwitchDirection();
        }
    }
/*
    private void StartSwitching()
    {
        float timeDelay = Random.Range(2.0f, 5.0f);
        Invoke("SwitchDirection", timeDelay);
    }
*/
    private void SwitchDirection()
    {
        goRight = !goRight;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
