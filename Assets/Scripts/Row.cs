using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Class for instantiating and modifying rows of our table.
public class Row : MonoBehaviour {
    public GameObject Icon;
    public GameObject RowName;
    public GameObject RowDescription;
    public GameObject RowTags;

    public void SetChildrenData(Sprite sprite, string name, string description, string tags)
    {
        Image i = Icon.GetComponent<Image>();
        i.sprite = sprite;
        Text t = RowName.GetComponent<Text>();
        t.text = name;
        t = RowDescription.GetComponent<Text>();
        t.text = description;
        t = RowTags.GetComponent<Text>();
        t.text = tags;
    }
}
