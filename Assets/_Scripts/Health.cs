using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDestructable {

    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth = 10;
    [SerializeField] Slider healthSlider;
    

    public void Kill() {
        //TODO make this not the wrong way, but you are out of time
        if(this.gameObject.GetComponent<Enemy>() != null) {
            TurnManager.Instance.CallYouWin();
        }
        else {
            TurnManager.Instance.CallGameOver();
        }
        
        Destroy(gameObject);
    }

    public void TakeDamage(float amount) {
        if (this.enabled) {
            currentHealth -= amount;
            if (currentHealth <= 0) {
                Kill();
            }
        }
        
    }
    private void OnEnable() {
        //Debug.Log("enabled on " + this.gameObject);
       
    }
    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        if(currentHealth == 0) {
            healthSlider.value = 0;
        }
        else {
            healthSlider.value = currentHealth / maxHealth;
        }
	}
}
