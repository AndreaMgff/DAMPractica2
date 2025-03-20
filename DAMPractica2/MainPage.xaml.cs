using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;

namespace DAMPractica2
{
    public partial class MainPage : ContentPage
    {
        private TicTacToe game;

        public MainPage()
        {
            InitializeComponent();
            game = new TicTacToe();
            ActualizarTurno();
        }

        void ButtonClick(object sender, EventArgs e) //Metodo para cuando el jugador pulsa un boton
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
                DisplayAlert("Ganador", $"Jugador {ganador} ha ganado!", "OK");
                buttonsDisabled();
                actualizarMarcador(ganador);
                limpiarTablero(); 
            }
            else if (game.PartidaFinalizadaSinGanador()) 
            {
                DisplayAlert("Empate", "El juego ha terminado en empate!", "OK");
                limpiarTablero(); 
            }
        }
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

        void actualizarMarcador(int ganador) // Actualiza el marcador de los jugadores
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
        void buttonsDisabled()// Deshabilitar todos los botones
        {
            foreach (var child in grid.Children)
            {
                if (child is Button button)
                    button.IsEnabled = false;
            }
        }
        void limpiarTablero() //Quita los simbolos del tablero sin afectar al recuento de victorias
        {
            foreach (var child in grid.Children)
            {
                if (child is Button button)
                {
                    button.IsEnabled = true;
                    button.BackgroundColor = Colors.Black;
                    button.ImageSource = ImageSource.FromFile("tresenraya.png");  // Pongo otra imagen de fondo base para todos los botones
                }
            }
            game.Reiniciar();  // Reiniciar
        }
        public bool PartidaFinalizadaSinGanador() //Metodo qeu comprueba si no hay espacio en el tablero(se pulsaron todos los botones) y no hay ganador, e indica que es empate
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
