using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class GenerateTable : MonoBehaviour {

    public InputField searchInput;
    public Scrollbar scrollbar;
    public RectTransform rectTransform;
    public List<Row> rows;


    // Use this for initialization
    public void Start () {
        GenerateTableRows("");
        searchInput.onValueChanged.AddListener(GenerateTableRows);
        scrollbar.onValueChanged.AddListener(Virtualize);
        StartCoroutine(ResetScrollbar());
        
	}

    //resets the scrollbar when searching, Needed for when the table is first created
    //Optionally is used when changing the search value(I prefer how it resets to the top)
    public IEnumerator<Object> ResetScrollbar()
    {
        yield return null;
        scrollbar.value = 1;
    }

    public void Virtualize(float f)
    {
        string search = searchInput.text;
    }

    public void GenerateTableRows(string search)
    {
        for (int i = rows.Count - 1; i >= 0; i--)
        {
            Row g = rows[i]; 
            rows.RemoveAt(i);
            Destroy(g.gameObject);
        }

        
        //Would be better to generate the rows at the start and just pick which ones to display
        //All these loads + instantiates are bad(could reduce to one load and just reuse to instantiate)
        int yOffset = 0;
        foreach (BuildEntry be in CSVLoader.buildentries)
        {

            if (be.MatchSearch(search))
            {
                GameObject rowPrefab = Resources.Load("Row") as GameObject;
                Row row = rowPrefab.GetComponent<Row>();
                StringBuilder sb = new StringBuilder();
                int count = 0;
                foreach (string beTag in be.tags.Keys)
                {
                    sb.Append(beTag);
                    if (count != be.tags.Count - 1)
                    {
                        sb.Append(", ");
                    }
                    else
                    {
                        sb.Append(System.Environment.NewLine);
                    }
                    count++;
                }
                row.SetChildrenData(be.sprite, be.displayName, be.description, sb.ToString());
                Row newRow = Instantiate(row);

                newRow.transform.localScale = new Vector3(.5f, .5f);
                newRow.transform.localPosition += new Vector3(0, -yOffset * 35, 0);
                newRow.transform.SetParent(transform, false);
                yOffset++;
                rows.Add(newRow);
            }
        }
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,  Mathf.Max(150, yOffset*35));
        StartCoroutine(ResetScrollbar());
    }

    void OnDestroy()
    {
        searchInput.onValueChanged.RemoveListener(GenerateTableRows);
    }
}
