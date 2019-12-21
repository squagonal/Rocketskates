using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;

public class DynamicTile : Tile
{
    [SerializeField]
    int seed;
    [SerializeField]
    bool supress_read_write_warning;
    [SerializeField]
    Sprite[] textures;
    Dictionary<directionFlags, List<Sprite>> lookup;
    Dictionary<Vector2Int, directionFlags> posToDirection;
    bool dirty = true;
    int lookup_count = 0;
    [System.Flags]
    public enum directionFlags
    {
        None = 0x00,    //0b00000000
        nw = 0x01,      //0b00000001
        n = 0x02,       //0b00000010
        ne = 0x04,      //0b00000100
        e = 0x08,       //0b00001000
        se = 0x10,      //0b00010000
        s = 0x20,       //0b00100000
        sw = 0x40,      //0b01000000
        w = 0x80,       //0b10000000
        Cardinal = n | e | s | w,    //0b10101010
        Cornaers = ne | nw | sw | se,//0b01010101
        All = Cardinal | Cornaers    //0b11111111
    }

    bool checkSideEmpty(Sprite s, int x, int y)
    {
        x++;
        y++;
        Rect r = s.rect;
        int stepx = (int)(r.width - 1) / 2;
        int stepy = (int)(r.height - 1) / 2;
        return s.texture.GetPixel((int)r.x + (x * stepx), (int)r.y + (y * stepy)).a == 0;
    }
    private bool checkSideEmpty(Sprite s, Vector2Int key)
    {
        return checkSideEmpty(s, key.x, key.y);
    }

    // Use this for initialization

    void setup()
    {
        if (posToDirection == null)
        {
            posToDirection = new Dictionary<Vector2Int, directionFlags>();
            posToDirection.Add(new Vector2Int(-1, -1), directionFlags.nw);
            posToDirection.Add(new Vector2Int(0, -1), directionFlags.n);
            posToDirection.Add(new Vector2Int(1, -1), directionFlags.ne);
            posToDirection.Add(new Vector2Int(1, 0), directionFlags.e);
            posToDirection.Add(new Vector2Int(1, 1), directionFlags.se);
            posToDirection.Add(new Vector2Int(0, 1), directionFlags.s);
            posToDirection.Add(new Vector2Int(-1, 1), directionFlags.sw);
            posToDirection.Add(new Vector2Int(-1, 0), directionFlags.w);
        }
        lookup = new Dictionary<directionFlags, List<Sprite>>();
        foreach (Sprite s in textures)
        {

            directionFlags current = directionFlags.None;
            foreach (var key in posToDirection.Keys)
                if (!checkSideEmpty(make_readable(s), key))
                    current |= posToDirection[key];
            if (!lookup.ContainsKey(current))
                lookup[current] = new List<Sprite>();
            lookup[current].Add(s);
        }
        lookup_count = lookup.Count;
    }

    public bool hasFlag(directionFlags value, directionFlags flag)
    {
        return (value & flag) != directionFlags.None;
    }
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        if (lookup == null || lookup.Count != lookup_count)
            setup();
        directionFlags cardinal_nabours = directionFlags.None;
        directionFlags all_nabours = directionFlags.None;
        List<Vector2Int> diag = new List<Vector2Int>();
        foreach (var key in posToDirection.Keys)
        {
            if (!hasFlag(directionFlags.Cardinal, posToDirection[key]))
                diag.Add(key);
            if (hasNabour(tilemap, position, key))
            {
                all_nabours |= posToDirection[key];
            }
        }

        cardinal_nabours = all_nabours & directionFlags.Cardinal;

        foreach (var corner in diag)
        {
            bool should_consider_corners =
                hasFlag(all_nabours, posToDirection[corner]) &&
                hasFlag(cardinal_nabours, posToDirection[corner * Vector2Int.right]) &&
                hasFlag(cardinal_nabours, posToDirection[corner * Vector2Int.up]);
            if (should_consider_corners)
                cardinal_nabours |= posToDirection[corner];
        }

        Sprite last = tileData.sprite;
        if (lookup.ContainsKey(all_nabours))
        {
            tileData = setSprite(position, tileData, all_nabours);
        }
        else
        {
            if (lookup.ContainsKey(cardinal_nabours))
            {
                tileData = setSprite(position, tileData, cardinal_nabours);
            }
            else
            {
                Debug.Log(cardinal_nabours + " not in look up.\t number of item types in lookup: " + lookup.Keys.Count);
                tileData.sprite = textures.Length >= 1 ? textures[0] : last;
            }
        }
        if (last != tileData.sprite)
            dirty = true;
    }

    private TileData setSprite(Vector3Int position, TileData tileData, directionFlags current)
    {
        var tmp_state = Random.state;
        Random.InitState(position.GetHashCode() + seed);
        tileData.sprite = pick(lookup[current]);
        Random.state = tmp_state;
        return tileData;
    }



    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
        if (dirty)
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (hasNabour(tilemap, position, x, y))
                    {
                        Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);
                        tilemap.RefreshTile(nPos);
                    }
                }
            }
    }
    private bool hasNabour(ITilemap tilemap, Vector3Int position, Vector2Int key)
    {
        return hasNabour(tilemap, position, key.x, key.y);
    }
    private bool hasNabour(ITilemap tilemap, Vector3Int position, int dx, int dy)
    {
        return tilemap.GetTile(position + Vector3Int.right * dx + Vector3Int.up * dy) == this;
    }
    private T pick<T>(List<T> t)
    {
        if (t.Count <= 0)
            return default(T);
        return t[Random.Range(0, t.Count)];
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        if (lookup == null || lookup.Count != lookup_count)
            setup();
        return base.StartUp(position, tilemap, go);
    }


    Sprite make_readable(Sprite s)
    {
        try
        {
            Color c = s.texture.GetPixel(0, 0);
            return s;
        }
        catch
        {
            if (!supress_read_write_warning)
                Debug.LogWarning("it is advised to set read/write on textures");
        }
        Texture2D texture = s.texture;
        // Create a temporary RenderTexture of the same size as the texture
        RenderTexture tmp = RenderTexture.GetTemporary(
                            texture.width,
                            texture.height,
                            0,
                            RenderTextureFormat.Default,
                            RenderTextureReadWrite.Linear);

        // Blit the pixels on texture to the RenderTexture
        Graphics.Blit(texture, tmp);
        // Backup the currently set RenderTexture
        RenderTexture previous = RenderTexture.active;
        // Set the current RenderTexture to the temporary one we created
        RenderTexture.active = tmp;
        // Create a new readable Texture2D to copy the pixels to it
        Texture2D myTexture2D = new Texture2D(texture.width, texture.height);
        // Copy the pixels from the RenderTexture to the new Texture
        myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
        myTexture2D.Apply();
        // Reset the active RenderTexture
        RenderTexture.active = previous;
        // Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tmp);
        return Sprite.Create(myTexture2D, s.rect, Vector2.zero);
        // "myTexture2D" now has the same pixels from "texture" and it's readable.
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/DynamicTile")]
    public static void CreateWaterTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Dynamic Tile", "New Dynamic Tile", "asset", "Save Dynamic Tile", "Assets/TileSets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DynamicTile>(), path);
    }

#endif
}