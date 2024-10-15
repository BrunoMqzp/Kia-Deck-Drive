using System.Collections;
using System.Collections.Generic;
using KDDC;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public int Vida;
    public int Energia;
    public int Escudo;
    public Salud salud;
    public Escudo escudo;
    private Mazo mazogestion;
    private CartAtaque cartAtaque;
    private CartVida cartVida;
    private CartEscudo cartEscudo;
    private CartEspecialCorregido cartEspecial;
    //public Carta DatosCarta;
    public List<Carta> CartasJugadas = new List<Carta>();
    public bool cartaDescartada = false;
    public Turnos turnos;
    public CartAtaqueCorregido cartAtaqueCorregido;
    public GameControlador endgame;
    public Enemigo enemigo;
    public SaludEnemigo saludenemigo;
    public SistemaPuntos sistemaPuntos;
    public ActualizarPuntuaciones actualizarPuntuaciones;
    private bool puntosEntregados = false;
    void Start()
    {
        salud = FindObjectOfType<Salud>();
        if (salud != null)
        {
            Vida = salud.saludmaxima;
        }
        else
        {
            Debug.LogError("Salud component not found on the GameObject.");
        }
        escudo = FindObjectOfType<Escudo>();
        cartAtaque = FindObjectOfType<CartAtaque>();
        cartVida = FindObjectOfType<CartVida>();
        cartEscudo = FindObjectOfType<CartEscudo>();
        cartEspecial = FindObjectOfType<CartEspecialCorregido>();
        turnos = FindObjectOfType<Turnos>();
        cartAtaqueCorregido = FindObjectOfType<CartAtaqueCorregido>();
        enemigo = FindObjectOfType<Enemigo>();
        saludenemigo = FindObjectOfType<SaludEnemigo>();
        sistemaPuntos = FindObjectOfType<SistemaPuntos>();
    }

    void Update()
    {
        Vida = salud.salud;
        Escudo = escudo.escudo;
        if (cartaDescartada)
        {
            Debug.Log("Efecto ATAQUE");
            cartaDescartada = false;
            //cartAtaqueCorregido.EfectoAtaque();
            //cartAtaque.EfectoAtaque();
            cartAtaqueCorregido.EfectoAtaque();
            cartVida.EfectoCuracion();
            cartEscudo.EfectoEscudo();
            turnos.VerificarTurno();
            sistemaPuntos.PuntosPorVida();
            cartEspecial.EfectoMasTurnos();
            cartEspecial.EfectoAgregar();
            //sistemaPuntos.PuntosAlGanar();
            //sistemaPuntos.PuntosAlPerder();
            //cartEspecial.EfectoDebuff();
        }

        if (Vida <= 0 && !puntosEntregados)
        {
            endgame = FindObjectOfType<GameControlador>();
            sistemaPuntos.PuntosAlPerder();
            actualizarPuntuaciones = FindObjectOfType<ActualizarPuntuaciones>();
            actualizarPuntuaciones.GuardarPuntaje();
            endgame.GameOver();
            puntosEntregados = true;
        }
        if (enemigo.enemigo_Vida <= 0 && !puntosEntregados)
        {
            endgame = FindObjectOfType<GameControlador>();
            sistemaPuntos.PuntosAlGanar();
            actualizarPuntuaciones = FindObjectOfType<ActualizarPuntuaciones>();
            actualizarPuntuaciones.GuardarPuntaje();
            endgame.Victory();
            puntosEntregados = true;
        }
    }

    public void AgregarCartaJugada(Carta carta)
    {
        CartasJugadas.Add(carta);

    }

    public void CreacionCartasJugadas(List<Carta> cartasJugadas)
    {
        CartasJugadas.AddRange(cartasJugadas);

    }

    public void ReiniciarCartasJugadas()
    {
        CartasJugadas.Clear();


    }
}
