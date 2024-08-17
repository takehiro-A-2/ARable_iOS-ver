using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_Functions : MonoBehaviour
{
    public Button File;
    public Button VC_Image;
    public Button Movie;
    public Button Comments;
    public Button Update_Button;
    public Button Setting;
    public Button Good;
    public Button Bad;
    public Button Home;
    public Button Camera;
    public Button Orientation;

    public Button CameraUI_Camera;
    public Button CameraUI_Home;
    public GameObject CameraUI_Panel;

    public RectTransform DeletePanel;
    public ToggleGroup ToggleGroup;
    public Toggle TogglePreview;
    public Toggle ToggleImage;
    public Toggle ToggleText;
    public Toggle Delete_Mode;

    public Slider Slider;
    public InputField InputField;
    public Image Image;

    public Text Imagepath;
    public Text Coordinate;
    public Text Information;

    DeviceOrientation PrevOrientation;

    public Canvas Canvas_UI;
    public Canvas Canvas0;
    public GameObject GameObject1;

    void Start()
    {

        //PrevOrientation = DeviceOrientation.Portrait;
        PrevOrientation = getOrientation();

        File.gameObject.SetActive(true);
        VC_Image.gameObject.SetActive(true);
        Movie.gameObject.SetActive(true);
        Comments.gameObject.SetActive(true);
        Update_Button.gameObject.SetActive(true);
        Setting.gameObject.SetActive(true);
        Good.gameObject.SetActive(true);
        Bad.gameObject.SetActive(true);
        Home.gameObject.SetActive(true);
        Camera.gameObject.SetActive(true);

        ToggleGroup.gameObject.SetActive(true);
        TogglePreview.gameObject.SetActive(true);
        ToggleImage.gameObject.SetActive(true);
        ToggleText.gameObject.SetActive(true);
        Delete_Mode.gameObject.SetActive(true);

        Slider.gameObject.SetActive(true);
        InputField.gameObject.SetActive(true);
        Image.gameObject.SetActive(true);

        Imagepath.gameObject.SetActive(true);
        Coordinate.gameObject.SetActive(true);
        Information.gameObject.SetActive(true);

        Coordinate.gameObject.SetActive(true);
        Imagepath.gameObject.SetActive(true);
        Coordinate.gameObject.SetActive(false);
        Movie.gameObject.SetActive(false);
        Home.gameObject.SetActive(false);

        Image.transform.localPosition = new Vector3(-415, -1680, 0);
        Image.transform.localScale = new Vector3(3f, 3f, 0.001f);

        Quaternion rotation = Image.transform.localRotation; ///ここだけ書き換える
        Vector3 rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        //////Image.transform.localRotation = rotation;

        Delete_Mode.transform.localPosition = new Vector3(1650, 2460, 0);
        Delete_Mode.transform.localScale = new Vector3(1.3f, 1.4f, 0.001f);

        rotation = Delete_Mode.transform.localRotation; ///ここだけ書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        Delete_Mode.transform.localRotation = rotation;

        ToggleGroup.transform.localPosition = new Vector3(876, -1950, 0);///-1950
        //ToggleGroup.transform.localPosition=new Vector3(700,0,0);
        ToggleGroup.transform.localScale = new Vector3(2.66f, 2.78f, 1);

        rotation = ToggleGroup.transform.localRotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        ToggleGroup.transform.localRotation = rotation;/////

        File.transform.localPosition = new Vector3(-580, -2390, 0);
        File.transform.localScale = new Vector3(1.03f, 1.03f, 0.001f);

        rotation = File.transform.localRotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        File.transform.localRotation = rotation;///

        Slider.transform.localPosition = new Vector3(-322, -1820, 0);
        Slider.transform.localScale = new Vector3(7.2f, 4f, 1);

        rotation = Slider.transform.rotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        Slider.transform.localRotation = rotation;

        InputField.transform.localPosition = new Vector3(-1186, -2204, 0);
        InputField.transform.localScale = new Vector3(2f, 2f, 1);

        rotation = InputField.transform.localRotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        InputField.transform.rotation = rotation;

        VC_Image.transform.localPosition = new Vector3(-456, -2394, 0);
        VC_Image.transform.localScale = new Vector3(1f, 1f, 0.001f);

        rotation = VC_Image.transform.localRotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        VC_Image.transform.localRotation = rotation;

        Camera.transform.localPosition = new Vector3(0, -2380, 0);
        //Camera.transform.localPosition=new Vector3(0,-400,0);
        Camera.transform.localScale = new Vector3(1.22f, 1.22f, 1);

        rotation = Camera.transform.rotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        Camera.transform.rotation = rotation;

        Comments.transform.localPosition = new Vector3(387, -2385, 0);
        Comments.transform.localScale = new Vector3(0.9f, 0.9f, 0.001f);

        rotation = Comments.transform.localRotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        Comments.transform.localRotation = rotation;

        Setting.transform.localPosition = new Vector3(-890, 2453, 0);
        Setting.transform.localScale = new Vector3(1f, 1f, 0.001f);

        rotation = Setting.transform.localRotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        Setting.transform.localRotation = rotation;

        Good.transform.localPosition = new Vector3(995, -2430, 0);
        Good.transform.localScale = new Vector3(2f, 2f, 0.001f);

        rotation = Good.transform.localRotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        Good.transform.localRotation = rotation;

        Bad.transform.localPosition = new Vector3(747, -2430, 0);
        Bad.transform.localScale = new Vector3(2f, 2f, 0.001f);

        rotation = Bad.transform.localRotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        Bad.transform.localRotation = rotation;

        Imagepath.transform.localPosition = new Vector3(-125, -3000, 0);
        Imagepath.transform.localScale = new Vector3(0.5f, 0.5f, 0.001f);

        rotation = Imagepath.transform.localRotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        Imagepath.transform.localRotation = rotation;

        Information.transform.localPosition = new Vector3(34, 2000, 0);
        Information.transform.localScale = new Vector3(0.4f, 0.4f, 0);

        rotation = Information.transform.localRotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        Information.transform.localRotation = rotation;

        Update_Button.transform.localPosition = new Vector3(-1075, -2356, 0);
        Update_Button.transform.localScale = new Vector3(0.7f, 0.7f, 0.001f);

        rotation = Update_Button.transform.localRotation; ///書き換える
        rotationAngles = rotation.eulerAngles;
        rotationAngles = new Vector3(0.0f, 0.0f, 0f);
        rotation = Quaternion.Euler(rotationAngles);
        Update_Button.transform.localRotation = rotation;

        CameraUI_Panel.transform.localPosition = new Vector3(469, -2165, 0);
        CameraUI_Panel.transform.localScale = new Vector3(3.6f, 3.6f, 3);
        CameraUI_Panel.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0f));

        Orientation.transform.localPosition = new Vector3(848, -1420, 0);

        Orientation.transform.localScale = new Vector3(1.22f, 1.22f, 1);
    }
    void Update()
    {
    if(ToggleImage.isOn)
        {
            Information.text="ディレクトリから画像を選択し，     置きたい場所を２秒間タップしてください";
            Information.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);

            Imagepath.gameObject.SetActive(true);
            Slider.gameObject.SetActive(false);
            Image.gameObject.SetActive(true);
            InputField.gameObject.SetActive(false);
            File.gameObject.SetActive(true);
            Orientation.gameObject.SetActive(true);

            VC_Image.gameObject.SetActive(false);////////////////////////////////VC用
            Movie.gameObject.SetActive(false);//////////////今後trueにする予定
            Comments.gameObject.SetActive(true);
            Imagepath.gameObject.SetActive(true);

            Good.gameObject.SetActive(true);
            Bad.gameObject.SetActive(true);
            Update_Button.gameObject.SetActive(true);
            Setting.gameObject.SetActive(true);
        }
    if(ToggleText.isOn)
        {
            Information.text="テキストボックスに入力し，       置きたい場所を２秒間タップしてください";
            Information.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);

            Slider.gameObject.SetActive(true);
            Image.gameObject.SetActive(false);
            InputField.gameObject.SetActive(true);
            File.gameObject.SetActive(false);
            VC_Image.gameObject.SetActive(false);
            Movie.gameObject.SetActive(false);
            Comments.gameObject.SetActive(true);
            Imagepath.gameObject.SetActive(false);
            Orientation.gameObject.SetActive(false);

            Good.gameObject.SetActive(true);
            Bad.gameObject.SetActive(true);
            Update_Button.gameObject.SetActive(true);
            Setting.gameObject.SetActive(true);
        }
    if(Delete_Mode.isOn)
        {
            Information.text="　注意！！  アイテムはタップすると削除されます";
            Information.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);

            Slider.gameObject.SetActive(false);
            Image.gameObject.SetActive(false);
            InputField.gameObject.SetActive(false);
            File.gameObject.SetActive(false);
            VC_Image.gameObject.SetActive(false);
            Movie.gameObject.SetActive(false);
            Good.gameObject.SetActive(false);
            Bad.gameObject.SetActive(false);
            Update_Button.gameObject.SetActive(false);
            Setting.gameObject.SetActive(false);
            Coordinate.gameObject.SetActive(false);
            Imagepath.gameObject.SetActive(false);
            Camera.gameObject.SetActive(false);
            Orientation.gameObject.SetActive(false);

            Comments.gameObject.SetActive(true);
        }
        
        DeviceOrientation currentOrientation = getOrientation();
        // 画面が横向きの場合の処理
        if(currentOrientation == DeviceOrientation.LandscapeLeft)
            {
            
           
        }

        // 画面が縦向きの場合の処理
        if(currentOrientation==DeviceOrientation.Portrait)
            {
                


           
        }
        PrevOrientation = currentOrientation;
    }
    // 端末の向きを取得するメソッド
    DeviceOrientation getOrientation() 
    {
        DeviceOrientation result = Input.deviceOrientation;

              // Unkownならピクセル数から判断
        if (result == DeviceOrientation.Unknown)
        {
            if (Screen.width < Screen.height)
            {
                result = DeviceOrientation.Portrait;
            }
            else
            {
                result = DeviceOrientation.LandscapeLeft;
            }
        }
        return result;
    }
    public void Comments_Button()
    {
        ToggleText.isOn=true;
    }
    public void UIBack()
    {
        Canvas0.gameObject.SetActive (false);
        Canvas_UI.gameObject.SetActive (true);
        
	    ToggleText.isOn=true;
    }
    public void PreviewToggleOn()
	{
	Canvas_UI.gameObject.SetActive (false);
	//slider1.gameObject.SetActive (false);
	GameObject1.gameObject.SetActive(true);
	}
    public void Calibration()
    {
        
        SceneManager.LoadScene("Calibration");
        Debug.Log("成功");

    }
    public void Configure()
        {
        SceneManager.LoadScene("Configure");

        //#if UNITY_ANDROID
        //string url = "http://40.115.247.138:8080/PBL2019ASARI/web/dellPhp.php";
        //Application.OpenURL(url);
        //#elif UNITY_IPHONE
        //string url = "http://40.115.247.138:8080/PBL2019ASARI/web/dellPhp.php";
        //Application.OpenURL(url);
        //#else
        //#endif
    }
}

