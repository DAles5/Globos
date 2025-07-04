
using UnityEngine;

public class BallonProperties : MonoBehaviour
{
    // Start is called before the first frame update
    public string TypeId;
    private PoolObjectsBallons pool;    

    void Start()
    {
        pool = FindObjectOfType<PoolObjectsBallons>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Top"))
        {
            pool.Retornar(TypeId,gameObject);
        }
    }
    private void OnMouseDown()
    {
        ScoreManager.Instance.AddPointsbyID(TypeId);
        pool.Retornar(TypeId, gameObject);
    }
}
