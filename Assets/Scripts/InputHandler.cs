using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Player;
namespace InputManager
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;

        private RaycastHit hit;

        private List<Transform> selectedUnits = new List<Transform>();

        private bool isDragging = false;

        private Vector3 mousePos;
        void Start()
        {
            instance = this;
        }

        // Update is called once per frame
        private void OnGUI()
        {
            if (isDragging)
            {
                Rect rect = MultiSelect.GetScreenRect(mousePos, Input.mousePosition);
                MultiSelect.DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.25f));
                MultiSelect.DrawScreenRectBorder(rect, 3, Color.blue);
            }
        }
        public void HandleUnitMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8: //Units Layer
                            SelectUnit(hit.transform, Input.GetKey(KeyCode.LeftShift));
                            break;

                        default:
                            isDragging = true;
                            DeselectUnits();
                            break;
                    }

                }
            }
            if (Input.GetMouseButtonUp(0))
            {

                
                foreach (Transform child in RTS.Player.playerManager.instance.playerUnits)
                {
                    foreach (Transform unit in child)
                    {
                        if (iswithinSelectionBounds(unit))
                        {
                            SelectUnit(unit, true);
                        }
                    }
                }
                isDragging = false;
            }
            if (Input.GetMouseButtonDown(1)&&HaveSleletedUnits())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8: //Units Layer
                            
                            break;
                        case 9: //enemy untis layer

                            break;
                        default:
                            foreach(Transform unit in selectedUnits)
                            {
                                PlayerUnit pU = unit.gameObject.GetComponent<PlayerUnit>();
                                pU.SetDestinatin(hit.point);
                                
                            }
                            break;
                    }

                }
            }
        }
        private void SelectUnit(Transform unit, bool canMultiselect = false)
        {
            if (!canMultiselect)
            {
                DeselectUnits();

            }
            selectedUnits.Add(unit);
            unit.Find("Highlight").gameObject.SetActive(true);
        }
        private void DeselectUnits()
        {
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].Find("Highlight").gameObject.SetActive(false);
            }
            selectedUnits.Clear();
        }
        private bool iswithinSelectionBounds(Transform tf)
        {
            if (!isDragging)
            {
                return false;
            }
            Camera cam = Camera.main;
            Bounds vpBounds = MultiSelect.GetVPBounds(cam, mousePos, Input.mousePosition);
            return vpBounds.Contains(cam.WorldToViewportPoint(tf.position));
        }
        private bool HaveSleletedUnits()
        {
            if (selectedUnits.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
