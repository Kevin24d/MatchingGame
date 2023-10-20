using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MatchingGame.VentanaExtra;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        // firstClicked apunta al primer control Etiqueta
        // que el jugador haga clic, pero será nulo
        // si el jugador aún no ha hecho clic en una etiqueta
        Label firstClicked = null;

        // secondClicked apunta al segundo control Label
        // que el jugador hace clic 
        Label secondClicked = null;

        // Usa este objeto aleatorio para elegir iconos aleatorios para los cuadrados
        Random random = new Random();

        // Cada una de estas letras es un ícono interesante
        // en la fuente Webdings,
        // y cada icono aparece dos veces en esta lista
        List<string> icons = new List<string>()
        {
        "r", "r", "t", "t", "p", "p", "l", "l",
        "x", "x", "o", "o", "k", "k", "q", "q"
        };

        /// <summary>
        /// Asigna cada ícono de la lista de íconos a un cuadrado aleatorio
        /// </summary>
        private void AssignIconsToSquares()
        {
            // TableLayoutPanel tiene 16 etiquetas,
            // y la lista de íconos tiene 16 íconos,
            // entonces se extrae un ícono al azar de la lista
            // y agregado a cada etiqueta
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }

        /// <summary>
        /// El evento Click de cada etiqueta es manejado por este controlador de eventos
        /// </summary>
        /// <param name="sender">The label that was clicked</param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            // El cronómetro sólo se activa después de dos no coincidentes
            // los iconos se han mostrado al jugador,
            // así que ignora cualquier clic si el cronómetro está funcionando
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Si la etiqueta en la que se hizo clic es negra, el jugador hizo clic
                // un ícono que ya ha sido revelado --
                //ignora el clic
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // Si firstClicked es nulo, este es el primer icono
                // en el par en el que el jugador hizo clic,
                // entonces establecemos firstClicked en la etiqueta que el jugador
                // se hace clic, cambia su color a negro y regresa 
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // Si el jugador llega tan lejos, el cronómetro no corre
                // corriendo y firstClicked no es nulo,
                // entonces este debe ser el segundo ícono en el que el jugador hizo clic
                // Establece su color en negro
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Verifica si el jugador ganó
                CheckForWinner();

                // Si el jugador hizo clic en dos íconos iguales, mantenlos
                // negro y restablecer firstClicked y secondClicked
                // para que el jugador pueda hacer clic en otro icono
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // Si el jugador llega hasta aquí, el jugador
                // hice clic en dos iconos diferentes, así que inicia el
                // temporizador (que esperará tres cuartos de
                // un segundo, y luego oculta los íconos)
                timer1.Start();

            }
             
        }

        /// <summary>
        /// Este temporizador se inicia cuando el jugador hace clic
        /// dos íconos que no coinciden,
        /// entonces cuenta tres cuartos de segundo
        /// y luego se apaga y oculta ambos íconos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
                // Detener el cronómetro
                timer1.Stop();

                // Ocultar ambos iconos
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = secondClicked.BackColor;

                // Restablecer el primer clic y el segundo clic
                // así la próxima vez que se coloque una etiqueta
                // hecho clic, el programa sabe que es el primer clic
                firstClicked = null;
                secondClicked = null;
        }


        /// <summary>
        /// Verifica cada ícono para ver si coincide, mediante
        /// comparando su color de primer plano con su color de fondo.
        /// Si todos los iconos coinciden, el jugador gana
        /// </summary>
        private void CheckForWinner()
        {
            // Revisa todas las etiquetas en TableLayoutPanel,
            // comprobando cada uno para ver si su icono coincide
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                    
                }
                
            }

            // Si el bucle no regresó, no encontró
            // cualquier icono no coincidente
            // Eso significa que el usuario ganó. Mostrar un mensaje y cerrar el formulario.
            MessageBox.Show("¡Has coincidido con todos los iconos!", "Felicitaciones");
            Hide();
            VentanaCreditos home = new VentanaCreditos();
            home.Show();
        }
    }
}
