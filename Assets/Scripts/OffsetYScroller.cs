using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetYScroller : MonoBehaviour {
    [SerializeField] private float speed = 0.2F;
    private Material _material;
    private Vector2 _offset;

    // Start is called before the first frame update
    void Start() {
        _material = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update() {
        _offset = new Vector2(0f, speed);
        _material.mainTextureOffset += _offset * Time.deltaTime;
    }
}