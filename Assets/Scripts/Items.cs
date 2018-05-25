using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

//items class that makes tasks
public class Items : MonoBehaviour {
	OrderedDictionary task1 = new OrderedDictionary();
	// Use this for initialization
	void Start() {
		task1.Add("Blue Shirt", 2);
		task1.Add("Blue Ball", 3);
	}
	public string[] GetTask1Keys() {
		string[] mykeys = new string[2];
		task1.Keys.CopyTo(mykeys, 0);
		return mykeys;
	}
	public int[] GetTask1Values() {
		int[] myvalues = new int[2];
		task1.Values.CopyTo(myvalues, 0);
		return myvalues;
	}
	
	public string[] GetTask1Locations() {
		string[] loc = new string[2];
		loc[0] = "A-1-A-2-1";
		loc[1] = "A-1-A-3-1";
		return loc;
	}
	public int Numitems() {
		return 2;
	}
}
