using TMPro;
using UnityEngine;
using Zenject;

// Линковка кнопок, переходы

class OperatorPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _page1;
    [SerializeField]
    private GameObject _page2;
    private SaveSystem _saveSystem;
    private Operator _operator;
    private TMP_Text _timeLabel;

    private void Start()
    {
        _operator = GetComponent<Operator>();
    }

    [Inject]
    private void Construct(SaveSystem saveSystem)
    {
        _saveSystem = saveSystem;
    }

    public void ShowPage2()
    {
        _page1.SetActive(false);
        _page2.SetActive(true);
    }

    public void SyncTimeAndLabel()
    {
        long minutes = _operator.LostTime / 60;
        long seconds = _operator.LostTime / 60 / 60;
        _timeLabel.text = string.Format("{0}:{1}:{2}", minutes, seconds);
    }

    public void OnFIOChanged()
    {
        // todo
    }

}