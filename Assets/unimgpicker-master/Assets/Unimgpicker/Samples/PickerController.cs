using UnityEngine;
using System.Collections;

namespace Kakera
{
    public class PickerController : MonoBehaviour
    {
        [SerializeField]
        private Unimgpicker imagePicker;

        [SerializeField]
        private MeshRenderer imageRenderer;

        void Awake()
        {
            imagePicker.Completed += (string path) =>
            {
                StartCoroutine(LoadImage(path, imageRenderer));
            };
        }

        public void OnPressShowPicker()
        {
            imagePicker.Show("Select Image", "unimgpicker", 1024);
        }

        private IEnumerator LoadImage(string path, MeshRenderer output)
        {
            var url = "file://" + path;
            var www = new WWW(url);
            yield return www;
        
            Texture2D texture = www.texture;

            float wi = texture.width;        //重要？？
		    float he = texture.height;

            //Sprite sprite = Sprite.Create(texture, new Rect(0,0,wi, he), new Vector2(0.5f, 0.5f));
            //Image image ;
            //image.sprite = sprite;

        
            if (texture == null)
            {
                Debug.LogError("Failed to load texture url:" + url);
            }

            output.material.mainTexture = texture;
            output.transform.localScale = new Vector3 (-wi/40000, -he/40000, 0.000001f);
        }
    }
}