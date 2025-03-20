using Android.Graphics.Drawables;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
/// <summary>
/// Interfaz del juego e interaccion entre los jugadores y los botones
/// </summary>
namespace DAMPractica2
{
    /// <summary>
    /// Gestiona la interfaz de usuario y la interacción entre los jugadores
    /// </summary>
    public partial class MainPage : ContentPage
    {
        private TicTacToe game;

        /// <summary>
        /// Inicia el juego
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            game = new TicTacToe();
            ActualizarTurno();
        }
        /// <summary>
        /// Metodo para cuando el jugador pulsa cualquier de los botones, lo que sucede cuando se pulsa y cuando ya no se pueden pulsar más botones (se da un ganador o empate)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ButtonClick(object sender, EventArgs e)
        {
            Button b = sender as Button;
            int row = Microsoft.Maui.Controls.Grid.GetRow(b) - 2;
            int col = Microsoft.Maui.Controls.Grid.GetColumn(b);

            int turno = game.jugada(row, col);
            if (turno != -1)
            {
                b.IsEnabled = false;
                b.ImageSource = turno % 2 == 1 ? ImageSource.FromFile("tresenraya_cruz.png") : ImageSource.FromFile("tresenraya_circulo.png"); //Dependiendo el turno, se asigna una imagen y otraç
                ActualizarTurno();
            }
            int ganador = game.Ganador();
            if (ganador == 1 || ganador == 2) // Verificar si alguien gano
            {
                DisplayAlert("Ganador", $"Jugador {ganador} has ganado", "OK"); //Mensaje indicando quien gano
                buttonsDisabled();
                actualizarMarcador(ganador);
                limpiarTablero();
            }
            else if (game.PartidaFinalizadaSinGanador())
            {
                DisplayAlert("Empate", "El juego ha terminado en empate", "OK"); //Mensaje indicando que hubo empate
                limpiarTablero();
            }
        }
        /// <summary>
        /// Metodo que indica el turno del jugador, dependiendo del simbolo con el que juegan.
        /// </summary>
        void ActualizarTurno()
        {
            if (game.TurnoActual() % 2 == 1)
            {
                TurnoScore.Text = "O";
            }
            else
            {
                TurnoScore.Text = "X";
            }
        }
        /// <summary>
        /// Actualiza el marcador de los jugadores. Comprueba el valor del ganador y a que jugador pertecene y suma 1 al jugador al que pertenezca (si es símbolo O, suma 1 al Jugador 1, si es simbolo X suma 1 a jugador 2)
        /// </summary>
        /// <param name="ganador"></param>
        void actualizarMarcador(int ganador)
        {
            if (ganador == 1)
            {
                LabelXScore.Text = (int.Parse(LabelXScore.Text) + 1).ToString();
            }
            else if (ganador == 2)
            {
                LabelOScore.Text = (int.Parse(LabelOScore.Text) + 1).ToString();
            }
        }
        /// <summary>
        /// Deshabilita todos los botones
        /// </summary>
        void buttonsDisabled()
        {
            foreach (var child in grid.Children)
            {
                if (child is Button button)
                    button.IsEnabled = false;
            }
        }
        /// <summary>
        /// Quita los simbolos del tablero sin afectar al recuento de victorias y coloca las imagenes iniciales
        /// </summary>
        void limpiarTablero()
        {
            foreach (var child in grid.Children)
            {
                if (child is Button button)
                {
                    button.IsEnabled = true;
                    button.BackgroundColor = Colors.Black;
                    if (button == Button1) button.ImageSource = ImageSource.FromFile("tresenraya_r4_c0.png");
                    else if (button == Button2) button.ImageSource = ImageSource.FromFile("tresenraya_r4_c1.png");
                    else if (button == Button3) button.ImageSource = ImageSource.FromFile("tresenraya_r4_c2.png");
                    else if (button == Button4) button.ImageSource = ImageSource.FromFile("tresenraya_r5_c0.png");
                    else if (button == Button5) button.ImageSource = ImageSource.FromFile("tresenraya_r5_c1.png");
                    else if (button == Button6) button.ImageSource = ImageSource.FromFile("tresenraya_r5_c2.png");
                    else if (button == Button7) button.ImageSource = ImageSource.FromFile("tresenraya_r6_c0.png");
                    else if (button == Button8) button.ImageSource = ImageSource.FromFile("tresenraya_r6_c1.png");
                    else if (button == Button9) button.ImageSource = ImageSource.FromFile("tresenraya_r6_c2.png");
                }
            }
            game.Reiniciar();  // Reiniciar
        }
        /// <summary>
        /// Metodo que comprueba si no hay espacio en el tablero (se pulsaron todos los botones) y si no hay ganador devuelve que es empate.
        /// </summary>
        /// <returns></returns>
        public bool PartidaFinalizadaSinGanador()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
