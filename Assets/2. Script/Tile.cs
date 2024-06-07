using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text numberText;
    public Board board;
    Vector3 currentPosition;
    public Image image;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void Number(string number)
    {
        numberText.text = number;
        if(number == "16")
        {
            gameObject.SetActive(false);
        }
    }

    public void ChangeTile()
    {
        currentPosition = GetComponent<RectTransform>().localPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void Move()
    {

    }

   

}
