using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CSVLoader : MonoBehaviour {
    
	public static IList<BuildEntry> buildentries;
	public TextAsset csvFile;

	void Awake ()
	{
		buildentries = new List<BuildEntry>();
        string[] csvLines = csvFile.text.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None);
        string[] splitFirstLine = csvLines[0].Split(',');

        int assetIdIndex = Array.IndexOf(splitFirstLine, "ID");
        int displayNameIndex = Array.IndexOf(splitFirstLine, "DisplayName");
        int descriptionIndex = Array.IndexOf(splitFirstLine, "Description");
        int iconIdIndex = Array.IndexOf(splitFirstLine, "Icon");

        for (int i = 1; i < csvLines.Length - 1; i++) {
			string[] splitLine = csvLines[i].Split (',');
			string assetId = splitLine[assetIdIndex];
			string displayName = splitLine[displayNameIndex];
            string description = splitLine[descriptionIndex];
            Sprite sprite = SpriteFinder.GetIcon(splitLine[iconIdIndex]);

            //Assuming I know when Tags start
            //In some ways I'd like XML more since I'd have less knowledge about the table
            //in the code + c# has an xml parser too
            IDictionary <string, string> tags = new Dictionary<string, string>();
			for (int j = 4; j < splitLine.Length; j++) {
                //While I only use the value to determine if the tab is valid for the build entry,
                //there is probably info that we would be interested in here
                //(easy enough to swap to a list if we just want to ignore the values
                //(i.e. values for the build menu instea of just listing tags)
				if (!splitLine [j].Equals ("")) {
					tags.Add(splitFirstLine[j], splitLine[j]);
				}
			}
			buildentries.Add(new BuildEntry(assetId, displayName, description, sprite, tags));
		}
	}
}
