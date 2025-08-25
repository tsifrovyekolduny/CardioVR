using TMPro;
using UnityEngine;
using Zenject;

public class HiderUI : MonoBehaviour
{
    private RectTransform _panel;
    private TMP_Text _label;
    private IOperator _operator;

    [Inject]
    private void Construct(IOperator @operator)
    {
        _operator = @operator;
    }

    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 0.10f;
        _panel = GetComponentInChildren<RectTransform>();
        _label = _panel.GetComponentInChildren<TMP_Text>();

        _operator.OnSessionStart += OnSessionStart;

        // TODO ��� ����� ��� �������
        OnSessionStart();

        // TODO ���� ��� ����� ������ - ����� ��� ������ � ��� ���������, ����� ����� ������
        // �������� hider ����� ����� ������� ����� ������
        // _operator.OnSessionEnd += OnSessionEnd;
    }

    void OnSessionStart()
    {
        _panel.gameObject.SetActive(false);
    }

    void OnSessionEnd()
    {
        _panel.gameObject.SetActive(true);
        _label.text = "����� ��������. ������� ''������ ����� �����'' � ������ ��� ����������� �������";
    }
}
