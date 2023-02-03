using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private InputManager _inputManager;
    
    public CanvasManager CanvasManager { get; set; }

    public PlayerController PlayerController { get; set; }

    public NpcManager NpcManager { get; set; }


    [SerializeField] private float myCash;

    
    private float _slideValue;
    
    public float Multiplier { get; set; }
    public float MyCash => myCash;

    


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }


    public void StartGame()
    {
        PlayerController.StartMovement();
    }
    public void MultiplierMode()
    {
        StopPlayer();
        StopAllNpcsFollowing();
        CanvasManager.InputPanelController(false);
    }
    
    public IEnumerator WinAction()
    {
        StopPlayer();
        StopAllNpcsFollowing();
        CanvasManager.UiGameControl(false);
        yield return new WaitForSeconds(1f);
        
        CanvasManager.WinSliderValues(NpcManager.NpcDead, NpcManager.Npcs.Length);
        CanvasManager.WinCanva();
    }

    public void GameOverAction()
    {
        CanvasManager.GameOverCanva();
        StopAllNpcsFollowing();
        StopPlayer();
    }
    public void StopAllNpcsFollowing()
    {
        NpcManager.SetSpeedOfAllNpcs(0,true);
    }

    public void StopPlayer()
    {
        PlayerController.CancelMovement();
        CanvasManager.InputPanelController(false);
    }
    public void AddCash(float amount)
    {
        myCash += amount;
        CanvasManager.UpdateCashToShowInCanva();
    }

    public void SliderValueAdd(float value)
    {
        CanvasManager.FatigueCanvaValue += value;
        CanvasManager.T = 0;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void NextLevel()
    {
        int actualScene = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.sceneCountInBuildSettings - 1 >= actualScene + 1)
        {
            SceneManager.LoadScene(actualScene + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
    }

}
