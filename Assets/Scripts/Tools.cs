using System.Collections.Generic;
using UnityEngine;

public class Tools {

    public static List<Transform> listTransformChildren(GameObject obj) {
        var path = new List<Transform>();
        foreach (Transform t in obj.transform) {
            path.Add(t);
        }
        return path;
    }
}