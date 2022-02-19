using UnityEngine;
using UnityEngine.UI;

public class RewardsContainerSwitcher : MonoBehaviour
{
    [SerializeField] private Button _dailyButton;
    [SerializeField] private Button _weeklyButton;
    [SerializeField] private Image _dailyButtonImage;
    [SerializeField] private Image _weeklyButtonImage;
    [SerializeField] private GameObject _dailyContainer;
    [SerializeField] private GameObject _weeklyContainer;

    public void Init()
    {
        _dailyButton.onClick.AddListener(ShowDailyContainer);
        _weeklyButton.onClick.AddListener(ShowWeeklyContainer);
        ShowDailyContainer();
    }

    private void ShowWeeklyContainer()
    {
        _dailyContainer.SetActive(false);
        _weeklyContainer.SetActive(true);
        _dailyButtonImage.color = Color.white;
        _weeklyButtonImage.color = Color.green;
    }

    private void ShowDailyContainer()
    {
        _weeklyContainer.SetActive(false);
        _dailyContainer.SetActive(true);
        _dailyButtonImage.color = Color.green;
        _weeklyButtonImage.color = Color.white;
    }

    public void OnDestroy()
    {
        _dailyButton.onClick.RemoveAllListeners();
        _weeklyButton.onClick.RemoveAllListeners();
    }
}
