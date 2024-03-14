using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class HexGridEditor : MonoBehaviour
{
    private UITest uiTest;

    public Tilemap tilemap;
    public Tile[] tiles;
    public List<GameObject> UITiles;

    public int selectedTile = 0;

    public Transform tileGridUI;
    public GameObject tileGridUIPrefab;

    private Vector3 offset = new Vector3(0, 0, 10);

    private void Start()
    {
        uiTest = GetComponent<UITest>();
        for (int i = 0; i < tiles.Length; i++)
        {
            int localIndex = i;

            GameObject UITile = new GameObject("UITile");

            UITile.transform.SetParent(tileGridUI, false);
            UITile.transform.localScale = new Vector3(1f, 1f, 1f);

            Button button = UITile.AddComponent<Button>();

            UnityEngine.Events.UnityAction select = () => { this.SelectTile(localIndex); };

            button.onClick.AddListener(select);

            Image UIImage = UITile.AddComponent<Image>();
            UIImage.sprite = tiles[i].sprite;

            Color tileColor = UIImage.color;
            tileColor.a = 0.5f;

            if(i == selectedTile)
            {
                tileColor.a = 1f;
            }
            UIImage.color = tileColor;
            UITiles.Add(UITile);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && (uiTest.IsPointerOverUIElement() == false))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position += offset;

            tilemap.SetTile(tilemap.WorldToCell(position), tiles[selectedTile]);
        }
        if (Input.GetMouseButton(1) && (uiTest.IsPointerOverUIElement() == false))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position += offset;

            tilemap.SetTile(tilemap.WorldToCell(position), null);
        }
    }

    private void RenderUITiles()
    {
        int i = 0;
        foreach (GameObject tile in UITiles)
        {
            Image UIImage = tile.GetComponent<Image>();
            Color tileColor = UIImage.color;
            tileColor.a = 0.5f;

            if (i == selectedTile)
            {
                tileColor.a = 1f;
            }

            UIImage.color = tileColor;

            i++;
        }
    }

    public void SelectTile(int tileindex)
    {
        Debug.Log(tileindex + " tile selected");
        selectedTile = tileindex;
    }
}
