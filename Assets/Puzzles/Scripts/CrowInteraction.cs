using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowInteraction : MonoBehaviour {

    private void OnMouseDown()
    {
        Inventory.Instance.AddItem(3);
        Destroy(this.gameObject);
    }
}
