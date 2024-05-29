using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Tarea3_SS_Cliente
{
    public partial class Form1 : Form
    {
        //Declaracion socket / ip / puerto
        Socket miPrimerSocket;
        string _strIP = "127.0.0.1";
        int _intPuerto = 1234;
        private bool _blnClienteConectado = false;


        //Declaracion delegado Escribir a servidor
        delegate void delegadoEscribir();
        delegate void delegadoLeer();
        private bool _blnDetenerHilos = false;

        //Declaracion variables para el juego
        string _strSecuenciaJugador = null;
        int _intContadorSecuencia = 0;
        private bool _blnSolicitudEnviada = false;
        int _intPuntuacionFinal;


        public Form1()
        {
            InitializeComponent();
            //Inicializa colores apagados para los pictureBox que simulan ser botones
            pbRed.BackColor = Color.DarkRed;
            pbGreen.BackColor = Color.DarkGreen;
            pbBlue.BackColor = Color.DarkBlue;
            pbYellow.BackColor = Color.YellowGreen;
            btnFinalizar.Enabled = false;
            lblINFO.Text = "¿PREPARADO?";
        }

        #region BotonesMenuRegion
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            btnFinalizar.Enabled = true;
            btnIniciar.Enabled = false;
            lblColoresJugador.Text = "";
            _intPuntuacionFinal = 0;
            Conectar(); //Llama al metodo conectar
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            _blnDetenerHilos = true; // Indica a los hilos que deben detenerse
            MessageBox.Show("Juego Finalizado!" + Environment.NewLine + "Puntuación Total: " + _intPuntuacionFinal + "pts.");
            Desconectar();
        }
        #endregion

        #region ConexionRegion
        //******************************************************************************
        // Intenta establecer la conexión del cliente con el servidor
        public void Conectar()
        {
            try
            {
                //Crea un nuevo socket para la conexion
                miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //Define la direccion IP y el puerto del servidor al que se conectara
                IPEndPoint miDireccion = new IPEndPoint(IPAddress.Parse(_strIP), _intPuerto);

                //Intenta conectar el socket con la dirección especificada
                miPrimerSocket.Connect(miDireccion);

                //Actualiza la etiqueta de salida para indicar una conexion exitosa
                lblSalida.Text = "Conectado con éxito\n";

                //Muestra un mensaje para indicar que el jugador debe memorizar la secuencia
                lblINFO.Text = "MEMORIZA!";

                //Espera un segundo antes de enviar la solicitud de secuencia al servidor
                Thread.Sleep(2000);

                //Inicia los hilos para escribir y leer informacion del servidor
                HiloEscribir("PedirSecuencia");
                HiloLeer();
            }
            catch (Exception ex)
            {
                //En caso de error muestra un mensaje y habilita el boton de inicio para volver a intentar la conexion
                lblColoresJugador.Text = "ERROR de conexión." + Environment.NewLine + "Vuelva a intentarlo.";
                btnIniciar.Enabled = true;
            }
        }
        #endregion

        #region EscribirRegion
        //*******************************************************************************************
        //INI ESCRIBIR
        private void Escribir(string strSecuencia)
        {
            //Pedir secuencia
            if (miPrimerSocket.Connected)
            {
                if (strSecuencia.Equals("PedirSecuencia"))
                {
                    //Creamos el array de bytes y Codificamos la cadena de texto a bytes
                    byte[] infoEnviar = Encoding.Default.GetBytes("PedirSecuencia");
                    miPrimerSocket.Send(infoEnviar, 0, infoEnviar.Length, 0);//enviamos la información
                }
                else
                {
                    //Creamos el array de bytes y Codificamos la cadena de texto a bytes
                    byte[] infoEnviar = Encoding.Default.GetBytes(strSecuencia);
                    miPrimerSocket.Send(infoEnviar, 0, infoEnviar.Length, 0);//enviamos la información
                    _strSecuenciaJugador = null;
                }
            }
            else
            {
                miPrimerSocket.Close();
            }
        }

        //Metodo que utiliza un delegado para invocar el metodo Escribir con la secuencia recibida por parametro
        private void metodoDelegadoEscribir(string strSecuencia)
        {
            // Expresión lambda: Se puede entender como una función sin parámetros () que, cuando se llama, ejecuta => el método Escribir con el argumento strSecuencia
            delegadoEscribir EscribirDelegado = new delegadoEscribir(() => Escribir(strSecuencia));
            //Invoca el delegado para llamar al metodo Escribir 
            EscribirDelegado.Invoke();
        }

        //Inicia un hilo para ejecutar el metodoDelegadoEscribir con la secuencia proporcionada
        private void HiloEscribir(string strSecuencia)
        {
            //Crea un nuevo hilo que ejecutara el metodoDelegadoEscribir con la secuencia recibida
            Thread hiloEscribir = new Thread(() => metodoDelegadoEscribir(strSecuencia));
            hiloEscribir.Start(); // Inicia el hilo
        }

        //FIN ESCRIBIR
        //*******************************************************************************************
        #endregion

        #region LeerRegion
        //*******************************************************************************************
        //INI LEER
        private void Leer()
        {
            while (miPrimerSocket.Connected && !_blnDetenerHilos)
            {
                try
                {
                    byte[] ByRec = new byte[255];//Para trabajar los flujos de información en los sockets
                    int a = miPrimerSocket.Receive(ByRec, 0, ByRec.Length, 0);//se le añade a nuestro socket la información
                    Array.Resize(ref ByRec, a);//redimensionamos el array

                    string strSecRecibida = Encoding.Default.GetString(ByRec);

                    lblSalida.Invoke((MethodInvoker)delegate
                    {
                        lblSalida.Text = "SERVIDOR dice: " + strSecRecibida; //mostramos lo recibido (convertimos un array de bytes a caracteres

                    });

                    if (strSecRecibida.Contains("SecCorrecta"))
                    {
                        _blnSolicitudEnviada = false;

                        //Muestra mensaje para avisar de correcto y de que memorices
                        lblINFO.Invoke((MethodInvoker)delegate
                        {
                            lblINFO.Text = "CORRECTO! +50pts!";

                        });

                        Thread.Sleep(1500);

                        lblINFO.Invoke((MethodInvoker)delegate
                        {
                            lblINFO.Text = "MEMORIZA!";
                        });
                        lblColoresJugador.Invoke((MethodInvoker)delegate
                        {
                            lblColoresJugador.Text = "";

                        });

                        Thread.Sleep(1500);
                        Escribir("PedirSecuencia");
                    }
                    else if (strSecRecibida.Contains("SecIncorrecta"))
                    {
                        _blnSolicitudEnviada = false;
                        Thread.Sleep(1000);
                        MessageBox.Show("Has perdido..." + Environment.NewLine + "Puntuación Total: " + _intPuntuacionFinal + "pts.");

                        Desconectar();
                    }
                    else if (strSecRecibida.Length == 4 && !_blnSolicitudEnviada)
                    {
                        _blnSolicitudEnviada = true;
                        ReproducirSecuencia(strSecRecibida);
                    }

                    if (!_blnSolicitudEnviada)
                    {
                        //Actualiza puntuacion
                        lblPuntuacion.Invoke((MethodInvoker)delegate
                        {
                            lblPuntuacion.Text = "Puntuacion: " + ObtenerPuntuacion(strSecRecibida) + " pts";
                        });
                    }

                    //Punto de salida para el bucle cuando se detiene el hilo
                    if (_blnDetenerHilos)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    lblSalida.Invoke((MethodInvoker)delegate
                    {
                        lblSalida.Text = "Error al leer del servidor.";
                    });
                }
            }

            miPrimerSocket.Close();//cerramos el socket
        }

        //Metodo que utiliza un delegado para invocar el metodo Leer
        private void metodoDelegadoLeer()
        {
            //Crea un delegadoLeer para invocar el metodo Leer
            delegadoLeer DelegadoLeer = new delegadoLeer(Leer);
            //Invoca el delegado para llamar al metodo Leer
            DelegadoLeer.Invoke();
        }

        //Inicia un hilo para ejecutar el metodoDelegadoLeer que a su vez ejecuta Leer
        private void HiloLeer()
        {
            //Crea un nuevo hilo que ejecuta el metodoDelegadoLeer
            Thread hiloLeer = new Thread(metodoDelegadoLeer);
            hiloLeer.Start();// Inicia el hilo
        }

        //FIN LEER
        #endregion

        #region MetodoRepdroSecuenciaRegion
        //*******************************************************************************************
        //REPRODUCIR SECUENCIA

        //Metodo para reoroducir la secuencia recibida (efecto luz encendido/apagado en picture box)
        private void ReproducirSecuencia(string strSecuencia)
        {
            //Bucle que recorre los caracteres del string de la secuencia
            for (int i = 0; i < strSecuencia.Length; i++)
            {
                switch (strSecuencia[i]) //Selector que pone colores mas vivos (efecto encendido) a los picturebox
                {
                    case 'R':
                        pbRed.BackColor = Color.Red;
                        break;
                    case 'G':
                        pbGreen.BackColor = Color.LightGreen;
                        break;
                    case 'B':
                        pbBlue.BackColor = Color.Blue;
                        break;
                    case 'Y':
                        pbYellow.BackColor = Color.Yellow;
                        break;
                    default:
                        break;
                }
                Thread.Sleep(300); //Duerme el hilo para hacer efecto parpadeo de luz

                //Pone colores mas apagados (efecto apagado) a los picturebox
                pbRed.BackColor = Color.DarkRed;
                pbGreen.BackColor = Color.DarkGreen;
                pbBlue.BackColor = Color.DarkBlue;
                pbYellow.BackColor = Color.DarkOliveGreen;

                Thread.Sleep(300); //Duerme el hilo para hacer efecto parpadeo de luz

                //Muestra mensaje para avisar de que repitas la secuencia
                lblINFO.Invoke((MethodInvoker)delegate
                {
                    lblINFO.Text = "REPITELO!";
                });
            }
        }
        #endregion

        #region ImagenClickRegion
        //*******************************************************************************************
        //EVENTOS CLICK IMAGENES COLORES

        //Evento que se ejecuta al hacer click en el boton de color rojo (pbRed)
        private async void pbRed_Click(object sender, EventArgs e)
        {
            pbRed.BackColor = Color.Red; // Cambia el color del boton a rojo
            //Espera 50 milisegundos sin bloquear el hilo de la interfaz de usuario
            await Task.Delay(50);
            pbRed.BackColor = Color.DarkRed; //Restaura el color del boton a rojo oscuro

            _intContadorSecuencia++; //Incrementa el contador de la secuencia

            //Llama al metodo para crear y comprobar la secuencia pasando 'R' como parametro
            CrearYComprobarSecuencia('R');
        }


        private async void pbBlue_Click(object sender, EventArgs e)
        {
            pbBlue.BackColor = Color.Blue;
            await Task.Delay(50);
            pbBlue.BackColor = Color.DarkBlue;
            _intContadorSecuencia++;
            CrearYComprobarSecuencia('B');
        }

        private async void pbGreen_Click(object sender, EventArgs e)
        {
            pbGreen.BackColor = Color.LightGreen;
            await Task.Delay(50);
            pbGreen.BackColor = Color.DarkGreen;
            _intContadorSecuencia++;
            CrearYComprobarSecuencia('G');
        }

        private async void pbYellow_Click(object sender, EventArgs e)
        {
            pbYellow.BackColor = Color.Yellow;
            await Task.Delay(50);
            pbYellow.BackColor = Color.DarkOliveGreen;
            _intContadorSecuencia++;
            CrearYComprobarSecuencia('Y');
        }

        #endregion

        #region MetodoRepdroSecuenciaRegion
        //******************************************************************************************************************
        //Metodos auxiliares

        //Metodo crear y comprobar secuencia la secuencia de colores que va introduciendo el jugador
        private void CrearYComprobarSecuencia(char cColor)
        {
            if (_intContadorSecuencia < 4) //Comprueba que la secuencia sea menor de 4 caracteres
            {
                _strSecuenciaJugador += cColor; //Añade el caracter (color) a la secuencia
            }
            else //Envia secuencia de colores del jugador al servidor 
            {
                _strSecuenciaJugador += cColor;
                _intContadorSecuencia = 0;
                HiloEscribir(_strSecuenciaJugador);
            }

            lblColoresJugador.Invoke((MethodInvoker)delegate
            {
                lblColoresJugador.Text = _strSecuenciaJugador;

            });
        }

        private int ObtenerPuntuacion(string resultado)
        {
            //Divide la cadena por ":" y coge la segunda parte
            string[] partes = resultado.Split(':');

            if (partes.Length >= 2)
            {
                //coge la segunda parte y la convierte a entero
                if (int.TryParse(partes[1].Trim(), out int puntuacion))
                {
                    _intPuntuacionFinal = puntuacion;
                    return puntuacion;
                }
            }

            // Si no se puede extraer el numero devuelve un valor por defecto
            return 0;
        }

        //Metodo para finalizar la conexion
        private void Desconectar()
        {
            try
            {
                //Comprueba si el objeto socket no esta vacio y sigue conectado
                if (miPrimerSocket != null && miPrimerSocket.Connected)
                {
                    //Cierra la conexion de socket
                    miPrimerSocket.Shutdown(SocketShutdown.Both);
                    miPrimerSocket.Close();
                }

                // Actualizar la interfaz de usuario
                lblSalida.Text = "Desconectado";
                btnIniciar.Enabled = true;  
            }
            catch (Exception ex)
            {
                lblSalida.Text = "Error al desconectar.";
            }
        }
        #endregion
    }
}