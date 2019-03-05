using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BitScience
{
    public class ExampleStreetView : MonoBehaviour
    {
        [System.Serializable]
        public class ExampleStreetViewCoords
        {
            [SerializeField]
            public string location;
            [SerializeField]
            public float latitude;
            [SerializeField]
            public float longitude;
        }

        [SerializeField]
        private List<ExampleStreetViewCoords> _coordList;

        [SerializeField]
        private StreetViewCube _streetViewCube;

        [SerializeField]
        private Text _label;

        private int i = 0;

        void Start()
        {
            StartCoroutine(CycleLocations());
        }

        /// <summary>
        /// Ideally - you'd never do anything like this.   
        /// You'd update the latitude and longitude when the user 
        /// needed to update, or tie lat/long updates
        /// to User Interaction (Map pan, etc.)
        /// 
        /// But for demonstration Purposes
        /// </summary>
        private IEnumerator CycleLocations()
        {
            while (true)
            {
                i = (i + 1) % _coordList.Count;
                _streetViewCube.Latitude = _coordList[i].latitude;
                _streetViewCube.Longitude = _coordList[i].longitude;
                _label.text = "Requesting: " + _coordList[i].location;

                yield return new WaitForSeconds(10f);
            }
        }
    }
}