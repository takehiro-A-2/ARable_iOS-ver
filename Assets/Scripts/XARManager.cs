using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.IO;
using NCMB;
using UnityEngine.Events;
using UnityEngine.Video;
using FantomLib;
using UnityEngine.XR.ARSubsystems;
using Const;

namespace Kakera
{
[RequireComponent(typeof(ARRaycastManager))]
public class XARManager : MonoBehaviour
{
    ARRaycastManager raycastManager;
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    public Camera ARCamera;
    public GameObject Camera;

    public bool activation = true;
    private string account_id = "0";
    public string account_name;
    private string account_mail;

    public string filename;
    public string extention;
    public int size;
    public int volume;

    public static string change_name;
    public static string sum;
    public static string introduction;
    public static string town;
    public static string birthday;
    public static Texture2D icon1;
    public static Texture2D icon2;
    public static string Kagi = "false";

        [SerializeField]
    public Unimgpicker imagePicker;

    [SerializeField]
    public MeshRenderer imageRenderer;

    //private const string Server_Address = ServerURL.LocalIP;
    //private const string Server_GlobalIP = "http://192.168.0.104:3000";
    private const string Server_Address = "127.0.0.1:3000";

    //private const string Server_GlobalIP = "http://10.213.220.214:3000";        ///UTokyoWifi使用時////

        private const string Server_index = Server_Address + "/posts/index";
    private const string Server_show = Server_Address + "/posts/show/";
    private const string Post_create = Server_Address + "/posts/create";
    private const string URL_UserInfo = Server_Address + "/configs/show";////////

    public string[] textMessage; //テキストの加工前の一行を入れる変数
    public string[,] textWords; //テキストの複数列を入れる2次元は配列 
    
    public int Latest_rowLength;
    //public bool fitOrientation = true;    
    private int defaultWidth = 384;     
    private int defaultHeight = 384;    
    public int image_Orientation = 0;

    public Toggle toggleimage;
    public Toggle toggletext;
    public Slider slider;
    public string Imagepath;
    //public Text Imagepath;

    public GameObject clone; 
    private GameObject cube;
    public Image image;                  //Image to apply texture.    //テクスチャを適用する画像
    Texture2D texture ;

    public GameObject ScaleObject;
    public GameObject TextClone;
    public TextMesh Text;
    public GameObject mirror;

    public ID ID_clone;
    public ID ID_mirror;

    float longTapTime = 1.8f;               // ロングタップと判定する秒数
    float nowTapTime;                       // タップしてからの時間
    bool isLongtap;  

    public float currentTime = 0f;
    public int currentObjectNumber = 0;

void Awake()
    {
            account_id = LogInManager.account_id;
            account_name = LogInManager.account_name;
            account_mail = LogInManager.account_mail;
            Debug.Log(account_id + "だz！");
            //Debug.Log(account_name + "だzzzzzzzzzzzzzz！");
            //Debug.Log(account_mail + "だz！");

            ReceiveUserInfo(account_id, account_mail);
            //Debug.Log(change_name + "だzzzzsedzgsz！");

            Updater();
            raycastManager = GetComponent<ARRaycastManager>();

            imagePicker.Completed += (string path) =>
            {
                StartCoroutine(LoadImage(path));
            };
    }
    
void Update()
    {
        if (Input.GetMouseButton(0))
        {    
            nowTapTime += Time.deltaTime;   // 秒数をカウント

            // タップし続けた時間が規定値を超えたらロングタップとして扱う
            if (nowTapTime >= longTapTime && !isLongtap) {
                nowTapTime = 0;             // タイマーリセット
                isLongtap = true;           // ロングタップしたフラグを立てる

                Create_Prefab();           // Prefabの作成
        }    
                Debug.Log("Long Tap");
        
        } else if (Input.GetMouseButtonUp(0)) {
            // クリックを終えたら初期化
            nowTapTime = 0;                 
            isLongtap = false;
        }
    
        float span = 1.4f;
        currentTime += Time.deltaTime;
        if(currentTime > span)
        {
            //Debug.LogFormat ("{0}秒経過", span);
            //Debug.LogFormat ("更新");
            Updater();
            currentTime = 0f;
        }
}

