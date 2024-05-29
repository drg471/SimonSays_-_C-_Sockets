using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tarea3_SS_Servidor
{
    public partial class Form1 : Form
    {
        //Declaracion socket / ip / puerto
        Socket miPrimerSocket;
        //string _strIP = "127.0.0.1";
        int _intPuerto = 1234;
        private bool _blnServidorConectado = false;

        //Diccionario para poner id a los clientes
        Dictionary<Socket, int> dicClientes = new Dictionary<Socket, int>();
        //Diccionario para almacenar la secuencia de colores de cada cliente
        Dictionary<Socket, string> dicSecuenciasClientes = new Dictionary<Socket, string>();
        //Diccionario para almacenar la puntuacion de cada cliente
        Dictionary<Socket, int> dicPuntuacionesClientes = new Dictionary<Socket, int>();
        int _intContadorClientes = 0;

        //Declaracion variables secuencia de colores y puntuacion cliente
        string strSecuenciaAleatoria;
        int _intPuntuacionCliente = 0;

        //Declaracion delegado Escuchar de cliente y Responder en cliente
        delegate void delegadoEscucharResponder();
        delegate void delegadoConectar();

        public Form1()
        {
            InitializeComponent();
            btnDesconectar.Enabled = false;
        }


        #region BotonesMenuRegion
        private void btnConectar_Click(object sender, EventArgs e)
        {
            _blnServidorConectado = true;

            HiloConectar(); //Lama al metodo que inicia el hilo de conectar servidor

            //Habilita/Inhabilita botones
            btnConectar.Enabled = false;
            btnDesconectar.Enabled = true;
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            _blnServidorConectado = false;
            btnConectar.Enabled = true;
            btnDesconectar.Enabled = false;


            if (miPrimerSocket != null) //Si el objeto socket no es null
            {
                miPrimerSocket.Close(); //Cierra la conexion
            }
        }
        #endregion

        #region ConexionRegion
        //*****************************************************************************************************************
        //METODOS DE CONEXION

        //Metodo para conectar el servidor a la escucha de clientes
        private void conectar()
        {
            // Utiliza Invoke para acceder al control txtSalidaMensajes desde el hilo principal.
            txtSalidaMensajes.Invoke((MethodInvoker)delegate
            {
                txtSalidaMensajes.AppendText("*** SERVIDOR INICIADO ***" + Environment.NewLine);

            });

            try
            {
                //Creación socket
                miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //Crear la dirección Ip (IPAddress.Any => acepta cualquier ip que pida conexion)
                IPEndPoint direccion = new IPEndPoint(IPAddress.Any, _intPuerto);
                //establecer la dirección con la que va a trabajar ese socket
                miPrimerSocket.Bind(direccion);

                //Conexiones que puede aceptar
                miPrimerSocket.Listen(5);
                txtSalidaMensajes.Invoke((MethodInvoker)delegate
                {
                    txtSalidaMensajes.AppendText("Esperando conexiones de clientes...");
                    txtSalidaMensajes.AppendText(Environment.NewLine);
                });
            }
            catch (Exception ex)
            {
                txtSalidaMensajes.Invoke((MethodInvoker)delegate
                {
                    txtSalidaMensajes.AppendText($"Error: {ex.ToString()}");
                    txtSalidaMensajes.AppendText(Environment.NewLine);
                });
            }

            //Aceptar multiples conexiones
            while (_blnServidorConectado)
            {
                try //Prueba a conectar con algun cliente
                {
                    //Creamos una nueva instancia de otro socket que nos permita conectarnos al principal
                    Socket scktCliente = miPrimerSocket.Accept();

                    //Añade ID al diccionario de clientes y asigna una secuencia aleatoria
                    _intContadorClientes++;
                    dicClientes.Add(scktCliente, _intContadorClientes);
                    dicSecuenciasClientes.Add(scktCliente, GenerarSecuenciaAleatoria());
                    dicPuntuacionesClientes.Add(scktCliente, 0); //Inicializa la puntuación del cliente

                    txtSalidaMensajes.Invoke((MethodInvoker)delegate
                    {
                        txtSalidaMensajes.AppendText($"Conectado con exito con cliente {dicClientes[scktCliente]}");
                        txtSalidaMensajes.AppendText(Environment.NewLine);
                    });

                    //Llamada a los metodos que inician el hilo Escuchar y Responder a cliente
                    HiloEscucharResponder(scktCliente);
                }
                catch (SocketException ex) //Maneja los errores del socket cliente
                {
                    //El cliente se desconecto
                    txtSalidaMensajes.Invoke((MethodInvoker)delegate
                    {
                        txtSalidaMensajes.AppendText($"Desconexion del socket.");
                        txtSalidaMensajes.AppendText(Environment.NewLine);
                    });
                    //Continua con el proximo cliente sin salir del bucle
                    continue;
                }
                catch (Exception ex) //Maneja los errores de conexion general con clientes
                {
                    _intPuntuacionCliente = 0;
                    txtSalidaMensajes.Invoke((MethodInvoker)delegate
                    {
                        txtSalidaMensajes.AppendText($"ERROR conexion.");
                        txtSalidaMensajes.AppendText(Environment.NewLine);
                    });
                    break;
                }
            }
        }

        //Metodo Delegado para el metodo conectar
        private void metodoDelegadoConectar()
        {
            // Crea una instancia del delegado con el método conectar
            delegadoConectar DelegadoConectar = new delegadoConectar(conectar);
            // Invoca el método conectar usando el delegado
            DelegadoConectar.Invoke();
        }

        //Metodo para iniciar un hilo que ejecute el método conectar
        private void HiloConectar()
        {
            // Crea un nuevo hilo y le asigna el métodoDelegadoConectar
            Thread hiloConectar = new Thread(metodoDelegadoConectar);
            hiloConectar.Start(); // Inicia la ejecución del hilo
        }
        #endregion

        #region EscucharResponderRegion
        //*******************************************************************************************
        //INI ESCUCHAR-RESPONDER
        private void EscucharResponder(Socket scktCliente)
        {
            //Mientras la conexion con el cliente este activa y el servidor conectado
            while (scktCliente.Connected && _blnServidorConectado)
            {
                try
                {
                    //*** Lee peticion cliente ***
                    byte[] ByRec = new byte[255];//Para trabajar los flujos de información en los sockets
                    int a = scktCliente.Receive(ByRec, 0, ByRec.Length, 0);//se le añade a nuestro socket la información
                    Array.Resize(ref ByRec, a);//redimensionamos el array
                                               // Utiliza Invoke para acceder al control txtSalidaMensajes desde el hilo principal.
                    string strEntrada = Encoding.Default.GetString(ByRec);
                    txtSalidaMensajes.Invoke((MethodInvoker)delegate
                    {
                        //mostramos lo recibido (convertimos un array de bytes a caracteres)
                        txtSalidaMensajes.AppendText($"CLIENTE {dicClientes[scktCliente]} dice: {strEntrada}");
                        txtSalidaMensajes.AppendText(Environment.NewLine);
                    });

                    //Si lo recibido por el cliente es la peticion de secuencia de colores
                    if (strEntrada.Equals("PedirSecuencia"))
                    {
                        //** Envia secuencia **
                        EnviarSecuencia(scktCliente);
                    }
                    else
                    {   //** Comprueba la secuencia de colores que manda el cliente **
                        ComprobarSecuencia(scktCliente, strEntrada);
                    }

                    // Verifica si el cliente se desconectó
                    if (scktCliente.Poll(0, SelectMode.SelectRead) && scktCliente.Available == 0)
                    {
                        // El cliente se desconectó
                        txtSalidaMensajes.Invoke((MethodInvoker)delegate
                        {
                            txtSalidaMensajes.AppendText($"Cliente {dicClientes[scktCliente]} se ha desconectado.");
                            txtSalidaMensajes.AppendText(Environment.NewLine);
                        });

                        // Cierra el socket del cliente y elimina la información asociada
                        scktCliente.Close();
                        dicClientes.Remove(scktCliente);
                        dicSecuenciasClientes.Remove(scktCliente);

                        // Continúa con el próximo cliente sin salir del bucle
                        continue;
                    }
                }
                catch (SocketException ex) //Maneja los errores del socket cliente
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        //El cliente se ha desconectado

                        txtSalidaMensajes.Invoke((MethodInvoker)delegate
                        {
                            txtSalidaMensajes.AppendText($"Error: El cliente se ha desconectado");
                        });
                        break;
                    }

                    txtSalidaMensajes.Invoke((MethodInvoker)delegate
                    {
                        txtSalidaMensajes.AppendText($"Error: {ex.ToString()}");
                    });
                }
                catch (Exception e) //Maneja los errores de recepcion de mensajes con cliente
                {
                    txtSalidaMensajes.Invoke((MethodInvoker)delegate
                    {
                        txtSalidaMensajes.AppendText($"Error: {e.ToString()}");
                    });
                }
            }

            miPrimerSocket.Close();//cerramos el socket
        }

        //Metodo Delegado para el metodo EscucharResponder que toma un Socket como parámetro
        private void metodoDelegadoEscucharResponder(Socket scktCliente)
        {
            // Crea una instancia del delegado y le asigna una expresión lambda que invoca el método EscucharResponder con el Socket proporcionado
            delegadoEscucharResponder EscucharResponderDelegado = new delegadoEscucharResponder(() => EscucharResponder(scktCliente));
            // Invoca el método mediante el delegado
            EscucharResponderDelegado.Invoke();
        }

        //Metodo para iniciar un hilo que ejecute el metodoDelegadoEscucharResponder
        private void HiloEscucharResponder(Socket scktCliente)
        {
            //Expresion lambda //se puede entender como una función sin parámetros () que, cuando se llama, ejecuta => el método Escuchar con el argumento scktCliente.
            Thread hiloEscucharResponder = new Thread(() => metodoDelegadoEscucharResponder(scktCliente));
            hiloEscucharResponder.Start(); //Inicia el hilo
        }


        //========================================================================================================
        //Metodo para enviar la secuencia de colores al cliente
        private void EnviarSecuencia(Socket scktCliente)
        {
            // Obtener la nueva secuencia aleatoria para el cliente actual
            string secuenciaActual = GenerarSecuenciaAleatoria();

            // Actualizar la secuencia asociada al cliente en el diccionario
            dicSecuenciasClientes[scktCliente] = secuenciaActual;

            //Creamos el array de bytes y Codificamos la cadena de texto a bytes
            byte[] infoEnviar = Encoding.Default.GetBytes(secuenciaActual);
            scktCliente.Send(infoEnviar, 0, infoEnviar.Length, 0);//enviamos la información     

            txtSalidaMensajes.Invoke((MethodInvoker)delegate
            {
                txtSalidaMensajes.AppendText("Secuencia enviada: " + dicSecuenciasClientes[scktCliente] + Environment.NewLine);

            });
        }

        private void EnviarResultado(Socket scktCliente, string strResultado)
        {
            //Creamos el array de bytes y Codificamos la cadena de texto a bytes
            byte[] infoEnviar = Encoding.Default.GetBytes(strResultado);
            scktCliente.Send(infoEnviar, 0, infoEnviar.Length, 0);//enviamos la información     

            txtSalidaMensajes.Invoke((MethodInvoker)delegate
            {
                txtSalidaMensajes.AppendText("Resultado: " + strResultado + Environment.NewLine);

            });
        }
        //FIN ESCUCHAR
        //*******************************************************************************************
        #endregion

        #region GenerarSecyComprobarRegion
        //Metodo que genera y devuelve una secuencia aleatoria de colores (R, G, B, Y)
        private string GenerarSecuenciaAleatoria()
        {
            Random random = new Random();
            char[] letras = { 'R', 'G', 'B', 'Y' };
            char[] resultado = new char[4];

            //Obtiene cuatro colores aleatorios
            for (int i = 0; i < 4; i++)
            {
                int indiceLetra = random.Next(0, letras.Length);
                resultado[i] = letras[indiceLetra];
            }

            return new string(resultado); //Devuelve la secuencia
        }

        //Metodo que comprueba la secuencia enviada por el cliente y actualiza la puntuacion
        private void ComprobarSecuencia(Socket scktCliente, string strSecuencia)
        {
            string secuenciaActual = dicSecuenciasClientes[scktCliente];

            // Compara la secuencia del cliente con la secuencia actual
            if (strSecuencia.Equals(secuenciaActual))
            {
                //Incrementa la puntuacion en 50 puntos si la secuencia es correcta
                dicPuntuacionesClientes[scktCliente] += 50;
                EnviarResultado(scktCliente, "SecCorrecta . Puntuacion: " + dicPuntuacionesClientes[scktCliente]);
            }
            else
            {
                //Informa al cliente sobre la secuencia incorrecta y muestra la puntuacion
                EnviarResultado(scktCliente, "SecIncorrecta. Puntuacion: " + dicPuntuacionesClientes[scktCliente]);
            }
        }

        #endregion
    }
}