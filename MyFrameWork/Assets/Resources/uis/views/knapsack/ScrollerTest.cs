using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using UnityEngine.UI;

public class ScrollerTest : MonoBehaviour {

    public GameObject go;
    ListViewSimpleDelegate list;
    // Use this for initialization
    void Start () {
        list = go.GetComponent<ListViewSimpleDelegate>();
        list.NumberOfCellsDel = NumberOfCellsDel;
        list.CellRefreshDel = CellRefreshDel;
    }
	
    int NumberOfCellsDel()
    {
        return 50;
    }

    public Dictionary<ListViewCell, GameObject> cell_list = new Dictionary<ListViewCell, GameObject>();

    void CellRefreshDel(ListViewCell cell, int dataIndex, int cellIndex)
    {
        cell.transform.FindChild("Index").GetComponent<Text>().text = dataIndex.ToString();
        Debug.Log("dataIndex----" + dataIndex + ",cellIndex---------" + cellIndex);
    }
}
