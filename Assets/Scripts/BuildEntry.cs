using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildEntry {

	public string assetId;
	public string displayName;
	public string description;
	public Sprite sprite;
	//Switched from string[] since we dont know the number of tags there will be.
	public IDictionary<string, string> tags;

    public BuildEntry(string assetId, string displayName, string description, Sprite sprite, IDictionary<string, string> tags)
    {
        this.assetId = assetId;
        this.displayName = displayName;
        this.description = description;
        this.sprite = sprite;
        this.tags = tags;
    }

    //Very simple search, if wanted to make better, could parse on spaces for multiple
    //descriptors + make decide on inclusivity or exclusivity for the search
    public bool MatchSearch(string search)
    {
        search = search.ToLower();
        if (search.Equals(""))
        {
            return true;
        }

        if (assetId.ToLower().IndexOf(search) != -1 ||
                displayName.ToLower().IndexOf(search) != -1 ||
                description.ToLower().IndexOf(search) != -1)
        {
            return true;
        }
        foreach(string s in tags.Keys)
        {
            if (s.ToLower().IndexOf(search) != -1)
            {
                return true;
            }
        }
        return false;
    }

}
