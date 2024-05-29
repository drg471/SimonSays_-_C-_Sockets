using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tarea3_SS_Servidor
{
    internal class Servidor
    {
        //Declaracion socket
        Socket miPrimerSocket;
        string _strIP = "127.0.0.1";
        int _intPuerto = 1234;
        //Diccionario para poner id a los clientes
        Dictionary<Socket, int> dicClientes = new Dictionary<Socket, int>();
        int _intContadorClientes = 0;

        //Declaracion delegado Escuchar de cliente y Escribir en cliente
        delegate void delegadoEscuchar();
        delegate void delegadoEscribir();

        public void conectar()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*** SERVIDOR INICIADO ***");
            Console.ResetColor();

            try
            {
                //Creación socket
                miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //Crear la dirección Ip
                IPEndPoint direccion = new IPEndPoint(IPAddress.Any, _intPuerto);
                //establecer la dirección con la que va a trabajar ese socket
                miPrimerSocket.Bind(direccion);

                //Conexiones que puede aceptar
                miPrimerSocket.Listen(5);
                Console.WriteLine("Escuchando...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
            }


            //Aceptar multiples conexiones
            while (true)
            {
                try
                {
                    //Creamos una nueva instancia de otro socket que nos permita conectarnos al principal
                    Socket scktCliente = miPrimerSocket.Accept();

                    //Añade ID al diccionario de clientes
                    _intContadorClientes++;
                    dicClientes.Add(scktCliente, _intContadorClientes);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Conectado con exito con cliente {dicClientes[scktCliente]}");
                    Console.ResetColor();

                    BienvenidaCliente(scktCliente);

                    //Llamada a los metodos que inician el hilo Escuchar y Escribir de cliente
                    HiloEscuchar(scktCliente);
                    HiloEscribir(scktCliente);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al aceptar la conexion: {ex.ToString()}");
                }
            }
        }

        //*******************************************************************************************
        //INI ESCUCHAR
        private void Escuchar(Socket scktCliente)
        {
            while (scktCliente.Connected)
            {
                try
                {
                    byte[] ByRec = new byte[255];//Para trabajar los flujos de información en los sockets
                    int a = scktCliente.Receive(ByRec, 0, ByRec.Length, 0);//se le añade a nuestro socket la información
                    Array.Resize(ref ByRec, a);//redimensionamos el array
                    //mostramos lo recibido (convertimos un array de bytes a caracteres)
                    Console.WriteLine($"CLIENTE {dicClientes[scktCliente]} dice: {Encoding.Default.GetString(ByRec)}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.ToString()}");
                }
            }

            miPrimerSocket.Close();//cerramos el socket
        }

        private void metodoDelegadoEscuchar(Socket scktCliente)
        {
            delegadoEscuchar EscucharDelegado = new delegadoEscuchar(() => Escuchar(scktCliente));
            EscucharDelegado.Invoke();
        }

        private void HiloEscuchar(Socket scktCliente)
        {
            //Expresion lambda //se puede entender como una función sin parámetros () que, cuando se llama, ejecuta => el método Escuchar con el argumento scktCliente.
            Thread hiloEscuchar = new Thread(() => metodoDelegadoEscuchar(scktCliente));
            hiloEscuchar.Start();
        }
        //FIN ESCUCHAR
        //*******************************************************************************************
        //*******************************************************************************************
        //INI ESCRIBIR
        private void Escribir(Socket scktCliente)
        {
            Console.WriteLine("Ingrese La Informacion a Enviar al cliente\n\n");

            while (scktCliente.Connected)
            {
                string info = Console.ReadLine();
                if (!string.IsNullOrEmpty(info))
                {
                    //Creamos el array de bytes y Codificamos la cadena de texto a bytes
                    byte[] infoEnviar = Encoding.Default.GetBytes(info);
                    scktCliente.Send(infoEnviar, 0, infoEnviar.Length, 0);//enviamos la información
                }
            }

            miPrimerSocket.Close();
        }

        private void metodoDelegadoEscribir(Socket scktCliente)
        {
            delegadoEscribir DelegadoEscribir = new delegadoEscribir(() => Escribir(scktCliente));
            DelegadoEscribir.Invoke();
        }

        private void HiloEscribir(Socket scktCliente)
        {
            Thread hiloEscribir = new Thread(() => metodoDelegadoEscribir(scktCliente));
            hiloEscribir.Start();
        }
        //FIN ESCRIBIR
        //*******************************************************************************************

        //metodo que envia mensaje de bienvenida al cliente
        private void BienvenidaCliente(Socket scktCliente)
        {
            string info = $"Bienvenido cliente ID {dicClientes[scktCliente]}";
            //Creamos el array de bytes y Codificamos la cadena de texto a bytes
            byte[] infoEnviar = Encoding.Default.GetBytes(info);
            scktCliente.Send(infoEnviar, 0, infoEnviar.Length, 0);//enviamos la información           
        }
    }
}
