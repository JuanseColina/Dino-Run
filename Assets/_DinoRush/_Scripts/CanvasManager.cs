using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{

    [Header("Panels")] [SerializeField] private GameObject inputPanel;
    [SerializeField] private GameObject startRunPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject deadPanel;

    [Space] 
    
    [Header("Texts")] [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI winTextMoney;
    [SerializeField] private TextMeshProUGUI npcsDeadText;
    [SerializeField] private TextMeshProUGUI multiplierText;
        
    [Space]
    
    [Header("Sliders")] [SerializeField] private Slider slider;
    [SerializeField] private Slider winSlider;
    [SerializeField] private float duration;
    
    [Space]
    
    [Header("Images")] [SerializeField] private Image moneyInRunImage;
    [SerializeField] private Image handImage;
    
    private void Start()
    {
        HandTweening();
        GameManager.Instance.CanvasManager = this;
        UpdateCashToShowInCanva();
    }

    private void Update()
    {
        slider.value = Slider();
    }

    private float _timeHlp;
    
    public float T
    {
        set => _timeHlp = value;
    }

    public float FatigueCanvaValue { get; set; }

    public float Slider()
    {
        _timeHlp += Time.deltaTime / duration;
        FatigueCanvaValue = Mathf.Lerp(FatigueCanvaValue, 0, _timeHlp);
        FatigueCanvaValue = Mathf.Clamp(FatigueCanvaValue, 0, slider.maxValue);
        return FatigueCanvaValue; 
    }

    public void StartGamePanelAction()
    {
        inputPanel.SetActive(true);
        UiGameControl(true);
        startRunPanel.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void InputPanelController(bool mode)
    {
        inputPanel.SetActive(mode);
    }
    public void WinCanva()
    {
        UiGameControl(false);
        multiplierText.text = "x"+GameManager.Instance.Multiplier;
        winPanel.SetActive(true);
        winTextMoney.text = "" + GameManager.Instance.MyCash;
    }

    public void GameOverCanva()
    {
        UiGameControl(false);
        deadPanel.SetActive(true);
    }
    public void WinSliderValues(int current, int max)
    {
        winSlider.maxValue = max + 1;
        winSlider.value = current;
        npcsDeadText.text = current + "/" + max;
    }

    public void UiGameControl(bool active)
    {
        slider.gameObject.SetActive(active);
        moneyText.gameObject.SetActive(active);
        moneyInRunImage.gameObject.SetActive(active);
    }

    public void RestartLevelButton()
    {
        GameManager.Instance.RestartLevel();
    }

    public void NextLevelButton()
    {
        GameManager.Instance.NextLevel();
    }

    public void UpdateCashToShowInCanva()
    {
        moneyText.text = "" + GameManager.Instance.MyCash;
    }

    private void HandTweening()
    {
        LeanTween.moveLocalX(handImage.gameObject, -300, 1f).setLoopPingPong(-1);
    }
}