        public void Updater()
        {
            StartCoroutine(Receive());
        }
        IEnumerator Receive()
        {
            UnityWebRequest www0 = UnityWebRequest.Get(Server_index);
            yield return www0.SendWebRequest();

            if (www0.isNetworkError || www0.isHttpError)
            {
                //Debug.Log(www0.error);
                Debug.Log("オブジェクト数取得の失敗");
            }
            else
            {
                string[] del0 = { " ,, " };
                string[] a = www0.downloadHandler.text.Split(del0, StringSplitOptions.None);

                //Debug.Log(a[1]);   //最大
                //Debug.Log(a[2]);   //最小

                for (int i = currentObjectNumber + 1; i <= int.Parse(a[1]); i++)
                {
                    UnityWebRequest www00 = UnityWebRequest.Get(Server_show + i);
                    yield return www00.SendWebRequest();

                    if (www00.isNetworkError || www00.isHttpError)
                    {
                        //Debug.Log(www00.error);
                        //Debug.Log("オブジェクト情報取得の失敗");
                    }
                    else
                    {
                        //Debug.Log(www00.downloadHandler.text);
                        string[] del = { " //-/// " };
                        string[] c = www00.downloadHandler.text.Split(del, StringSplitOptions.None);

                        //Debug.Log(c[0]);   //  使わないテキスト
                        //Debug.Log(c[1]);     //  URL
                        //Debug.Log(c[2]);     //  x
                        //Debug.Log(c[3]);     //  y
                        //Debug.Log(c[4]);     //  z
                        //Debug.Log(c[5]);     //  text
                        //Debug.Log(c[6]);     //  size
                        //Debug.Log(c[7]);     //  activation
                        //Debug.Log(c[8]);     //  account_id
                        //Debug.Log(c[9]);     //  object_id
                        //Debug.Log(c[10]);    //  volume
                        //Debug.Log(c[11]);    //  使わないテキスト

                            float data0 = Convert.ToSingle(c[2]);
                            float data1 = Convert.ToSingle(c[3]);
                            float data2 = Convert.ToSingle(c[4]);

                            Vector3 coordinate = new Vector3(data0, data1, data2);
                            Quaternion q = Quaternion.Euler(0f, 0f, 0f);

                            if (c[5] == " 画 像 ")
                            {
                                UnityWebRequest www = UnityWebRequestTexture.GetTexture(Server_Address + c[1]);
                                yield return www.SendWebRequest();

                                ID_clone.x = c[2];
                                ID_clone.y = c[3];
                                ID_clone.z = c[4];
                                ID_clone.text = c[5];
                                //ID_clone.size = c[6];
                                //ID_clone.activation = c[7];
                                ID_clone.account_id = c[8];
                                ID_clone.object_id = c[9];

                                var andy = Instantiate(clone, coordinate, q);
                                Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;

                                //取得した画像のテクスチャをRawImageのテクスチャに張り付ける
                                andy.GetComponent<Renderer>().material.mainTexture = tex;

                                float x;
                                float y;

                                if (tex.width > tex.height)
                                {
                                    float AspectRatio = tex.width / tex.height;

                                    y = (tex.height / tex.height) * 0.6f;
                                    x = AspectRatio * 0.6f;

                                    andy.transform.localScale = new Vector3(x, y, 0.000001f);
                                }

                                if (tex.height > tex.width)
                                {
                                    float AspectRatio = tex.height / tex.width;

                                    x = (tex.width / tex.width) * 0.6f;
                                    y = AspectRatio * 0.6f;

                                    andy.transform.localScale = new Vector3(x, y, 0.000001f);
                                }

                                andy.transform.LookAt(Camera.transform.position);
                                andy.transform.Rotate(new Vector3(0, 0, -1), int.Parse(c[10]));

                                andy.transform.name = "Image" + c[9];

                            }
                            else
                            {
                                ID_mirror.x = c[2];
                                ID_mirror.y = c[3];
                                ID_mirror.z = c[4];
                                ID_mirror.text = c[5];
                                //ID_mirror.size = c[6];
                                //ID_mirror.activation = c[7];
                                ID_mirror.account_id = c[8];
                                ID_mirror.object_id = c[9];

                                var andy = Instantiate(mirror, coordinate, q);
                                TextMesh textObject = andy.GetComponent<TextMesh>();
                                Vector3 scale = new Vector3(-0.1f, 0.1f, 0.1f); ///////受信オブジェクトの大きさ//////////
                                Text.text = c[5].Replace("\r\n", "\\n");
                                float width = 0f;
                                float height = 1f;
                                width = Text.text.Length;
                                height = 1 + (float)1.75 * (CountChar(Text.text, '\n'));
                                ScaleObject.transform.localScale = new Vector3((((float)(width / 4.3f))), height, 0.00001f);
                                textObject.text = Text.text;
                                andy.transform.localScale = scale;
                                andy.transform.LookAt(Camera.transform.position);
                                andy.transform.name = "Text" + c[9];

                            }
                            currentObjectNumber = int.Parse(a[1]);
                    }
                }
            }
        }

