using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que controla o inventário de itens coletados pelo jogaor.
/// </summary>
public class InventoryScript : MonoBehaviour {

    #region ENUMS
    /// <summary>
    /// Enum dos elementos que são metais
    /// </summary>
    public enum MetalType {
        Iron, Nickel, Aluminum, Magnesium, Potassium, Sodium, Calcium
    }

    /// <summary>
    /// Enum dos elementos que são gases
    /// </summary>
    public enum GasType {
        Oxygen, Hydrogen, Helium, Methane
    }

    /// <summary>
    /// Enum dos elementos que são rochas
    /// </summary>
    public enum RockType {
        Basalt, Silicon, Quartz, Cobalt
    }
    #endregion

    #region SERIALIZED FIELDS
    #endregion

    #region PRIVATE FIELDS
    private Dictionary<MetalType, int> metal;               // Dicionário que guarda os metais
    private Dictionary<GasType, int> gas;                   // Dicionário que guarda os gases
    private Dictionary<RockType, int> rock;                 // Dicionário que guarda as rochas
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
        metal = new Dictionary<MetalType, int> {
            { MetalType.Aluminum, 0 },
            { MetalType.Calcium, 0 },
            { MetalType.Iron, 0 },
            { MetalType.Magnesium, 0},
            { MetalType.Nickel, 0},
            { MetalType.Potassium, 0},
            { MetalType.Sodium, 0}
        };

        gas = new Dictionary<GasType, int> {
            { GasType.Helium, 0},
            { GasType.Hydrogen, 0},
            { GasType.Methane, 0},
            { GasType.Oxygen, 0}
        };

        rock = new Dictionary<RockType, int> {
            { RockType.Basalt, 0},
            { RockType.Cobalt, 0},
            { RockType.Quartz, 0},
            { RockType.Silicon, 0}
        };
    }
    #endregion

    #region PUBLIC METHODS
    /// <summary>
    /// Adiona um metal ao inventário
    /// </summary>
    /// <param name="metalType">Elemento metal</param>
    /// <param name="amount">Quantidade</param>
    public void AddMetal(MetalType metalType, int amount) {
        metal[metalType] += amount;
    }

    /// <summary>
    /// Adiciona um gás ao inventário
    /// </summary>
    /// <param name="gasType">Elemento gás</param>
    /// <param name="amount">Quantidade</param>
    public void AddGas(GasType gasType, int amount) {
        gas[gasType] += amount;
    }

    /// <summary>
    /// Adiciona uma rocha ao inventário
    /// </summary>
    /// <param name="rockType">Tipo de rocha</param>
    /// <param name="amount">Quantidade</param>
    public void AddRock(RockType rockType, int amount) {
        rock[rockType] += amount;
    }
    #endregion

    #region PUBLIC PROPERTIES
    public int Aluminum {
        get { return metal[MetalType.Aluminum]; }
    }

    public int Calcium {
        get { return metal[MetalType.Calcium]; }
    }

    public int Iron {
        get { return metal[MetalType.Iron]; }
    }

    public int Magnesium {
        get { return metal[MetalType.Magnesium]; }
    }

    public int Nickel {
        get { return metal[MetalType.Nickel]; }
    }

    public int Potassium {
        get { return metal[MetalType.Potassium]; }
    }

    public int Sodium {
        get { return metal[MetalType.Sodium]; }
    }

    public int Helium {
        get { return gas[GasType.Helium];}
    }

    public int Hydrogen {
        get { return gas[GasType.Hydrogen]; }
    }

    public int Methane {
        get { return gas[GasType.Methane]; }
    }

    public int Oxygen {
        get { return gas[GasType.Oxygen]; }
    }

    public int Basalt {
        get { return rock[RockType.Basalt]; }
    }

    public int Cobalt {
        get { return rock[RockType.Cobalt]; }
    }

    public int Quartz {
        get { return rock[RockType.Quartz]; }
    }

    public int Silicon {
        get { return rock[RockType.Silicon]; }
    }
    #endregion
}
