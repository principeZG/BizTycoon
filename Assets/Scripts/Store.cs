using UnityEngine;
using UnityEngine.UI;

    public class Store : MonoBehaviour
    {

        [Header("Store configuration")]
        [SerializeField] public float BaseStoreCost = 1.5f;            // Starting cost of each Store
        [SerializeField] public float BaseStoreProfit;                 // Starting profit of each Store
        [SerializeField] public Text BaseStoreProfitText;

        [Header("Configuration")]
        [SerializeField] private bool managerUnlocked = false;  // Autocollection of profits
        [SerializeField] public float StoreTimer = 1f;          // Time to earn profits
        [SerializeField] public float StoreMultiplier = 1.1f;   // Store inflation rate

        public Text StoreName;


        // Cahced references
        public static GameManager GameManager;


        public int StoreTimerDivision = 5;

        public int StoreCount = 0;                            // Track how manny of each store we bought
        public Text StoreCountText;
	
        private float _nextStoreCost;                           // Var for defining Store cost including inflation
        private float _currentTimer = 0f;                       // Actual timer counting for profits
        private bool _timerStarted = false;

        public UIStore UIStore;


        void Awake()
        {
            UIStore = transform.GetComponent<UIStore>();
        }

        // Use this for initialization
        void Start ()
        {
            // Assign starting cost of a store to later calucalte inflation costs - BuyStoreOnClick() 
            //_nextStoreCost = BaseStoreCost;
            GameManager = FindObjectOfType<GameManager>();

            StoreCountText.text = StoreCount.ToString();
            BaseStoreProfitText.text = BaseStoreProfit.ToString("C2");
        }

        // Update is called once per frame
        void Update ()
        {
            CheckTimerStatus();
	    
        }

        // Check if Timer running, and update the Timer
        private void CheckTimerStatus() 
        {
            if (_timerStarted)
            {
                _currentTimer += Time.deltaTime;
                if (StoreCount > 0)
                {
                    CheckStoreTimerAndAddProfits(); // Timer is up&running now go to Store level 
                }
            }
        }

        // Compare Timer and Store timer and collect profit money. Also check if Manager is active for autocollect
        private void CheckStoreTimerAndAddProfits()
        {
            if (_currentTimer > StoreTimer)
            {
                CheckIsManagerUnlocked();

                _currentTimer = 0f;
                GameManager.AddToBalance(BaseStoreProfit * StoreCount);
            }
        }

        private void CheckIsManagerUnlocked()
        {
            if (!managerUnlocked)
            {
                _timerStarted = false;
            }
        }

        public bool AvailableFundsToBuy(float storeCost)
        {
            if (GameManager.GetCurrentBalance() >= _nextStoreCost)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void BuyStore()
        {
            if (AvailableFundsToBuy(_nextStoreCost))
            {
                // Update no. of stores
                StoreCount += 1;
                
                // Update current balance
                GameManager.AddToBalance(-_nextStoreCost);
                
                // Calculate the new store cost
                _nextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier, StoreCount + 1));
                
                // Calculate a new collection store timer
                if (StoreCount % StoreTimerDivision == 0)
                {
                    StoreTimer = StoreTimer * 1.25f;
                    Debug.Log(StoreTimer.ToString());
                }
            }
        }

        public void OnStartTimer()
        {
            if (!_timerStarted)
            {
                _timerStarted = true;
            }

        }

        public float GetCurrentTimer()
        {
            return _currentTimer;
        }

        public float GetStoreTimer()
        {
            return StoreTimer;
        }

        public void SetNextStoreCost(float cost)
        {
            _nextStoreCost = cost;
        }

        public float GetNextStoreCost()
        {
            return _nextStoreCost;
        }
}

