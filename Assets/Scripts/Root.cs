using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private PointsArray[] _leafPoints;
    [SerializeField] private GameObject[] _leafPrefabs;
    [SerializeField] private int _leafesCountToGenerate = 30;
    [SerializeField] private Transform _container;

    private List<GameObject> _leafes = new List<GameObject>();

    private void Start()
    {
        CreateLeafes();

        for (int i = 0; i < _leafPoints.Length; i++)
        {
            Transform point = _leafPoints[i].GetPoint();
            
            if (TryGetLeaf(out GameObject leaf))
            {
                leaf.transform.position = point.position;
                leaf.SetActive(true);
            }
        }
    }

    private void CreateLeafes()
    {
        for (int i = 0; i < _leafesCountToGenerate; i++)
        {
            GameObject leaf = Instantiate(_leafPrefabs[Random.Range(0, _leafPrefabs.Length)], _container);
            leaf.SetActive(false);
            _leafes.Add(leaf);
        }
    }

    private bool TryGetLeaf(out GameObject leaf)
    {
        leaf = _leafes.FirstOrDefault(x => x.activeSelf == false);
        return leaf != null;
    }
}

[System.Serializable]
public class PointsArray
{
    [SerializeField] private Transform[] _points;

    public Transform GetPoint()
    {
        Transform point = _points[Random.Range(0, _points.Length)];
        return point;
    }
}

