using UnityEngine;
using System.Collections;

public class SpriteFinder : MonoBehaviour {

    public static Sprite GetIcon(string id)
    {
        Sprite sprite = Resources.Load<Sprite>("icons/" + id);
        if (sprite == null)
        {
            return new Sprite();
        }
        return sprite;
    }
}
