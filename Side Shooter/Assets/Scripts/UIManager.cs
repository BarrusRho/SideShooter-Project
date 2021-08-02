using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject userInterface;

    public Text livesText;

    public Slider healthBar, shieldBar;

    public Text scoreText, highScoreText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu() 
    {
    
    }
}