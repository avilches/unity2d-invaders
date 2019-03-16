using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private int laserSpeed;
    [SerializeField] private int health;
    [SerializeField] private List<GameObject> explosion;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] [Range(0, 1)] private float deathVolume;

    [SerializeField] private float minSecondsShoot;
    [SerializeField] private float maxSecondsShoot;
    private float nextShootTime = 0;

    // Start is called before the first frame update
    void Start() {
        nextShootTime = Random.Range(minSecondsShoot, maxSecondsShoot);
    }

    // Update is called once per frame
    void Update() {
        nextShootTime -= Time.deltaTime;
        if (nextShootTime <= 0) {
            Shoot();
            nextShootTime = Random.Range(minSecondsShoot, maxSecondsShoot);
        }
    }

    void Shoot() {
        var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var damage = collision.GetComponent<Damage>();
        if (damage != null) {
            health -= damage.DamagePoints;
            damage.Hit();

            if (health <= 0) {
                GameManager.Instance.AddScore(1);
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
                for (int i = 0; i < explosion.Count; i++) {
                    var expl = Instantiate(explosion[i], transform.position, explosion[i].transform.rotation);
                    Destroy(expl, 1F);
                }
            }
        }
    }
}