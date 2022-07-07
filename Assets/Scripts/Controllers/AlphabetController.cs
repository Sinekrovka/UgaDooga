using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphabetController : MonoBehaviour
{
    private GridController _gridController;
    private SpawnController _spawnController;
    private void Awake()
    {
        _gridController = FindObjectOfType<GridController>();
        InputController input = FindObjectOfType<InputController>();
        input.mixed += MixedAlphabet;
        _spawnController = FindObjectOfType<SpawnController>();
    }

    private void MixedAlphabet(bool mixed)
    {
        StartCoroutine(Wait(mixed));
    }

    private IEnumerator MovedAlpha(RectTransform alpha, Vector2 endValue)
    {
        Vector3 frwd = Vector3.MoveTowards(alpha.anchoredPosition, endValue, 1);
        alpha.anchoredPosition = frwd;
        yield return new WaitForEndOfFrame();
        if (alpha.anchoredPosition != endValue)
        {
            StartCoroutine(MovedAlpha(alpha, endValue));
        }
    }

    private IEnumerator NewPosition(RectTransform alpha, Vector2 endValue)
    {
        yield return new WaitForEndOfFrame();
        alpha.anchoredPosition = endValue;
    }

    private IEnumerator Wait(bool mixed)
    {
        if (mixed)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }
        List<RectTransform> cells = _gridController.GetPoints();
        cells.RemoveAt(0);

        foreach (var alpha in _spawnController.SpawnedAlphabet)
        {
            alpha.localScale = Vector3.one;
            alpha.sizeDelta = _gridController.CellSize;
            int index = Random.Range(0, cells.Count);
            if (mixed)
            {
                StartCoroutine(MovedAlpha(alpha, cells[index].anchoredPosition));
            }
            else
            {
                StartCoroutine(NewPosition(alpha, cells[index].anchoredPosition));
            }
            cells.RemoveAt(index);
        }
    }
}
