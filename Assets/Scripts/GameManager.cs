using UnityEngine;


    public class GameManager : MonoBehaviour
    {

        public delegate void OnUpdateBalanceHandler();
        public static event OnUpdateBalanceHandler OnUpdateBalance;

        public static GameManager GameManagerInstance;
        [SerializeField] public float CurrentBalance = 6f;

        private UIManager _uiManager;

        void Awake()
        {
            // Singleton pattern
            if (GameManagerInstance == null)
            {
                GameManagerInstance = this;
            }
            else if (GameManagerInstance != this)
            {
                Destroy(gameObject);
            }
            else 
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    
        // Use this for initialization
        void Start()
        {
            if (OnUpdateBalance != null)
            {
                OnUpdateBalance();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddToBalance(float amount)
        {
            CurrentBalance += amount;
            if (OnUpdateBalance != null)
            {
                OnUpdateBalance();
            }
        }

        public float GetCurrentBalance()
        {
            return CurrentBalance;
        }
    }
