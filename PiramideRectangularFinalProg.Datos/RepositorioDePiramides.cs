using PiramideRectangularFinalProg.Entidades;

namespace PiramideRectangularFinalProg.Datos
{
    public class RepositorioDePiramides
    {
        private List<PiramideRegular>? piramides;
        private string rutaProyecto = Environment.CurrentDirectory;
        private string nombreArchivo = "Piramides.txt";
        private string rutaCompletaArchivo = string.Empty;
        public RepositorioDePiramides()
        {
            rutaCompletaArchivo = Path.Combine(rutaProyecto, nombreArchivo);
            piramides = LeerDatos();
        }
        public int GetCantidad(Material? materialSeleccionado=null){
            switch (materialSeleccionado)
            {
                case Material.Madera:
                    return piramides.Count(p => p.Material == Material.Madera);
                case Material.Plastico:
                    return piramides.Count(p => p.Material == Material.Plastico);
                case Material.Vidrio:
                    return piramides.Count(p => p.Material == Material.Vidrio);
                default:
                    return piramides.Count();
            }
        }

        public void AgregarPiramide(PiramideRegular piramide)
        {

            piramides!.Add(piramide);
        }

        public List<PiramideRegular>? GetPiramides()
        {
            return piramides;
        }
        public bool ExistePiramide(PiramideRegular piramide)
        {
            return piramides!.Any(p => p.LadoBase == piramide.LadoBase &&
                p.Altura == piramide.Altura && p.Material == piramide.Material);
        }
        public void EliminarPiramide(PiramideRegular piramide)
        {
            piramides!.Remove(piramide);
        }
        public void GuardarDatos()
        {
            using (var escritor = new StreamWriter(rutaCompletaArchivo))
            {
                foreach (var piramide in piramides!)
                {
                    string linea = ConstruirLinea(piramide);
                    escritor.WriteLine(linea);
                }
            }
        }

        private string ConstruirLinea(PiramideRegular piramide)
        {
            return $"{piramide.LadoBase}|{piramide.Altura}|{piramide.Material.GetHashCode()}";
        }

        public List<PiramideRegular> LeerDatos()
        {
            var lista = new List<PiramideRegular>();
            if (!File.Exists(rutaCompletaArchivo)) return lista;
            using (var lector = new StreamReader(rutaCompletaArchivo))
            {
                while (!lector.EndOfStream)
                {
                    string? linea = lector.ReadLine();
                    PiramideRegular piramide = ConstruirPiramide(linea);
                    lista.Add(piramide);
                }
            }
            return lista;
        }

        private PiramideRegular ConstruirPiramide(string? linea)
        {
            var campos = linea!.Split('|');
            var lado = int.Parse(campos[0]);
            var altura = int.Parse(campos[1]);
            var material = (Material)int.Parse(campos[2]);
            return new PiramideRegular(lado, altura, material);
        }
        public List<PiramideRegular>? OrdenarAsc()
        {
            return piramides!.OrderBy(e => e.GetVolumen()).ToList();
        }

        public List<PiramideRegular>? OrdenarDesc()
        {
            return piramides!.OrderByDescending(e => e.GetVolumen()).ToList();
        }

        public List<PiramideRegular>? Filtrar(Material materialSeleccionado)
        {
            switch (materialSeleccionado)
            {
                case Material.Madera:
                    return piramides.Where(p => p.Material == Material.Madera).ToList();
                case Material.Plastico:
                    return piramides.Where(p => p.Material == Material.Plastico).ToList();
                case Material.Vidrio:
                    return piramides.Where(p => p.Material == Material.Vidrio).ToList();
                default:
                    return null;
            }
        }
    }
}
