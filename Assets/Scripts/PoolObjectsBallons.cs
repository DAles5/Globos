using System.Collections.Generic;
using UnityEngine;

//caracteristicas del globo
[System.Serializable]
public class BallonType {
    public string id;
    public GameObject prefab;
    public int points;
    public int initAmount;
    public float spawnSpeed;
    public float spawnWeight;
    public int requiredScore;
}
public class PoolObjectsBallons : MonoBehaviour
{
    [SerializeField] private List<BallonType> ballonsTypes;
    private Dictionary<string, Queue<GameObject>> pools =
    new Dictionary<string, Queue<GameObject>>();
    private void Awake()
    {
        foreach (var tipo in ballonsTypes)
        {
            Queue<GameObject> cola = new Queue<GameObject>();

            for (int i = 0; i < tipo.initAmount; i++)
            {
                GameObject ballon = Instantiate(tipo.prefab, transform);
                ballon.SetActive(false);
                cola.Enqueue(ballon);
            }
            pools.Add(tipo.id, cola);

        }
    }
    public GameObject Obtener(string id)
    {
        if (!pools.ContainsKey(id))
        {
            Debug.Log("el globo " + id + " no existe");
            return null;
        }

        var cola = pools[id];
        if (cola.Count > 0)
        {
            GameObject ballon = cola.Dequeue();
            ballon.SetActive(true);
            return ballon;
        }
        else
        {
            Debug.LogWarning("See agotó el pool de : " + id);
            return null;
        }
    }
    public void Retornar(string id, GameObject balloon)
    {
        balloon.SetActive(false);
        balloon.transform.SetParent(transform);
        pools[id].Enqueue(balloon);
    }
    public List<BallonType> GetTypes() {
        return ballonsTypes;
    }
}