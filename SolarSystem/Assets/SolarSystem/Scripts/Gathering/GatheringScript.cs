using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringScript : MonoBehaviour {

    #region PUBLIC OBJECTS
    public InventoryScript.CollectibleType collectibleType;
    public int amount = 10;
    #endregion

    #region PRIVATE OBJECTS
    private bool gathering = false;
    private float currentTime = 0f;
    private InventoryScript inventoryScript;
    #endregion

    #region PRIVATE METHODS
    private void Start() {
        inventoryScript = InventoryScript.Instance;
    }

    private void Update() {

        // Enquanto coletando e não acabou
        if (gathering) {
            if (currentTime <= 1f) {
                currentTime += Time.deltaTime;
            } else {
                amount -= 1;
                currentTime = 0f;
                // Acabou?
                if (amount < 0) {
                    Debug.Log("Volume esgotado");
                    gathering = false;
                    gameObject.SetActive(false);
                } else {
                    // Chama o inventário e passa mais um para ele
                    inventoryScript.AddToInventory(collectibleType, 1);
                    Debug.Log("Coletou 1");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            gathering = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            gathering = false;
        }
    }

    #endregion

    #region PUBLIC METHODS
    #endregion

    #region PUBLIC PROPERTIES
    #endregion
}
