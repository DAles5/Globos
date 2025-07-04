using System.Collections.Generic;
using UnityEngine;

public class BallonnsSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PoolObjectsBallons pool;
    private List<BallonType> types;
    private BoxCollider2D spawnerArea;
   
    public float spawnSpeed;

    
    void Start()
    {
        types = pool.GetTypes();
        spawnerArea = GetComponent<BoxCollider2D>();

        InvokeRepeating(nameof(spawnBaloon), 1f, spawnSpeed);
    }

    // Update is called once per frame
    string randomBallon(List<BallonType> available)
    {
        float totalWeight = 0;
        foreach (var _ in available)
        {
            totalWeight += _.spawnWeight;
        }
        float r = Random.Range(0, totalWeight);
        float amount = 0;

        foreach(var _ in available)
        {
            amount += _.spawnWeight;
            if (r < amount)
                return _.id;
        }
        return available[0].id;
    }
    void spawnBaloon()
    {
        int currentScore = ScoreManager.Instance.GetScore();

        //Filtrar globos
        var available = types.FindAll(t => currentScore >= t.requiredScore);
        if (available.Count == 0) available = types.FindAll(t=> t.requiredScore <=0);

        //Elige un globo aleatoro según su peso
        string tipo = randomBallon(available);
        GameObject ballon = pool.Obtener(tipo);
        BallonProperties props = ballon.GetComponent<BallonProperties>();
        
        //Asignar correctamente un id
        if(props != null)
        {
            props.TypeId = tipo;
        }

        if(ballon != null)
        {
            ballon.transform.position = randomPositionSpawn();

            //Aplicar una fuerza hacia arriba
            Rigidbody2D rb = ballon.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                float fuerza = available.Find(t => t.id == tipo).spawnSpeed;
                rb.AddForce(Vector2.up * fuerza, ForceMode2D.Impulse);
            }
        }
    }
    Vector2 randomPositionSpawn()
    {
        Vector2 min = spawnerArea.bounds.min;
        Vector2 max = spawnerArea.bounds.max;
        return new Vector2(Random.Range(min.x, max.x), transform.position.y);
    }
}
