using System.Threading.Tasks;
using UnityEngine;

public class GetTargetScript : MonoBehaviour
{
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
            int targetNumber = gameManager.target;
            Debug.Log("Target Found: " + targetNumber);
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
