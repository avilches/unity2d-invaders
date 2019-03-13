using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideShowLogs : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        //Destroy(collision.gameObject);
        Debug.Log(name + ". Triggered by " + collision.gameObject.name);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(name + ". Object " + collision.gameObject.name + " entered");
        //Destroy(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        Debug.Log(name + ". Object " + collision.gameObject.name + " exited");
    }
}