        public void Create_Prefab()
        {
            Touch touch = Input.GetTouch(0);
      
        if (raycastManager.Raycast(touch.position, hitResults, TrackableType.All))
            {
                GameObject prefab;
                    if(toggleimage.isOn)
                    {
                        prefab = cube;
                        
                        var andyObject = Instantiate(prefab, hitResults[0].pose.position, Quaternion.identity);

                        Send(hitResults[0].pose.position.x.ToString(), hitResults[0].pose.position.y.ToString(), hitResults[0].pose.position.z.ToString()
                        , " 画 像 ", texture, Imagepath, image_Orientation, account_id, true);
                            
                        andyObject.transform.LookAt(Camera.transform.position);
                        andyObject.transform.Rotate(new Vector3(0, 0, -1), image_Orientation);

                        Destroy(andyObject); /////////////////将来的にはコメントアウト
                }
                    if(toggletext.isOn)
                    {
                        prefab = TextClone;
                        //judge=0;
                        TextMesh TextContents = prefab.GetComponent<TextMesh>();
                        int countchar=TextContents.text.Length;
                        if(countchar!=0)
                        {
                        var andyObject =Instantiate(prefab, hitResults[0].pose.position, Quaternion.identity);

                        Send(hitResults[0].pose.position.x.ToString(), hitResults[0].pose.position.y.ToString(), hitResults[0].pose.position.z.ToString()
                      , TextContents.text, null, null, 0 , account_id, true);
                        
                        andyObject.transform.LookAt(Camera.transform.position);

                        Destroy(andyObject); /////////////////将来的にはコメントアウト
                        }
                    }
            }
}

    public void Send(string x, string y, string z, string text, Texture2D texture, string Imagepath, int Orientation, string account_id,bool activation)
    {
        //if (account_id != null)
        {
            //コルーチンを呼び出す
            StartCoroutine(OnSend(x, y, z, text, texture, Imagepath, Orientation, account_id, activation));
        }
        //else
        {
            //  Debug.Log("オブジェクトをプレースするならば，会員登録またはログインして下さい。");
        }
    }
    IEnumerator OnSend(string x, string y, string z, string text, Texture2D texture, string Imagepath, int Orientation, string account_id, bool activation)
    {
        //POSTする情報
        WWWForm post = new WWWForm();

        if (text == " 画 像 ")
        {
            //byte[] postimage = texture.GetRawTextureData(); datファイル．しかし壊れる
            byte[] postimage = texture.EncodeToJPG();

            //Imagepath = Imagepath.ToString();////////////////////////ファイルパス及びファイル名と拡張子の取得は未実装
            //var imageInfo = Imagepath.Split('/');
            //string filename = imageInfo[imageInfo.Length - 1];
            //var ext = filename.Split('.');
            //extention = ext[1];
            //post.AddField("extention", extention);
            //////////////////////////////////////////////////////////

            post.AddField("filename", Imagepath);
            // formにバイナリデータを追加
            post.AddBinaryData("image", postimage);
            Debug.Log("binary追加");
            size = postimage.Length;
            post.AddField("size", size);
            post.AddField("volume", Orientation);
        }
 
        string object_id = account_id + currentObjectNumber;

        post.AddField("x", x);
        post.AddField("y", y);
        post.AddField("z", z);
        post.AddField("text", text);
        post.AddField("account_id", account_id);
        post.AddField("object_id", object_id);
        post.AddField("activation", activation.ToString());

        //URLをPOSTで用意
        UnityWebRequest webRequest = UnityWebRequest.Post(Post_create, post);
        //UnityWebRequestにバッファをセット
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        //URLに接続して結果が戻ってくるまで待機
        yield return webRequest.SendWebRequest();
        //エラーが出ていないかチェック
        if (webRequest.isNetworkError)
        {
            //通信失敗
            Debug.Log(webRequest.error);
            Debug.Log("オブジェクト情報の送信に失敗しました");
        }
    }

