using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe abstrata que repreent um objeto coletável
/// </summary>
public abstract class GatheringScript : MonoBehaviour {

    #region SERIALIZED OBJECTS
    [SerializeField] private int startAmount = 10;                      // Quantiade inicial máxima que o jogador pode coletar
    #endregion

    #region PROTECTED FIELDS
    protected int amount = 0;                                            //Quantidade restante que o jogador pode coletar
    protected bool gathering = false;                                    // Se está coletano ou não
    #endregion

    #region PRIVATE METHODS
    protected virtual void Start() {
        amount = startAmount;
    }

    protected virtual void Update() {
        Gathering();
    }

    /// <summary>
    /// Método de coleta para um tipo de elemento
    /// </summary>
    protected virtual void Gathering() {

    }

    /// <summary>
    ///  Calcula a porcentagem da quantiade coletável
    /// </summary>
    /// <returns> Retorna a porcentagem da quantiade coletável</returns>
    protected float AmountPercent(){
        return (amount / 100) * amount;
    }

    /// <summary>
    /// Quando o player entra no trigger, então a coleta inicia
    /// </summary>
    /// <param name="other">O outro objeto da colisão</param>
    protected virtual void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            gathering = true;
        }
    }

    /// <summary>
    /// Quando o player sai do trigger a coleta para
    /// </summary>
    /// <param name="other">O outro objeto da colisão</param>
    protected virtual void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            gathering = false;
        }
    } 
    #endregion
}