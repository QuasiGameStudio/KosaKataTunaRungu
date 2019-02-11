using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pengenalan : MonoBehaviour {

	[SerializeField] private Sprite[] frames;

	private string[] categories = {
		"Ganti Orang","Ganti Orang","Sifat","Ganti Orang","Sifat",
		"Benda","Ganti Orang","Sapaan","Benda","Keterangan",
		"Ganti Orang","Sifat","Kerja","Benda","Kerja",
		"Sifat","Benda","Sifat","Kerja","Benda",

	
		
	};

	private string[] names = {
		"Aku","Bapak","Cantik","Dia","Enak",
		"Foto","Guru","Hai","Ilmu","Jangan",
		"Kita","Lama","Makan","Nama","Olahraga",
		"Panas","Rumah","Semangat","Tidur","Uang", //19

	
	};

	private int[] nFrames = {3,2,4,2,2,2,2,2,2,2,2,3,2,2,2,2,2,2,2,2 ,1,1,1,1,1 ,1,1,1,1,1 ,1,1,1,1,1,1,1 ,1,1,1,1,1};
	
	[SerializeField] private int index;
	[SerializeField] float framesPerSecond;

	private int[] bendaIndex = {5,8,13,16,19 ,20,21,22,23,24};
	private int[] sifatIndex = {2,4,11,15,17 ,37,38,39,40,41};
	private int[] orangIndex = {0,1,3,6,10 ,25,26,27,28,29};
	private int[] kerjaIndex = {12,14,18 ,30,31,32,33,34,35,36};
	

	private int[] mainIndex = new int[10];

	[Header("GUI")]

	[SerializeField] private Text nameText;
	[SerializeField] private Text categoryText;
	[SerializeField] private Image renderer;

	// Use this for initialization
	void Start () {
		switch (GameData.Instance.GetCategory())
		{
			case 0:
				mainIndex = bendaIndex;
				break;
			case 1:
				mainIndex = sifatIndex;
				break;
			case 2:
				mainIndex = orangIndex;
				break;
			case 3:
				mainIndex = kerjaIndex;
				break;
			default:
				mainIndex = bendaIndex;
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {

		// for (int i = 0; i < mainIndex.Length; i++)
		// {
		// 	Debug.Log(mainIndex[i]);
		// }

		//Animate
		Animate();
		

		//change name text
		nameText.text = names[mainIndex[index]];
		categoryText.text = "Kata " + categories[mainIndex[index]];


	}

	void Animate(){
		int startFrameIdx = 0;
		for (int j = 0; j < mainIndex[index]; j++)
		{
			startFrameIdx += nFrames[j];
		}
		float i = (Time.time * framesPerSecond) % nFrames[mainIndex[index]];		
		renderer.sprite = frames[(int) i + startFrameIdx];
		// renderer.SetNativeSize();
	}

	public void Next(){		
		if(index < 4){
			index++;	
		}		
	}

	public void Prev(){
		if(index > 0){
			index--;			
		}
	}

	
	
	
	
}
