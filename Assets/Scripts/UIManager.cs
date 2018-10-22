using UnityEngine;
using UnityEngine.UI;

    public class UIManager : MonoBehaviour
    {

        public Text CurrentBalanceText;
        private static GameManager _gameManager;


        /* OLD VERSION WITHOUT DELEGATES
    void Awake()
	{
		// Singleton pattern implementation
		if (_UIManagerInstance == null)
		{
			_UIManagerInstance = this;
		}
		else if (_UIManagerInstance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
		
	}*/

        // Use this for initialization
        void Start ()
        {
            _gameManager = FindObjectOfType<GameManager>();
            UpdateUI();
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        // This function is called when the object becomes enabled and active. 
        void OnEnable() 
        {
            GameManager.OnUpdateBalance += UpdateUI;
        }

        // This function is called when the behaviour becomes disabled. 
        // This is also called when the object is destroyed and can be used for any cleanup code.
        // When scripts are reloaded after compilation has finished, OnDisable will be called, 
        // followed by an OnEnable after the script has been loaded.
        void OnDisable()
        {
            GameManager.OnUpdateBalance -= UpdateUI;
        }


        public void UpdateUI()
        {
            CurrentBalanceText.text = _gameManager.GetCurrentBalance().ToString("C2");
        }


    }

