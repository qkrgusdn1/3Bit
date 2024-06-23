using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickOutMission : Mission
{
    BrickBall brickBall;
    public List<GameObject> Bricks = new List<GameObject>();
    void Start()
    {
        brickBall = GetComponentInChildren<BrickBall>();
    }
    private void Update()
    {
        if(brickBall.breakBrickCount == Bricks.Count)
        {
            CheckClear();
        }
    }

}
