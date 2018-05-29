using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//機能：ランダムに白黒の背景画像を変えるスクリプト
//　　　それと同時に色を変える必要があるGameObjectも一括管理
//アタッチ：ランダムにしたい背景にアタッチ
public class BackgroundRandom : MonoBehaviour {
	//定数=============================
	enum SPRITE {
		BACKGROUND_BLACK,		//0,1:背景画像
		BACKGROUND_WHITE,
		MEMOLIST_BLACK,			//2,3:メモリストボタン
		MEMOLIST_WHITE,
		RETURN_BLACK,			//4,5:戻るボタン
		RETURN_WHITE,
		FIRST_STAGE_BLACK,		//6,7:stage1～3ボタン
		FIRST_STAGE_WHITE,
		SECOND_STAGE_BLACK,		//8,9:stage4～6ボタン
		SECOND_STAGE_WHITE,
		THIRD_STAGE_BLACK,		//10,11:stage7～9ボタン
		THIRD_STAGE_WHITE,
		TUTORIAL_STAGE_BLACK,	//12,13:チュートリアルボタン
		TUTORIAL_STAGE_WHITE,
		BOSS_STAGE_BLACK,		//14,15:ボスステージボタン
		BOSS_STAGE_WHITE,
		MEMOLIST_SCENE_BLACK,	//16,17 メモリストシーンのボタン
		MEMOLIST_SCENE_WHITE,
		SPRITE_TYPE_COUNT		//18 スプライトの種類数
	};
	enum TITLE_COLOR {
		BLACK,
		WHITE,
		COUNT
	};
	const int TEXT_COUNT = 14;
	enum STAGE_BUTTON {
		FIRST_AREA_ONE,
		FIRST_AREA_TWO,
		FIRST_AREA_THREE,
		SECOND_AREA_ONE,
		SECOND_AREA_TWO,
		SECOND_AREA_THREE,
		THIRD_AREA_ONE,
		THIRD_AREA_TWO,
		THIRD_AREA_THREE,
		COUNT
	};
	//======================================


	private SpriteRenderer _backgroundSpriteRenderer;											//背景画像のSpriteRendererを参照する変数
	[SerializeField] private Sprite[] _sprite = new Sprite[(int)SPRITE.SPRITE_TYPE_COUNT];		//スプライト
	[SerializeField] private Text[] _texts = new Text[TEXT_COUNT];								//テキスト(色を変える用)
//	[SerializeField] private Image _image = null;												//Image(色を変える用)
	//タイトルシーン専用========================================================================================================
	private GameObject[] _lightImage = new GameObject[(int)TITLE_COLOR.COUNT];					//タイトルロゴのライト ※Title
	private GameObject[] _titleText = new GameObject[(int)TITLE_COLOR.COUNT];					//タイトルロゴ　		  ※Title
	//=========================================================================================================================
	//ステージセレクトシーン専用==============================================================================================================
	private Button _memoListButton;													//メモリストボタンを参照する変数　  ※StageSelect
	private Button _returnButton;													//戻るボタンを参照する変数　		  ※StageSelect
	private  Button[] _firstStageButtons = new Button[3];							//stage1から3ボタンを参照する変数　※StageSelect
	private Button[] _secondStageButtons = new Button[3];							//stage4から6ボタンを参照する変数　※StageSelect
	private Button[] _thirdStageButtons = new Button[3];							//stage7から9ボタンを参照する変数　※StageSelect
	private Button _tutorialButton;													//チュートリアルステージボタンを参照する変数　※StageSelect
	private Button[] _bossStageButtons = new Button[1];								//ボスステージボタンを参照する変数　※StageSelect
	//=======================================================================================================================================
	//メモリストシーン専用===================================================================================
	[SerializeField] private Button[] _memoListMainSceneButtons = new Button[5];	//MemoryListMainのボタン
	//======================================================================================================

	private bool _backgroundColorChange = false;				//背景の色が変化したかどうかを示す変数(false→黒、true→白)

	//ゲッター---------------------------------
	public bool GetBackgroundColorChange( ) {
		return _backgroundColorChange;
	}
	//-----------------------------------------

