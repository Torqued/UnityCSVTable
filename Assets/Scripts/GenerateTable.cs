using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class GenerateTable : MonoBehaviour {

    public InputField searchInput;
    public Scrollbar scrollbar;
    public RectTransform rectTransform;
    public List<Row> rows;

    private Row _loadedRow;
    private int _displayedRows;
    private int _currIndex;
    private string _search;

    // Use this for initialization
    public void Start () {
        GameObject rowPrefab = Resources.Load("Row") as GameObject;
        _loadedRow = rowPrefab.GetComponent<Row>();
        GenerateTableRows("");
        searchInput.onValueChanged.AddListener(GenerateTableRows);
        scrollbar.onValueChanged.AddListener(AddNewRows);
        StartCoroutine(ResetScrollbar());
        
	}

    //resets the scrollbar when searching, Needed for when the table is first created
    //Optionally is used when changing the search value(I prefer how it resets to the top)
    public IEnumerator<Object> ResetScrollbar()
    {
        yield return null;
        scrollbar.value = 1;
    }

    public void AddNewRows(float f)
    {
        if (f < 0.05f)
        {
            AddRows();
        }
    }

    public void GenerateTableRows(string search)
    {
        _search = search;
        _displayedRows = 0;
        _currIndex = 0;
        for (int i = rows.Count - 1; i >= 0; i--)
        {
            Row g = rows[i];
            rows.RemoveAt(i);
            Destroy(g.gameObject);
        }
        AddRows();
        StartCoroutine(ResetScrollbar());
    }

    private void AddRows()
    { 
        int newLength = Mathf.Min(_currIndex + 10, CSVLoader.buildentries.Count);

        for (int i = _currIndex; i < newLength; i++)
        {
            BuildEntry be = CSVLoader.buildentries[i];

            if (be.MatchSearch(_search))
            {

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
                _loadedRow.SetChildrenData(be.sprite, be.displayName, be.description, sb.ToString());
                Row newRow = Instantiate(_loadedRow);

                newRow.transform.localScale = new Vector3(.5f, .5f);
                newRow.transform.localPosition += new Vector3(0, -_displayedRows * 35, 0);
                newRow.transform.SetParent(transform, false);
                _displayedRows++;
                rows.Add(newRow);
            }
            else
            {
                newLength = Mathf.Min(newLength+1, CSVLoader.buildentries.Count);
            }
            _currIndex++;

        }
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,  Mathf.Max(150, _displayedRows * 35));
    }

    void OnDestroy()
    {
        searchInput.onValueChanged.RemoveListener(GenerateTableRows);
        scrollbar.onValueChanged.RemoveListener(AddNewRows);
    }
}
