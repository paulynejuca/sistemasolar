using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectiblesScript : MonoBehaviour {

    #region COLLECTIBLES STRUCTS
    [System.Serializable]
    public struct Metal {
        public InventoryScript.MetalType metalType;
        public int amount;
        public GameObject prefab;
    }

    [System.Serializable]
    public struct Gas {
        public InventoryScript.GasType gasType;
        public int amount;
        public GameObject prefab;
    }

    [System.Serializable]
    public struct Rock {
        public InventoryScript.RockType rockType;
        public int amount;
        public GameObject prefab;
    }
    #endregion

    #region SERIALIZED FIELDS
    [SerializeField] private Metal metal;
    [SerializeField] private Gas gas;
    [SerializeField] private Rock rock;
    [SerializeField] private List<Transform> spawnList;
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
            SpawnMetal(spawnList[currentSpawnIndex].position);
        }

        for (; gas.amount > 0;) {
            RandomSpawnIndex();
            SpawnGas(spawnList[currentSpawnIndex].position);
        }

        for (; rock.amount > 0;) {
            RandomSpawnIndex();
            SpawnRock(spawnList[currentSpawnIndex].position);
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

    private void SpawnMetal(Vector3 pos) {
            GameObject go = Instantiate<GameObject>(metal.prefab);
            metal.amount--;
            go.transform.position = pos;
            spawnedObjects.Add(go);
    }

    private void SpawnGas(Vector3 pos) {
            GameObject go = Instantiate<GameObject>(gas.prefab);
            gas.amount--;
            go.transform.position = pos;
            spawnedObjects.Add(go);
    }

    private void SpawnRock(Vector3 pos) {
            GameObject go = Instantiate<GameObject>(rock.prefab);
            rock.amount--;
            go.transform.position = pos;
            spawnedObjects.Add(go);
    }
}
