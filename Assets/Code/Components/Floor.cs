using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Sprite sprite;
    public Color a = Color.white;
    public Color b = Color.gray;

    private float width;
    private float height;

    private void Update()
    {
        //if the screen changed resolutions
        if(Screen.width != width || Screen.height != height)
        {
            width = Screen.width;
            height = Screen.height;

            Refresh();
        }
    }

    private void Refresh()
    {
        var tiles = transform.GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i < tiles.Length;i++)
        {
            Destroy(tiles[i].gameObject);
        }

        //100 is ppu
        int width = Mathf.RoundToInt(this.width / 100);
        int height = Mathf.RoundToInt(this.height / 100);

        for (int x = -width; x <= width; x++)
        {
            for (int y = -height; y <= height; y++)
            {
                SpriteRenderer tile = new GameObject("Tile").AddComponent<SpriteRenderer>();
                tile.sprite = sprite;
                tile.color = (x + y) % 2 == 0 ? a : b;
                tile.sortingOrder = -20;

                tile.transform.SetParent(transform);
                tile.transform.localPosition = new Vector3(x, y);
            }
        }
    }
}
