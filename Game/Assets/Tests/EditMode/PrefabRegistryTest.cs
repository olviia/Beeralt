// using System.Collections.Generic;
// using System.Text.RegularExpressions;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;
//
//
// public class PrefabRegistryTest
// {
//     private const string test_id = "my_test_id";
//     private PrefabRegistry prefabRegistry;
//     private GameObject go;
//     private GameObject anotherGO;
//     
//     // A Test behaves as an ordinary method
//     [Test]
//     public void Registry_GetPrefab_ReturnsNull_WhenNotInitialized()
//     {
//         prefabRegistry = ScriptableObject.CreateInstance<PrefabRegistry>();
//         LogAssert.Expect(LogType.Warning, new Regex("does not exist"));
//         Assert.IsNull(prefabRegistry.GetPrefab(test_id));
//         
//     }
//
//     [Test]
//     public void Registry_GetPrefab_WithUnknownID_WhenInitialized()
//     {
//         prefabRegistry = ScriptableObject.CreateInstance<PrefabRegistry>();
//         go = new GameObject("TestObject");
//         var entries = new List<PrefabEntry>
//         {
//             new() { id = test_id+test_id, prefab = go, }
//         };
//         prefabRegistry.Initialize(entries);
//         LogAssert.Expect(LogType.Warning, new Regex("does not exist"));
//         Assert.IsNull(prefabRegistry.GetPrefab(test_id));
//     }
//
//     [Test]
//     public void Registry_GetPrefab_ReturnsPrefab()
//     {
//         prefabRegistry = ScriptableObject.CreateInstance<PrefabRegistry>();
//         go = new GameObject("TestObject");
//         var entries = new List<PrefabEntry>
//         {
//             new() { id = test_id, prefab = go }
//         };
//         prefabRegistry.Initialize(entries);
//         Assert.That(go, Is.EqualTo(prefabRegistry.GetPrefab(test_id)));
//     }
//
//     [Test]
//     public void Registry_Initialize_DuplicateID()
//     {
//         prefabRegistry = ScriptableObject.CreateInstance<PrefabRegistry>();
//         go = new GameObject("TestObject");
//         anotherGO = new GameObject("TestObject");
//         var entries = new List<PrefabEntry>
//         {
//             new() { id = test_id, prefab = go, },
//             new() { id = test_id, prefab = anotherGO, }
//         };
//         prefabRegistry.Initialize(entries);
//         LogAssert.Expect(LogType.Warning, new Regex("already exists"));
//         Assert.That(go, Is.EqualTo(prefabRegistry.GetPrefab(test_id)));
//     }
//     
//
//     [TearDown]
//     public void TearDown()
//     {
//         Object.DestroyImmediate(prefabRegistry);
//         if(go != null) Object.DestroyImmediate(go);
//         if(anotherGO != null) Object.DestroyImmediate(anotherGO);
//     }
// }
