using System.Collections.Generic;
using UnityEngine;

public class LineToTarget : MonoBehaviour
{
    public static LineToTarget instance;

    [SerializeField] private Transform dron;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject arrowPrefab;

    [SerializeField] private float spacing = 1f;

    private List<GameObject> arrows = new List<GameObject>();
    private bool onExercice;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    public void SetBool(bool value)
    {
        this.onExercice = value;
    }
    void Start()
    {
        if(instance == null)
            instance = this;
    }
    void Update()
    {
        if (onExercice)
        {
            if (target == null) return;

            ClearArrows();

            Vector3 start = dron.position;
            Vector3 end = target.position;

            Vector3 direction = (end - start).normalized;
            float distance = Vector3.Distance(start, end);
            int count = Mathf.FloorToInt(distance / spacing);

            for (int i = 1; i < count; i++)
            {
                Vector3 pos = start + direction * spacing * i;

                GameObject arrow = Instantiate(arrowPrefab, pos, Quaternion.identity, this.transform);

                Vector3 dirToTarget = (target.position - pos).normalized;
                if (dirToTarget != Vector3.zero)
                {
                    arrow.transform.rotation = Quaternion.LookRotation(dirToTarget);
                    arrow.transform.Rotate(-90, 0, 0);
                }
                arrows.Add(arrow);
            }
        }
    }

    void ClearArrows()
    {
        foreach (GameObject a in arrows)
        {
            Destroy(a);
        }
        arrows.Clear();
    }
}