        public void ReceiveUserInfo(string account_id, string account_mail)
        {
            StartCoroutine(OnReceiveUserInfo(account_id, account_mail));
        }
        IEnumerator OnReceiveUserInfo(string account_id, string account_mail)
        {
            //POSTする情報
            WWWForm post = new WWWForm();

            post.AddField("account_id", account_id);
            post.AddField("account_mail", account_mail);

            //URLをPOSTで用意
            UnityWebRequest webRequest = UnityWebRequest.Post(URL_UserInfo, post);
            //UnityWebRequestにバッファをセット
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            //URLに接続して結果が戻ってくるまで待機
            yield return webRequest.SendWebRequest();
            //エラーが出ていないかチェック
            if (webRequest.isNetworkError)
            {
                //通信失敗
                //Debug.Log(webRequest.error);
                Debug.Log("ユーザー情報のリクエストに失敗しました");
            }
            else
            {
                string[] del0 = { " ////-/// " };
                string[] a = webRequest.downloadHandler.text.Split(del0, StringSplitOptions.None);

                change_name = Convert.ToString(a[2]);
                introduction = Convert.ToString(a[4]);
                town = Convert.ToString(a[6]);
                sum = Convert.ToString(a[5]);
                birthday = Convert.ToString(a[7]);
                Kagi = Convert.ToString(a[8]);

                //Debug.Log(change_name);
                //Debug.Log(introduction);
                //Debug.Log(town);
                //Debug.Log(sum);
                //Debug.Log(birthday);
                //Debug.Log(Kagi);
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        public void OnPressShowPicker()
        {
            imagePicker.Show("Select Image", "unimgpicker", 1024);
        }
    private IEnumerator LoadImage(string path)
        {
            var url = "file://" + path;
            var www = new WWW(url);
            yield return www;

        texture = www.texture;
        //Texture2D texture = www.texture;

        bool fitOrientation = true;

        int width = texture.width > 0 ? texture.width : defaultWidth;         //Alternate value when width get failed.    //幅の取得に失敗したときの代替値
        int height = texture.height > 0 ? texture.height : defaultHeight;     //Alternate value when height get failed.   //高さの取得に失敗したときの代替値
        //int orientation = fitOrientation ? texture.orientation : 0;        //It also becomes 0 even if get failed.     //取得に失敗したときにも 0 となる
        int orientation = 1;

        cube = GameObject.Instantiate(clone);       //重要!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        LoadAndSetImage(path, width, height, orientation);

        Imagepath = path;

        //Imagepath.text = path;

            //private IEnumerator LoadImage(string path, MeshRenderer output)
            //{
            //var url = "file://" + path;
            //var www = new WWW(url);
            //yield return www;
            //Texture2D texture = www.texture;

            //float wi = texture.width > 0 ? texture.width : defaultWidth;         //Alternate value when width get failed.    //幅の取得に失敗したときの代替値
            //float he = texture.height > 0 ? texture.height : defaultHeight; ;

            //Sprite sprite = Sprite.Create(texture, new Rect(0,0,wi, he), new Vector2(0.5f, 0.5f));

            //image.sprite = sprite;
            //image.transform.localScale = new Vector3 (wi/900, he/900, 0.000001f);
            //if (texture == null)
            //{
            //  Debug.LogError("Failed to load texture url:" + url);
            //}

            //output.material.mainTexture = texture;
            //output.transform.localScale = new Vector3 (-wi/4000, -he/4000, 0.000001f);
            //cube=GameObject.Instantiate(clone);       //重要!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //Imagepath.text=url;
        }
    public void OnGalleryPick(ImageInfo info)
    {
        XDebug.Log("OnGalleryPick: " + info);
        bool fitOrientation = true;

        int width = info.width > 0 ? info.width : defaultWidth;         //Alternate value when width get failed.    //幅の取得に失敗したときの代替値
        int height = info.height > 0 ? info.height : defaultHeight;     //Alternate value when height get failed.   //高さの取得に失敗したときの代替値
        int orientation = fitOrientation ? info.orientation : 0;        //It also becomes 0 even if get failed.     //取得に失敗したときにも 0 となる
             
        cube = GameObject.Instantiate(clone);       //重要!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        LoadAndSetImage(info.path, width, height, orientation);

        Imagepath = info.path;
        //Imagepath.text = info.path;
        }
   
    public void LoadAndSetImage(string path, int width, int height, int orientation)
    {
        Texture2D texture = LoadToTexture2D(path, width, height, TextureFormat.ARGB32, false, FilterMode.Bilinear);//imageについてはリサイズ，textureについては何も行わず
        if (texture != null && image != null)
        {
            RectTransform rt = image.rectTransform;
            orientation = (int)Mathf.Repeat(orientation, 360);

            int w, h;
            if (orientation == 90 || orientation == 270)
            {
                w = defaultWidth;
                h = height * w / width;
            }
            else
            {
                h = defaultHeight;
                w = width * h / height;
            }

            rt.sizeDelta = new Vector2(w, h);   //Make it the same ratio as the image.  //画像と同じ比率にする
            rt.localRotation = Quaternion.Euler(0, 0, -orientation);
            try
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0,0,width, height), new Vector2(0.5f, 0.5f));
                image.sprite = sprite;

                cube.GetComponent<Renderer>().material.mainTexture  = texture;
                image_Orientation = 0;
                float wi = image.GetComponent<RectTransform>().sizeDelta.x;        //重要？？
                float he = image.GetComponent<RectTransform>().sizeDelta.y;

                cube.transform.localScale = new Vector3 (wi/(800-(slider.value*100)), he/(800-(slider.value*100)), 0.000001f);

                BoxCollider m_Collider=cube.GetComponent<BoxCollider>();
                m_Collider.size = new Vector3 (-wi/(800-(slider.value*100)), -he/(800-(slider.value*100)), 0.000001f);
            }
            catch (Exception)
            {
                XDebug.Log("Sprite.Create failed.");
            }
        }
        else
        {
            XDebug.Log("CreateTexture2D failed.");
#if !UNITY_EDITOR && UNITY_ANDROID
            XDebug.Log("'READ_EXTERNAL_STORAGE' permission = " + AndroidPlugin.CheckPermission("android.permission.READ_EXTERNAL_STORAGE"));
#endif 
        }
    }

    //Load the image from the specified path and generates a Texture2D.     //指定パスから画像を読み込み、テクスチャを生成する。
public Texture2D LoadToTexture2D(string path, int width, int height, TextureFormat format, bool mipmap, FilterMode filter)
    {
        if (string.IsNullOrEmpty(path))
            return null;
        try
        {
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(width, height, format, mipmap);
            texture.LoadImage(bytes);
            texture.filterMode = filter;
            texture.Compress(false);
            return texture;
        }
        catch (Exception e)
        {
            XDebug.Log(e.ToString());
            return null;
        }
    }
public void Texture_Orientation()
    {
        int orientation1 = image_Orientation;
        image_Orientation = orientation1- 90;
        image.transform.Rotate(new Vector3(0, 0, 1), -90.0f);
    }

public static int CountChar(string s, char c) 
    {
        return s.Length - s.Replace(c.ToString(), "").Length;
    }
}
}