	void Awake() {	//別シーンから遷移アニメーション時、Canvasは別スクリプトで非アクティブになるので、その前に変数を取得する
		switch( gameObject.scene.name ) {
		case "Title":	//タイトルシーンでのみ背景色でアニメーションが変わるもの
			_lightImage [0] = GameObject.Find ("LightImage");
			_lightImage [1] = GameObject.Find ("LightImageBlack");
			_titleText [0] = GameObject.Find ("Image");
			_titleText [1] = GameObject.Find ("ImageBlack");
			break;
		case "StageSelect":	//ステージセレクトシーンでのみ背景色に応じて変わるもの
			_memoListButton = GameObject.Find ("MemoListButton").GetComponent<Button> ();
			_returnButton = GameObject.Find ("ReturnButton").GetComponent<Button> ();
			for (int i = 0; i < _firstStageButtons.Length; i++) {
				_firstStageButtons [i] = GameObject.Find ("Stage" + (i + 1)).GetComponent<Button> ();
			}
			for (int i = 0; i < _secondStageButtons.Length; i++) {
				_secondStageButtons [i] = GameObject.Find ("Stage" + (i + _firstStageButtons.Length + 1)).GetComponent<Button> ();
			}
			for (int i = 0; i < _thirdStageButtons.Length; i++) {
				//_thirdStageButtons [i] = GameObject.Find ("Stage" + (i + _firstStageButtons.Length + _secondStageButtons.Length + 1)).GetComponent<Button> ();
			}
			_tutorialButton = GameObject.Find ("Tutorial").GetComponent<Button> ();
			for (int i = 0; i < _bossStageButtons.Length; i++) {
				_bossStageButtons [i] = GameObject.Find ("BossStage" + (i + 1)).GetComponent<Button> ();
			}
			break;
		case "MemoryListMain":
			_returnButton = GameObject.Find ("ReturnButton").GetComponent<Button> ();
			break;
		case "MemoryList1":
			_returnButton = GameObject.Find ("ReturnButton").GetComponent<Button> ();
			break;
		case "MemoryList2":
			_returnButton = GameObject.Find ("ReturnButton").GetComponent<Button> ();
			break;
		case "MemoryList3":
			_returnButton = GameObject.Find ("ReturnButton").GetComponent<Button> ();
			break;
		case "MemoryList4":
			_returnButton = GameObject.Find ("ReturnButton").GetComponent<Button> ();
			break;
		case "MemoryList5":
			_returnButton = GameObject.Find ("ReturnButton").GetComponent<Button> ();
			break;
		default :
			break;
		}
	}

