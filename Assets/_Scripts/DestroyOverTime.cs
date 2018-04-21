using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {


    [SerializeField]
    float lifetime = 1f;
    private void Start() {
        Invoke("Destroy", lifetime);
    }
    void Destroy() {
        Destroy(gameObject);
    }
}
