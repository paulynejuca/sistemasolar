using UnityEngine;
using UnityEngine.UI;

public class SpaceshipHealthScript : MonoBehaviour {

    [SerializeField] private int healthLevel;
    private int health;
    [SerializeField] private Slider healthSlider;

    private void Start() {
        health = healthLevel;
        healthSlider.wholeNumbers = true;
        healthSlider.maxValue = health;
        healthSlider.value = health;
        healthSlider.minValue = 0;
    }

    public void Hit(int hitValue) {
        health -= hitValue;
        healthSlider.value = health;

        if (health <= 0) {
            // Game Over
            Debug.Log("Game Over");
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Asteroid")) {
            Hit(3);
        }
    }
}
