using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

//items class that makes tasks
public class Items : MonoBehaviour {
	OrderedDictionary task1 = new OrderedDictionary();
	string[] loc1;
	int numitems;
	// Use this for initialization
	void Start() {
		task1.Add("Blue Shirt", 2);
		task1.Add("Blue Ball", 3);
		task1.Add("Toy", 4);
		loc1 = new string[] {"A-1-A-2-1", "A-1-A-3-1", "A-1-A-2-1"};
		numitems = 3;
	}
	public string[] GetTask1Keys() {
		string[] mykeys = new string[numitems];
		task1.Keys.CopyTo(mykeys, 0);
		return mykeys;
	}
	public int[] GetTask1Values() {
		int[] myvalues = new int[numitems];
		task1.Values.CopyTo(myvalues, 0);
		return myvalues;
	}
	
	public string[] GetTask1Locations() {
		return loc1;
	}
	public int Numitems() {
		return numitems;
	}
}
