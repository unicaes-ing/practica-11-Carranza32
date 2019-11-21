using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiaPractica11MC
{
    class Program
    {
        #region atributos
        private static bool isNumber;
        private static ArchivosController archivos;
        private static Dictionary<string, Alumno2> diccAlumnos; 
        #endregion

        #region Estructura
        [Serializable]
        public struct Alumno
        {
            public string Nombre { get; set; }
            public string Telefono { get; set; }
            public string Fecha { get; set; }
            public double Salario { get; set; }
        }
        [Serializable]
        public struct Alumno2
        {
            public string Carnet { get; set; }
            public string Nombre { get; set; }
            public string Carrera { get; set; }
            public double CUM { get; set; }
        }
        #endregion

        static void Main(string[] args)
        {
            archivos = new ArchivosController();
            int opc = 0;
            bool isNumber;
            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Escoja una opcion");
                    Console.WriteLine("1 - Ejercicio 1");
                    Console.WriteLine("2 - Ejercicio 2");
                    Console.WriteLine("3 - Ejercicio 3");
                    Console.WriteLine("0 - Salir");
                    isNumber = int.TryParse(Console.ReadLine(), out opc);
                } while (isNumber == false || opc < 0);
                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        Ejercicio1();
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Ejercicio2();
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Ejercicio3();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
            } while (opc != 0);
        }

        private static void Ejercicio1()
        {
            Alumno alumno = new Alumno();
            Console.WriteLine("Nombre: ");
            alumno.Nombre = Console.ReadLine();
            Console.WriteLine("Telefono: ");
            alumno.Telefono = Console.ReadLine();
            Console.WriteLine("Fecha de nacimiento: ");
            alumno.Fecha = Console.ReadLine();
            Console.WriteLine("Sueldo: ");
            alumno.Salario = Convert.ToDouble(Console.ReadLine());

            archivos.Serializar("alumnos.bin", alumno);
            Console.WriteLine("Archivo serializado!");
        }

        private static void Ejercicio2()
        {
            var lista = archivos.Deserializar<Alumno>("alumnos.bin");

            if (lista == null)
            {
                lista = new List<Alumno>();
            }

            foreach (var item in lista)
            {
                Console.WriteLine("Nombre: {0}", item.Nombre);
                Console.WriteLine("Telefono: {0}", item.Telefono);
                Console.WriteLine("Fecha: {0}", item.Fecha);
                Console.WriteLine("Salario: {0}", item.Salario);
                Console.WriteLine("=====================================");
            }
        }

        private static void Ejercicio3()
        {
            int opc = 0;
            bool isNumber;
            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Escoja una opcion");
                    Console.WriteLine("1 - Agregar alumno");
                    Console.WriteLine("2 - Mostrar Todos los alumnos");
                    Console.WriteLine("2 - Mostrar Todos los alumnos por CUM");
                    Console.WriteLine("3 - Buscar Alumno");
                    Console.WriteLine("0 - Salir");
                    isNumber = int.TryParse(Console.ReadLine(), out opc);
                } while (isNumber == false || opc < 0);
                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        FileStream stream = new FileStream("alumnos.bin", FileMode.Append, FileAccess.Write);
                        BinaryWriter archivo = new BinaryWriter(stream);
                        Console.WriteLine("Carnet: ");
                        string carnet = Console.ReadLine();
                        Console.WriteLine("Nombre: ");
                        string nombre = Console.ReadLine();
                        Console.WriteLine("Edad: ");
                        int edad = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Cum: ");
                        double cum = Convert.ToDouble(Console.ReadLine());
                        archivo.Write(carnet);
                        archivo.Write(nombre);
                        archivo.Write(edad);
                        archivo.Write(cum);
                        archivo.Close();
                        Console.WriteLine("Datos guardados...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();

                        BinaryReader leer = null;
                        try
                        {
                            FileStream stream2 = new FileStream("alumnos.bin", FileMode.Open, FileAccess.Read);
                            leer = new BinaryReader(stream2);

                            Console.WriteLine("========== ALUMNOS ==========");
                            do
                            {
                                string carnet2 = leer.ReadString();
                                string nombre2 = leer.ReadString();
                                int edad2 = leer.ReadInt32();
                                double cum2 = leer.ReadDouble();
                                Console.WriteLine($"Nombre: {carnet2}");
                                Console.WriteLine($"Nombre: {nombre2}");
                                Console.WriteLine($"Edad: {edad2}");
                                Console.WriteLine($"Sueldo: {cum2}");
                            } while (leer != null);
                        }
                        finally
                        {
                            leer.Close();
                            Console.ReadKey();
                        }

                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Ejercicio3();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
            } while (opc != 0);
        }
    }
}
