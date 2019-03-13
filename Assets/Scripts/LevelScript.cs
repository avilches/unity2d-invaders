using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour {
    // Start is called before the first frame update
    public void GoToMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void GoToGame() {
        GameManager.Instance.ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void GoToGameOver() {
        StartCoroutine(Die());
    }

    private IEnumerator Die() {
        yield return new WaitForSeconds(1F);
        SceneManager.LoadScene("Game Over");
    }

    public void Quit() {
        Application.Quit();
    }
}