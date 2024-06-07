using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tilePrefab;
    Vector2Int size = new Vector2Int(4, 4);
    public List<string> numbers = new List<string>();
    public List<GameObject> tiles = new List<GameObject>();

    private void Start()
    {
        
        for (int i = 1; i <= 16; i++)
        {
            numbers.Add(i.ToString());
        }
        
        Spawn();
        RPCRandomNumber();
        NumbersToTiles();
    }

    void Spawn()
    {
        for(int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                GameObject tile = Instantiate(tilePrefab, transform);
                tiles.Add(tile);
            }
        }
    }

    void NumbersToTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            Tile tileComponent = tiles[i].GetComponent<Tile>();
            tileComponent.Number(numbers[i]);
        }
    }

    public void RPCRandomNumber()
    {
        for (int i = numbers.Count -1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            string changeNumber = numbers[i];
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = changeNumber;
        }
    }
}
