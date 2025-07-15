using System.Collections;
using UnityEngine;

public class DisplaySetter : MonoBehaviour
{
    [SerializeField] float _updateTime = 10f;
    private Camera _mainCamera;
    private Camera _uiCamera;
    private bool _isAllSet = false;
    void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _uiCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        InitDisplays();
        StartCoroutine(SearchForVR());
    }   

    public IEnumerator SearchForVR()
    {
        InitDisplays();
        yield return new WaitForSeconds(_updateTime);
    }

    void InitDisplays()
    {
        if (Display.displays.Length > 1 && !_isAllSet)
        {
            Debug.Log($"VR found. Displays is setted");
            Display.displays[0].Activate();

            _mainCamera.targetDisplay = 0;
            _uiCamera.targetDisplay = 1;
            _isAllSet = true;
        }
        //else
        //{
        //    Debug.LogWarning("No second display. Rendering in one instead");
        //    _mainCamera.targetDisplay = 0;
        //    _uiCamera.targetDisplay = 0;
        //    _isAllSet = false;
        //}
    }
}
