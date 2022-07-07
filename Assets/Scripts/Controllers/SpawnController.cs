using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject prefabAlphabet;
    [SerializeField] private Transform parentCanvas;
    
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private List<RectTransform> _spawnedAlphabet;
    private InputController _input;

    private void Awake()
    {
        _input = FindObjectOfType<InputController>();
        _input.gridParams += SpawnItems;
    }

    private void SpawnItems(int x, int y, bool spawn)
    {
        if (spawn)
        {
            _spawnedAlphabet = new List<RectTransform>();
            int count = x * y;
            for (int i = 0; i < count; ++i)
            {
                GameObject newItem = Instantiate(prefabAlphabet, parentCanvas, false);
                TextMeshProUGUI text = newItem.GetComponentInChildren<TextMeshProUGUI>();
                text.text = chars[Random.Range(0, chars.Length)].ToString();
                _spawnedAlphabet.Add(newItem.GetComponent<RectTransform>());
                _spawnedAlphabet[_spawnedAlphabet.Count-1].localScale = Vector3.zero;
            }
            _input.mixed.Invoke(false);
        }
    }

    public List<RectTransform> SpawnedAlphabet => _spawnedAlphabet;
}
