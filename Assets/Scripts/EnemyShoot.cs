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

    private GameObject points;

    private List<Transform> path;
    private WaveConfig config;
    private int pos;


    // Start is called before the first frame update
    void Start() {
        transform.position = path[0].position;

        nextShootTime = Random.Range(minSecondsShoot, maxSecondsShoot);
        points = transform.GetChild(0).gameObject;
        points.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (points.activeSelf) {
            points.transform.Translate(Vector3.up * Time.deltaTime);
            points.transform.Translate(Vector3.left * Time.deltaTime *Random.Range(-4, 4F));
            var textMesh = points.GetComponent<TextMesh>();
            Color color = textMesh.color;
            //set the alpha and color
            color.a = color.a - Time.deltaTime;
            textMesh.color = color;
        } else {
            Move();
            nextShootTime -= Time.deltaTime;
            if (nextShootTime <= 0) {
                Shoot();
                nextShootTime = Random.Range(minSecondsShoot, maxSecondsShoot);
            }
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
                DisableAnHideBodyAndShowScore();
                AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
                for (int i = 0; i < explosion.Count; i++) {
                    var expl = Instantiate(explosion[i], transform.position, explosion[i].transform.rotation);
                    Destroy(expl, 1F);
                }
                Camera.main.GetComponent<CameraShake>().Shake(0.07f, 0.2f);
                Destroy(gameObject, 1F);
            }
        }
    }

    private void DisableAnHideBodyAndShowScore() {
        points.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        if (GetComponent<PolygonCollider2D>() != null) {
            GetComponent<PolygonCollider2D>().enabled = false;
        }

        if (GetComponent<CircleCollider2D>() != null) {
            GetComponent<CircleCollider2D>().enabled = false;
        }

        points.GetComponent<TextMesh>().text = "" + GameManager.Instance.AddScore(1);
    }

    public void setConfig(WaveConfig config) {
        this.config = config;
        // var obj = Resources.Load<GameObject>("Paths/Path (0)");
        path = config.getWaypoints();
    }

    // Start is called before the first frame update
    // Update is called once per frame
    private void Move() {
        if (!config) return;
        if (pos < path.Count) {
            var next = path[pos];
            transform.position =
                Vector2.MoveTowards(transform.position, next.position, config.MoveSpeed * Time.deltaTime);
            //Debug.Log("from "+transform.position);
            //Debug.Log("to "+next.position);
            if (transform.position.x == next.position.x && transform.position.y == next.position.y) {
//            Debug.Log("" + pos);
//            Debug.Log("" + pos + 1);
//            Debug.Log("" + (pos + 1) % path.Count);
                pos++;
            }
        } else {
            Destroy(gameObject);
        }
    }    
}