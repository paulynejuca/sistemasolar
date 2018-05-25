using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringMetal : GatheringScript {

    #region SERIALIZED FIELDS
    [SerializeField] private InventoryScript.MetalType metalType;           // Tipo de metal do coletável
    #endregion

    #region PRIVATE FIELDS
    private float currentTime = 0f;                                         // Auxiliar para ajudar a contar o tempo
    private InventoryScript inventory;                                      // Referência do controle de inventário
    #endregion

    #region OVERRIDE METHOS
    protected override void Start () {
        base.Start();
        inventory = InventoryScript.Instance;
	}
	
	protected override void Update () {
        base.Update();
    }

    protected override void Gathering() {
        base.Gathering();

        // Enquanto coleta
        if (gathering) {
            if (currentTime <= 1f) {
                currentTime += Time.deltaTime;
            } else {
                amount -= 1;
                currentTime = 0f;
                // Reduz o tamanho da nuvem a medida que o jogador coleta
                transform.localScale *= AmountPercent() + .5f;

                // Se acabou o objeto é desativado
                if (amount < 0) {
                    gathering = false;
                    gameObject.SetActive(false);
                } else {
                    // Adiciona ao inventário
                    inventory.AddMetal(metalType, amount);
                }
            }
        }
    }

    protected override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other) {
        base.OnTriggerExit(other);
    }
    #endregion

    public InventoryScript.MetalType MetalType {
        get { return metalType; }
        set { metalType = value; }
    }
}
