using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {

    // Tipos de coletáveis
    public enum CollectibleType {
        Gas, Metal, Rock
    }

    #region PUBLIC OBJECTS
    public Text gasText;// Text que exibe a quantidade de gás na interface
    public Text metalText;// Text que exibe a quantidade de metal na interface
    public Text rockText;// Text que exibe a quantidade de rochas na interface
    #endregion

    #region PRIVATE OBJECTS
    private Dictionary<CollectibleType, int> collectibles;// Dictionary que guarda todos os coletáveis e suas quantidades
    #endregion

    #region SINGLETON
    private static InventoryScript instance = null;
    public static InventoryScript Instance {
        get { return instance; }
    }

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    #region PRIVATE METHODS
    private void Start () {
        collectibles = new Dictionary<CollectibleType, int> {
            { CollectibleType.Gas, 0 },
            { CollectibleType.Metal, 0 },
            { CollectibleType.Rock, 0 }
        };
    }
    #endregion

    #region PUBLIC METHODS
    /// <summary>
    /// Adiciona uma quantidade de um material ao inventário
    /// </summary>
    /// <param name="collectibleType">Tipo de coletável</param>
    /// <param name="value">Quantidade recebida</param>
    public void AddToInventory(CollectibleType collectibleType, int value) {
        collectibles[collectibleType] += value;

        switch (collectibleType) {
            case CollectibleType.Gas: gasText.text = "Gas: " + collectibles[collectibleType]; break;
            case CollectibleType.Metal: gasText.text = "Metal: " + collectibles[collectibleType]; break;
            case CollectibleType.Rock: gasText.text = "Rock: " + collectibles[collectibleType]; break;
        }

        Debug.Log("Mais um adicionado ao inventário");
    }
    #endregion

    #region PUBLIC PROPERTIES
    #endregion
}
