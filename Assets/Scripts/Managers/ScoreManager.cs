
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Singleton simple
    public static ScoreManager Instance;    
    [SerializeField] private PoolObjectsBallons pool;
    [SerializeField] private TextMeshProUGUI scoreText;

    private Dictionary<string, int> Type_By_Score = new();
    private int score = 0;

    void Start()
    {
        
    }
    private void Awake() {
        //Crear singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        //Obtener los tipos del globo desde el pool
        if(pool == null)
        {
            Debug.LogError("Score Manager necesita una referencia");
            return;
        }
        foreach(var _ in pool.GetTypes()){
            Type_By_Score[_.id] = _.points;
        }
        UpdateUI();
    }

    public void AddPointsbyID(string typeId)
    {
        //El programa busca si el globo existe en el diccionario
        //y guarda sus puntos en la variable points
        if (Type_By_Score.TryGetValue(typeId, out int points))
        {
            score += points;
            UpdateUI();
        }
    }
    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + score;
        }
    }
    //función reducida a una linea
    public int GetScore() => score;
}
