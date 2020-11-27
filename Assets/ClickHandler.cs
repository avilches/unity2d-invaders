using UnityEngine;

public class ClickHandler : MonoBehaviour {
    public GameObject floatingText;

	void Update () {
	    if(floatingText != null && Input.GetMouseButtonDown(0))
        {
            //get mouse position
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;

            //instantiate game object at point
            Instantiate(floatingText, pos, Quaternion.identity);
        }
	}
}
