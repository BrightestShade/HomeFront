using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PromixityButton : MonoBehaviour
{
    [Header("Distance")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform target; // what is the object or position
    [SerializeField] private float maxDistance = 0.5f; //distance to enable button 

    [SerializeField] private Button actionButton; //refer button 

    [SerializeField] GameObject shopMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionButton.onClick.AddListener(TryPerformAction);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, target.position);

        // Disable the button if the player is too far
        actionButton.interactable = distance <= maxDistance;
    }

    void TryPerformAction()
    {
        float distance = Vector3.Distance(player.position, target.position);

        if (distance <= maxDistance)
        {
            PerformAction();
        }
        else
        {
            Debug.Log("Too far away to use this.");
        }
    }

    void PerformAction()
    {
        Debug.Log("Action performed!");
        // Put your actual game logic here
    }

    public void Shop()
    {
        shopMenu.SetActive(true); 
        Time.timeScale = 0f;
    }
}
