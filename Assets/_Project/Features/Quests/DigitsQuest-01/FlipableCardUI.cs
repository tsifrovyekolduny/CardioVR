using System;
using TMPro;
using UnityEngine;

public class FlipableCardUI : MonoBehaviour
{
    TextMeshPro _text;
    [SerializeField]

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponentInChildren<TextMeshPro>();
    }

    private int _number { get; set; }

    public void Flip(bool open, bool temporary = false)
    {
        if(transform.rotation.y == 0)
        {

        }
        float degree = open ? 180 : -180;
    }

    public void ShowCard()
    {
        Flip(true);
    }

    public void SetNumber(int number)
    {
        _text.text = _number.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
