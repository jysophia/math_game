using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GetTargetScript : MonoBehaviour
{
    public TMP_Text targetText;
    public int targetNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetTargetNumber();
    }

    async void GetTargetNumber()
    {
        GameManagerScript gameManager = FindFirstObjectByType<GameManagerScript>();
        if (gameManager != null)
        {
            while (gameManager.target == 0)
            {
                await Task.Yield();
            }
            targetNumber = gameManager.target;
            targetText.text = targetNumber.ToString();
        }
        else
        {
            Debug.LogError("GameManagerScript not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
