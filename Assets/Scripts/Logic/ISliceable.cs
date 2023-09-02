using System.Threading.Tasks;
using UnityEngine;

namespace Logic
{
    public interface ISliceable
    {
        Task Slice(Plane plane);
        GameObject Positive { get; }
        GameObject Negative { get; }
    }
}