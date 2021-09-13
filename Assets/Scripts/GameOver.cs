using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void SetResultScore(float maxSpeed)
    {
        _text.text = $"Max Speed {String.Format("{0:0.#}", maxSpeed)}";
    }
    
}
