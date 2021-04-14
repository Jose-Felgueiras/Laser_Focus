using System.Collections;
using UnityEngine;

public interface IBehaviour<D, P>
{

    void OnLaserHit(D direction, P position);
    void OnLaserStopHit(D direction, P position);
    void OnStartTurn(P position);
    void OnEndTurn(P position);
    void OnPlace(P position);
    void OnRemove(P position);
}
