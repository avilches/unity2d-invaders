using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton {
    private AudioSource audioSource;

    private int score;

    protected override void Init() {
        Debug.Log(SceneManager.GetActiveScene().name);
        audioSource = gameObject.GetComponent<AudioSource>();
        ResetScore();
    }

    public int Score => score;

    public void ResetScore() {
        score = 0;
    }

    public int AddScore(int x) {
        score += x;
        return score;
    }

    public static GameManager Instance => instance as GameManager;
}