	// Use this for initialization
	void Start () {
		_backgroundSpriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		//ランダム処理------------------------------------------------------------
		string[] colorArray = {"black", "white"};
		int randnum = Random.Range ( (int)0, (int)2 );
		switch (gameObject.scene.name) {
		case "Title":
			ChangeTitleUI (colorArray[randnum]);
			break;
		case "StageSelect":
			ChangeStageSelectUI (colorArray[randnum]);
			break;
		case "MemoryListMain":
			ChangeMemoristUI (colorArray[randnum]);
			break;
		case "MemoryList1":
			ChangeMemoristUI (colorArray[randnum]);
			break;
		case "MemoryList2":
			ChangeMemoristUI (colorArray[randnum]);
			break;
		case "MemoryList3":
			ChangeMemoristUI (colorArray[randnum]);
			break;
		case "MemoryList4":
			ChangeMemoristUI (colorArray[randnum]);
			break;
		case "MemoryList5":
			ChangeMemoristUI (colorArray[randnum]);
			break;
		default :
			Debug.LogError ("例外処理が行われました");
			break;
		}
		if (colorArray [randnum] == "white") {
			_backgroundColorChange = true;
		} else {
			_backgroundColorChange = false;
		}
		//----------------------------------------------------------------------
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	//--タイトルシーンの背景に合わせたUIの変更処理
	void ChangeTitleUI( string color ) {
		switch (color) {
		case "black":
			_backgroundSpriteRenderer.sprite = _sprite [(int)SPRITE.BACKGROUND_BLACK];
			_lightImage [0].SetActive (true);
			_lightImage [1].SetActive (false);
			_titleText [0].SetActive (true);
			_titleText [1].SetActive (false);
			Debug.Log ("背景：黒");
			break;
		case "white":
			_backgroundSpriteRenderer.sprite = _sprite [(int)SPRITE.BACKGROUND_WHITE];
			_lightImage [0].SetActive (false);
			_lightImage [1].SetActive (true);
			_titleText [0].SetActive (false);
			_titleText [1].SetActive (true);
			Debug.Log ("背景：白");
			break;
		default :
			Debug.LogError ("引数をblackかwhiteにしてください");
			break;
		}
	}


	//--ステージセレクトシーンの背景に合わせたUIの変更処理
	void ChangeStageSelectUI( string color ) {
		switch (color) {
		case "black":
			_backgroundSpriteRenderer.sprite = _sprite [(int)SPRITE.BACKGROUND_BLACK];
			for (int i = 0; i < _texts.Length; i++) {
				if (_texts [i]) {
					_texts [i].color = new Color (255f, 255f, 255f);
				}
			}
			_memoListButton.image.sprite = _sprite [(int)SPRITE.MEMOLIST_BLACK];
			_returnButton.image.sprite = _sprite [(int)SPRITE.RETURN_BLACK];
			for (int i = 0; i < _firstStageButtons.Length; i++) {
				_firstStageButtons [i].image.sprite = _sprite [(int)SPRITE.FIRST_STAGE_BLACK];
			}
			for (int i = 0; i < _secondStageButtons.Length; i++) {
				_secondStageButtons [i].image.sprite = _sprite [(int)SPRITE.SECOND_STAGE_BLACK];
			}
			for (int i = 0; i < _thirdStageButtons.Length; i++) {
				if (_thirdStageButtons [i]) {	//thirdStageが追加されるまでif文で囲む
					_thirdStageButtons [i].image.sprite = _sprite [(int)SPRITE.THIRD_STAGE_BLACK];
				}
			}
			_tutorialButton.image.sprite = _sprite [(int)SPRITE.TUTORIAL_STAGE_BLACK];
			for (int i = 0; i < _bossStageButtons.Length; i++) {
				if (_bossStageButtons [i]) {	//bossStageが全部できるまでif文で囲む
					_bossStageButtons [i].image.sprite = _sprite [(int)SPRITE.BOSS_STAGE_BLACK];
				}
			}
			Debug.Log ("背景：黒");
			break;
		case "white":
			_backgroundSpriteRenderer.sprite = _sprite [(int)SPRITE.BACKGROUND_WHITE];
			for (int i = 0; i < _texts.Length; i++) {
				if (_texts [i]) {
					_texts [i].color = new Color (0f, 0f, 0f);
				}
			}
			_memoListButton.image.sprite = _sprite [(int)SPRITE.MEMOLIST_WHITE];
			_returnButton.image.sprite = _sprite [(int)SPRITE.RETURN_WHITE];
			for (int i = 0; i < _firstStageButtons.Length; i++) {
				_firstStageButtons [i].image.sprite = _sprite [(int)SPRITE.FIRST_STAGE_WHITE];
			}
			for (int i = 0; i < _secondStageButtons.Length; i++) {
				_secondStageButtons [i].image.sprite = _sprite [(int)SPRITE.SECOND_STAGE_WHITE];
			}
			for (int i = 0; i < _thirdStageButtons.Length; i++) {
				if (_thirdStageButtons [i]) {	//thirdStageが追加されるまでif文で囲む
					_thirdStageButtons [i].image.sprite = _sprite [(int)SPRITE.THIRD_STAGE_WHITE];
				}
			}
			_tutorialButton.image.sprite = _sprite [(int)SPRITE.TUTORIAL_STAGE_WHITE];
			for (int i = 0; i < _bossStageButtons.Length; i++) {
				if (_bossStageButtons [i]) {	//bossStageが全部できるまでif文で囲む
					_bossStageButtons [i].image.sprite = _sprite [(int)SPRITE.BOSS_STAGE_WHITE];
				}
			}
			Debug.Log ("背景：白");
			break;
		default :
			Debug.LogError ("引数をblackかwhiteにしてください");
			break;
		}
	}


	//--メモリストシーンの背景に合わせたUIの変更処理
	void ChangeMemoristUI( string color ) {
		switch (color) {
		case "black":
			_backgroundSpriteRenderer.sprite = _sprite [(int)SPRITE.BACKGROUND_BLACK];
			for (int i = 0; i < _texts.Length; i++) {
				if (_texts [i]) {
					_texts [i].color = new Color (255f, 255f, 255f);
				}
			}
			for (int i = 0; i < _memoListMainSceneButtons.Length; i++) {
				_memoListMainSceneButtons [i].image.sprite = _sprite [(int)SPRITE.MEMOLIST_SCENE_BLACK];
			}
			Debug.Log ("背景：黒");
			break;
		case "white":
			_backgroundSpriteRenderer.sprite = _sprite [(int)SPRITE.BACKGROUND_WHITE];
			for (int i = 0; i < _texts.Length; i++) {
				if (_texts [i]) {
					_texts [i].color = new Color (0f, 0f, 0f);
				}
			}
			for (int i = 0; i < _memoListMainSceneButtons.Length; i++) {
				_memoListMainSceneButtons [i].image.sprite = _sprite [(int)SPRITE.MEMOLIST_SCENE_WHITE];
			}
			Debug.Log ("背景：白");
			break;
		default :
			Debug.LogError ("引数をblackかwhiteにしてください");
			break;
		}
	}
}
