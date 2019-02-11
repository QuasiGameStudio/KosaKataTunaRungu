using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData> {


	public void SetCategory(int cat){
		PlayerPrefs.SetInt("Category",cat);
	}
	public int GetCategory(){
		return PlayerPrefs.GetInt("Category",0);
	}
}
