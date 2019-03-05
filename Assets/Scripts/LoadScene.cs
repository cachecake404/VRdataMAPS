using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using OVR;

namespace BitScience
{
    public class LoadScene : MonoBehaviour
    {

        private int RenderPage = 1; //Turn this to zero if you fail to turn webpage to image.
        private float time = 6f;  // Change it accordingly for results
        private float Vtime = 6f; // Time for voice recognition

        //public GameObject SphereObject;
        public GameObject WebPage;
        public GameObject PlaceName;
        public GameObject ForeClose;
        public int ID;
        public List<string> information;
        [SerializeField]
        private StreetViewCube _streetViewCube;

        public class StreetViewObject
        {
            public string location;
            public float latitude;
            public float longitude;
        }

        StreetViewObject GetStreetObject(string location,float latitude,float longitude)
        {
            StreetViewObject obj = new StreetViewObject();
            obj.location = location;
            obj.latitude = latitude;
            obj.longitude = longitude;
            return obj;

        }

        private void run_file()
        {
            ProcessStartInfo pythonInfo = new ProcessStartInfo();
            Process python;
            pythonInfo.FileName = @"C:\Users\sam\AppData\Local\Programs\Python\Python36\pythonw.exe";
            pythonInfo.Arguments = @"D:\Users\sam\Documents\VRdataMaps\VRdataMAPS\Assets\Scripts\risk.pyw";
            pythonInfo.CreateNoWindow = false;
            pythonInfo.UseShellExecute = true;
            pythonInfo.WorkingDirectory = @"D:\Users\sam\Documents\VRdataMaps\VRdataMAPS\Assets\Scripts";
            python = Process.Start(pythonInfo);
            
            
        }

        void run_voice()
        {
            ProcessStartInfo pythonInfo = new ProcessStartInfo();
            Process python;
            pythonInfo.FileName = @"C:\Users\sam\AppData\Local\Programs\Python\Python36\pythonw.exe";
            pythonInfo.Arguments = @"D:\Users\sam\Documents\VRdataMaps\VRdataMAPS\Assets\Scripts\speechTEXT.pyw";
            pythonInfo.CreateNoWindow = false;
            pythonInfo.UseShellExecute = true;
            pythonInfo.WorkingDirectory = @"D:\Users\sam\Documents\VRdataMaps\VRdataMAPS\Assets\Scripts";
            python = Process.Start(pythonInfo);
            PlaceName.GetComponent<VTextInterface>().RenderText = "Speak now and wait";

        }



        IEnumerator ExecuteBackend()
        {
            _streetViewCube.Latitude = 32.44874f;
            _streetViewCube.Longitude = -99.73315f;
            WebPage.gameObject.SetActive(false);
            ForeClose.gameObject.SetActive(false);
            PlaceName.GetComponent<VTextInterface>().RenderText = "Loading Please Wait";
            PlaceName.transform.localPosition = new Vector3(-0.5f, 0.5f, 1f);


            run_file();



            yield return new WaitForSeconds(time);

            SetDataArray();
            if (information.Count != 0)
            {

                YesRender();
            }
            else
            {
                NoRender();
            }

        }

        void SetDataArray()
        {

            // Example #2: Write one string to a text file.
            //string text = ID.ToString();
            //System.IO.File.WriteAllText(@"D:\Users\sam\Documents\VRdataMaps\VRdataMAPS\Assets\Scripts\ID.txt", text);
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"D:\Users\sam\Documents\VRdataMaps\VRdataMAPS\Assets\Scripts\RES.txt");
            while ((line = file.ReadLine()) != null)
            {
                information.Add(line);
            }

        }

        void YesRender()
        {
            WebPage.gameObject.SetActive(true);
            ForeClose.gameObject.SetActive(true);
            string name = information[2];
            float lat = float.Parse(information[0]);
            float lon = float.Parse(information[1]);
            StreetViewObject obj = GetStreetObject(name, lat, lon);
            print(information[2]);
            //SphereObject.GetComponent<StreetViewCube>().Latitude = obj.latitude;
            //SphereObject.GetComponent<StreetViewCube>().Longitude = obj.longitude;

            _streetViewCube.Latitude = obj.latitude;
            _streetViewCube.Longitude = obj.longitude;
            PlaceName.GetComponent<VTextInterface>().RenderText = name;
            if (information[3] == "1")
            {
                ForeClose.GetComponent<VTextInterface>().RenderText = "Likely to be foreclosed";
            }
            if (information[3] == "0")
            {
                ForeClose.GetComponent<VTextInterface>().RenderText = "Unlikely to be foreclosed";
            }
            if (RenderPage == 0)
            {
                WebPage.gameObject.SetActive(false);
            }
            information.Clear();
        }

        void NoRender()
        {
            _streetViewCube.Latitude = 32.44874f;
            _streetViewCube.Longitude = -99.73315f;
            PlaceName.transform.localPosition = new Vector3(-0.5f, 0.5f, 1f);
            PlaceName.GetComponent<VTextInterface>().RenderText = "Sorry this ID isn't available";
            WebPage.gameObject.SetActive(false);
            ForeClose.gameObject.SetActive(false);
            information.Clear();
        }


        IEnumerator ExecuteVoice()
        {
            run_voice();
            yield return new WaitForSeconds(Vtime);
            string text = System.IO.File.ReadAllText(@"D:\Users\sam\Documents\VRdataMaps\VRdataMAPS\Assets\Scripts\ID.txt");
            ID = int.Parse(text);
            print(ID);
            information.Clear();
            StartCoroutine(ExecuteBackend());

        }




        // Use this for initialization
        void Start()
        {
            //Process.Start(@"D:\Users\sam\Documents\VRdataMaps\VRdataMAPS\Assets\Scripts\risk.py");
            ID = 123; //Default ID to load  
            StartCoroutine(ExecuteBackend());
           
        }



        // Update is called once per frame
        void Update()
        {
            OVRInput.Update();
            if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two))
            {
                
                run_voice();
                StartCoroutine(ExecuteVoice());
            }
        }
    }

}
