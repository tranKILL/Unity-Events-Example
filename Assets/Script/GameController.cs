namespace NJ_Event {
    using System.Collections.Generic;
    using UnityEngine;

#if UNITY_EDITOR
    [DisallowMultipleComponent]
    //[RequireComponent(typeof(InputManager))]
    [RequireComponent(typeof(ScoreManager))]
    //[RequireComponent(typeof(LifeManager))]
#endif

    public class GameController : InputManager {
        public static event System.Action<Vector3> OnMouseClick;
        public static event System.Action<KeyCode> OnKeyPress;

        public static event System.Action FreezeCube;
        public static event System.Action UnfreezeCube;

        //private InputManager m_inputManager;
        private ScoreManager m_scoreManager;
        private LifeManager m_lifeManager;

        private Vector3 screenBounds;

        private void Awake()
        {
            //m_inputManager = gameObject.GetComponent<InputManager>();
            m_scoreManager = GetComponent<ScoreManager>(); //-> Child
            m_lifeManager = GetComponentInChildren<LifeManager>();
        }
 
        private void Start()
        {
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            CubeEventProducer.OnLeftCubeClick += OnLeftCubeClick;
            Cube2Producer.OnLeftCube2Click += OnLeftCube2Click;
            CubeMove.OnSwitchCube += OnSwitchCube;
        }
        private void OnSwitchCube(int _nbCubeSwitch)
        {
            m_lifeManager.DecreaseLife(_nbCubeSwitch);
        }
        private void OnDestroy()
        {
            // Assurez-vous de vous désabonner de l'événement pour éviter des fuites de mémoire
            CubeEventProducer.OnLeftCubeClick -= OnLeftCubeClick;
            Cube2Producer.OnLeftCube2Click -= OnLeftCube2Click;
            CubeMove.OnSwitchCube -= OnSwitchCube;
            OnDisable();
        }
        private void OnLeftCubeClick(Vector3 leftCubeSize)
        {
            m_scoreManager.IncreaseScore((int) leftCubeSize.x);
            FreezeCube?.Invoke();
        }
        private void OnLeftCube2Click(Vector3 leftCube2Size)
        {
            m_lifeManager.IncreaseLife(2);
            UnfreezeCube?.Invoke();
        }

        private void Update()
        {
            /*CheckProjectileOutOfBounds();
            if (!menuOnSreen && tireLaserActif)
            {
                FireContinuousLaser();
            }*/
            HandleKeyboardInput();
            //HandleMouseInput();
        }

        private void HandleKeyboardInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnKeyPress?.Invoke(KeyCode.Space);
            }

        }

        private void HandleMouseInput()
        {
            Vector3 mousePosition = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
//Debug.Log("4 HandleLeftMouseInput");
                OnMouseClick?.Invoke(mousePosition);
            }
            /*else if (Input.GetMouseButtonDown(1))
            {
                OnMouseClick?.Invoke(mousePosition, 1);
            }
            else if (Input.GetMouseButtonDown(2))
            {
                OnMouseClick?.Invoke(mousePosition, 2);
            }*/
        }

        private void CheckInput()
        {
//Debug.Log(Input.GetMouseButtonDown(0) + " - " + Input.GetMouseButtonDown(1) + " - " + Input.GetMouseButtonDown(2));
            if (Input.GetMouseButtonDown(0)) // Left mouse button
            {
                EventsHandler.TriggerEvent("leftClic", new Dictionary<string, object> { { "leftClic", 1 } });
            }
            else if (Input.GetMouseButtonDown(1)) // Right mouse button
            {
                EventsHandler.TriggerEvent("rightClic", new Dictionary<string, object> { { "rightClic", 2 } });
            }
            else if (Input.GetMouseButtonDown(2)) // Middle mouse button
            {
                EventsHandler.TriggerEvent("middleClic", new Dictionary<string, object> { { "middleClic", 3 } });
            }
        }

        void OnEnable() {
            EventsHandler.StartListening("scoreUpdate", OnScoreUpdate);
            //EventManager.StartListening("addCoins", OnAddCoins);
            EventsHandler.StartListening("leftClicCubeDown", OnLeftClicCubeDown);
            EventsHandler.StartListening("leftClicCubeUp", OnLeftClicCubeUp);
            EventsHandler.StartListening("leftClic", OnLeftClic);
            EventsHandler.StartListening("rightClic", OnRightClic);
        }

        void OnLeftClicCubeDown(Dictionary<string, object> message)
        {
//Debug.Log("3 " + message + " OnLeftClicCubeDown - " + message["clic"]);
        }
        void OnLeftClicCubeUp(Dictionary<string, object> message)
        {
//Debug.Log(message + " OnLeftClicCubeUp - " + message["clic"]);
            m_scoreManager.DecreaseScore(1);
        }

        void OnDisable() {
            EventsHandler.StopListening("scoreUpdate", OnScoreUpdate);
            //EventManager.StopListening("addCoins", OnAddCoins);
            EventsHandler.StopListening("leftClicCubeDown", OnLeftClicCubeDown);
            EventsHandler.StopListening("leftClicCubeUp", OnLeftClicCubeUp);
            EventsHandler.StopListening("leftClic", OnLeftClic);
            EventsHandler.StopListening("rightClic", OnRightClic);
        }


        void OnScoreUpdate(Dictionary<string, object> message)
        {
            m_scoreManager.UpdateScoreText();
        }
        void OnAddCoins(Dictionary<string, object> message) {
Debug.Log(message + " OnAddCoins - " + message["amount"]);
            var amount = (int) message["amount"];
            //coins += amount;
        }
        void OnLeftClic(Dictionary<string, object> message)
        {
Debug.Log(message + " OnLeftClic - " + message["clic"]);
            var clic = (int)message["clic"];
            //totalClic += clic;
        }
        void OnRightClic(Dictionary<string, object> message)
        {
            Debug.Log(message + " OnRightClic - " + message["clic"]);
            var clic = (int)message["clic"];
            //totalClic += clic;
        }
        void OnMiddleClic(Dictionary<string, object> message)
        {
Debug.Log(message + " OnMiddleClic - " + message["clic"]);
            var clic = (int)message["clic"];
            //totalClic += clic;
        }


        public override void ProcessMouseButtonDown()
        {

        }
        public override void ProcessMouseButtonUp()
        {

        }
        public override void ProcessMouseRightButtonDown()
        {

        }
        public override void ProcessMouseRightButtonUp()
        {

        }
        public override void ProcessMouseMiddleButtonDown()
        {

        }
        public override void ProcessMouseMiddleButtonUp()
        {

        }
        public override void ProcessMousePosition(Vector2 _mousePosition)
        {

        }
        public override void ProcessInputAxes(Vector2 _inputAxes)
        {

        }
        public override void ProcessKeyCodeDown(KeyCode _keyCode)
        {

        }
        public override void ProcessKeyCodeUp(KeyCode _keyCode)
        {

        }
        public override void ProcessInputAxesRaw(Vector2 _inputRaw)
        {

        }
    }
}