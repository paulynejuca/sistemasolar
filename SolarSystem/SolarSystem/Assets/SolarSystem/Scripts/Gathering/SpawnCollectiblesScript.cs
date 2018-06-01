using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectiblesScript : MonoBehaviour {

    #region COLLECTIBLES STRUCTS
    [System.Serializable]
    public struct Metal {
        public int amount;
        public GameObject prefab;
    }

    [System.Serializable]
    public struct Gas {
        public int amount;
        public GameObject prefab;
    }

    [System.Serializable]
    public struct Rock {
        public int amount;
        public GameObject prefab;
    }
    #endregion

    #region SERIALIZED FIELDS
    [SerializeField] private Metal metal;
    [SerializeField] private Gas gas;
    [SerializeField] private Rock rock;
    [SerializeField] private List<Transform> spawnList;
    [SerializeField] private Vector3 spawnSize;
    #endregion

    #region PRIVATE FIELDS
    private List<GameObject> spawnedObjects;
    private int currentSpawnIndex = 0;
    #endregion

    private void Awake() {
        spawnedObjects = new List<GameObject>();
    }

    void Start() {
        SpawnObjects();
    }

    void SpawnObjects() {
        for (; metal.amount > 0;) {
            RandomSpawnIndex();
            Vector3 pos = RandomSpawnArea(spawnList[currentSpawnIndex]);
            SpawnMetal(pos);
            metal.amount--;
        }

        for (; gas.amount > 0;) {
            RandomSpawnIndex();
            Vector3 pos = RandomSpawnArea(spawnList[currentSpawnIndex]);
            SpawnGas(pos);
            gas.amount--;
        }

        for (; rock.amount > 0;) {
            RandomSpawnIndex();
            Vector3 pos = RandomSpawnArea(spawnList[currentSpawnIndex]);
            SpawnRock(pos);
            rock.amount--;
        }
    }

    private void RandomSpawnIndex() {
        int random = Random.Range(0, spawnList.Count - 1);
        if (random == currentSpawnIndex) {
            RandomSpawnIndex();
        } else {
            currentSpawnIndex = random;
        }
    }

    private Vector3 RandomSpawnArea(Transform spawn) {
        return new Vector3(
            Random.Range(((-spawnSize.x / 2) + spawn.position.x), ((spawnSize.x / 2) + spawn.position.x)),
            Random.Range(((-spawnSize.y / 2) + spawn.position.y), ((spawnSize.y / 2) + spawn.position.y)),
            Random.Range(((-spawnSize.z / 2) + spawn.position.z), ((spawnSize.z / 2) + spawn.position.z)));
    }

    private void SpawnMetal(Vector3 pos) {
        GameObject go = Instantiate<GameObject>(metal.prefab);
        go.transform.position = pos;
        spawnedObjects.Add(go);

        int randomMetal = Random.Range(0, 6);
        switch(randomMetal){
            case 0: go.GetComponent<GatheringMetal>().MetalType = InventoryScript.MetalType.Aluminum; break;
            case 1: go.GetComponent<GatheringMetal>().MetalType = InventoryScript.MetalType.Calcium; break;
            case 2: go.GetComponent<GatheringMetal>().MetalType = InventoryScript.MetalType.Iron; break;
            case 3: go.GetComponent<GatheringMetal>().MetalType = InventoryScript.MetalType.Magnesium; break;
            case 4: go.GetComponent<GatheringMetal>().MetalType = InventoryScript.MetalType.Nickel; break;
            case 5: go.GetComponent<GatheringMetal>().MetalType = InventoryScript.MetalType.Potassium; break;
            default: go.GetComponent<GatheringMetal>().MetalType = InventoryScript.MetalType.Sodium; break;
        }
    }

    private void SpawnGas(Vector3 pos) {
        GameObject go = Instantiate<GameObject>(gas.prefab);
        go.transform.position = pos;
        spawnedObjects.Add(go);

        int randomGas = Random.Range(0, 3);
        switch (randomGas) {
            case 0: go.GetComponent<GatheringGas>().GasType = InventoryScript.GasType.Helium; break;
            case 1: go.GetComponent<GatheringGas>().GasType = InventoryScript.GasType.Hydrogen; break;
            case 2: go.GetComponent<GatheringGas>().GasType = InventoryScript.GasType.Methane; break;
            default: go.GetComponent<GatheringGas>().GasType = InventoryScript.GasType.Oxygen; break;
        }
    }

    private void SpawnRock(Vector3 pos) {
        GameObject go = Instantiate<GameObject>(rock.prefab);
        go.transform.position = pos;
        spawnedObjects.Add(go);

        int randomRock = Random.Range(0, 3);
        switch (randomRock) {
            case 0: go.GetComponent<GatheringRock>().RockType = InventoryScript.RockType.Basalt; break;
            case 1: go.GetComponent<GatheringRock>().RockType = InventoryScript.RockType.Cobalt; break;
            case 2: go.GetComponent<GatheringRock>().RockType = InventoryScript.RockType.Quartz; break;
            default: go.GetComponent<GatheringRock>().RockType = InventoryScript.RockType.Silicon; break;
        }
    }

    private void OnDrawGizmosSelected() {
        foreach (Transform t in spawnList) {
            Gizmos.DrawCube(t.position, spawnSize);
        }
    }
}
