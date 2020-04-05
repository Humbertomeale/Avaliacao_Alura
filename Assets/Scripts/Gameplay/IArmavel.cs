using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArmavel
{
    string PegarNome();
    int PegarDano();
    float PegarCadencia();
    float PegarVelocidadeDaBala();
    float PegarPrecisao();
    bool temDisparoautomatico();
}
