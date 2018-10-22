using UnityEngine;
using UnityEngine.UI;

    public class UIStore : MonoBehaviour {

        public Slider ProgressSlider;
        public Text BuyButtonText;
        public Text StoreCountText;
        public Button BuyButton;
        private bool _storeUnlocked = true;             // Control point to show stores

        public Store Store;
        private GameManager _gameManager;


        void OnEnable()
        {
            GameManager.OnUpdateBalance += UpdateUI;
            LoadGameData.OnLoadDataComplete += UpdateUI;
        }

        void OnDisable()
        {
            GameManager.OnUpdateBalance -= UpdateUI;
            LoadGameData.OnLoadDataComplete -= UpdateUI;
    }

        void Awake()
        {
            Store = transform.GetComponent<Store>();
        }


        // Use this for initialization
        void Start ()
        {
            UpdateBuyButtonAndStoreCount(); 

        }
	
        // Update is called once per frame
        void Update () {

            ProgressSlider.value = Store.GetCurrentTimer() / Store.GetStoreTimer();
        }

        // Check our balance against Store and update Buy button and canvas group
        public void UpdateUI()
        {

            // Hide panel untit you can afford
            CanvasGroup cg = this.transform.GetComponent<CanvasGroup>();

            if (!_storeUnlocked && !Store.AvailableFundsToBuy(Store.GetNextStoreCost()))
            {
                cg.interactable = false;
                cg.alpha = 0.10f;
            }
            else
            {
                cg.interactable = true;
                cg.alpha = 1f;
                _storeUnlocked = true;
            }

            // Update button if you can afford the store
            if (Store.GameManager.GetCurrentBalance() >= Store.GetNextStoreCost())
            {
                UpdateBuyButtonAndStoreCount();
                BuyButton.interactable = true;
            }
            else
            {
                BuyButton.interactable = false;
                UpdateBuyButtonAndStoreCount();
            }
        
        }

        public void UpdateBuyButtonAndStoreCount()
        {
            BuyButtonText.text = "Buy " + Store.GetNextStoreCost().ToString("C2");
            StoreCountText.text = Store.StoreCount.ToString();
        }

        public void BuyStoreOnClick()
        {
            if (!Store.AvailableFundsToBuy(Store.GetNextStoreCost()))
            {
                //TODO Implement dialog box to cash in for some perks/coins
                return;
            }
            Store.BuyStore();
            UpdateUI();
    }

        public void OnTimerClick()
        {
            Store.OnStartTimer();
        }
    }

