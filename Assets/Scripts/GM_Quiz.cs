using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_Quiz : MonoBehaviour {

	[SerializeField] private Sprite[] frames;
	private int[] nFrames = {3,2,4,2,2,2,2,2,2,2,2,3,2,2,2,2,2,2,2,2};

	[SerializeField] private GameObject[] optionButtons;

	private int[] arrIndex = {0,1,2,3,4,5,6,7,8,9};

	private int index;

	private string answer;
	private int answerIndex;

	private bool gameIsPlaying = true;

	private bool hint;
	private int hintIndex;

	private int scores;

	[Header("GUI")]


	[SerializeField] private Image[] stars;

	[SerializeField] private Image renderer;

	[SerializeField] private Button hintButton;

	[SerializeField] private GameObject resultAnswer;

	[SerializeField] private GameObject finishPanel;
	[SerializeField] private Sprite[] resultAnswerSprites;
	
	[SerializeField] private Text answerText;

	[SerializeField] private Text scoreText;

	private string[] names = {
		"Aku","Bapak","Cantik","Dia","Enak",
		"Foto","Guru","Hai","Ilmu","Jangan",
		"Kita","Lama","Makan","Nama","Olahraga",
		"Panas","Rumah","Semangat","Tidur","Uang"		
	};

	// Use this for initialization
	void Start () {

		RandomIntArrayOrder(arrIndex);

		SetQuiz();
	}
	
	// Update is called once per frame
	void Update () {

		if(gameIsPlaying){
			//Animate
			Animate();
		} 
		
	}

	void Animate(){
		float framesPerSecond = 2;

		int startFrameIdx = 0;
		for (int j = 0; j < arrIndex[index]; j++)
		{
			startFrameIdx += nFrames[j];
		}
		float i = (Time.time * framesPerSecond) % nFrames[arrIndex[index]];		
		renderer.sprite = frames[(int) i + startFrameIdx];
		renderer.SetNativeSize();
	}

	public void ClickOption(Text t){

		if(answerIndex < answerText.text.Length - 2){
			char c = t.text[0];
			answer += c;
			hintIndex++;
			string tempText = answerText.text;		
			char[] ch = tempText.ToCharArray();
			ch[answerIndex + 1] = c; // index starts at 0!
			string newstring = new string (ch);		
			Debug.Log(answerIndex + 1);

			answerText.text = newstring;

			answerIndex+=2;
		} 
		
		if(answerIndex >= answerText.text.Length - 2){
			CheckAnswer();
		}
		
		
	}

	public void Hint(){
		if(!hint){
			hint = true;
			if(answerIndex < answerText.text.Length - 2){
				char c = names[arrIndex[index]].ToUpper()[hintIndex];
				hintIndex++;
				answer += c;

				string tempText = answerText.text;		
				char[] ch = tempText.ToCharArray();
				ch[answerIndex + 1] = c; // index starts at 0!
				string newstring = new string (ch);		
				Debug.Log(answerIndex + 1);

				answerText.text = newstring;

				answerIndex+=2;
			} 
			
			if(answerIndex >= answerText.text.Length - 2){
				CheckAnswer();
			}
		}
	}

	public void Delete(){
		if(answerIndex >= 2){
			answerIndex-=2;

		
			answer = answer.Substring(0, answer.Length - 1);

			// Debug.Log(answer);

			string tempText = answerText.text;		
			char[] ch = tempText.ToCharArray();
			ch[answerIndex + 1] = '_'; // index starts at 0!
			string newstring = new string (ch);		
			// Debug.Log(answerIndex + 1);

			Debug.Log(newstring);

			answerText.text = newstring;
		}
		


	}

	void CheckAnswer(){

		hintIndex = 0;
		hint = false;
		
		if(answer == names[arrIndex[index]].ToUpper()){
			Debug.Log("True");
			Correct();
		} else {
			Debug.Log("False");
			Wrong();
		}
	}

	void Correct(){
		StartCoroutine(ShowResult(1));
		scores++;
		scoreText.text = "Scores: " + scores;
	}

	void Wrong(){
		StartCoroutine(ShowResult(0));
	}

	IEnumerator ShowResult(int isCorrect){
		if(isCorrect == 1){			
			resultAnswer.transform.GetChild(0).GetComponent<Image>().sprite = resultAnswerSprites[1];
		} else {			
			resultAnswer.transform.GetChild(0).GetComponent<Image>().sprite = resultAnswerSprites[0];
		}		

		for (int i = 0; i < 3; i++)
		{
			resultAnswer.SetActive(true);
			yield return new WaitForSeconds(0.2f);
			resultAnswer.SetActive(false);
			yield return new WaitForSeconds(0.2f);
		}

		NextQuiz();
		
	}

	void Finish(){
		Debug.Log("Finish");
		gameIsPlaying = false;
		finishPanel.SetActive(true);

		switch (scores)
		{
			case 10:
				stars[0].enabled = true;
				stars[1].enabled = true;
				stars[2].enabled = true;
				break;
			case 8:
				stars[0].enabled = true;
				stars[1].enabled = true;
				break;
			case 5:
				stars[0].enabled = true;
				break;
			default:
				
				break;
		}
	}

	void NextQuiz(){
		index++;
		if(index < 10){
			SetQuiz();
			
		} else {
			Finish();
		}
	}

	void SetQuiz(){

		//reset answer
		answer = "";
		answerText.text = " ";
		answerIndex = 0;
	
		int nChar = names[arrIndex[index]].Length;
		char[] nameChars = new char[10];
		string tempName = names[arrIndex[index]].ToUpper();
	
		int m = 10 - names[arrIndex[index]].Length;
		//add char to 10		
		for (int i = nChar; i < 10; i++)
		{
			int r = Random.Range(0,names.Length);
			tempName += names[r].ToUpper()[Random.Range(0,names[r].Length)];		
			
		}
		
		nameChars = tempName.ToCharArray();

		RandomCharArrayOrder(nameChars);

		for (int i = 0; i < optionButtons.Length; i++)
		{
			optionButtons[i].SetActive(true);
			optionButtons[i].transform.GetChild(0).GetComponent<Text>().text = "" + nameChars[i];
		}

		//Set Answer String
		for (int i = 0; i < nChar; i++)
		{
			answerText.text += "_ ";
		}

		hintButton.interactable = true;

		
	}

	void RandomIntArrayOrder(int[] arr){
		for (int i = 0; i < 50; i++){
			int r = Random.Range(0,arr.Length - 1);
			int r_ = Random.Range(0,arr.Length - 1);
			
			int tempInt = arr[r];
			arr[r] = arr[r_];
			arr[r_] = tempInt;
		}

		// Show array value
		for (int i = 0; i < arr.Length; i++){
			Debug.Log(arr[i]);
		}
	}

	void RandomCharArrayOrder(char[] arr){
		for (int i = 0; i < 50; i++){
			int r = Random.Range(0,arr.Length - 1);
			int r_ = Random.Range(0,arr.Length - 1);
			
			char tempChar = arr[r];
			arr[r] = arr[r_];
			arr[r_] = tempChar;
		}

		//Show array value
		// for (int i = 0; i < arr.Length; i++){
		// 	Debug.Log(arr[i]);
		// }
	}
}
