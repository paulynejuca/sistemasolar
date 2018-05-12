using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringScript : MonoBehaviour {

    #region PUBLIC OBJECTS
    public InventoryScript.CollectibleType collectibleType;// Tipo de coletável
    public int startAmount = 10;// Quantiade inicial máxima que o jogador pode coletar
    #endregion

    #region PRIVATE OBJECTS
    private int amount = 0;//Quantidade restante que o jogador pode coletar
    private bool gathering = false;// Se está coletano ou não
    private float currentTime = 0f;// Auxiliar para contar o tempo para coletar -1
    private InventoryScript inventoryScript;// Referência para o script que armazena os valores coletados de cada tipo de coletável
    private ParticleSystem particle;// Sistema de partículas do objeto
    private ParticleSystem.EmissionModule particleEmission;// Módulo de emissão das partículas do objeto
    private float startRateOverTime = 0;// Rate de emissão de partículas inicial
    #endregion

    #region PRIVATE METHODS
    private void Start() {
        inventoryScript = InventoryScript.Instance;
        particle = GetComponent<ParticleSystem>();
        particleEmission = particle.emission;
        amount = startAmount;
        startRateOverTime = particleEmission.rateOverTime.constant;
    }

    private void Update() {

        // Enquanto coletando e não acabou
        if (gathering) {
            if (currentTime <= 1f) {
                currentTime += Time.deltaTime;
            } else {
                amount -= 1;
                currentTime = 0f;
                // Reduz o tamanho da nuvem a medida que o jogador coleta
                particleEmission.rateOverTime = Percent(amount) * startRateOverTime;
                // Acabou? Se acabou a nuvem é desativada
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

    // Calcula a porcentagem de um valor v
    private float Percent(float v){
        return (v / 100) * v;
    }

    // Se oplayer entra no trigger, então a coleta inicia
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            gathering = true;
        }
    }

    // Se o player sai do trigger a coleta para
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