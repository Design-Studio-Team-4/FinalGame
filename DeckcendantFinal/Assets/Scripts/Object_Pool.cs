//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//// object pool to store available card purchases
//public class simpleObjectsPool : MonoBehaviour
//{
//    // prefab that the pool returns
//    public GameObject prefab;
//    // all inactive card purchase instances
//    private Stack<GameObject> inactiveInstance = new Stack<GameObject>();

//    // returns instantiated card purchases
//    public GameObject GetObject()
//    {
//        GameObject spawnedGameObject;

//        // returns any inactive instances
//        if (inactiveInstance.Count > 0)
//        {
//            // removes instance
//            spawnedGameObject = inactiveInstance.Pop();
//        }
//        // else create a new instance
//        else
//        {
//            spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);

//            ObjectsPool objectPool = spawnedGameObject.AddComponent<ObjectsPool>();
//            objectPool.pool = this;
//        }

//        // put object in scene, enable it, and return it
//        spawnedGameObject.transform.SetParent(null);
//        spawnedGameObject.SetActive = true;
//        return spawnedGameObject;
//    }

//    // deliver the instance to the pool
//    public void returnGameObject(GameObject toReturn)
//    {
//        ObjectsPool objectPool = toReturn.GetComponent<ObjectsPool>();

//        // return instance to this pool
//        if (objectPool != null && objectPool.pool == this)
//        {
//            // make instance a child of toReturn
//            toReturn.transform.SetParent(transform);
//            toReturn.SetActive(false);
//            // disable this instance
//            inactiveInstance.Push(toReturn);
//        }

//        //otherwise destroy
//        else
//        {
//            //Debug.LogWarning(toRuturn.name + "was deleted");
//            Destroy(toReturn);
//        }
//    }
//}

//public class ObjectsPool : MonoBehaviour
//{
//    public simpleObjectsPool pool;
//}
