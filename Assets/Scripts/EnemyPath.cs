using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {
    private List<Transform> path;
    private WaveConfig config;
    private int pos;

    public void setConfig(WaveConfig config) {
        this.config = config;
        // var obj = Resources.Load<GameObject>("Paths/Path (0)");
        path = config.getWaypoints();
    }

    // Start is called before the first frame update
    void Start() {
        transform.position = path[0].position;
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

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