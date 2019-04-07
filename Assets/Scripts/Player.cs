using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Player : MonoBehaviour {
    float xMin, xMax, yMin, yMax;


    [SerializeField] float speed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] float laserSpeed = 1f;
    [SerializeField] float laserAuto = 0.2f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] private int health;

    Coroutine fire;


    void Start() {
        updateBoundaries();
        health = 1;
    }

    private void updateBoundaries() {
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update() {
        Move();
        Fire();
    }

    void Fire() {
        if (Input.GetButtonDown("Fire1")) {
            fire = StartCoroutine(FireTask());
        }

        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(fire);
        }
    }

    IEnumerator FireTask() {
        while (true) {
            var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(laserAuto);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var damage = collision.GetComponent<Damage>();
        if (damage != null) {
            health -= damage.DamagePoints;
            damage.Hit();

            if (health <= 0) {
                Destroy(gameObject);
                FindObjectOfType<LevelScript>().GoToGameOver();
            }
        }
    }

    void Move() {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, -6F, 6F);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, -11.58F, 9F);
        transform.position = new Vector2(newXPos, newYPos);
    }
}