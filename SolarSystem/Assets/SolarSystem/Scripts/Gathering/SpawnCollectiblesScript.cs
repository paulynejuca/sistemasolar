using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectiblesScript : MonoBehaviour {

    // Representação e um coletável
    [System.Serializable]
    public struct Collectible {
        public InventoryScript.CollectibleType collectibleType;
        public int amount;
        public GameObject prefab;
    }

    private Transform[] spawnPoints;// Transforms de spawn
    public Vector3 spawnSize;//Tamanho dos spawns
    public Collectible[] collectibles;// Lista dos coletáveis configurados no editor

    private int currentSpawn = 0;// Auxiliar para calcular o próximo ínice de spawn
    
	private void Start () {
        spawnPoints = GetComponentsInChildren<Transform>();
        Spawn();
	}

    // Seleciona um índice de spawn correspondente a uma posição na lista de spawns
    private int GetSpawnIndex() {
        currentSpawn = (currentSpawn + 1) % spawnPoints.Length;
        return currentSpawn;
    }

    // Retorna um Transform da lista de spawns de acordo com um index
    private Transform GetSpawnPoint(int spawnIndex) {
        return spawnPoints[spawnIndex];
    }

    // Retorna um Vector3 randômigo em relação a posição de um transform
    private Vector3 GetPoint(Transform spawnPoint) {
        return new Vector3(
            spawnPoint.position.x + Random.Range(-spawnSize.x / 2, spawnSize.x / 2),
            spawnPoint.position.y + Random.Range(-spawnSize.y / 2, spawnSize.y / 2),
            spawnPoint.position.z + Random.Range(-spawnSize.z / 2, spawnSize.z / 2));
    }

    // Spawna cada um dos objetos o vetor de coletáveis
    private void Spawn() {
        for (int i = 0; i < collectibles.Length; i++) {

            Collectible c = collectibles[i];

            switch (c.collectibleType) {
                case InventoryScript.CollectibleType.Gas:
                    SpawnObject(c.prefab, c.amount);
                    break;
                case InventoryScript.CollectibleType.Metal:
                    SpawnObject(c.prefab, c.amount);
                    break;
                case InventoryScript.CollectibleType.Rock:
                    SpawnObject(c.prefab, c.amount);
                    break;
            }
        }
        
    }

    // Spawna um amount de objetos de um tipo
    private void SpawnObject(GameObject prefab, int amount) {
        for (int i = 0; i < amount; i++) {
            int spwanIndex = GetSpawnIndex();
            Transform spawnPoint = GetSpawnPoint(spwanIndex);
            Vector3 pos = GetPoint(spawnPoint);

            GameObject go = Instantiate<GameObject>(prefab, pos, Quaternion.identity);
            // Ainda alguns bugs ao tentar setar o transform o spawn selecionado em pai do objeto instânciado
            // Alguns objetos não estão relacionaos a quem deveria ser o pai
            go.transform.parent = spawnPoint;// Seta o spawn selecionado como pai desse objeto
        }
    }
    
    // Desenha as áreas de spawn no editor
    // Ainda algusn bugs com relação a desenhar as áreas de spawn no editor
    private void OnDrawGizmosSelected() {
        if (spawnPoints.Length > 0) {
            foreach (Transform point in spawnPoints) {
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(point.position, spawnSize);
            }
        }else {
            Debug.LogWarning("No spawns");
        }
    }
}
