using UnityEngine;
using System.Collections;

public class mainMenu : MonoBehaviour {

    public GUIText _start;
    bool _startSelected;
    bool startSelect;

    
    public GUIText _instruct;
    bool instructSelect;
    bool showInstruct;
    bool exitSelect;
	//bool pressed = false;


    public GUIText _exit;
    int count;

    
    public GameObject instructSheet;


    Color yellow = new Color(248, 230, 0);
    Color green = new Color(37, 188, 57);


	// Use this for initialization
	void Start () {
        count = 1;
        	 _startSelected = true;
             instructSelect = false;
             exitSelect = false;
             _start.color = yellow;
             _instruct.color = Color.green;
             _exit.color = Color.green;

             instructSheet.SetActive(false);
             showInstruct = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.W) )
        {
            count -= 1;
            if(count <=0)
            {
                count = 3;
            }

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            count += 1;

            if(count> 3)
            {
                count = 1;
            }
        }
        updateColour();


        if(Input.GetKeyDown(KeyCode.Return))
        {
        	if (_startSelected == true)
            {
            	Application.LoadLevel("Intro");
            }
            else if (instructSelect == true)
            {
            	checkShowInstruct();
                //Application.Quit();
            }
            else if(exitSelect == true)
            {
            	Application.Quit();
            }
		}  
	}


    void checkShowInstruct()
    {
        if(showInstruct == true)
        {
            showInstruct = false;
            instructSheet.SetActive(false);
            _start.enabled = true;
            _instruct.enabled = true;
            _exit.enabled = true;
        }
        else
            if (showInstruct == false)
            {
                showInstruct = true;
                instructSheet.SetActive(true);
                _start.enabled = false;
                _instruct.enabled = false;
                _exit.enabled = false;
            }
    }

    void updateColour()
    {
       
        switch (count)
        {
            case 1: _start.color = yellow;
                _instruct.color = Color.gray;
                _exit.color = Color.gray;

                _startSelected = true;
             instructSelect = false;
             exitSelect = false;
                break;

            case 2: _start.color = Color.gray;
                _instruct.color = yellow;
                _exit.color = Color.gray;

                _startSelected = false;
                instructSelect = true;
             exitSelect = false;break;

            case 3:
                _start.color = Color.gray;
                _instruct.color = Color.gray;
                _exit.color = yellow;break;

            default: count = 1;
                _startSelected = true;
             instructSelect = false;
             exitSelect = false;
                break;
        }
    }
}
